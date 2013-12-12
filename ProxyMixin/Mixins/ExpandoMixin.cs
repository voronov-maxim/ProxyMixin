using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace ProxyMixin.Mixins
{
    public class ExpandoMixin<T> : DynamicMixin<T>
    {
        private readonly MethodInfo[] _methodInfos;
        private readonly PropertyDescriptorCollection _propertyDescriptors;
        private readonly Dictionary<String, Object> _values;

        public ExpandoMixin()
        {
            _propertyDescriptors = TypeDescriptor.GetProperties(typeof(T));
            _values = new Dictionary<String, Object>();
            _methodInfos = typeof(T).GetMethods();
        }

        private MethodInfo GetMethod(String name)
        {
            foreach (MethodInfo methodInfo in _methodInfos)
                if (methodInfo.Name == name)
                    return methodInfo;

            return null;
        }
        protected override Object GetValue(String name)
        {
            Object result;
            if (TryGetValue(name, out result))
                return result;

            return _values[name];
        }
        protected override Object Invoke(String name, DynamicMetaObject[] args)
        {
            Object result;
            if (TryInvoke(name, args, out result))
                return result;

            return null;
        }
        protected override void SetValue(String name, Object value)
        {
            if (!TrySetValue(name, value))
                _values[name] = value;
        }
        protected bool TryGetValue(String name, out Object result)
        {
            PropertyDescriptor propertyDescriptor = _propertyDescriptors[name];
            if (propertyDescriptor == null)
            {
                result = null;
                return false;
            }

            result = propertyDescriptor.GetValue(base.ProxyObject);
            return true;
        }
        protected bool TryInvoke(String name, DynamicMetaObject[] args, out Object result)
        {
            MethodInfo methodInfo = GetMethod(name);
            if (methodInfo == null)
            {
                result = null;
                return false;
            }

            var parameters = new Object[args.Length];
            for (int i = 0; i < parameters.Length; i++)
                parameters[i] = args[i].Value;
            result = methodInfo.Invoke(ProxyObject, parameters);
            return true;
        }
        protected bool TrySetValue(String name, Object value)
        {
            PropertyDescriptor propertyDescriptor = _propertyDescriptors[name];
            if (propertyDescriptor == null)
                return false;

            propertyDescriptor.SetValue(ProxyObject.WrappedObject, value);
            return true;
        }
    }
}
