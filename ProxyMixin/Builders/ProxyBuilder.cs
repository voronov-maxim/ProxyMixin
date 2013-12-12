using ProxyMixin.Mappers;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;

namespace ProxyMixin.Builders
{
    public sealed class ProxyBuilder
    {
        private FieldBuilder _mapperField;
        private FieldBuilder _mixinsField;
        private readonly TypeBuilder _typeBuilder;
        private FieldBuilder _wrappedObjectField;

        private ProxyBuilder(TypeBuilder typeBuilder)
        {
            _typeBuilder = typeBuilder;
        }

        private void CreateMapper()
        {
            PropertyInfo propertyInfo = typeof(IDynamicProxy).GetProperty("Mapper");
            MethodBuilder getMethodBuilder = ProxyBuilderHelper.DefineMethod(_typeBuilder, propertyInfo.GetGetMethod(),
                il => ProxyBuilderHelper.GenerateFieldMethod(il, _mapperField));
            ProxyBuilderHelper.DefineProperty(_typeBuilder, propertyInfo, getMethodBuilder, null);
        }
        private void CreateMixins()
        {
            PropertyInfo propertyInfo = typeof(IDynamicProxy).GetProperty("Mixins");
            MethodBuilder getMethodBuilder = ProxyBuilderHelper.DefineMethod(_typeBuilder, propertyInfo.GetGetMethod(),
                il => ProxyBuilderHelper.GenerateFieldMethod(il, _mixinsField));
            ProxyBuilderHelper.DefineProperty(_typeBuilder, propertyInfo, getMethodBuilder, null);
        }
        internal static T CreateProxy<T>(Type proxyType, T wrappedObject, IProxyMapper proxyMapper, Object[] mixins)
        {
            var proxy = (T)FormatterServices.GetUninitializedObject(proxyType);

            FieldInfo fieldInfo = proxyType.GetField("mixins", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo.SetValue(proxy, mixins);

            fieldInfo = proxyType.GetField("wrappedObject", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo.SetValue(proxy, wrappedObject);

            fieldInfo = proxyType.GetField("mapper", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo.SetValue(proxy, proxyMapper);

            proxyMapper.Map(wrappedObject, proxy);
            foreach (Object mixin in mixins)
            {
                var dynamicMixin = mixin as IDynamicMixin;
                if (dynamicMixin != null)
                    dynamicMixin.ProxyObject = (IDynamicProxy)proxy;
            }
            return proxy;
        }
        public static Type CreateType<T>(params Object[] mixins)
        {
            return new ProxyBuilder(ProxyFactory.GetTypeBuilder<T>()).CreateTypeInternal<T>(mixins);
        }
        private Type CreateTypeInternal<T>(params Object[] mixins)
        {
            DefineInterfaceDynamicProxy();
            for (int i = 0; i < mixins.Length; i++)
                DefineMixin(_typeBuilder, mixins[i], _mixinsField, i);
            return _typeBuilder.CreateType();
        }
        private void CreateWrappedObject()
        {
            PropertyInfo propertyInfo = typeof(IDynamicProxy).GetProperty("WrappedObject");
            MethodBuilder getMethodBuilder = ProxyBuilderHelper.DefineMethod(_typeBuilder, propertyInfo.GetGetMethod(),
                il => ProxyBuilderHelper.GenerateFieldMethod(il, _wrappedObjectField));
            ProxyBuilderHelper.DefineProperty(_typeBuilder, propertyInfo, getMethodBuilder, null);
        }
        private void DefineInterfaceDynamicProxy()
        {
            _typeBuilder.AddInterfaceImplementation(typeof(IDynamicProxy));

            _mapperField = _typeBuilder.DefineField("mapper", typeof(IProxyMapper), FieldAttributes.Private);
            _mixinsField = _typeBuilder.DefineField("mixins", typeof(Object[]), FieldAttributes.Private);
            _wrappedObjectField = _typeBuilder.DefineField("wrappedObject", typeof(Object), FieldAttributes.Private);

            CreateMapper();
            CreateMixins();
            CreateWrappedObject();
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
