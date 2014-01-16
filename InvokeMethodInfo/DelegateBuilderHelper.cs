using System;

namespace InvokeMethodInfo
{
    internal static class DelegateBuilderHelper
    {
        public static Type CreateRefActionType(MethodDef methodDef)
        {
            Type invokerType;
            switch (methodDef.ParameterCount)
            {
                case 1:
                    invokerType = typeof(RefActionMethodInfoInvoker<,>);
                    break;
                case 2:
                    invokerType = typeof(RefActionMethodInfoInvoker<,,>);
                    break;
                case 3:
                    invokerType = typeof(RefActionMethodInfoInvoker<,,,>);
                    break;
                case 4:
                    invokerType = typeof(RefActionMethodInfoInvoker<,,,,>);
                    break;
                case 5:
                    invokerType = typeof(RefActionMethodInfoInvoker<,,,,,>);
                    break;
                case 6:
                    invokerType = typeof(RefActionMethodInfoInvoker<,,,,,,>);
                    break;
                case 7:
                    invokerType = typeof(RefActionMethodInfoInvoker<,,,,,,,>);
                    break;
                case 8:
                    invokerType = typeof(RefActionMethodInfoInvoker<,,,,,,,,>);
                    break;
                case 9:
                    invokerType = typeof(RefActionMethodInfoInvoker<,,,,,,,,,>);
                    break;
                default:
                    throw new InvalidOperationException("out of range parameter count");
            }
            return invokerType.MakeGenericType(methodDef.GenericParameters);
        }
        public static Type CreateRefFuncType(MethodDef methodDef)
        {
            Type invokerType;
            switch (methodDef.ParameterCount)
            {
                case 1:
                    invokerType = typeof(RefFuncMethodInfoInvoker<,,>);
                    break;
                case 2:
                    invokerType = typeof(RefFuncMethodInfoInvoker<,,,>);
                    break;
                case 3:
                    invokerType = typeof(RefFuncMethodInfoInvoker<,,,,>);
                    break;
                case 4:
                    invokerType = typeof(RefFuncMethodInfoInvoker<,,,,,>);
                    break;
                case 5:
                    invokerType = typeof(RefFuncMethodInfoInvoker<,,,,,,>);
                    break;
                case 6:
                    invokerType = typeof(RefFuncMethodInfoInvoker<,,,,,,,>);
                    break;
                case 7:
                    invokerType = typeof(RefFuncMethodInfoInvoker<,,,,,,,,>);
                    break;
                case 8:
                    invokerType = typeof(RefFuncMethodInfoInvoker<,,,,,,,,,>);
                    break;
                case 9:
                    invokerType = typeof(RefFuncMethodInfoInvoker<,,,,,,,,,,>);
                    break;
                default:
                    throw new InvalidOperationException("out of range parameter count");
            }
            return invokerType.MakeGenericType(methodDef.GenericParameters);
        }
        public static Type CreateValActionType(MethodDef methodDef)
        {
            Type invokerType;
            switch (methodDef.ParameterCount)
            {
                case 0:
                    invokerType = typeof(ValActionMethodInfoInvoker<>);
                    break;
                case 1:
                    invokerType = typeof(ValActionMethodInfoInvoker<,>);
                    break;
                case 2:
                    invokerType = typeof(ValActionMethodInfoInvoker<,,>);
                    break;
                case 3:
                    invokerType = typeof(ValActionMethodInfoInvoker<,,,>);
                    break;
                case 4:
                    invokerType = typeof(ValActionMethodInfoInvoker<,,,,>);
                    break;
                case 5:
                    invokerType = typeof(ValActionMethodInfoInvoker<,,,,,>);
                    break;
                case 6:
                    invokerType = typeof(ValActionMethodInfoInvoker<,,,,,,>);
                    break;
                case 7:
                    invokerType = typeof(ValActionMethodInfoInvoker<,,,,,,,>);
                    break;
                case 8:
                    invokerType = typeof(ValActionMethodInfoInvoker<,,,,,,,,>);
                    break;
                case 9:
                    invokerType = typeof(ValActionMethodInfoInvoker<,,,,,,,,,>);
                    break;
                default:
                    throw new InvalidOperationException("out of range parameter count");
            }
            return invokerType.MakeGenericType(methodDef.GenericParameters);
        }
        public static Type CreateValFuncType(MethodDef methodDef)
        {
            Type invokerType;
            switch (methodDef.ParameterCount)
            {
                case 0:
                    invokerType = typeof(ValFuncMethodInfoInvoker<,>);
                    break;
                case 1:
                    invokerType = typeof(ValFuncMethodInfoInvoker<,,>);
                    break;
                case 2:
                    invokerType = typeof(ValFuncMethodInfoInvoker<,,,>);
                    break;
                case 3:
                    invokerType = typeof(ValFuncMethodInfoInvoker<,,,,>);
                    break;
                case 4:
                    invokerType = typeof(ValFuncMethodInfoInvoker<,,,,,>);
                    break;
                case 5:
                    invokerType = typeof(ValFuncMethodInfoInvoker<,,,,,,>);
                    break;
                case 6:
                    invokerType = typeof(ValFuncMethodInfoInvoker<,,,,,,,>);
                    break;
                case 7:
                    invokerType = typeof(ValFuncMethodInfoInvoker<,,,,,,,,>);
                    break;
                case 8:
                    invokerType = typeof(ValFuncMethodInfoInvoker<,,,,,,,,,>);
                    break;
                case 9:
                    invokerType = typeof(ValFuncMethodInfoInvoker<,,,,,,,,,,>);
                    break;
                default:
                    throw new InvalidOperationException("out of range parameter count");
            }
            return invokerType.MakeGenericType(methodDef.GenericParameters);
        }
    }
}
