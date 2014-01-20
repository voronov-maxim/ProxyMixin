using InvokeMethodInfo;
using ProxyMixin.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.Mixins
{
    public class InterceptorMixin<T, I> : IDynamicMixin, IMixinSource
        where T : I
        where I : class
    {
        private struct InterfaceMethodMapping
        {
            public readonly MethodInfo InterfaceMethod;
            public readonly MethodInfo TargetMethod;

            public InterfaceMethodMapping(MethodInfo interfaceMethod, MethodInfo targetMethod)
            {
                InterfaceMethod = interfaceMethod;
                TargetMethod = targetMethod;
            }
        }

        private sealed class InterfaceBuilder : InterfaceBuilder<I>
        {
            private readonly InterfaceMethodInfoMappingCollection _methodMappings;
            private readonly InterceptorMixin<T, I> _mixin;

            public InterfaceBuilder(TypeBuilder typeBuilder, InterceptorMixin<T, I> mixin, InterfaceMethodInfoMappingCollection methodMappings)
                : base(typeBuilder)
            {
                _mixin = mixin;
                _methodMappings = methodMappings;
            }

            private FieldBuilder CreateField(MethodInfo methodInfo)
            {
                Type type = methodInfo.DeclaringType;
                String fieldName = type.Namespace + "." + type.Name + "." + methodInfo.Name;
                return base.TypeBuilder.DefineField(fieldName, typeof(MethodInfoInvoker), FieldAttributes.Static | FieldAttributes.Private);
            }
            protected override void GenerateGetIndexProperty(ILGenerator il, InterfaceMethodInfo mapping)
            {
                mapping.FieldBuilderGetOrAdd = CreateField(mapping.MethodInfoGetOrAdd);
                MethodInfo invokeInfo = ((Func<MethodInfoInvoker, IMethodInfoInvokerParameters, Object>)_mixin.GetIndexProperty).Method;
                GenerateMethod(il, mapping.FieldBuilderGetOrAdd, mapping.MethodInfoGetOrAdd, invokeInfo);
            }
            protected override void GenerateGetProperty(ILGenerator il, InterfaceMethodInfo mapping)
            {
                mapping.FieldBuilderGetOrAdd = CreateField(mapping.MethodInfoGetOrAdd);
                MethodInfo invokeInfo = ((Func<MethodInfoInvoker, IMethodInfoInvokerParameters, Object>)_mixin.GetProperty).Method;
                GenerateMethod(il, mapping.FieldBuilderGetOrAdd, (MethodInfo)mapping.MethodInfoGetOrAdd, invokeInfo);
            }
            protected override void GenerateMethod(ILGenerator il, InterfaceMethodInfo mapping)
            {
                mapping.FieldBuilder = CreateField((MethodInfo)mapping.MemberInfo);
                MethodInfo invokeInfo = ((Func<MethodInfoInvoker, IMethodInfoInvokerParameters, Object>)_mixin.Invoke).Method;
                GenerateMethod(il, mapping.FieldBuilder, (MethodInfo)mapping.MemberInfo, invokeInfo);
            }
            private void GenerateMethod(ILGenerator il, FieldBuilder fieldBuilder, MethodInfo interfaceInfo, MethodInfo invokeInfo)
            {
                PropertyInfo proxyObject = typeof(InterceptorMixin<T, I>).GetProperty("ProxyObject");
                Type declaringType = _methodMappings.FindByInterfaceMethod(interfaceInfo).TargetMethod.DeclaringType;
                MethodInfo createParametersInfo = MethodInfoInvoker.GetCreateParametersMethodInfo(interfaceInfo, declaringType);

                il.Emit(OpCodes.Ldarg_0);

                il.Emit(OpCodes.Ldsfld, fieldBuilder);

                il.Emit(OpCodes.Ldsfld, fieldBuilder);

                int parameterCount = interfaceInfo.GetParameters().Length + 1;
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Call, proxyObject.GetGetMethod());
                for (int i = 1; i < parameterCount; i++)
                    il.Emit(OpCodes.Ldarg, i);
                il.Emit(OpCodes.Callvirt, createParametersInfo);
                il.Emit(OpCodes.Call, invokeInfo);

                if (interfaceInfo.ReturnType == typeof(void))
                {
                    if (invokeInfo.ReturnType != typeof(void))
                        il.Emit(OpCodes.Pop);
                }
                else
                {
                    if (interfaceInfo.ReturnType.IsValueType)
                        il.Emit(OpCodes.Unbox_Any, interfaceInfo.ReturnType);
                }

                il.Emit(OpCodes.Ret);
            }
            protected override void GenerateSetIndexProperty(ILGenerator il, InterfaceMethodInfo mapping)
            {
                mapping.FieldBuilderSetOrRemove = CreateField(mapping.MethodInfoSetOrRemove);
                MethodInfo invokeInfo = ((Action<MethodInfoInvoker, IMethodInfoInvokerParameters>)_mixin.SetIndexProperty).Method;
                GenerateMethod(il, mapping.FieldBuilderSetOrRemove, (MethodInfo)mapping.MethodInfoSetOrRemove, invokeInfo);
            }
            protected override void GenerateSetProperty(ILGenerator il, InterfaceMethodInfo mapping)
            {
                mapping.FieldBuilderSetOrRemove = CreateField(mapping.MethodInfoSetOrRemove);
                MethodInfo invokeInfo = ((Action<MethodInfoInvoker, IMethodInfoInvokerParameters>)_mixin.SetProperty).Method;
                GenerateMethod(il, mapping.FieldBuilderSetOrRemove, (MethodInfo)mapping.MethodInfoSetOrRemove, invokeInfo);
            }
        }

        private readonly IndirectInvokerBuilder _indirectInvoker;
        private readonly Type[] _noImplementInterfaces;
        private IDynamicProxy _proxyObject;

        public InterceptorMixin()
        {
            _noImplementInterfaces = new Type[] { typeof(IMixinSource) };
        }
        public InterceptorMixin(IndirectInvokerBuilder indirectInvoker)
            : this()
        {
            _indirectInvoker = indirectInvoker;
        }

        private IDynamicMixin Create()
        {
            ModuleBuilder moduleBuilder = ProxyCtor.GetModuleBuilder();
            String name = "Interceptor." + typeof(I).FullName;
            TypeBuilder typeBuilder = moduleBuilder.DefineType(name, TypeAttributes.Class | TypeAttributes.Sealed, base.GetType(), new Type[] { typeof(I) });

            InterfaceMethodInfoMappingCollection methodMappings = InterfaceMethodInfoMappingCollection.Create(typeof(I), typeof(T));
            var interfaceBuilder = new InterfaceBuilder(typeBuilder, this, methodMappings);
            InterfaceMethodInfoCollection mappings = interfaceBuilder.DefineInterface();
            Type interceptorType = typeBuilder.CreateType();

            foreach (InterfaceMethodInfo mapping in mappings)
                if (mapping.FieldBuilder != null)
                {
                    MethodInfo methodInfo = methodMappings.FindByInterfaceMethod((MethodInfo)mapping.MemberInfo).TargetMethod;
                    FieldInfo fieldInfo = interceptorType.GetField(mapping.FieldBuilder.Name, BindingFlags.NonPublic | BindingFlags.Static);
                    fieldInfo.SetValue(null, MethodInfoInvoker.Create(methodInfo, _indirectInvoker));
                }
                else
                {
                    if (mapping.FieldBuilderGetOrAdd != null)
                    {
                        MethodInfo methodInfo = methodMappings.FindByInterfaceMethod(mapping.MethodInfoGetOrAdd).TargetMethod;
                        FieldInfo fieldInfo = interceptorType.GetField(mapping.FieldBuilderGetOrAdd.Name, BindingFlags.NonPublic | BindingFlags.Static);
                        fieldInfo.SetValue(null, MethodInfoInvoker.Create(methodInfo, _indirectInvoker));
                    }
                    if (mapping.FieldBuilderSetOrRemove != null)
                    {
                        MethodInfo methodInfo = methodMappings.FindByInterfaceMethod(mapping.MethodInfoSetOrRemove).TargetMethod;
                        FieldInfo fieldInfo = interceptorType.GetField(mapping.FieldBuilderSetOrRemove.Name, BindingFlags.NonPublic | BindingFlags.Static);
                        fieldInfo.SetValue(null, MethodInfoInvoker.Create(methodInfo, _indirectInvoker));
                    }
                }

            return (IDynamicMixin)Activator.CreateInstance(interceptorType);
        }
        protected virtual Object GetIndexProperty(MethodInfoInvoker invoker, IMethodInfoInvokerParameters parameters)
        {
            return invoker.Invoke(parameters);
        }
        protected virtual Object GetProperty(MethodInfoInvoker invoker, IMethodInfoInvokerParameters parameters)
        {
            return invoker.Invoke(parameters);
        }
        protected virtual Object Invoke(MethodInfoInvoker invoker, IMethodInfoInvokerParameters parameters)
        {
            return invoker.Invoke(parameters);
        }
        protected virtual void SetIndexProperty(MethodInfoInvoker invoker, IMethodInfoInvokerParameters parameters)
        {
            invoker.Invoke(parameters);
        }
        protected virtual void SetProperty(MethodInfoInvoker invoker, IMethodInfoInvokerParameters parameters)
        {
            invoker.Invoke(parameters);
        }

        public virtual Type[] NoImplementInterfaces
        {
            get
            {
                return _noImplementInterfaces;
            }
        }

        public IDynamicProxy ProxyObject
        {
            get
            {
                return _proxyObject;
            }
            set
            {
                _proxyObject = value;
            }
        }

        IDynamicMixin IMixinSource.GetMixin()
        {
            return Create();
        }
    }
}
