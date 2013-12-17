using ProxyMixin.Mappers;
using ProxyMixin.Mixins;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProxyMixin.Factories
{
    public class InterceptorFactory : ProxyFactory
    {
        public I Create<T, I>(T wrappedObject, InterceptorMixin<I> mixin) where T : I where I : class
        {
            return (I)base.CreateCore(wrappedObject, ProxyMapper<T>.Mapper, new Object[] { mixin });
        }
    }
}
