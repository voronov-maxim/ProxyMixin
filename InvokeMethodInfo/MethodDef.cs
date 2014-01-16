using System;
using System.Linq.Expressions;
using System.Reflection;

namespace InvokeMethodInfo
{
    internal sealed class MethodDef
    {
        private readonly Type[] _genericParameters;
        private readonly bool _isAction;
        private readonly bool _isByRef;
        private readonly MethodInfo _methodInfo;
        private readonly int _parameterCount;

        public MethodDef(MethodInfo methodInfo)
        {
            _methodInfo = methodInfo;

            if (methodInfo.ReturnType == typeof(void))
            {
                _isAction = true;
                _genericParameters = CreateActionGenericParameters(methodInfo, out _parameterCount, out _isByRef);
            }
            else
            {
                _isAction = false;
                _genericParameters = CreateFuncGenericParameters(methodInfo, out _parameterCount, out _isByRef);
            }
        }

        private static Type[] CreateActionGenericParameters(MethodInfo methodInfo, out int parameterCount, out bool isByRef)
        {
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            parameterCount = parameterInfos.Length;
            isByRef = false;

            var parameterTypes = new Type[parameterInfos.Length + 1];
            parameterTypes[0] = methodInfo.DeclaringType;
            for (int i = 1; i < parameterTypes.Length; i++)
            {
                Type parameterType = parameterInfos[i - 1].ParameterType;
                if (parameterType.IsByRef)
                {
                    isByRef = true;
                    parameterTypes[i] = typeof(IntPtr);
                }
                else
                    parameterTypes[i] = parameterType;
            }
            return parameterTypes;
        }
        public Type GetDelegateType()
        {
            return IsAction ? Expression.GetActionType(GenericParameters) : Expression.GetFuncType(GenericParameters);
        }
        private static Type[] CreateFuncGenericParameters(MethodInfo methodInfo, out int parameterCount, out bool isByRef)
        {
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            parameterCount = parameterInfos.Length;
            isByRef = false;

            var parameterTypes = new Type[parameterInfos.Length + 2];
            parameterTypes[0] = methodInfo.DeclaringType;
            for (int i = 1; i < parameterTypes.Length - 1; i++)
            {
                Type parameterType = parameterInfos[i - 1].ParameterType;
                if (parameterType.IsByRef)
                {
                    isByRef = true;
                    parameterTypes[i] = typeof(IntPtr);
                }
                else
                    parameterTypes[i] = parameterType;
            }
            parameterTypes[parameterTypes.Length - 1] = methodInfo.ReturnType;
            return parameterTypes;
        }

        public Type[] GenericParameters
        {
            get
            {
                return _genericParameters;
            }
        }
        public bool IsAction
        {
            get
            {
                return _isAction;
            }
        }
        public bool IsByRef
        {
            get
            {
                return _isByRef;
            }
        }
        public MethodInfo MethodInfo
        {
            get
            {
                return _methodInfo;
            }
        }
        public int ParameterCount
        {
            get
            {
                return _parameterCount;
            }
        }
        public Type ReturnType
        {
            get
            {
                return _methodInfo.ReturnType;
            }
        }
    }
}
