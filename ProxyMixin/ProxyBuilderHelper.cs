using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin
{
    internal static class ProxyBuilderHelper
    {
        public static void DefineEvent(TypeBuilder typeBuilder, EventInfo eventInfo,
            MethodBuilder addMethodBuilder, MethodBuilder removeMethodBuilder)
        {
            String eventName = eventInfo.DeclaringType.FullName + "." + eventInfo.Name;
            EventBuilder eventBuilder = typeBuilder.DefineEvent(eventName, EventAttributes.None, eventInfo.EventHandlerType);

            eventBuilder.SetAddOnMethod(addMethodBuilder);
            eventBuilder.SetRemoveOnMethod(removeMethodBuilder);
        }
        public static void DefineInterface(TypeBuilder typeBuilder, Type interfaceType, Type implementType,
            Action<ILGenerator, MethodInfo, MethodInfo> ilGenerator)
        {
            InterfaceMapping mapping = implementType.GetInterfaceMap(interfaceType);
            for (int i = 0; i < mapping.InterfaceMethods.Length; i++)
                mapping.TargetMethods[i] = ProxyBuilderHelper.DefineMethod(typeBuilder, mapping.InterfaceMethods[i],
                    il => ilGenerator(il, mapping.InterfaceMethods[i], mapping.TargetMethods[i]));

            foreach (PropertyInfo propertyInfo in interfaceType.GetProperties())
            {
                MethodBuilder getMethodBuilder = null;
                MethodInfo getMethodInfo = propertyInfo.GetGetMethod();
                if (getMethodInfo != null)
                    getMethodBuilder = (MethodBuilder)GetTargetMethodInfo(mapping, getMethodInfo);

                MethodBuilder setMethodBuilder = null;
                MethodInfo setMethodInfo = propertyInfo.GetSetMethod();
                if (setMethodInfo != null)
                    setMethodBuilder = (MethodBuilder)GetTargetMethodInfo(mapping, setMethodInfo);

                ProxyBuilderHelper.DefineProperty(typeBuilder, propertyInfo, getMethodBuilder, setMethodBuilder);
            }

            foreach (EventInfo eventInfo in interfaceType.GetEvents())
            {
                var addMethodBuilder = (MethodBuilder)GetTargetMethodInfo(mapping, eventInfo.GetAddMethod());
                var removeMethodBuilder = (MethodBuilder)GetTargetMethodInfo(mapping, eventInfo.GetRemoveMethod());
                ProxyBuilderHelper.DefineEvent(typeBuilder, eventInfo, addMethodBuilder, removeMethodBuilder);
            }

        }
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
        private static MethodInfo GetTargetMethodInfo(InterfaceMapping mapping, MethodInfo interfaceMethod)
        {
            for (int i = 0; i < mapping.InterfaceMethods.Length; i++)
                if (mapping.InterfaceMethods[i] == interfaceMethod)
                    return mapping.TargetMethods[i];

            throw new InvalidOperationException("mixin target method not found");
        }
    }
}
