using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin
{
    public class ProxyFactory
    {
        private sealed class ProxyTypeDef
        {
            private readonly int _hashCode;
            private readonly HashSet<Type> _interfaceTypes;
            private readonly Type _wrappedType;

            public ProxyTypeDef(Type wrappedType, HashSet<Type> interfaceTypes)
            {
                _wrappedType = wrappedType;
                _interfaceTypes = interfaceTypes;

                _hashCode = ComputeHashCode();
            }

            private int ComputeHashCode()
            {
                int h1 = _wrappedType.GetHashCode();
                foreach (Type interfaceType in _interfaceTypes)
                    h1 = (h1 << 5) + h1 ^ interfaceType.GetHashCode();
                return h1;
            }
            public override bool Equals(Object obj)
            {
                var proxyTypeDef = obj as ProxyTypeDef;
                if (proxyTypeDef == null)
                    return false;

                if (WrappedType != proxyTypeDef.WrappedType)
                    return false;

                return InterfaceTypes.SetEquals(proxyTypeDef.InterfaceTypes);
            }
            public override int GetHashCode()
            {
                return _hashCode;
            }

            public HashSet<Type> InterfaceTypes
            {
                get
                {
                    return _interfaceTypes;
                }
            }
            public Type WrappedType
            {
                get
                {
                    return _wrappedType;
                }
            }
        }
        private static ModuleBuilder _moduleBuilder;
        private readonly ProxyBuilder _proxyBuilder;
        private static readonly Dictionary<ProxyTypeDef, Type> _typeProxyCache = new Dictionary<ProxyTypeDef, Type>();

        public ProxyFactory()
        {
            _proxyBuilder = new ProxyBuilder();
        }

        protected T CreateCore<T>(T wrappedObject, ProxyMapper<T> proxyMapper, Object[] mixins)
        {
            var interfaceTypes = new HashSet<Type>();
            foreach (Object mixin in mixins)
            {
                interfaceTypes.UnionWith(mixin.GetType().GetInterfaces());
                var dynamicMixin = mixin as IDynamicMixin;
                if (dynamicMixin != null)
                    interfaceTypes.ExceptWith(dynamicMixin.NoImplementInterfaces);
            }
            var proxyTypeDef = new ProxyTypeDef(wrappedObject.GetType(), interfaceTypes);

            Type proxyType = null;
            if (!_typeProxyCache.TryGetValue(proxyTypeDef, out proxyType))
            {
                proxyType = _proxyBuilder.CreateType<T>(proxyMapper, mixins);
                _typeProxyCache.Add(proxyTypeDef, proxyType);
            }

            return ProxyBuilder.CreateProxy<T>(proxyType, wrappedObject, mixins);
        }
        public static T CreateDynamic<T>(T wrappedObject)
        {
            var proxyMapper = new ProxyMapper<T>();
            Object[] mixins = { new Mixins.DynamicMixin<T>() };
            return new ProxyFactory().CreateCore(wrappedObject, proxyMapper, mixins);
        }
        public static T CreatePropertyChanged<T>(T wrappedObject, String isChangedPropertyName = null)
        {
            var proxyMapper = new ProxyMapper<T>();
            Object[] mixins = { new Mixins.PropertyChangedMixin<T>(isChangedPropertyName) };
            return new ProxyFactory().CreateCore(wrappedObject, proxyMapper, mixins);
        }
        internal static TypeBuilder GetTypeBuilder<T>()
        {
            if (_moduleBuilder == null)
            {
                var asmName = new AssemblyName("DynamicProxyMixin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
                AssemblyBuilder asmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);
                _moduleBuilder = asmBuilder.DefineDynamicModule(asmName.Name);
            }

            String name = typeof(T).Name + "(" + (_typeProxyCache.Count + 1).ToString() + ")";
            return _moduleBuilder.DefineType(name, TypeAttributes.Class, typeof(T));
        }
    }
}
