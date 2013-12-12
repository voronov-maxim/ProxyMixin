using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.Builders
{
    internal abstract class InterfaceBuilder<T> where T : class
    {
        private readonly TypeBuilder _typeBuilder;

        public InterfaceBuilder(TypeBuilder typeBuilder)
        {
            _typeBuilder = typeBuilder;
        }

        public ReadOnlyCollection<MethodInfoMapping> DefineInterface()
        {
            List<MethodInfoMapping> mappings = DefineInterface(_typeBuilder, typeof(T)).ToList();
            foreach (Type interfaceType in typeof(T).GetInterfaces())
                mappings.AddRange(DefineInterface(_typeBuilder, interfaceType));
            return mappings.AsReadOnly();
        }
        private MethodInfoMapping[] DefineInterface(TypeBuilder typeBuilder, Type interfaceType)
        {
            var mappings = GetMethodInfoMapping(interfaceType);
            foreach (MethodInfoMapping mapping in mappings)
            {
                MethodInfo methodInfo;
                PropertyInfo propertyInfo;
                EventInfo eventInfo;

                if ((methodInfo = mapping.MemberInfo as MethodInfo) != null)
                {
                    MethodBuilder methodBuilder = ProxyBuilderHelper.DefineMethod(typeBuilder, methodInfo, il => GenerateMethod(il, mapping));
                }
                else if ((propertyInfo = mapping.MemberInfo as PropertyInfo) != null)
                {
                    MethodBuilder getMethodBuilder = null;
                    if (mapping.GetOrAddMethodInfo != null)
                        getMethodBuilder = ProxyBuilderHelper.DefineMethod(typeBuilder, mapping.GetOrAddMethodInfo, il => GenerateGetProperty(il, mapping));

                    MethodBuilder setMethodBuilder = null;
                    if (mapping.SetOrRemoveMethodInfo != null)
                        getMethodBuilder = ProxyBuilderHelper.DefineMethod(typeBuilder, mapping.SetOrRemoveMethodInfo, il => GenerateSetProperty(il, mapping));

                    ProxyBuilderHelper.DefineProperty(typeBuilder, propertyInfo, getMethodBuilder, setMethodBuilder);
                }
                else if ((eventInfo = mapping.MemberInfo as EventInfo) != null)
                {
                    MethodBuilder addMethodBuilder = null;
                    if (mapping.GetOrAddMethodInfo != null)
                        addMethodBuilder = ProxyBuilderHelper.DefineMethod(typeBuilder, mapping.GetOrAddMethodInfo, il => GenerateAddEvent(il, mapping));

                    MethodBuilder removeMethodBuilder = null;
                    if (mapping.SetOrRemoveMethodInfo != null)
                        addMethodBuilder = ProxyBuilderHelper.DefineMethod(typeBuilder, mapping.SetOrRemoveMethodInfo, il => GenerateRemoveEvent(il, mapping));

                    ProxyBuilderHelper.DefineProperty(typeBuilder, propertyInfo, addMethodBuilder, removeMethodBuilder);
                }
            }
            return mappings;
        }
        protected virtual void GenerateAddEvent(ILGenerator il, MethodInfoMapping mapping)
        {
            il.Emit(OpCodes.Ret);
        }
        protected virtual void GenerateGetProperty(ILGenerator il, MethodInfoMapping mapping)
        {
            il.Emit(OpCodes.Ret);
        }
        protected virtual void GenerateMethod(ILGenerator il, MethodInfoMapping mapping)
        {
            il.Emit(OpCodes.Ret);
        }
        protected virtual void GenerateRemoveEvent(ILGenerator il, MethodInfoMapping mapping)
        {
            il.Emit(OpCodes.Ret);
        }
        protected virtual void GenerateSetProperty(ILGenerator il, MethodInfoMapping mapping)
        {
            il.Emit(OpCodes.Ret);
        }
        private static MethodInfoMapping[] GetMethodInfoMapping(Type interfaceType)
        {
            var props = interfaceType.GetProperties().Select(p => new
            {
                memberInfo = (MemberInfo)p,
                getMethod = p.GetGetMethod(),
                setMethod = p.GetSetMethod()
            }).ToList();

            var events = interfaceType.GetEvents().Select(e => new
            {
                memberInfo = (MemberInfo)e,
                getMethod = e.GetAddMethod(),
                setMethod = e.GetRemoveMethod()
            }).ToList();

            List<MethodInfo> specMethods =
                props.Select(p => p.getMethod).Union(
                props.Select(p => p.setMethod).Union(
                events.Select(e => e.getMethod).Union(
                events.Select(e => e.setMethod)))).Where(m => m != null).ToList();

            return interfaceType.GetMethods().
                Except(specMethods).Select(m => new MethodInfoMapping(m, null, null)).Union(
                props.Select(p => new MethodInfoMapping(p.memberInfo, p.getMethod, p.setMethod)).Union(
                events.Select(e => new MethodInfoMapping(e.memberInfo, e.getMethod, e.setMethod)))).ToArray();
        }

        protected TypeBuilder TypeBuilder
        {
            get
            {
                return _typeBuilder;
            }
        }
    }

    internal sealed class MethodInfoMapping
    {
        private readonly MemberInfo _memberInfo;
        private readonly MethodInfo _getOrAddMethodInfo;
        private readonly MethodInfo _setOrRemoveMethodInfo;

        public MethodInfoMapping(MemberInfo memberInfo, MethodInfo getOrAddMethodInfo, MethodInfo setOrRemoveMethodInfo)
        {
            _memberInfo = memberInfo;
            _getOrAddMethodInfo = getOrAddMethodInfo;
            _setOrRemoveMethodInfo = setOrRemoveMethodInfo;
        }

        public FieldBuilder FieldBuilder
        {
            get;
            set;
        }
        public MethodInfo GetOrAddMethodInfo
        {
            get
            {
                return _getOrAddMethodInfo;
            }
        }
        public MemberInfo MemberInfo
        {
            get
            {
                return _memberInfo;
            }
        }
        public MethodInfo SetOrRemoveMethodInfo
        {
            get
            {
                return _setOrRemoveMethodInfo;
            }
        }
    }
}
