using ProxyMixin.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.Mappers
{
    public class ProxyMapper<T, K> where K : ProxyMapper<T, K>
    {
        private sealed class ProxyMapperImpl : IProxyMapper
        {
            private readonly Action<T, T> _mapperAction;

            public ProxyMapperImpl(Action<T, T> mapperAction)
            {
                _mapperAction = mapperAction;
            }

            public void Map(Object source, Object target)
            {
                _mapperAction((T)source, (T)target);
            }
        }

        public static IProxyMapper Mapper = CreateProxyMapper();

        protected ProxyMapper()
        {
        }

        private static IProxyMapper CreateProxyMapper()
        {
            ConstructorInfo ctor = typeof(K).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
            var mapperBuilder = (K)ctor.Invoke(null);
            Action<T, T> mapperAction = mapperBuilder.CreateMapperAction();
            return new ProxyMapperImpl(mapperAction);
        }
        private Action<T, T> CreateMapperAction()
        {
            Type type = typeof(T);
            String name = typeof(K).Name + "[" + typeof(T).Name + "]";
            var dynamicMethod = new DynamicMethod(name, null, new Type[] { type, type }, ProxyFactory.GetModuleBuilder(), true);
            ILGenerator il = dynamicMethod.GetILGenerator();

            while (type != typeof(Object))
            {
                FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                foreach (FieldInfo fieldInfo in fieldInfos)
                    Emit(il, fieldInfo);
                type = type.BaseType;
            }
            il.Emit(OpCodes.Ret);

            return (Action<T, T>)dynamicMethod.CreateDelegate(typeof(Action<T, T>));
        }
        protected virtual void Emit(ILGenerator il, FieldInfo fieldInfo)
        {
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, fieldInfo);
            il.Emit(OpCodes.Stfld, fieldInfo);
        }
    }

    public sealed class ProxyMapper<T> : ProxyMapper<T, ProxyMapper<T>>
    {
        private ProxyMapper()
        {
        }
    }
}
