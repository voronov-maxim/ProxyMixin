using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.Mappers
{
    public class ProxyMapper<T>
    {
        private Action<T, T> _mapperDelegate;
        private FieldBuilder _mixinsField;
        private TypeBuilder _typeBuilder;
        private FieldBuilder _wrappedObjectField;

        private Action<T, T> CreateMemberwiseMap()
        {
            Type type = typeof(T);
            var dynamicMethod = new DynamicMethod("DynamicMapper." + type.FullName, null, new Type[] { type, type }, TypeBuilder.Module, true);
            ILGenerator il = dynamicMethod.GetILGenerator();

            while (type != typeof(Object))
            {
                FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                foreach (FieldInfo fieldInfo in fieldInfos)
                    Emit(il, fieldInfo);
                type = type.BaseType;
            }
            il.Emit(OpCodes.Ret);

            var mapperDelegate = (Action<T, T>)dynamicMethod.CreateDelegate(typeof(Action<T, T>));
            MethodInfo mapperInvoker = ((Action<T, T>)mapperDelegate.Invoke).Method;
            FieldBuilder mapperField = TypeBuilder.DefineField(typeof(IDynamicProxy).Name + ".mapperDelegate",
                typeof(Action<T, T>), FieldAttributes.Static | FieldAttributes.Private);

            MethodInfo methodInfo = typeof(IDynamicProxy).GetMethod("MemberwiseMapFromWrappedObject");
            ProxyBuilderHelper.DefineMethod(TypeBuilder, methodInfo,
                il2 => GenerateMemberwiseMapFromWrappedObject(il2, _wrappedObjectField, mapperInvoker, mapperField));

            methodInfo = typeof(IDynamicProxy).GetMethod("MemberwiseMapToWrappedObject");
            ProxyBuilderHelper.DefineMethod(TypeBuilder, methodInfo,
                il2 => GenerateMemberwiseMapToWrappedObject(il2, _wrappedObjectField, mapperInvoker, mapperField));

            return mapperDelegate;
        }
        protected virtual void Emit(ILGenerator il, FieldInfo fieldInfo)
        {
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, fieldInfo);
            il.Emit(OpCodes.Stfld, fieldInfo);
        }
        private void CreateMixins()
        {
            PropertyInfo propertyInfo = typeof(IDynamicProxy).GetProperty("Mixins");
            MethodBuilder getMethodBuilder = ProxyBuilderHelper.DefineMethod(TypeBuilder, propertyInfo.GetGetMethod(),
                il => ProxyBuilderHelper.GenerateFieldMethod(il, MixinsField));
            ProxyBuilderHelper.DefineProperty(TypeBuilder, propertyInfo, getMethodBuilder, null);
        }
        private void CreateWrappedObject()
        {
            PropertyInfo propertyInfo = typeof(IDynamicProxy).GetProperty("WrappedObject");
            MethodBuilder getMethodBuilder = ProxyBuilderHelper.DefineMethod(TypeBuilder, propertyInfo.GetGetMethod(),
                il => ProxyBuilderHelper.GenerateFieldMethod(il, _wrappedObjectField));
            ProxyBuilderHelper.DefineProperty(TypeBuilder, propertyInfo, getMethodBuilder, null);
        }
        public void DefineInterfaceDynamicProxy(TypeBuilder typeBuilder)
        {
            _typeBuilder = typeBuilder;

            TypeBuilder.AddInterfaceImplementation(typeof(IDynamicProxy));
            _mixinsField = TypeBuilder.DefineField("mixins", typeof(Object[]), FieldAttributes.Private);
            _wrappedObjectField = TypeBuilder.DefineField(typeof(IDynamicProxy).Name + ".wrappedObject", typeof(Object), FieldAttributes.Private);

            CreateWrappedObject();
            CreateMixins();
            _mapperDelegate = CreateMemberwiseMap();
        }
        private static void GenerateMemberwiseMapFromWrappedObject(ILGenerator il, FieldBuilder wrappedObjectField,
            MethodInfo mapperInvoker, FieldBuilder mapperField)
        {
            il.Emit(OpCodes.Ldsfld, mapperField);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, wrappedObjectField);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, mapperInvoker);
            il.Emit(OpCodes.Ret);
        }
        private static void GenerateMemberwiseMapToWrappedObject(ILGenerator il, FieldBuilder wrappedObjectField,
            MethodInfo mapperInvoker, FieldBuilder mapperField)
        {
            il.Emit(OpCodes.Ldsfld, mapperField);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, wrappedObjectField);
            il.Emit(OpCodes.Call, mapperInvoker);
            il.Emit(OpCodes.Ret);
        }
        public void InitializeStaticFields(Type proxyType)
        {
            var proxyMapperField = proxyType.GetField(typeof(IDynamicProxy).Name + ".mapperDelegate", BindingFlags.NonPublic | BindingFlags.Static);
            proxyMapperField.SetValue(null, _mapperDelegate);
        }

        public FieldBuilder MixinsField
        {
            get
            {
                return _mixinsField;
            }
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
