using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using ProxyMixin.Mixins;
using ProxyMixin.Mappers;

namespace ProxyMixin.Ctors
{
    public sealed class ChangeTrackingCtor
    {
        private readonly Dictionary<Object, IDynamicProxy> _proxies;

        public ChangeTrackingCtor()
        {
            _proxies = new Dictionary<Object, IDynamicProxy>();
        }

        internal Object Create(Object wrappedObject, IChangeTrackingMixin parent)
        {
            if (wrappedObject == null || wrappedObject.GetType().IsSealed)
                return wrappedObject;

            var func = (Func<Object, String, IChangeTrackingMixin, Object>)Create<Object>;
            MethodInfo methodInfo = func.Method.GetGenericMethodDefinition().MakeGenericMethod(wrappedObject.GetType());
            return (IDynamicProxy)methodInfo.Invoke(this, new Object[] { wrappedObject, null, parent });
        }
        public T Create<T>(T wrappedObject, String isChangedPropertyName = null, IChangeTrackingMixin parent = null)
        {
            if (wrappedObject == null || typeof(T).IsSealed)
                return wrappedObject;

            IDynamicProxy dynamicProxy;
            if (_proxies.TryGetValue(wrappedObject, out dynamicProxy))
                return (T)dynamicProxy;

            T proxy = CreateList<T>(wrappedObject, isChangedPropertyName, parent);
            if (proxy == null)
            {
                Object[] mixins = { new Mixins.ChangeTrackingMixin<T>(this, isChangedPropertyName, parent) };
                proxy = ProxyCtor.CreateCore(wrappedObject, PropertyChangedProxyMapper<T>.Mapper, mixins);
            }

            _proxies[wrappedObject] = (IDynamicProxy)proxy;
            return proxy;
        }
        private T CreateList<T>(T wrappedObject, String isChangedPropertyName = null, IChangeTrackingMixin parent = null)
        {
            Type k = ListBindingHelper.GetListItemType(wrappedObject);
            if (k == null)
                return default(T);

            if (!typeof(IList<>).MakeGenericType(k).IsAssignableFrom(typeof(T)))
                return default(T);

            Type mixinType = typeof(ListChangeTrackingMixin<,>).MakeGenericType(typeof(T), k);
            Object[] mixins = { (IDynamicMixin)Activator.CreateInstance(mixinType, this, isChangedPropertyName, parent) };
            return ProxyCtor.CreateCore(wrappedObject, PropertyChangedProxyMapper<T>.Mapper, mixins);
        }
        public void Update(IChangeTrackingMixin parent, bool acceptChanges)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            foreach (var pair in _proxies)
            {
                var mixin = (IChangeTrackingMixin)pair.Value.Mixins[0];
                if (mixin.ChangedStates == ChangedStates.None)
                    continue;

                IChangeTrackingMixin parentMixin = mixin;
                do
                {
                    if (parentMixin == parent)
                    {
                        mixin.Update(acceptChanges);
                        break;
                    }
                    parentMixin = parentMixin.Parent;
                }
                while (parentMixin != null);
            }
        }
    }
}
