using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace ProxyMixin.Mixins
{
    public abstract class DynamicMixin<T> : IDynamicMetaObjectProvider, IDynamicMixin
    {
        private sealed class MetaObjectContext
        {
            private readonly MethodInfo _getValueInfo;
            private readonly ConstantExpression _instance;
            private readonly MethodInfo _invokeInfo;
            private readonly DynamicMixin<T> _mixin;
            private readonly MethodInfo _setValueInfo;

            public MetaObjectContext(DynamicMixin<T> mixin)
            {
                _getValueInfo = ((Func<String, Object>)mixin.GetValue).Method;
                _instance = Expression.Constant(mixin);
                _invokeInfo = ((Func<String, DynamicMetaObject[], Object>)mixin.Invoke).Method;
                _mixin = mixin;
                _setValueInfo = ((Func<String, Object, Object>)mixin.SetValueInternal).Method;
            }

            public MethodInfo GetValueInfo
            {
                get
                {
                    return _getValueInfo;
                }
            }
            public ConstantExpression Instance
            {
                get
                {
                    return _instance;
                }
            }
            public MethodInfo InvokeInfo
            {
                get
                {
                    return _invokeInfo;
                }
            }
            public T ProxyObject
            {
                get
                {
                    return (T)_mixin.ProxyObject;
                }
            }
            public MethodInfo SetValueInfo
            {
                get
                {
                    return _setValueInfo;
                }
            }
        }

        private sealed class MetaObject : DynamicMetaObject
        {
            private readonly MetaObjectContext _ctx;
            private readonly BindingRestrictions _restrictions;

            public MetaObject(Expression expression, MetaObjectContext ctx)
                : base(expression, BindingRestrictions.Empty, ctx.ProxyObject)
            {
                _ctx = ctx;
                _restrictions = BindingRestrictions.GetTypeRestriction(expression, _ctx.ProxyObject.GetType()); ;
            }

            public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
            {
                MethodCallExpression getValue = Expression.Call(_ctx.Instance, _ctx.GetValueInfo,
                    Expression.Constant(binder.Name));
                return new DynamicMetaObject(getValue, _restrictions);
            }
            public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
            {
                MethodCallExpression invoke = Expression.Call(_ctx.Instance, _ctx.InvokeInfo,
                    Expression.Constant(binder.Name),
                    Expression.Constant(args));
                return new DynamicMetaObject(invoke, _restrictions);
            }
            public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
            {
                MethodCallExpression setValue = Expression.Call(_ctx.Instance, _ctx.SetValueInfo,
                    Expression.Constant(binder.Name),
                    Expression.Convert(value.Expression, typeof(Object)));
                return new DynamicMetaObject(setValue, _restrictions);
            }
        }

        private readonly MetaObjectContext _ctx;

        public DynamicMixin()
        {
            _ctx = new MetaObjectContext(this);
        }

        protected abstract Object GetValue(String name);
        protected abstract Object Invoke(String name, DynamicMetaObject[] args);
        protected abstract void SetValue(String name, Object value);
        protected Object SetValueInternal(String name, Object value)
        {
            SetValue(name, value);
            return null;
        }

        DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
        {
            return new MetaObject(parameter, _ctx);
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