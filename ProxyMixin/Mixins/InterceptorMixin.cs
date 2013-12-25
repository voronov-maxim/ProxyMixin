using ProxyMixin.Builders;
using ProxyMixin.MethodInfoInvokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.Mixins
{
    public class InterceptorMixin<T, I> : IDynamicMixin, IMixinSource where T : I where I : class
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
            private readonly InterceptorMixin<T, I> _mixin;

            public InterfaceBuilder(TypeBuilder typeBuilder, InterceptorMixin<T, I> mixin)
                : base(typeBuilder)
            {
                _mixin = mixin;
            }

			private void CreateGetField(MethodInfoMapping mapping)
			{
				Type type = mapping.MemberInfo.DeclaringType;
				String fieldName = type.Namespace + "." + type.Name + "." + mapping.MethodInfoGetOrAdd.Name;
				mapping.FieldBuilderGetOrAdd = base.TypeBuilder.DefineField(fieldName, typeof(MethodInfoInvoker), FieldAttributes.Static | FieldAttributes.Private);
			}
			private void CreateMethodField(MethodInfoMapping mapping)
            {
                Type type = mapping.MemberInfo.DeclaringType;
                String fieldName = type.Namespace + "." + type.Name + "." + mapping.MemberInfo.Name;
				mapping.FieldBuilder = base.TypeBuilder.DefineField(fieldName, typeof(MethodInfoInvoker), FieldAttributes.Static | FieldAttributes.Private);
            }
			private void CreateSetField(MethodInfoMapping mapping)
			{
				Type type = mapping.MemberInfo.DeclaringType;
				String fieldName = type.Namespace + "." + type.Name + "." + mapping.MethodInfoSetOrRemove.Name;
				mapping.FieldBuilderSetOrRemove = base.TypeBuilder.DefineField(fieldName, typeof(MethodInfoInvoker), FieldAttributes.Static | FieldAttributes.Private);
			}
			protected override void GenerateGetIndexProperty(ILGenerator il, MethodInfoMapping mapping)
            {
                CreateGetField(mapping);
                MethodInfo getPropertyInfo = ((Func<PropertyInfo, Object[], Object>)_mixin.GetIndexProperty).Method;
                GenerateMethod(il, mapping.FieldBuilder, mapping.MethodInfoGetOrAdd, getPropertyInfo);
            }
            protected override void GenerateGetProperty(ILGenerator il, MethodInfoMapping mapping)
            {
                CreateGetField(mapping);
                MethodInfo getPropertyInfo = ((Func<PropertyInfo, Object>)_mixin.GetProperty).Method;

                il.Emit(OpCodes.Ldarg_0);

                il.Emit(OpCodes.Ldsfld, mapping.FieldBuilder);

                il.Emit(OpCodes.Call, getPropertyInfo);
                if (mapping.MethodInfoGetOrAdd.ReturnType.IsValueType)
                    il.Emit(OpCodes.Unbox_Any, mapping.MethodInfoGetOrAdd.ReturnType);
                il.Emit(OpCodes.Ret);
            }
            protected override void GenerateMethod(ILGenerator il, MethodInfoMapping mapping)
            {
                CreateMethodField(mapping);
                MethodInfo invokeInfo = ((Func<MethodInfoInvoker, MethodInfoInvokerParameters, Object>)_mixin.Invoke).Method;
                GenerateMethod(il, mapping.FieldBuilder, (MethodInfo)mapping.MemberInfo, invokeInfo);
            }
            private static void GenerateMethod(ILGenerator il, FieldBuilder fieldBuilder, MethodInfo interfaceInfo, MethodInfo invokeInfo)
            {
				MethodInfo createParametersInfo = MethodInfoInvoker.GetCreateParametersMethodInfo(interfaceInfo, typeof(T));

				il.Emit(OpCodes.Ldarg_0);

				il.Emit(OpCodes.Ldsfld, fieldBuilder);

				var maps = typeof(InterceptorMixin<T, I>).GetInterfaceMap(typeof(IDynamicMixin));
				var prop = typeof(InterceptorMixin<T, I>).GetProperty("ProxyObject");
				var trg = prop.GetGetMethod();

				il.Emit(OpCodes.Ldsfld, fieldBuilder);
				int parameterCount = interfaceInfo.GetParameters().Length + 1;
				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Call, trg);
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
            protected override void GenerateSetIndexProperty(ILGenerator il, MethodInfoMapping mapping)
            {
                CreateSetField(mapping);
                MethodInfo setPropertyInfo = ((Action<PropertyInfo, Object[]>)_mixin.SetIndexProperty).Method;
                GenerateMethod(il, mapping.FieldBuilder, mapping.MethodInfoSetOrRemove, setPropertyInfo);
            }
            protected override void GenerateSetProperty(ILGenerator il, MethodInfoMapping mapping)
            {
                CreateSetField(mapping);
                MethodInfo setPropertyInfo = ((Action<PropertyInfo, Object>)_mixin.SetProperty).Method;

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldsfld, mapping.FieldBuilder);
                il.Emit(OpCodes.Ldarg_1);

                il.Emit(OpCodes.Call, setPropertyInfo);
                il.Emit(OpCodes.Ret);
            }
        }

        private InterfaceMethodMapping[] _interfaceMethodMapping;
        private readonly Type[] _noImplementInterfaces;
        private IDynamicProxy _proxyObject;

        public InterceptorMixin()
        {
            _noImplementInterfaces = new Type[] { typeof(IMixinSource) };
        }

        private IDynamicMixin Create()
        {
            ModuleBuilder moduleBuilder = ProxyCtor.GetModuleBuilder();
            String name = "Interceptor." + typeof(I).FullName;
            TypeBuilder typeBuilder = moduleBuilder.DefineType(name, TypeAttributes.Class | TypeAttributes.Sealed, base.GetType(), new Type[] { typeof(I) });

            var interfaceBuilder = new InterfaceBuilder(typeBuilder, this);
            var mappings = interfaceBuilder.DefineInterface();
            Type interceptorType = typeBuilder.CreateType();

			var interfaceMap = typeof(T).GetInterfaceMap(typeof(I));
			foreach (MethodInfoMapping mapping in mappings)
				if (mapping.FieldBuilder != null)
				{
					var methodInfo = ProxyBuilderHelper.GetTargetMethodInfo(ref interfaceMap, (MethodInfo)mapping.MemberInfo);
					FieldInfo fieldInfo = interceptorType.GetField(mapping.FieldBuilder.Name, BindingFlags.NonPublic | BindingFlags.Static);
					fieldInfo.SetValue(null, MethodInfoInvoker.Create(methodInfo));
				}
				else
				{
					if (mapping.FieldBuilderGetOrAdd != null)
					{
						FieldInfo fieldInfo = interceptorType.GetField(mapping.FieldBuilderGetOrAdd.Name, BindingFlags.NonPublic | BindingFlags.Static);
						fieldInfo.SetValue(null, MethodInfoInvoker.Create(mapping.MethodInfoGetOrAdd));
					}
					if (mapping.FieldBuilderSetOrRemove != null)
					{
						FieldInfo fieldInfo = interceptorType.GetField(mapping.FieldBuilderSetOrRemove.Name, BindingFlags.NonPublic | BindingFlags.Static);
						fieldInfo.SetValue(null, MethodInfoInvoker.Create(mapping.MethodInfoSetOrRemove));
					}
				}

            return (IDynamicMixin)Activator.CreateInstance(interceptorType);
        }
        private MethodInfo FindTargetMethod(MethodInfo interfaceMethod)
        {
            foreach (InterfaceMethodMapping mapping in _interfaceMethodMapping)
                if (mapping.InterfaceMethod == interfaceMethod)
                    return mapping.TargetMethod;

            throw new InvalidOperationException(interfaceMethod + " no method found");
        }
        protected virtual Object GetIndexProperty(PropertyInfo propertyInfo, Object[] args)
        {
            MethodInfo targetMethodInfo = FindTargetMethod(propertyInfo.GetGetMethod(false));
            return targetMethodInfo.Invoke(ProxyObject, args);
        }
        private static InterfaceMethodMapping[] GetInterfaceMethodMapping(Object proxyObject)
        {
            List<InterfaceMethodMapping> methodMappings = new List<InterfaceMethodMapping>();
            Type type = proxyObject.GetType().BaseType;

            InterfaceMapping mapping = type.GetInterfaceMap(typeof(I));
            for (int i = 0; i < mapping.InterfaceMethods.Length; i++)
                methodMappings.Add(new InterfaceMethodMapping(mapping.InterfaceMethods[i], mapping.TargetMethods[i]));

            foreach (Type interfaceType in typeof(I).GetInterfaces())
            {
                mapping = type.GetInterfaceMap(interfaceType);
                for (int i = 0; i < mapping.InterfaceMethods.Length; i++)
                    methodMappings.Add(new InterfaceMethodMapping(mapping.InterfaceMethods[i], mapping.TargetMethods[i]));
            }

            return methodMappings.ToArray();
        }
        protected virtual Object GetProperty(PropertyInfo propertyInfo)
        {
            MethodInfo targetMethodInfo = FindTargetMethod(propertyInfo.GetGetMethod(false));
            return targetMethodInfo.Invoke(ProxyObject, null);
        }
        protected virtual Object Invoke(MethodInfoInvoker invoker, MethodInfoInvokerParameters parameters)
        {
			return invoker.Invoke(parameters);
        }
        protected virtual void SetIndexProperty(PropertyInfo propertyInfo, Object[] args)
        {
            MethodInfo targetMethodInfo = FindTargetMethod(propertyInfo.GetSetMethod(false));
            targetMethodInfo.Invoke(ProxyObject, args);
        }
        protected virtual void SetProperty(PropertyInfo propertyInfo, Object value)
        {
            MethodInfo targetMethodInfo = FindTargetMethod(propertyInfo.GetSetMethod(false));
            targetMethodInfo.Invoke(ProxyObject, new Object[] { value });
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
                _interfaceMethodMapping = GetInterfaceMethodMapping(value);
                _proxyObject = value;
            }
        }

        IDynamicMixin IMixinSource.GetMixin()
        {
            return Create();
        }
    }
}
