using ProxyMixin.Mappers;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;

namespace ProxyMixin
{
    public interface IDynamicProxy
    {
        void MemberwiseMapFromWrappedObject();
        void MemberwiseMapToWrappedObject();

        Object[] Mixins
        {
            get;
        }
        Object WrappedObject
        {
            get;
        }
    }

    public interface IDynamicMixin
    {
        Type[] NoImplementInterfaces
        {
            get;
        }
        IDynamicProxy ProxyObject
        {
            get;
            set;
        }
    }

    public sealed class ProxyBuilder
    {
        private T Create<T>(T wrappedObject, params Object[] mixins)
        {
            return CreateProxy<T>(CreateType<T, ProxyMapper<T>>(ProxyMapper<T>.Instance, mixins), wrappedObject, mixins);
        }
        internal static T CreateProxy<T>(Type proxyType, T wrappedObject, Object[] mixins)
        {
            var proxy = (T)FormatterServices.GetUninitializedObject(proxyType);
            FieldInfo mixinsField = proxyType.GetField("mixins", BindingFlags.Instance | BindingFlags.NonPublic);
            mixinsField.SetValue(proxy, mixins);

            FieldInfo fieldInfo = proxyType.GetField(typeof(IDynamicProxy).Name + ".wrappedObject", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo.SetValue(proxy, wrappedObject);
            ((IDynamicProxy)proxy).MemberwiseMapFromWrappedObject();

            foreach (Object mixin in mixins)
            {
                var dynamicMixin = mixin as IDynamicMixin;
                if (dynamicMixin != null)
                    dynamicMixin.ProxyObject = (IDynamicProxy)proxy;
            }
            return proxy;
        }
        internal Type CreateType<T, K>(ProxyMapper<T, K> proxyMapper, params Object[] mixins) where K : ProxyMapper<T, K>
        {
            TypeBuilder typeBuilder = ProxyFactory.GetTypeBuilder<T>();
            proxyMapper.DefineInterfaceDynamicProxy(typeBuilder);
            for (int i = 0; i < mixins.Length; i++)
                DefineMixin(typeBuilder, mixins[i], proxyMapper.MixinsField, i);
            Type proxyType = typeBuilder.CreateType();
            proxyMapper.InitializeStaticFields(proxyType);

            return proxyType;
        }
        private static void DefineMixin(TypeBuilder typeBuilder, Object mixin, FieldBuilder mixinsField, int index)
        {
            Type mixinType = mixin.GetType();

            Type[] noImplementInterfaces = Type.EmptyTypes;
            var dynamicMixin = mixin as IDynamicMixin;
            if (dynamicMixin != null)
                noImplementInterfaces = dynamicMixin.NoImplementInterfaces;

            Type[] interfaceTypes = mixinType.GetInterfaces();
            foreach (Type interfaceType in interfaceTypes)
            {
                if (interfaceType == typeof(IDynamicMixin) || noImplementInterfaces.Contains(interfaceType))
                    continue;

                typeBuilder.AddInterfaceImplementation(interfaceType);
                ProxyBuilderHelper.DefineInterface(typeBuilder, interfaceType, mixinType,
                    (il, interfaceMethod, targetMethod) => GenerateMethod(il, interfaceMethod, targetMethod, mixinsField, index));
            }
        }
        private static void GenerateMethod(ILGenerator il, MethodInfo interfaceMethod, MethodInfo targetMethod, FieldBuilder mixinsField, int index)
        {
            MethodInfo stub = targetMethod.IsPrivate ? interfaceMethod : targetMethod;

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, mixinsField);
            il.Emit(OpCodes.Ldc_I4, index);
            il.Emit(OpCodes.Ldelem_Ref);

            ParameterInfo[] stubParameterInfo = stub.GetParameters();
            for (int i = 0; i < stubParameterInfo.Length; i++)
                il.Emit(OpCodes.Ldarg, i + 1);

            il.Emit(OpCodes.Call, stub);
            il.Emit(OpCodes.Ret);
        }
    }
}
