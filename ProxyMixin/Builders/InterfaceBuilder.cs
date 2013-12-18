﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.Builders
{
    public abstract class InterfaceBuilder<T> where T : class
    {
        private readonly TypeBuilder _typeBuilder;

        protected InterfaceBuilder(TypeBuilder typeBuilder)
        {
            _typeBuilder = typeBuilder;
        }

        protected void DefineField(MethodInfoMapping mapping)
        {
            if (mapping.FieldBuilder != null)
                return;

            var propertyInfo = mapping.MemberInfo as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException("mapping.MemberInfo is not PropertyInfo", "mapping");

            String name = typeof(T).FullName + "." + mapping.MemberInfo.Name;
            mapping.FieldBuilder = TypeBuilder.DefineField(name, propertyInfo.PropertyType, FieldAttributes.Private);
        }
        public MethodInfoMappingCollection DefineInterface()
        {
            List<MethodInfoMapping> mappings = DefineInterface(typeof(T)).ToList();
            foreach (Type interfaceType in typeof(T).GetInterfaces())
                mappings.AddRange(DefineInterface(interfaceType));
            return new MethodInfoMappingCollection(mappings);
        }
        private List<MethodInfoMapping> DefineInterface(Type interfaceType)
        {
            TypeBuilder.AddInterfaceImplementation(interfaceType);
			var proxyMethodBuilder = new ProxyMethodBuilder(TypeBuilder);
            var mappings = GetMethodInfoMapping(interfaceType);
            foreach (MethodInfoMapping mapping in mappings)
            {
                MethodInfo methodInfo;
                PropertyInfo propertyInfo;
                EventInfo eventInfo;

                if ((methodInfo = mapping.MemberInfo as MethodInfo) != null)
                {
					MethodBuilder methodBuilder = proxyMethodBuilder.DefineMethod(methodInfo, il => GenerateMethod(il, mapping));
                }
                else if ((propertyInfo = mapping.MemberInfo as PropertyInfo) != null)
                {
                    MethodBuilder getMethodBuilder = null;
                    int indexParameterCount = -1;
                    if (mapping.GetOrAddMethodInfo != null)
                    {
                        indexParameterCount = mapping.GetOrAddMethodInfo.GetParameters().Length;
                        if (indexParameterCount == 0)
							getMethodBuilder = proxyMethodBuilder.DefineMethod(mapping.GetOrAddMethodInfo, il => GenerateGetProperty(il, mapping));
                        else
							getMethodBuilder = proxyMethodBuilder.DefineMethod(mapping.GetOrAddMethodInfo, il => GenerateGetIndexProperty(il, mapping));
                    }

                    MethodBuilder setMethodBuilder = null;
                    if (mapping.SetOrRemoveMethodInfo != null)
                    {
                        if (indexParameterCount == -1)
                            indexParameterCount = mapping.SetOrRemoveMethodInfo.GetParameters().Length - 1;
                        if (indexParameterCount == 0)
							setMethodBuilder = proxyMethodBuilder.DefineMethod(mapping.SetOrRemoveMethodInfo, il => GenerateSetProperty(il, mapping));
                        else
							setMethodBuilder = proxyMethodBuilder.DefineMethod(mapping.SetOrRemoveMethodInfo, il => GenerateSetIndexProperty(il, mapping));
                    }

                    ProxyBuilderHelper.DefineProperty(TypeBuilder, propertyInfo, getMethodBuilder, setMethodBuilder);
                }
                else if ((eventInfo = mapping.MemberInfo as EventInfo) != null)
                {
                    MethodBuilder addMethodBuilder = null;
                    if (mapping.GetOrAddMethodInfo != null)
						addMethodBuilder = proxyMethodBuilder.DefineMethod(mapping.GetOrAddMethodInfo, il => GenerateAddEvent(il, mapping));

                    MethodBuilder removeMethodBuilder = null;
                    if (mapping.SetOrRemoveMethodInfo != null)
						addMethodBuilder = proxyMethodBuilder.DefineMethod(mapping.SetOrRemoveMethodInfo, il => GenerateRemoveEvent(il, mapping));

                    ProxyBuilderHelper.DefineProperty(TypeBuilder, propertyInfo, addMethodBuilder, removeMethodBuilder);
                }
            }
            return mappings;
        }
        protected virtual void GenerateAddEvent(ILGenerator il, MethodInfoMapping mapping)
        {
            throw new NotImplementedException("GenerateAddEvent");
        }
        protected virtual void GenerateGetIndexProperty(ILGenerator il, MethodInfoMapping mapping)
        {
            throw new NotImplementedException("GenerateGetIndexProperty");
        }
        protected virtual void GenerateGetProperty(ILGenerator il, MethodInfoMapping mapping)
        {
            throw new NotImplementedException("GenerateGetProperty");
        }
        protected virtual void GenerateMethod(ILGenerator il, MethodInfoMapping mapping)
        {
            throw new NotImplementedException("GenerateMethod");
        }
        protected virtual void GenerateRemoveEvent(ILGenerator il, MethodInfoMapping mapping)
        {
            throw new NotImplementedException("GenerateRemoveEvent");
        }
        protected virtual void GenerateSetIndexProperty(ILGenerator il, MethodInfoMapping mapping)
        {
            throw new NotImplementedException("GenerateSetIndexProperty");
        }
        protected virtual void GenerateSetProperty(ILGenerator il, MethodInfoMapping mapping)
        {
            throw new NotImplementedException("GenerateSetProperty");
        }
        private static List<MethodInfoMapping> GetMethodInfoMapping(Type interfaceType)
        {
            var props = interfaceType.GetProperties().Select(p => new
            {
                memberInfo = (MemberInfo)p,
                getMethod = p.GetGetMethod(true),
                setMethod = p.GetSetMethod(true)
            }).ToList();

            var events = interfaceType.GetEvents().Select(e => new
            {
                memberInfo = (MemberInfo)e,
                getMethod = e.GetAddMethod(true),
                setMethod = e.GetRemoveMethod(true)
            }).ToList();

            List<MethodInfo> specMethods =
                props.Select(p => p.getMethod).Union(
                props.Select(p => p.setMethod).Union(
                events.Select(e => e.getMethod).Union(
                events.Select(e => e.setMethod)))).Where(m => m != null).ToList();

            return interfaceType.GetMethods().
                Except(specMethods).Select(m => new MethodInfoMapping(m, null, null)).Union(
                props.Select(p => new MethodInfoMapping(p.memberInfo, p.getMethod, p.setMethod)).Union(
                events.Select(e => new MethodInfoMapping(e.memberInfo, e.getMethod, e.setMethod)))).ToList();
        }

        protected TypeBuilder TypeBuilder
        {
            get
            {
                return _typeBuilder;
            }
        }
    }
}
