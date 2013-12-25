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
        private sealed class DynamicProxyInterfaceBuilder : InterfaceBuilder<IDynamicProxy>
        {
            public DynamicProxyInterfaceBuilder(TypeBuilder typeBuilder)
                : base(typeBuilder)
            {
            }

            protected override void GenerateGetProperty(ILGenerator il, MethodInfoMapping mapping)
            {
                base.DefineField(mapping);
                ProxyBuilderHelper.GenerateGetFieldMethod(il, mapping.FieldBuilder);
            }
            protected override void GenerateSetProperty(ILGenerator il, MethodInfoMapping mapping)
            {
                base.DefineField(mapping);
                ProxyBuilderHelper.GenerateSetFieldMethod(il, mapping.FieldBuilder);
            }
        }

        private readonly TypeBuilder _typeBuilder;

        private ProxyBuilder(TypeBuilder typeBuilder)
        {
            _typeBuilder = typeBuilder;
        }

        internal static T CreateProxy<T>(Type proxyType, T wrappedObject, IProxyMapper proxyMapper, Object[] mixins)
        {
            var proxy = (T)FormatterServices.GetUninitializedObject(proxyType);
            String fieldName = typeof(IDynamicProxy).FullName + ".";

            FieldInfo fieldInfo = proxyType.GetField(fieldName + "Mapper", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo.SetValue(proxy, proxyMapper);

            fieldInfo = proxyType.GetField(fieldName + "Mixins", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo.SetValue(proxy, mixins);

            fieldInfo = proxyType.GetField(fieldName + "WrappedObject", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo.SetValue(proxy, wrappedObject);

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
            return new ProxyBuilder(ProxyCtor.GetTypeBuilder<T>()).CreateTypeInternal<T>(mixins);
        }
        private Type CreateTypeInternal<T>(params Object[] mixins)
        {
            var interfaceBuilder = new DynamicProxyInterfaceBuilder(_typeBuilder);
            MethodInfoMappingCollection mappings = interfaceBuilder.DefineInterface();

            FieldBuilder mixinsField = mappings["Mixins"].FieldBuilder;
            for (int i = 0; i < mixins.Length; i++)
                DefineMixin(_typeBuilder, mixins[i], mixinsField, i);
            return _typeBuilder.CreateType();
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
