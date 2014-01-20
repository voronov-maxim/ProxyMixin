using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace InvokeMethodInfo
{
    public abstract class MethodInfoInvoker
    {
        protected struct Dummy
        {
        }

        private readonly MethodInfo _methodInfo;

        protected MethodInfoInvoker(MethodInfo methodInfo)
        {
            _methodInfo = methodInfo;
        }

        public static MethodInfoInvoker Create(MethodInfo methodInfo, IndirectInvokerBuilder indirectInvoker)
        {
            var methodDef = new MethodDef(methodInfo);

            Type invokerType;
            if (methodDef.IsByRef)
            {
                if (methodDef.IsAction)
                    invokerType = DelegateBuilderHelper.CreateRefActionType(methodDef);
                else
                    invokerType = DelegateBuilderHelper.CreateRefFuncType(methodDef);
            }
            else
            {
                if (methodDef.IsAction)
                    invokerType = DelegateBuilderHelper.CreateValActionType(methodDef);
                else
                    invokerType = DelegateBuilderHelper.CreateValFuncType(methodDef);
            }

            var types = new Type[] { typeof(MethodDef), typeof(IndirectInvokerBuilder) };
            return (MethodInfoInvoker)invokerType.GetConstructor(types).Invoke(new Object[] { methodDef, indirectInvoker });
        }
        private static Delegate CreateDelegate(MethodDef methodDef)
        {
            Type delegateType = methodDef.GetDelegateType();
            IntPtr functPtr = methodDef.MethodInfo.MethodHandle.GetFunctionPointer();
            ConstructorInfo ctor = delegateType.GetConstructors()[0];
            return (Delegate)ctor.Invoke(new Object[] { null, functPtr });
        }
        internal static Delegate CreateInvoker(MethodDef methodDef, IndirectInvokerBuilder indirectInvoker)
        {
            if (methodDef.MethodInfo.IsVirtual && !methodDef.MethodInfo.IsFinal)
                return indirectInvoker.Create(methodDef.MethodInfo);
            else
                return CreateDelegate(methodDef);
        }
        public virtual IMethodInfoInvokerParameters CreateParameters<P>(P target)
        {
            throw new NotImplementedException();
        }
        public virtual IMethodInfoInvokerParameters CreateParameters<P, P1>(P target, P1 p1)
        {
            throw new NotImplementedException();
        }
        public virtual IMethodInfoInvokerParameters CreateParameters<P, P1, P2>(P target, P1 p1, P2 p2)
        {
            throw new NotImplementedException();
        }
        public virtual IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3>(P target, P1 p1, P2 p2, P3 p3)
        {
            throw new NotImplementedException();
        }
        public virtual IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4>(P target, P1 p1, P2 p2, P3 p3, P4 p4)
        {
            throw new NotImplementedException();
        }
        public virtual IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
        {
            throw new NotImplementedException();
        }
        public virtual IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
        {
            throw new NotImplementedException();
        }
        public virtual IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
        {
            throw new NotImplementedException();
        }
        public virtual IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7, P8>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
        {
            throw new NotImplementedException();
        }
        public virtual IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7, P8, P9>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
        {
            throw new NotImplementedException();
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

        public MethodInfo MethodInfo
        {
            get
            {
                return _methodInfo;
            }
        }
    }
}
