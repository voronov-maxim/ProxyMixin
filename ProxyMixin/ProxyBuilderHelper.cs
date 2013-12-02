using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin
{
    internal static class ProxyBuilderHelper
    {
        public static MethodBuilder DefineMethod(TypeBuilder typeBuilder, MethodInfo methodInfo, Action<ILGenerator> ilGenerator)
        {
            String methodName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            Type[] parameterTypes = methodInfo.GetParameters().Select(p => p.ParameterType).ToArray();
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodName,
                MethodAttributes.Private | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Final,
                methodInfo.ReturnType, parameterTypes);

            ilGenerator(methodBuilder.GetILGenerator());
            typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
            return methodBuilder;
        }
        public static void DefineProperty(TypeBuilder typeBuilder, PropertyInfo propertyInfo,
            MethodBuilder getMethodBuilder, MethodBuilder setMethodBuilder)
        {
            String propertyName = propertyInfo.DeclaringType.FullName + "." + propertyInfo.Name;
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.None, propertyInfo.PropertyType, Type.EmptyTypes);
            if (getMethodBuilder != null)
                propertyBuilder.SetGetMethod(getMethodBuilder);
            if (setMethodBuilder != null)
                propertyBuilder.SetSetMethod(setMethodBuilder);
        }
        public static void GenerateFieldMethod(ILGenerator il, FieldBuilder fieldBuilder)
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, fieldBuilder);
            il.Emit(OpCodes.Ret);
        }
        public static void GenerateStandardMethod(ILGenerator il, MethodInfo stub, FieldBuilder mixinsField, int index)
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, mixinsField);
            il.Emit(OpCodes.Ldc_I4, index);
            il.Emit(OpCodes.Ldelem_Ref);

            ParameterInfo[] stubParameterInfo = stub.GetParameters();
            for (int i = 0; i < stubParameterInfo.Length; i++)
                il.Emit(OpCodes.Ldarg, i + 1);

            il.Emit(OpCodes.Call, stub);
            il.Emit(OpCodes.Ret);
        }
    }
}
