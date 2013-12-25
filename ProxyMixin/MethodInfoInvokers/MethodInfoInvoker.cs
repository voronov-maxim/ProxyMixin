using ProxyMixin.Builders;
using System;
using System.Reflection;

namespace ProxyMixin.MethodInfoInvokers
{
	public abstract class MethodInfoInvoker
	{
		protected struct Dummy
		{
		}

		private readonly Delegate _invoker;
		private readonly MethodInfo _methodInfo;

		public MethodInfoInvoker(MethodInfo methodInfo)
		{
			_methodInfo = methodInfo;
			_invoker = ProxyBuilderHelper.CreateDelegateFromMethodInfo(methodInfo);
		}

		public static MethodInfoInvoker Create(MethodInfo methodInfo)
		{
			Type invokerType;
			if (methodInfo.ReturnType == typeof(void))
				invokerType = CreateActionType(methodInfo);
			else
				invokerType = CreateFuncType(methodInfo);

			return (MethodInfoInvoker)invokerType.GetConstructor(new Type[] { typeof(MethodInfo) }).Invoke(new Object[] { methodInfo });
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
		public virtual MethodInfoInvokerParameters CreateParameters<P>(P target)
		{
			throw new NotImplementedException();
		}
		public virtual MethodInfoInvokerParameters CreateParameters<P, P1>(P target, P1 p1)
		{
			throw new NotImplementedException();
		}
		public virtual MethodInfoInvokerParameters CreateParameters<P, P1, P2>(P target, P1 p1, P2 p2)
		{
			throw new NotImplementedException();
		}
		public virtual MethodInfoInvokerParameters CreateParameters<P, P1, P2, P3>(P target, P1 p1, P2 p2, P3 p3)
		{
			throw new NotImplementedException();
		}
		public virtual MethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4>(P target, P1 p1, P2 p2, P3 p3, P4 p4)
		{
			throw new NotImplementedException();
		}
		public virtual MethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
		{
			throw new NotImplementedException();
		}
		public virtual MethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
		{
			throw new NotImplementedException();
		}
		public virtual MethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
		{
			throw new NotImplementedException();
		}
		public virtual MethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7, P8>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
		{
			throw new NotImplementedException();
		}
		public virtual MethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7, P8, P9>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
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
		public abstract Object Invoke(MethodInfoInvokerParameters parameters);

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
