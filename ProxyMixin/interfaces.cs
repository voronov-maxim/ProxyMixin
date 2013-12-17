using System;

namespace ProxyMixin
{
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

    public interface IDynamicProxy
    {
        IProxyMapper Mapper
        {
            get;
        }
        Object[] Mixins
        {
            get;
        }
        Object WrappedObject
        {
            get;
        }
    }

    public interface IMixinSource
    {
        IDynamicMixin GetMixin();
    }

    public interface IProxyMapper
    {
        void Map(Object source, Object target);
    }
}
