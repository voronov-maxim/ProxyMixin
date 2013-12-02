using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ProxyMixin.Mixins
{
    public class DynamicMixin<T> : IDynamicMetaObjectProvider, IDynamicMixin
    {
        private sealed class MetaObject : DynamicMetaObject
        {
            private readonly MethodInfo _getValueInfo;
            private readonly ConstantExpression _instance;
            private readonly BindingRestrictions _restrictions;
            private readonly MethodInfo _setValueInfo;

            public MetaObject(Expression expression, DynamicMixin<T> mixin)
                : base(expression, BindingRestrictions.Empty, mixin.ProxyObject)
            {
                _getValueInfo = ((Func<String, Object>)mixin.GetValue).Method;
                _setValueInfo = ((Func<String, Object, Object>)mixin.SetValue).Method;
                _instance = Expression.Constant(mixin);
                _restrictions = BindingRestrictions.GetTypeRestriction(expression, mixin.ProxyObject.GetType()); ;
            }

            public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
            {
                MethodCallExpression getValue = Expression.Call(_instance, _getValueInfo,
                    Expression.Constant(binder.Name));
                return new DynamicMetaObject(getValue, _restrictions);
            }
            public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
            {
                MethodCallExpression setValue = Expression.Call(_instance, _setValueInfo,
                    Expression.Constant(binder.Name),
                    Expression.Convert(value.Expression, typeof(Object)));
                return new DynamicMetaObject(setValue, _restrictions);
            }
        }

        private readonly PropertyDescriptorCollection _propertyDescriptors;
        private readonly Dictionary<String, Object> _values;

        public DynamicMixin()
        {
            _propertyDescriptors = TypeDescriptor.GetProperties(typeof(T));
            _values = new Dictionary<String, Object>();
        }

        protected virtual Object GetValue(String key)
        {
            PropertyDescriptor propertyDescriptor = _propertyDescriptors[key];
            if (propertyDescriptor == null)
                return _values[key];

            return propertyDescriptor.GetValue(ProxyObject.WrappedObject);
        }
        protected virtual Object SetValue(String key, Object value)
        {
            PropertyDescriptor propertyDescriptor = _propertyDescriptors[key];
            if (propertyDescriptor == null)
                _values[key] = value;
            else
                propertyDescriptor.SetValue(ProxyObject.WrappedObject, value);

            return value;
        }

        DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
        {
            return new MetaObject(parameter, this);
        }

        public virtual Type[] NoImplementInterfaces
        {
            get
            {
                return Type.EmptyTypes;
            }
        }
        public IDynamicProxy ProxyObject
        {
            get;
            set;
        }
    }
}