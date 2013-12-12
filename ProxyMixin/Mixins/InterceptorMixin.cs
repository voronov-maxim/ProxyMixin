using ProxyMixin.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.Mixins
{
    public class InterceptorMixin<I> where I : class
    {
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

                String fieldName = typeof(I).FullName + "." + mapping.MemberInfo.Name;
                mapping.FieldBuilder = base.TypeBuilder.DefineField(fieldName, mapping.MemberInfo.GetType(), FieldAttributes.Static | FieldAttributes.Private);
            }
            protected override void GenerateGetProperty(ILGenerator il, MethodInfoMapping mapping)
            {
                CreateField(mapping);
                base.GenerateGetProperty(il, mapping);
            }
            protected override void GenerateMethod(ILGenerator il, MethodInfoMapping mapping)
            {
                CreateField(mapping);
                MethodInfo invokeInfo = ((Action<MethodInfo, Object[]>)_mixin.Invoke).Method;
                ParameterInfo[] parameterInfos = ((MethodInfo)mapping.MemberInfo).GetParameters();
                il.DeclareLocal(typeof(Object[]));

                il.Emit(OpCodes.Ldc_I4, parameterInfos.Length);
                il.Emit(OpCodes.Newarr, typeof(Object));
                il.Emit(OpCodes.Stloc_0);

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldsfld, mapping.FieldBuilder);

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
                il.Emit(OpCodes.Ret);
            }
            protected override void GenerateSetProperty(ILGenerator il, MethodInfoMapping mapping)
            {
                CreateField(mapping);
                MethodInfo setInfo = ((Action<PropertyInfo, Object>)_mixin.SetProperty).Method;

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldsfld, mapping.FieldBuilder);
                il.Emit(OpCodes.Ldarg_1);

                il.Emit(OpCodes.Call, setInfo);
                il.Emit(OpCodes.Ret);
            }
        }

        public InterceptorMixin()
        {
        }

        public I Create()
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
            return (I)Activator.CreateInstance(interceptorType);
        }
        private void GenerateMethod(ILGenerator il, MethodInfoMapping mapping)
        {
        }
        protected virtual void Invoke(MethodInfo methodInfo, Object[] args)
        {
            //methodInfo.Invoke()
        }
        protected virtual void GetProperty(PropertyInfo propertyInfo)
        {

        }
        protected virtual void SetProperty(PropertyInfo propertyInfo, Object value)
        {

        }
    }
}
