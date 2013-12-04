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
            var proxyMapper = new ProxyMapper<T>();
            return CreateProxy<T>(CreateType<T>(proxyMapper, mixins), wrappedObject, mixins);
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
        internal Type CreateType<T>(ProxyMapper<T> proxyMapper, params Object[] mixins)
        {
            TypeBuilder typeBuilder = ProxyFactory.GetTypeBuilder<T>();
            proxyMapper.DefineInterfaceDynamicProxy(typeBuilder);
            for (int i = 0; i < mixins.Length; i++)
                DefineMixin(typeBuilder, mixins[i], proxyMapper.MixinsField, i);
            Type proxyType = typeBuilder.CreateType();
            proxyMapper.InitializeStaticFields(proxyType);

            return proxyType;
        }
        private static void DefineEvent(TypeBuilder typeBuilder, EventInfo eventInfo,
            MethodBuilder addMethodBuilder, MethodBuilder removeMethodBuilder)
        {
            String eventName = eventInfo.DeclaringType.FullName + "." + eventInfo.Name;
            EventBuilder eventBuilder = typeBuilder.DefineEvent(eventName, EventAttributes.None, eventInfo.EventHandlerType);

            eventBuilder.SetAddOnMethod(addMethodBuilder);
            eventBuilder.SetRemoveOnMethod(removeMethodBuilder);
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
                InterfaceMapping mapping = mixinType.GetInterfaceMap(interfaceType);
                for (int i = 0; i < mapping.InterfaceMethods.Length; i++)
                {
                    MethodInfo stub = mapping.TargetMethods[i].IsPrivate ? mapping.InterfaceMethods[i] : mapping.TargetMethods[i];
                    mapping.TargetMethods[i] = ProxyBuilderHelper.DefineMethod(typeBuilder, mapping.InterfaceMethods[i],
                        il => ProxyBuilderHelper.GenerateStandardMethod(il, stub, mixinsField, index));
                }

                foreach (PropertyInfo propertyInfo in interfaceType.GetProperties())
                {
                    MethodBuilder getMethodBuilder = null;
                    MethodInfo getMethodInfo = propertyInfo.GetGetMethod();
                    if (getMethodInfo != null)
                        getMethodBuilder = (MethodBuilder)GetTargetMethodInfo(mapping, getMethodInfo);

                    MethodBuilder setMethodBuilder = null;
                    MethodInfo setMethodInfo = propertyInfo.GetSetMethod();
                    if (setMethodInfo != null)
                        setMethodBuilder = (MethodBuilder)GetTargetMethodInfo(mapping, setMethodInfo);

                    ProxyBuilderHelper.DefineProperty(typeBuilder, propertyInfo, getMethodBuilder, setMethodBuilder);
                }

                foreach (EventInfo eventInfo in interfaceType.GetEvents())
                {
                    var addMethodBuilder = (MethodBuilder)GetTargetMethodInfo(mapping, eventInfo.GetAddMethod());
                    var removeMethodBuilder = (MethodBuilder)GetTargetMethodInfo(mapping, eventInfo.GetRemoveMethod());
                    DefineEvent(typeBuilder, eventInfo, addMethodBuilder, removeMethodBuilder);
                }
            }
        }
        private static MethodInfo GetTargetMethodInfo(InterfaceMapping mapping, MethodInfo interfaceMethod)
        {
            for (int i = 0; i < mapping.InterfaceMethods.Length; i++)
                if (mapping.InterfaceMethods[i] == interfaceMethod)
                    return mapping.TargetMethods[i];

            throw new InvalidOperationException("mixin target method not found");
        }
    }
}
