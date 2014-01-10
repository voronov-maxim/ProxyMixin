using ProxyMixin.Builders;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.MethodInfoInvokers
{
    public abstract class MethodInfoInvoker
    {
        protected struct Dummy
        {
        }

        private readonly Delegate _invoker;
        private readonly MethodInfo _methodInfo;

        protected MethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
        {
            _methodInfo = methodInfo;
            if (methodInfo.IsFinal)
                _invoker = ProxyBuilderHelper.CreateDelegateFromMethodInfo(methodInfo);
            else
                _invoker = indirectInvoker.Create(methodInfo);
        }

        public static MethodInfoInvoker Create(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
        {
            Type invokerType;
            if (methodInfo.ReturnType == typeof(void))
                invokerType = CreateActionType(methodInfo);
            else
                invokerType = CreateFuncType(methodInfo);

            var types = new Type[] { typeof(MethodInfo), typeof(IndirectInvoker) };
            return (MethodInfoInvoker)invokerType.GetConstructor(types).Invoke(new Object[] { methodInfo, indirectInvoker });
        }
        private static Type CreateActionType(MethodInfo methodInfo)
        {
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            var parameterTypes = new Type[parameterInfos.Length + 1];
            parameterTypes[0] = methodInfo.DeclaringType;
            for (int i = 1; i < parameterTypes.Length; i++)
                parameterTypes[i] = parameterInfos[i - 1].ParameterType;

            Type invokerType;
            switch (parameterInfos.Length)
            {
                case 0:
                    invokerType = typeof(ActionMethodInfoInvoker<>);
                    break;
                case 1:
                    invokerType = typeof(ActionMethodInfoInvoker<,>);
                    break;
                case 2:
                    invokerType = typeof(ActionMethodInfoInvoker<,,>);
                    break;
                case 3:
                    invokerType = typeof(ActionMethodInfoInvoker<,,,>);
                    break;
                case 4:
                    invokerType = typeof(ActionMethodInfoInvoker<,,,,>);
                    break;
                case 5:
                    invokerType = typeof(ActionMethodInfoInvoker<,,,,,>);
                    break;
                case 6:
                    invokerType = typeof(ActionMethodInfoInvoker<,,,,,,>);
                    break;
                case 7:
                    invokerType = typeof(ActionMethodInfoInvoker<,,,,,,,>);
                    break;
                case 8:
                    invokerType = typeof(ActionMethodInfoInvoker<,,,,,,,,>);
                    break;
                case 9:
                    invokerType = typeof(ActionMethodInfoInvoker<,,,,,,,,,>);
                    break;
                default:
                    throw new InvalidOperationException("out of range parameter count");
            }
            return invokerType.MakeGenericType(parameterTypes);
        }
        private static Type CreateFuncType(MethodInfo methodInfo)
        {
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            var parameterTypes = new Type[parameterInfos.Length + 2];
            parameterTypes[0] = methodInfo.DeclaringType;
            for (int i = 1; i < parameterTypes.Length - 1; i++)
                parameterTypes[i] = parameterInfos[i - 1].ParameterType;
            parameterTypes[parameterTypes.Length - 1] = methodInfo.ReturnType;

            Type invokerType;
            switch (parameterInfos.Length)
            {
                case 0:
                    invokerType = typeof(FuncMethodInfoInvoker<,>);
                    break;
                case 1:
                    invokerType = typeof(FuncMethodInfoInvoker<,,>);
                    break;
                case 2:
                    invokerType = typeof(FuncMethodInfoInvoker<,,,>);
                    break;
                case 3:
                    invokerType = typeof(FuncMethodInfoInvoker<,,,,>);
                    break;
                case 4:
                    invokerType = typeof(FuncMethodInfoInvoker<,,,,,>);
                    break;
                case 5:
                    invokerType = typeof(FuncMethodInfoInvoker<,,,,,,>);
                    break;
                case 6:
                    invokerType = typeof(FuncMethodInfoInvoker<,,,,,,,>);
                    break;
                case 7:
                    invokerType = typeof(FuncMethodInfoInvoker<,,,,,,,,>);
                    break;
                case 8:
                    invokerType = typeof(FuncMethodInfoInvoker<,,,,,,,,,>);
                    break;
                case 9:
                    invokerType = typeof(FuncMethodInfoInvoker<,,,,,,,,,,>);
                    break;
                default:
                    throw new InvalidOperationException("out of range parameter count");
            }
            return invokerType.MakeGenericType(parameterTypes);
        }
        public static IMethodInfoInvokerParameters CreateParameters<P>(P target)
        {
            return new MethodInfoInvokerParameters<P, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target);
        }
        public static IMethodInfoInvokerParameters CreateParameters<P, P1>(P target, P1 p1)
        {
            return new MethodInfoInvokerParameters<P, P1, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1);
        }
        public static IMethodInfoInvokerParameters CreateParameters<P, P1, P2>(P target, P1 p1, P2 p2)
        {
            return new MethodInfoInvokerParameters<P, P1, P2, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1, p2);
        }
        public static IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3>(P target, P1 p1, P2 p2, P3 p3)
        {
            return new MethodInfoInvokerParameters<P, P1, P2, P3, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1, p2, p3);
        }
        public static IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4>(P target, P1 p1, P2 p2, P3 p3, P4 p4)
        {
            return new MethodInfoInvokerParameters<P, P1, P2, P3, P4, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1, p2, p3, p4);
        }
        public static IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
        {
            return new MethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, Dummy, Dummy, Dummy, Dummy>(target, p1, p2, p3, p4, p5);
        }
        public static IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
        {
            return new MethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, P6, Dummy, Dummy, Dummy>(target, p1, p2, p3, p4, p5, p6);
        }
        public static IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
        {
            return new MethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, P6, P7, Dummy, Dummy>(target, p1, p2, p3, p4, p5, p6, p7);
        }
        public static IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7, P8>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
        {
            return new MethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, P6, P7, P8, Dummy>(target, p1, p2, p3, p4, p5, p6, p7, p8);
        }
        public static IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7, P8, P9>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
        {
            return new MethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, P6, P7, P8, P9>(target, p1, p2, p3, p4, p5, p6, p7, p8, p9);
        }
        public static MethodInfo GetCreateParametersMethodInfo(MethodInfo methodInfo, Type targetType)
        {
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            Type[] parameterTypes = new Type[parameterInfos.Length + 1];
            parameterTypes[0] = targetType;
            for (int i = 1; i < parameterTypes.Length; i++)
                parameterTypes[i] = parameterInfos[i - 1].ParameterType;

            foreach (MethodInfo createParametersInfo in typeof(MethodInfoInvoker).GetMember("CreateParameters"))
                if (createParametersInfo.GetGenericArguments().Length == parameterTypes.Length)
                    return createParametersInfo.MakeGenericMethod(parameterTypes);

            throw new InvalidOperationException("CreateParameters not found");
        }
        public abstract Object Invoke(IMethodInfoInvokerParameters parameters);

        protected Delegate Invoker
        {
            get
            {
                return _invoker;
            }
        }
        public MethodInfo MethodInfo
        {
            get
            {
                return _methodInfo;
            }
        }
    }
}
