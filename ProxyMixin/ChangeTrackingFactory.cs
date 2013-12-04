using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using ProxyMixin.Mixins;
using ProxyMixin.Mappers;

namespace ProxyMixin
{
    public sealed class ChangeTrackingFactory : ProxyFactory
    {
        private sealed class KeyWeakReference : WeakReference
        {
            public KeyWeakReference(Object target)
                : base(target)
            {
            }

            public override bool Equals(Object obj)
            {
                return ((WeakReference)obj).Target == base.Target;
            }
            public override int GetHashCode()
            {
                Object target = base.Target;
                return target == null ? 0 : target.GetHashCode();
            }
        }

        private readonly Dictionary<Object, IDynamicProxy> _proxies;

        public ChangeTrackingFactory()
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
            var weakWrappedObject = new KeyWeakReference(wrappedObject);
            if (_proxies.TryGetValue(weakWrappedObject, out dynamicProxy))
                return (T)dynamicProxy;

            T proxy = CreateList<T>(wrappedObject, isChangedPropertyName, parent);
            if (proxy == null)
            {
                var proxyMapper = new PropertyChangedProxyMapper<T>();
                Object[] mixins = { new Mixins.ChangeTrackingMixin<T>(this, isChangedPropertyName, parent) };
                proxy = base.CreateCore(wrappedObject, proxyMapper, mixins);
            }

            _proxies[weakWrappedObject] = (IDynamicProxy)proxy;
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
            var proxyMapper = new PropertyChangedProxyMapper<T>();
            Object[] mixins = { (IDynamicMixin)Activator.CreateInstance(mixinType, this, isChangedPropertyName, parent) };
            return base.CreateCore(wrappedObject, proxyMapper, mixins);
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
