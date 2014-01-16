using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace InvokeMethodInfo
{
    public sealed class IndirectInvoker
    {
        private const String MethodName = "Invoke";
        private const String CarryingMethodName = "CarryingInvoke";

        private abstract class Metadata
        {
            private readonly MethodInfo _carryingInvoker;
            public readonly Func<IntPtr, Object> _ctor;
            public readonly MethodInfo _invoker;

            public Metadata(Func<IntPtr, Object> ctor, MethodInfo invoker, MethodInfo carryingInvoker)
            {
                _ctor = ctor;
                _invoker = invoker;
                _carryingInvoker = carryingInvoker;
            }

            public abstract Delegate CreateCarryingDelegate(MethodInfo methodInfo, Type[] parameterTypes);
            public abstract Delegate CreateDelegate(Type declaringType, Type returnType, Type[] parameterTypes);

            public MethodInfo CarryingInvoker
            {
                get
                {
                    return _carryingInvoker;
                }
            }
            public Func<IntPtr, Object> Ctor
            {
                get
                {
                    return _ctor;
                }
            }
            public MethodInfo Invoker
            {
                get
                {
                    return _invoker;
                }
            }
        }

        private sealed class ActionMetadata : Metadata
        {
            public ActionMetadata(Func<IntPtr, Object> ctor, MethodInfo invoker, MethodInfo carryingInvoker)
                : base(ctor, invoker, carryingInvoker)
            {
            }

            public override Delegate CreateCarryingDelegate(MethodInfo methodInfo, Type[] parameterTypes)
            {
                Type[] genericTypes = new Type[parameterTypes.Length + 1];
                genericTypes[0] = methodInfo.DeclaringType;
                Array.Copy(parameterTypes, 0, genericTypes, 1, parameterTypes.Length);
                MethodInfo closeMethod = base.CarryingInvoker.MakeGenericMethod(genericTypes);

                Type delegateType = Expression.GetActionType(genericTypes);
                Object target = base.Ctor(methodInfo.MethodHandle.GetFunctionPointer());
                return Delegate.CreateDelegate(delegateType, target, closeMethod);
            }
            public override Delegate CreateDelegate(Type declaringType, Type returnType, Type[] parameterTypes)
            {
                Type[] genericTypes = new Type[parameterTypes.Length + 1];
                genericTypes[0] = declaringType;
                Array.Copy(parameterTypes, 0, genericTypes, 1, parameterTypes.Length);
                MethodInfo closeMethod = base.Invoker.MakeGenericMethod(genericTypes);

                Type[] delegateTypes = new Type[genericTypes.Length + 1];
                delegateTypes[0] = typeof(IntPtr);
                Array.Copy(genericTypes, 0, delegateTypes, 1, genericTypes.Length);
                Type delegateType = Expression.GetActionType(delegateTypes);

                return Delegate.CreateDelegate(delegateType, closeMethod);
            }
        }

        private sealed class FuncMetadata : Metadata
        {
            public FuncMetadata(Func<IntPtr, Object> ctor, MethodInfo invoker, MethodInfo carryingInvoker)
                : base(ctor, invoker, carryingInvoker)
            {
            }

            public override Delegate CreateCarryingDelegate(MethodInfo methodInfo, Type[] parameterTypes)
            {
                Type[] genericTypes = new Type[parameterTypes.Length + 2];
                genericTypes[0] = methodInfo.DeclaringType;
                Array.Copy(parameterTypes, 0, genericTypes, 1, parameterTypes.Length);
                genericTypes[genericTypes.Length - 1] = methodInfo.ReturnType;
                MethodInfo closeMethod = base.CarryingInvoker.MakeGenericMethod(genericTypes);

                Type delegateType = Expression.GetFuncType(genericTypes);
                Object target = base.Ctor(methodInfo.MethodHandle.GetFunctionPointer());
                return Delegate.CreateDelegate(delegateType, target, closeMethod);
            }
            public override Delegate CreateDelegate(Type declaringType, Type returnType, Type[] parameterTypes)
            {
                Type[] genericTypes = new Type[parameterTypes.Length + 2];
                genericTypes[0] = declaringType;
                Array.Copy(parameterTypes, 0, genericTypes, 1, parameterTypes.Length);
                genericTypes[genericTypes.Length - 1] = returnType;
                MethodInfo closeMethod = base.Invoker.MakeGenericMethod(genericTypes);

                Type[] delegateTypes = new Type[genericTypes.Length + 1];
                delegateTypes[0] = typeof(IntPtr);
                Array.Copy(genericTypes, 0, delegateTypes, 1, genericTypes.Length);
                Type delegateType = Expression.GetFuncType(delegateTypes);

                return Delegate.CreateDelegate(delegateType, closeMethod);
            }
        }

        private readonly Metadata[] _actionMetadatas;
        private readonly Metadata[] _funcMetadatas;
        private readonly ModuleBuilder _moduleBuilder;

        public IndirectInvoker(ModuleBuilder moduleBuilder)
        {
            _moduleBuilder = moduleBuilder;
            _actionMetadatas = new Metadata[10];
            _funcMetadatas = new Metadata[10];
        }

        public Delegate Create(MethodInfo methodInfo)
        {
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            if (parameterInfos.Length >= _actionMetadatas.Length)
                throw new ArgumentOutOfRangeException("paramCount");

            Type[] parameterTypes = new Type[parameterInfos.Length];
            for (int i = 0; i < parameterTypes.Length; i++)
            {
                Type parameterType = parameterInfos[i].ParameterType;
                parameterTypes[i] = parameterType.IsByRef ? typeof(IntPtr) : parameterType;
            }

            return GetMetadata(methodInfo.ReturnType, parameterTypes.Length).CreateCarryingDelegate(methodInfo, parameterTypes);
        }
        public Delegate Create(Type declaringType, Type returnType, Type[] parameterTypes)
        {
            return GetMetadata(returnType, parameterTypes.Length).CreateDelegate(declaringType, returnType, parameterTypes);
        }
        private static Metadata CreateMetadata(Type invokerType, bool isFunc)
        {
            var ctor = (Func<IntPtr, Object>)Delegate.CreateDelegate(typeof(Func<IntPtr, Object>), invokerType.GetMethod("<Create>"));
            MethodInfo invoker = invokerType.GetMethod(MethodName);
            MethodInfo carryingInvoker = invokerType.GetMethod(CarryingMethodName);
            if (isFunc)
                return new FuncMetadata(ctor, invoker, carryingInvoker);
            else
                return new ActionMetadata(ctor, invoker, carryingInvoker);
        }
        private static void DefineCtor(TypeBuilder typeBuilder, FieldBuilder fieldBuilder)
        {
            var parameterTypes = new Type[] { fieldBuilder.FieldType };
            ConstructorBuilder ctorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, parameterTypes);
            ILGenerator il = ctorBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, fieldBuilder);
            il.Emit(OpCodes.Ret);

            MethodBuilder createBuider = typeBuilder.DefineMethod("<Create>", MethodAttributes.Static | MethodAttributes.Public, typeBuilder, parameterTypes);
            il = createBuider.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Newobj, ctorBuilder);
            il.Emit(OpCodes.Ret);
        }
        private Metadata DefineType(Type returnType, int paramCount)
        {
            int isFunc = returnType == typeof(void) ? 0 : 1;
            String typeName = "IndirectInvoker" + (isFunc == 0 ? "Action" : "Func") + paramCount.ToString();
            TypeBuilder typeBuilder;
            try
            {
                typeBuilder = _moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Sealed);
            }
            catch (ArgumentException)
            {
                throw new TypeExistException(typeName);
            }

            FieldBuilder fieldBuilder = typeBuilder.DefineField("fptr", typeof(IntPtr), FieldAttributes.Private | FieldAttributes.InitOnly);
            DefineCtor(typeBuilder, fieldBuilder);

            MethodBuilder method = typeBuilder.DefineMethod(MethodName, MethodAttributes.Public | MethodAttributes.Static);

            var genericParameterNames = new String[paramCount + 1 + isFunc];
            for (int i = 0; i < genericParameterNames.Length; i++)
                genericParameterNames[i] = "T" + (i + 1).ToString();
            GenericTypeParameterBuilder[] genericParameters = method.DefineGenericParameters(genericParameterNames);

            Type[] targetParameters;
            if (isFunc == 0)
                targetParameters = genericParameters;
            else
            {
                targetParameters = new Type[genericParameters.Length - 1];
                Array.Copy(genericParameters, targetParameters, targetParameters.Length);
                returnType = genericParameters[genericParameters.Length - 1];
            }

            var parameters = new Type[targetParameters.Length + 1];
            parameters[0] = typeof(IntPtr);
            for (int i = 0; i < targetParameters.Length; i++)
                parameters[i + 1] = targetParameters[i];

            method.SetParameters(parameters);
            method.SetReturnType(returnType);

            ILGenerator il = method.GetILGenerator();
            for (int i = 0; i < targetParameters.Length; i++)
                il.Emit(OpCodes.Ldarg, i + 1);
            il.Emit(OpCodes.Ldarg_0);
            il.EmitCalli(OpCodes.Calli, CallingConventions.Standard, returnType, targetParameters, null);
            il.Emit(OpCodes.Ret);

            MethodBuilder methodCarrying = typeBuilder.DefineMethod(CarryingMethodName, MethodAttributes.Public);
            genericParameters = methodCarrying.DefineGenericParameters(genericParameterNames);

            methodCarrying.SetParameters(targetParameters);
            methodCarrying.SetReturnType(returnType);

            il = methodCarrying.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, fieldBuilder);
            for (int i = 0; i < targetParameters.Length; i++)
                il.Emit(OpCodes.Ldarg, i + 1);
            il.Emit(OpCodes.Call, method);
            il.Emit(OpCodes.Ret);

            return CreateMetadata(typeBuilder.CreateType(), isFunc != 0);
        }
        private Metadata GetMetadata(Type returnType, int paramCount)
        {
            Metadata[] metadatas = returnType == typeof(void) ? _actionMetadatas : _funcMetadatas;
            if (paramCount >= metadatas.Length)
                throw new ArgumentOutOfRangeException("paramCount");

            if (metadatas[paramCount] == null)
            {
                try
                {
                    metadatas[paramCount] = DefineType(returnType, paramCount);
                }
                catch (TypeExistException e)
                {
                    Type invokerType = _moduleBuilder.GetType(e.TypeName);
                    metadatas[paramCount] = CreateMetadata(invokerType, returnType != typeof(void));
                }
            }
            return metadatas[paramCount];
        }
        public MethodInfo GetMethodInfo(Type returnType, int paramCount)
        {
            return GetMetadata(returnType, paramCount).Invoker;
        }
    }
}
