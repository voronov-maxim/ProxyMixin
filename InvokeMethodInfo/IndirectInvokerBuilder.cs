using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace InvokeMethodInfo
{
    public abstract class IndirectInvokerBuilder
    {
        protected const String CarryingMethodName = "CarryingInvoke";
        protected const int MaxParameterCount = 10;
        protected const String MethodName = "Invoke";

        protected abstract class Metadata
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
            public Object CreateClosure(IntPtr target)
            {
                return _ctor(target);
            }

            public MethodInfo CarryingInvoker
            {
                get
                {
                    return _carryingInvoker;
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

        protected sealed class ActionMetadata : Metadata
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
                Object target = base.CreateClosure(methodInfo.MethodHandle.GetFunctionPointer());
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

        protected sealed class FuncMetadata : Metadata
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
                Object target = base.CreateClosure(methodInfo.MethodHandle.GetFunctionPointer());
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

        public static IndirectInvokerBuilder Create()
        {
            return new StaticIndirectInvokerBuilder();
        }
        public static IndirectInvokerBuilder Create(ModuleBuilder moduleBuilder)
        {
            return new DynamicIndirectInvokerBuilder(moduleBuilder);
        }
        public Delegate Create(MethodInfo methodInfo)
        {
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
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
        protected static Metadata CreateMetadata(Type closureType, bool isAction)
        {
            var ctor = (Func<IntPtr, Object>)Delegate.CreateDelegate(typeof(Func<IntPtr, Object>), closureType.GetMethod("Create"));
            MethodInfo invoker = closureType.GetMethod(MethodName);
            MethodInfo carryingInvoker = closureType.GetMethod(CarryingMethodName);
            if (isAction)
                return new ActionMetadata(ctor, invoker, carryingInvoker);
            else
                return new FuncMetadata(ctor, invoker, carryingInvoker);
        }
        protected abstract Metadata GetMetadata(Type returnType, int paramCount);
        public MethodInfo GetMethodInfo(Type returnType, int paramCount)
        {
            return GetMetadata(returnType, paramCount).Invoker;
        }
        protected static String GetClosureClassName(bool isAction, int parameterCount)
        {
            return "CallMethodPointer.IndirectInvoker" + (isAction ? "Action" : "Func") + parameterCount.ToString();
        }
    }

    internal sealed class StaticIndirectInvokerBuilder : IndirectInvokerBuilder
    {
        private readonly Metadata[] _actionMetadatas;
        private readonly Type[] _closureTypes;
        private readonly Metadata[] _funcMetadatas;

        public StaticIndirectInvokerBuilder()
        {
            _actionMetadatas = new Metadata[MaxParameterCount];
            _funcMetadatas = new Metadata[MaxParameterCount];
            _closureTypes = typeof(CallMethodPointer.IndirectInvokerAction0).Assembly.GetTypes();
        }

        protected override Metadata GetMetadata(Type returnType, int paramCount)
        {
            if (paramCount >= MaxParameterCount)
                throw new ArgumentOutOfRangeException("paramCount");

            bool isAction = returnType == typeof(void);
            Metadata[] metadatas = isAction ? _actionMetadatas : _funcMetadatas;
            if (metadatas[paramCount] == null)
                foreach (Type closureType in _closureTypes)
                    if (closureType.Name == GetClosureClassName(isAction, paramCount))
                    {
                        metadatas[paramCount] = CreateMetadata(closureType, isAction);
                        break;
                    }
            return metadatas[paramCount];
        }
    }

    internal sealed class DynamicIndirectInvokerBuilder : IndirectInvokerBuilder
    {
        private readonly Metadata[] _actionMetadatas;
        private readonly Metadata[] _funcMetadatas;
        private readonly ModuleBuilder _moduleBuilder;

        public DynamicIndirectInvokerBuilder(ModuleBuilder moduleBuilder)
        {
            _moduleBuilder = moduleBuilder;
            _actionMetadatas = new Metadata[MaxParameterCount];
            _funcMetadatas = new Metadata[MaxParameterCount];
        }

        private static void DefineCtor(TypeBuilder typeBuilder, FieldBuilder fieldBuilder)
        {
            var parameterTypes = new Type[] { fieldBuilder.FieldType };
            ConstructorBuilder ctorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, parameterTypes);
            ctorBuilder.DefineParameter(1, ParameterAttributes.None, fieldBuilder.Name);

            ILGenerator il = ctorBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, fieldBuilder);
            il.Emit(OpCodes.Ret);

            MethodBuilder createBuider = typeBuilder.DefineMethod("Create", MethodAttributes.Static | MethodAttributes.Public, typeBuilder, parameterTypes);
            createBuider.DefineParameter(1, ParameterAttributes.None, fieldBuilder.Name);

            il = createBuider.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Newobj, ctorBuilder);
            il.Emit(OpCodes.Ret);
        }
        private static Metadata DefineType(ModuleBuilder moduleBuilder, Type returnType, int paramCount)
        {
            int isFunc = returnType == typeof(void) ? 0 : 1;
            String typeName = GetClosureClassName(isFunc == 0, paramCount);
            TypeBuilder typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Sealed);

            FieldBuilder fieldBuilder = typeBuilder.DefineField("methodPointer", typeof(IntPtr), FieldAttributes.Private | FieldAttributes.InitOnly);
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

            method.DefineParameter(1, ParameterAttributes.None, "target");
            for (int i = 1; i < parameters.Length; i++)
                method.DefineParameter(i + 1, ParameterAttributes.None, "p" + i.ToString());

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

            for (int i = 0; i < targetParameters.Length; i++)
                methodCarrying.DefineParameter(i + 1, ParameterAttributes.None, "p" + (i + 1).ToString());

            il = methodCarrying.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, fieldBuilder);
            for (int i = 0; i < targetParameters.Length; i++)
                il.Emit(OpCodes.Ldarg, i + 1);
            il.Emit(OpCodes.Call, method);
            il.Emit(OpCodes.Ret);

            return CreateMetadata(typeBuilder.CreateType(), isFunc == 0);
        }
        protected override Metadata GetMetadata(Type returnType, int paramCount)
        {
            if (paramCount >= MaxParameterCount)
                throw new ArgumentOutOfRangeException("paramCount");

            bool isAction = returnType == typeof(void);
            Metadata[] metadatas = isAction ? _actionMetadatas : _funcMetadatas;
            if (metadatas[paramCount] == null)
            {
                try
                {
                    metadatas[paramCount] = DefineType(_moduleBuilder, returnType, paramCount);
                }
                catch (ArgumentException)
                {
                    Type closureType = _moduleBuilder.GetType(GetClosureClassName(isAction, paramCount));
                    metadatas[paramCount] = CreateMetadata(closureType, isAction);
                }
            }
            return metadatas[paramCount];
        }
    }
}
