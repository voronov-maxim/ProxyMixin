using System;
using System.Reflection;

namespace ProxyMixin.MethodInfoInvokers
{
	public abstract class MethodInfoInvoker<T> : MethodInfoInvoker
	{
		public MethodInfoInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override MethodInfoInvokerParameters CreateParameters<P>(P target)
		{
			if (typeof(T) == typeof(P))
				return new MethodInfoInvokerParameters<P, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target);

			T t = (T)(Object)target;
			return new MethodInfoInvokerParameters<T, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(t);
		}
		public abstract override Object Invoke(MethodInfoInvokerParameters parameters);
	}

	public abstract class MethodInfoInvoker<T, T1> : MethodInfoInvoker
	{
		public MethodInfoInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override MethodInfoInvokerParameters CreateParameters<P, P1>(P target, P1 p1)
		{
			if (typeof(T) == typeof(P))
				return new MethodInfoInvokerParameters<P, P1, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1);

			T t = (T)(Object)target;
			return new MethodInfoInvokerParameters<T, P1, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(t, p1);
		}
		public abstract override Object Invoke(MethodInfoInvokerParameters parameters);
	}

	public abstract class MethodInfoInvoker<T, T1, T2> : MethodInfoInvoker
	{
		public MethodInfoInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override MethodInfoInvokerParameters CreateParameters<P, P1, P2>(P target, P1 p1, P2 p2)
		{
			if (typeof(T) == typeof(P))
				return new MethodInfoInvokerParameters<P, P1, P2, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1, p2);

			T t = (T)(Object)target;
			return new MethodInfoInvokerParameters<T, P1, P2, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(t, p1, p2);
		}
		public abstract override Object Invoke(MethodInfoInvokerParameters parameters);
	}

	public abstract class MethodInfoInvoker<T, T1, T2, T3> : MethodInfoInvoker
	{
		public MethodInfoInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override MethodInfoInvokerParameters CreateParameters<P, P1, P2, P3>(P target, P1 p1, P2 p2, P3 p3)
		{
			if (typeof(T) == typeof(P))
				return new MethodInfoInvokerParameters<P, P1, P2, P3, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1, p2, p3);

			T t = (T)(Object)target;
			return new MethodInfoInvokerParameters<T, P1, P2, P3, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(t, p1, p2, p3);
		}
		public abstract override Object Invoke(MethodInfoInvokerParameters parameters);
	}

	public abstract class MethodInfoInvoker<T, T1, T2, T3, T4> : MethodInfoInvoker
	{
		public MethodInfoInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override MethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4>(P target, P1 p1, P2 p2, P3 p3, P4 p4)
		{
			if (typeof(T) == typeof(P))
				return new MethodInfoInvokerParameters<P, P1, P2, P3, P4, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1, p2, p3, p4);

			T t = (T)(Object)target;
			return new MethodInfoInvokerParameters<T, P1, P2, P3, P4, Dummy, Dummy, Dummy, Dummy, Dummy>(t, p1, p2, p3, p4);
		}
		public abstract override Object Invoke(MethodInfoInvokerParameters parameters);
	}

	public abstract class MethodInfoInvoker<T, T1, T2, T3, T4, T5> : MethodInfoInvoker
	{
		public MethodInfoInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override MethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
		{
			if (typeof(T) == typeof(P))
				return new MethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, Dummy, Dummy, Dummy, Dummy>(target, p1, p2, p3, p4, p5);

			T t = (T)(Object)target;
			return new MethodInfoInvokerParameters<T, P1, P2, P3, P4, P5, Dummy, Dummy, Dummy, Dummy>(t, p1, p2, p3, p4, p5);
		}
		public abstract override Object Invoke(MethodInfoInvokerParameters parameters);
	}

	public abstract class MethodInfoInvoker<T, T1, T2, T3, T4, T5, T6> : MethodInfoInvoker
	{
		public MethodInfoInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override MethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
		{
			if (typeof(T) == typeof(P))
				return new MethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, P6, Dummy, Dummy, Dummy>(target, p1, p2, p3, p4, p5, p6);

			T t = (T)(Object)target;
			return new MethodInfoInvokerParameters<T, P1, P2, P3, P4, P5, P6, Dummy, Dummy, Dummy>(t, p1, p2, p3, p4, p5, p6);
		}
		public abstract override Object Invoke(MethodInfoInvokerParameters parameters);
	}

	public abstract class MethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7> : MethodInfoInvoker
	{
		public MethodInfoInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override MethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
		{
			if (typeof(T) == typeof(P))
				return new MethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, P6, P7, Dummy, Dummy>(target, p1, p2, p3, p4, p5, p6, p7);

			T t = (T)(Object)target;
			return new MethodInfoInvokerParameters<T, P1, P2, P3, P4, P5, P6, P7, Dummy, Dummy>(t, p1, p2, p3, p4, p5, p6, p7);
		}
		public abstract override Object Invoke(MethodInfoInvokerParameters parameters);
	}

	public abstract class MethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7, T8> : MethodInfoInvoker
	{
		public MethodInfoInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override MethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7, P8>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
		{
			if (typeof(T) == typeof(P))
				return new MethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, P6, P7, P8, Dummy>(target, p1, p2, p3, p4, p5, p6, p7, p8);

			T t = (T)(Object)target;
			return new MethodInfoInvokerParameters<T, P1, P2, P3, P4, P5, P6, P7, P8, Dummy>(t, p1, p2, p3, p4, p5, p6, p7, p8);
		}
		public abstract override Object Invoke(MethodInfoInvokerParameters parameters);
	}

	public abstract class MethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7, T8, T9> : MethodInfoInvoker
	{
		public MethodInfoInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override MethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7, P8, P9>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
		{
			if (typeof(T) == typeof(P))
				return new MethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, P6, P7, P8, P9>(target, p1, p2, p3, p4, p5, p6, p7, p8, p9);

			T t = (T)(Object)target;
			return new MethodInfoInvokerParameters<T, P1, P2, P3, P4, P5, P6, P7, P8, P9>(t, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}
		public abstract override Object Invoke(MethodInfoInvokerParameters parameters);
	}
}
