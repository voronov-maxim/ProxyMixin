using ProxyMixin.Mappers;
using ProxyMixin.Mixins;
using System;

namespace ProxyMixin.Ctors
{
    public static class InterceptorCtor
    {
        public static I Create<T, I>(T wrappedObject, InterceptorMixin<T, I> mixin) where T : I where I : class
        {
            return (I)ProxyCtor.CreateCore(wrappedObject, ProxyMapper<T>.Mapper, new Object[] { mixin });
        }
    }
}
