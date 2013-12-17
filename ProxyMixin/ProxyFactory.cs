using ProxyMixin.Builders;
using ProxyMixin.Mappers;
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

        private static int _counter;
        private static ModuleBuilder _moduleBuilder;
        private static readonly Dictionary<ProxyTypeDef, Type> _typeProxyCache = new Dictionary<ProxyTypeDef, Type>();
        private static readonly Dictionary<Type, Delegate> _interfaceInvokerCache = new Dictionary<Type, Delegate>();

        public ProxyFactory()
        {
        }

        public static T Create<T>(T wrappedObject, params Object[] mixins)
        {
            return new ProxyFactory().CreateCore(wrappedObject, ProxyMapper<T>.Mapper, mixins);
        }
        protected T CreateCore<T>(T wrappedObject, IProxyMapper proxyMapper, Object[] mixins)
        {
            Object[] mixins2 = mixins;
            for (int i = 0; i < mixins2.Length; i++)
            {
                var mixinSource = mixins2[i] as IMixinSource;
                if (mixinSource != null)
                    mixins2[i] = mixinSource.GetMixin();
            }

            var interfaceTypes = new HashSet<Type>();
            foreach (Object mixin in mixins2)
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
                proxyType = ProxyBuilder.CreateType<T>(mixins2);
                _typeProxyCache.Add(proxyTypeDef, proxyType);
            }

            return ProxyBuilder.CreateProxy<T>(proxyType, wrappedObject, proxyMapper, mixins2);
        }
        public static T CreateExpando<T>(T wrappedObject)
        {
            Object[] mixins = { new Mixins.ExpandoMixin<T>() };
            return new ProxyFactory().CreateCore(wrappedObject, ProxyMapper<T>.Mapper, mixins);
        }
        public static T CreatePropertyChanged<T>(T wrappedObject, String isChangedPropertyName = null)
        {
            Object[] mixins = { new Mixins.PropertyChangedMixin<T>(isChangedPropertyName) };
            return new ProxyFactory().CreateCore(wrappedObject, ProxyMapper<T>.Mapper, mixins);
        }
        public static I GetMethodInvoker<T, I>(T interfaceObject)
            where T : I
            where I : class
        {
            Func<I, I> ctor;
            Delegate @delegate;
            if (_interfaceInvokerCache.TryGetValue(typeof(T), out @delegate))
                ctor = (Func<I, I>)@delegate;
            else
            {
                ModuleBuilder moduleBuilder = GetModuleBuilder();
                String name = "Invoker" + typeof(I).Name;
                TypeBuilder typeBuilder = moduleBuilder.DefineType(name, TypeAttributes.Class | TypeAttributes.Sealed, null, new Type[] { typeof(I) });

                var interfaceInvokerBuilder = new InterfaceInvokerBuilder<T, I>(typeBuilder);
                ctor = interfaceInvokerBuilder.CreateType();
                _interfaceInvokerCache[typeof(T)] = ctor;
            }

            return ctor(interfaceObject);
        }
        internal static ModuleBuilder GetModuleBuilder()
        {
            if (_moduleBuilder == null)
            {
                var asmName = new AssemblyName("DynamicProxyMixin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
                AssemblyBuilder asmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);
                _moduleBuilder = asmBuilder.DefineDynamicModule(asmName.Name);
            }
            return _moduleBuilder;
        }
        internal static TypeBuilder GetTypeBuilder<T>()
        {
            ModuleBuilder moduleBuilder = GetModuleBuilder();
            String name = "Proxy" + typeof(T).Name + "(" + (++_counter).ToString() + ")";
            return moduleBuilder.DefineType(name, TypeAttributes.Class | TypeAttributes.Sealed, typeof(T));
        }
    }
}
