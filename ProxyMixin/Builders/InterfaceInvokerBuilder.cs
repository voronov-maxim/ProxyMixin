using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.Builders
{
    public sealed class InterfaceInvokerBuilder<T, I>
        where T : I
        where I : class
    {
        private FieldBuilder _objectField;
        private readonly TypeBuilder _typeBuilder;
        private readonly List<KeyValuePair<FieldBuilder, Delegate>> _privateMethods;

        public InterfaceInvokerBuilder(TypeBuilder typeBuilder)
        {
            _typeBuilder = typeBuilder;
            _privateMethods = new List<KeyValuePair<FieldBuilder, Delegate>>();
        }

        public Func<I, I> CreateType()
        {
            _objectField = _typeBuilder.DefineField("interfaceObject", typeof(I), FieldAttributes.InitOnly | FieldAttributes.Private);
            DefineCtor(_typeBuilder, _objectField);

            ProxyBuilderHelper.DefineInterface(_typeBuilder, typeof(I), typeof(T),
                (il, interfaceMethod, targetMethod) => GenerateMethod(il, interfaceMethod, targetMethod));
            foreach (Type interfaceType in typeof(I).GetInterfaces())
                ProxyBuilderHelper.DefineInterface(_typeBuilder, interfaceType, typeof(T),
                    (il, interfaceMethod, targetMethod) => GenerateMethod(il, interfaceMethod, targetMethod));

            Type interfaceInvokerType = _typeBuilder.CreateType();

            foreach (var pair in _privateMethods)
            {
                FieldInfo fieldInfo = interfaceInvokerType.GetField(pair.Key.Name, BindingFlags.NonPublic | BindingFlags.Static);
                fieldInfo.SetValue(null, pair.Value);
            }

            MethodInfo createMethodInfo = interfaceInvokerType.GetMethod("<Create>");
            return (Func<I, I>)Delegate.CreateDelegate(typeof(Func<I, I>), createMethodInfo);
        }
        private static void DefineCtor(TypeBuilder typeBuilder, FieldBuilder fieldBuilder)
        {
            ConstructorBuilder ctorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { typeof(I) });
            ILGenerator il = ctorBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, fieldBuilder);
            il.Emit(OpCodes.Ret);

            MethodBuilder createBuider = typeBuilder.DefineMethod("<Create>", MethodAttributes.Static | MethodAttributes.Public, typeof(I), new Type[] { typeof(I) });
            il = createBuider.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Newobj, ctorBuilder);
            il.Emit(OpCodes.Ret);
        }
        private void GenerateMethod(ILGenerator il,
            MethodInfo interfaceMethod, MethodInfo targetMethod)
        {
            MethodInfo stub;
            if (targetMethod.IsPrivate)
            {
                Delegate methodDelegate = ProxyBuilderHelper.CreateDelegateFromMethodInfo(targetMethod);
                String name = interfaceMethod.DeclaringType.FullName + "." + interfaceMethod.Name;
                FieldBuilder methodField = _typeBuilder.DefineField(name, methodDelegate.GetType(), FieldAttributes.Static | FieldAttributes.Private);
                _privateMethods.Add(new KeyValuePair<FieldBuilder, Delegate>(methodField, methodDelegate));

                stub = methodDelegate.GetType().GetMethod("Invoke");

                il.Emit(OpCodes.Ldsfld, methodField);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, _objectField);
                ParameterInfo[] stubParameterInfo = stub.GetParameters();
                for (int i = 1; i < stubParameterInfo.Length; i++)
                    il.Emit(OpCodes.Ldarg, i);
            }
            else
            {
                stub = targetMethod;

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, _objectField);
                ParameterInfo[] stubParameterInfo = stub.GetParameters();
                for (int i = 0; i < stubParameterInfo.Length; i++)
                    il.Emit(OpCodes.Ldarg, i + 1);
            }

            il.Emit(OpCodes.Call, stub);
            il.Emit(OpCodes.Ret);
        }
    }
}
