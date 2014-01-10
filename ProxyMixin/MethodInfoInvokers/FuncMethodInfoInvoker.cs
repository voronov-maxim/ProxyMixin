using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProxyMixin.MethodInfoInvokers
{
    public sealed class FuncMethodInfoInvoker<T, TResult> : MethodInfoInvoker
    {
        public FuncMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IMethodInfoInvokerParameters<T, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            return ((Func<T, TResult>)base.Invoker)(prms.Target);
        }
    }

    public sealed class FuncMethodInfoInvoker<T, T1, TResult> : MethodInfoInvoker
    {
        public FuncMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IMethodInfoInvokerParameters<T, T1, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            return ((Func<T, T1, TResult>)base.Invoker)(prms.Target, prms.Parameter1);
        }
    }

    public sealed class FuncMethodInfoInvoker<T, T1, T2, TResult> : MethodInfoInvoker
    {
        public FuncMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IMethodInfoInvokerParameters<T, T1, T2, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            return ((Func<T, T1, T2, TResult>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2);
        }
    }

    public sealed class FuncMethodInfoInvoker<T, T1, T2, T3, TResult> : MethodInfoInvoker
    {
        public FuncMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IMethodInfoInvokerParameters<T, T1, T2, T3, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            return ((Func<T, T1, T2, T3, TResult>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3);
        }
    }

    public sealed class FuncMethodInfoInvoker<T, T1, T2, T3, T4, TResult> : MethodInfoInvoker
    {
        public FuncMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IMethodInfoInvokerParameters<T, T1, T2, T3, T4, Dummy, Dummy, Dummy, Dummy, Dummy>;
            return ((Func<T, T1, T2, T3, T4, TResult>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4);
        }
    }

    public sealed class FuncMethodInfoInvoker<T, T1, T2, T3, T4, T5, TResult> : MethodInfoInvoker
    {
        public FuncMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, Dummy, Dummy, Dummy, Dummy>;
            return ((Func<T, T1, T2, T3, T4, T5, TResult>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5);
        }
    }

    public sealed class FuncMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, TResult> : MethodInfoInvoker
    {
        public FuncMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, Dummy, Dummy, Dummy>;
            return ((Func<T, T1, T2, T3, T4, T5, T6, TResult>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6);
        }
    }

    public sealed class FuncMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7, TResult> : MethodInfoInvoker
    {
        public FuncMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, Dummy, Dummy>;
            return ((Func<T, T1, T2, T3, T4, T5, T6, T7, TResult>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6, prms.Parameter7);
        }
    }

    public sealed class FuncMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7, T8, TResult> : MethodInfoInvoker
    {
        public FuncMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, Dummy>;
            return ((Func<T, T1, T2, T3, T4, T5, T6, T7, T8, TResult>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6, prms.Parameter7, prms.Parameter8);
        }
    }

    public sealed class FuncMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> : MethodInfoInvoker
    {
        public FuncMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>;
            return ((Func<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6, prms.Parameter7, prms.Parameter8, prms.Parameter9);
        }
    }
}
