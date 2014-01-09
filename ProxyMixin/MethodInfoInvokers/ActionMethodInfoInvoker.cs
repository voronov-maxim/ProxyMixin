using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProxyMixin.MethodInfoInvokers
{
    public sealed class ActionMethodInfoInvoker<T> : MethodInfoInvoker<T>
    {
        public ActionMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(MethodInfoInvokerParameters parameters)
        {
            var prms = parameters as MethodInfoInvokerParameters<T, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            ((Action<T>)base.Invoker)(prms.Target);
            return null;
        }
    }

    public sealed class ActionMethodInfoInvoker<T, T1> : MethodInfoInvoker<T, T1>
    {
        public ActionMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(MethodInfoInvokerParameters parameters)
        {
            var prms = parameters as MethodInfoInvokerParameters<T, T1, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            ((Action<T, T1>)base.Invoker)(prms.Target, prms.Parameter1);
            return null;
        }
    }

    public sealed class ActionMethodInfoInvoker<T, T1, T2> : MethodInfoInvoker<T, T1, T2>
    {
        public ActionMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(MethodInfoInvokerParameters parameters)
        {
            var prms = parameters as MethodInfoInvokerParameters<T, T1, T2, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            ((Action<T, T1, T2>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2);
            return null;
        }
    }

    public sealed class ActionMethodInfoInvoker<T, T1, T2, T3> : MethodInfoInvoker<T, T1, T2, T3>
    {
        public ActionMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(MethodInfoInvokerParameters parameters)
        {
            var prms = parameters as MethodInfoInvokerParameters<T, T1, T2, T3, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            ((Action<T, T1, T2, T3>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3);
            return null;
        }
    }

    public sealed class ActionMethodInfoInvoker<T, T1, T2, T3, T4> : MethodInfoInvoker<T, T1, T2, T3, T4>
    {
        public ActionMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(MethodInfoInvokerParameters parameters)
        {
            var prms = parameters as MethodInfoInvokerParameters<T, T1, T2, T3, T4, Dummy, Dummy, Dummy, Dummy, Dummy>;
            ((Action<T, T1, T2, T3, T4>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4);
            return null;
        }
    }

    public sealed class ActionMethodInfoInvoker<T, T1, T2, T3, T4, T5> : MethodInfoInvoker<T, T1, T2, T3, T4, T5>
    {
        public ActionMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(MethodInfoInvokerParameters parameters)
        {
            var prms = parameters as MethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, Dummy, Dummy, Dummy, Dummy>;
            ((Action<T, T1, T2, T3, T4, T5>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5);
            return null;
        }
    }

    public sealed class ActionMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6> : MethodInfoInvoker<T, T1, T2, T3, T4, T5, T6>
    {
        public ActionMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(MethodInfoInvokerParameters parameters)
        {
            var prms = parameters as MethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, Dummy, Dummy, Dummy>;
            ((Action<T, T1, T2, T3, T4, T5, T6>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6);
            return null;
        }
    }

    public sealed class ActionMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7> : MethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7>
    {
        public ActionMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(MethodInfoInvokerParameters parameters)
        {
            var prms = parameters as MethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, Dummy, Dummy>;
            ((Action<T, T1, T2, T3, T4, T5, T6, T7>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6, prms.Parameter7);
            return null;
        }
    }

    public sealed class ActionMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7, T8> : MethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7, T8>
    {
        public ActionMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(MethodInfoInvokerParameters parameters)
        {
            var prms = parameters as MethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, Dummy>;
            ((Action<T, T1, T2, T3, T4, T5, T6, T7, T8>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6, prms.Parameter7, prms.Parameter8);
            return null;
        }
    }

    public sealed class ActionMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7, T8, T9> : MethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        public ActionMethodInfoInvoker(MethodInfo methodInfo, IndirectInvoker indirectInvoker)
            : base(methodInfo, indirectInvoker)
        {
        }

        public override Object Invoke(MethodInfoInvokerParameters parameters)
        {
            var prms = parameters as MethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>;
            ((Action<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>)base.Invoker)(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6, prms.Parameter7, prms.Parameter8, prms.Parameter9);
            return null;
        }
    }
}
