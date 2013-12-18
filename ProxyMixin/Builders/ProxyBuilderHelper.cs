using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.Builders
{
    internal static class ProxyBuilderHelper
    {
        public static Delegate CreateDelegateFromMethodInfo(MethodInfo methodInfo)
        {
            bool isAction = methodInfo.ReturnType == typeof(void);
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            var parameterTypes = new Type[parameterInfos.Length + (isAction ? 1 : 2)];
            parameterTypes[0] = methodInfo.DeclaringType;
            for (int i = 0; i < parameterInfos.Length; i++)
                parameterTypes[i + 1] = parameterInfos[i].ParameterType;

            Type delegateType;
            if (isAction)
                delegateType = Expression.GetActionType(parameterTypes);
            else
            {
                parameterTypes[parameterTypes.Length - 1] = methodInfo.ReturnType;
                delegateType = Expression.GetFuncType(parameterTypes);
            }

            IntPtr functPtr = methodInfo.MethodHandle.GetFunctionPointer();
            ConstructorInfo ctor = delegateType.GetConstructors()[0];
            return (Delegate)ctor.Invoke(new Object[] { null, functPtr });
        }
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
			var proxyMethodBuilder = new ProxyMethodBuilder(typeBuilder);
            InterfaceMapping mapping = implementType.GetInterfaceMap(interfaceType);
            for (int i = 0; i < mapping.InterfaceMethods.Length; i++)
                mapping.TargetMethods[i] = proxyMethodBuilder.DefineMethod(mapping.InterfaceMethods[i],
					il => ilGenerator(il, mapping.InterfaceMethods[i], mapping.TargetMethods[i]));

            foreach (PropertyInfo propertyInfo in interfaceType.GetProperties())
            {
                MethodBuilder getMethodBuilder = null;
                MethodInfo getMethodInfo = propertyInfo.GetGetMethod();
                if (getMethodInfo != null)
                    getMethodBuilder = (MethodBuilder)GetTargetMethodInfo(ref mapping, getMethodInfo);

                MethodBuilder setMethodBuilder = null;
                MethodInfo setMethodInfo = propertyInfo.GetSetMethod();
                if (setMethodInfo != null)
                    setMethodBuilder = (MethodBuilder)GetTargetMethodInfo(ref mapping, setMethodInfo);

                ProxyBuilderHelper.DefineProperty(typeBuilder, propertyInfo, getMethodBuilder, setMethodBuilder);
            }

            foreach (EventInfo eventInfo in interfaceType.GetEvents())
            {
                var addMethodBuilder = (MethodBuilder)GetTargetMethodInfo(ref mapping, eventInfo.GetAddMethod());
                var removeMethodBuilder = (MethodBuilder)GetTargetMethodInfo(ref mapping, eventInfo.GetRemoveMethod());
                ProxyBuilderHelper.DefineEvent(typeBuilder, eventInfo, addMethodBuilder, removeMethodBuilder);
            }
        }
        public static MethodBuilder DefineMethod2(TypeBuilder typeBuilder, MethodInfo methodInfo, Action<ILGenerator> ilGenerator)
        {
			var methodAttributes = MethodAttributes.Private | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Final;
			if (methodInfo.DeclaringType.IsAssignableFrom(typeBuilder.BaseType))
			{
				InterfaceMapping mapping = typeBuilder.BaseType.GetInterfaceMap(methodInfo.DeclaringType);
				MethodInfo wrappedMethodInfo = GetTargetMethodInfo(ref mapping, methodInfo);
				if (!wrappedMethodInfo.IsFinal)
					methodAttributes = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig;
			}

            String methodName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            Type[] parameterTypes = methodInfo.GetParameters().Select(p => p.ParameterType).ToArray();
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodName, methodAttributes, methodInfo.ReturnType, parameterTypes);

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
        public static void GenerateGetFieldMethod(ILGenerator il, FieldBuilder fieldBuilder)
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, fieldBuilder);
            il.Emit(OpCodes.Ret);
        }
        public static void GenerateSetFieldMethod(ILGenerator il, FieldBuilder fieldBuilder)
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, fieldBuilder);
            il.Emit(OpCodes.Ret);
        }
        public static MethodInfo GetTargetMethodInfo(ref InterfaceMapping mapping, MethodInfo interfaceMethod)
        {
            for (int i = 0; i < mapping.InterfaceMethods.Length; i++)
                if (mapping.InterfaceMethods[i] == interfaceMethod)
                    return mapping.TargetMethods[i];

            throw new InvalidOperationException("mixin target method not found");
        }
    }
}
