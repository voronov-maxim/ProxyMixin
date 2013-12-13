using ProxyMixin.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.Mixins
{
    public class InterceptorMixin<I> : IDynamicMixin where I : class
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
            private readonly InterceptorMixin<I> _mixin;

            public InterfaceBuilder(TypeBuilder typeBuilder, InterceptorMixin<I> mixin)
                : base(typeBuilder)
            {
                _mixin = mixin;
            }

            private void CreateField(MethodInfoMapping mapping)
            {
                if (mapping.FieldBuilder != null)
                    return;

                Type type = mapping.MemberInfo.DeclaringType;
                String fieldName = type.Namespace + "." + type.Name + "." + mapping.MemberInfo.Name;
                mapping.FieldBuilder = base.TypeBuilder.DefineField(fieldName, mapping.MemberInfo.GetType(), FieldAttributes.Static | FieldAttributes.Private);
            }
            protected override void GenerateGetIndexProperty(ILGenerator il, MethodInfoMapping mapping)
            {
                CreateField(mapping);
                MethodInfo getPropertyInfo = ((Func<PropertyInfo, Object[], Object>)_mixin.GetIndexProperty).Method;
                GenerateMethod(il, mapping.FieldBuilder, mapping.GetOrAddMethodInfo, getPropertyInfo);
            }
            protected override void GenerateGetProperty(ILGenerator il, MethodInfoMapping mapping)
            {
                CreateField(mapping);
                MethodInfo getPropertyInfo = ((Func<PropertyInfo, Object>)_mixin.GetProperty).Method;

                il.Emit(OpCodes.Ldarg_0);

                il.Emit(OpCodes.Ldsfld, mapping.FieldBuilder);

                il.Emit(OpCodes.Call, getPropertyInfo);
                if (mapping.GetOrAddMethodInfo.ReturnType.IsValueType)
                    il.Emit(OpCodes.Unbox_Any, mapping.GetOrAddMethodInfo.ReturnType);
                il.Emit(OpCodes.Ret);
            }
            protected override void GenerateMethod(ILGenerator il, MethodInfoMapping mapping)
            {
                CreateField(mapping);
                MethodInfo invokeInfo = ((Func<MethodInfo, Object[], Object>)_mixin.Invoke).Method;
                GenerateMethod(il, mapping.FieldBuilder, (MethodInfo)mapping.MemberInfo, invokeInfo);
            }
            private static void GenerateMethod(ILGenerator il, FieldBuilder fieldBuilder, MethodInfo interfaceInfo, MethodInfo invokeInfo)
            {
                ParameterInfo[] parameterInfos = interfaceInfo.GetParameters();
                il.DeclareLocal(typeof(Object[]));

                il.Emit(OpCodes.Ldc_I4, parameterInfos.Length);
                il.Emit(OpCodes.Newarr, typeof(Object));
                il.Emit(OpCodes.Stloc_0);

                il.Emit(OpCodes.Ldarg_0);

                il.Emit(OpCodes.Ldsfld, fieldBuilder);

                for (int i = 0; i < parameterInfos.Length; i++)
                {
                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Ldc_I4, i);

                    il.Emit(OpCodes.Ldarg, i + 1);
                    if (parameterInfos[i].ParameterType.IsValueType)
                        il.Emit(OpCodes.Box, parameterInfos[i].ParameterType);

                    il.Emit(OpCodes.Stelem_Ref);
                }

                il.Emit(OpCodes.Ldloc_0);

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
                CreateField(mapping);
                MethodInfo setPropertyInfo = ((Action<PropertyInfo, Object[]>)_mixin.SetIndexProperty).Method;
                GenerateMethod(il, mapping.FieldBuilder, mapping.SetOrRemoveMethodInfo, setPropertyInfo);
            }
            protected override void GenerateSetProperty(ILGenerator il, MethodInfoMapping mapping)
            {
                CreateField(mapping);
                MethodInfo setPropertyInfo = ((Action<PropertyInfo, Object>)_mixin.SetProperty).Method;

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldsfld, mapping.FieldBuilder);
                il.Emit(OpCodes.Ldarg_1);

                il.Emit(OpCodes.Call, setPropertyInfo);
                il.Emit(OpCodes.Ret);
            }
        }

        private InterfaceMethodMapping[] _interfaceMethodMapping;
        private IDynamicProxy _proxyObject;

        public IDynamicMixin Create()
        {
            ModuleBuilder moduleBuilder = ProxyFactory.GetModuleBuilder();
            String name = "Interceptor." + typeof(I).FullName;
            TypeBuilder typeBuilder = moduleBuilder.DefineType(name, TypeAttributes.Class | TypeAttributes.Sealed, base.GetType(), new Type[] { typeof(I) });

            var interfaceBuilder = new InterfaceBuilder(typeBuilder, this);
            var mappings = interfaceBuilder.DefineInterface();
            Type interceptorType = typeBuilder.CreateType();

            foreach (MethodInfoMapping mapping in mappings)
            {
                FieldInfo fieldInfo = interceptorType.GetField(mapping.FieldBuilder.Name, BindingFlags.NonPublic | BindingFlags.Static);
                fieldInfo.SetValue(null, mapping.MemberInfo);
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
        protected virtual Object Invoke(MethodInfo methodInfo, Object[] args)
        {
            MethodInfo targetMethodInfo = FindTargetMethod(methodInfo);
            return targetMethodInfo.Invoke(ProxyObject, args);
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
                return Type.EmptyTypes;
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
    }
}
