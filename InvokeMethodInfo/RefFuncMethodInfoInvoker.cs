using System;
using System.Runtime.CompilerServices;

namespace InvokeMethodInfo
{
    internal sealed class RefFuncMethodInfoInvoker<T, T1, TResult> : MethodInfoInvoker
    {
        private readonly Func<T, T1, TResult> _func;

        public RefFuncMethodInfoInvoker(MethodDef methodDef, IndirectInvoker indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _func = (Func<T, T1, TResult>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1>(P target, P1 p1)
        {
            return new RefMethodInfoInvokerParameters<T, T1, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy,
                P, P1, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IRefMethodInfoInvokerParameters<T, T1, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            return _func(prms.Target, prms.Parameter1);
        }
    }

    internal sealed class RefFuncMethodInfoInvoker<T, T1, T2, TResult> : MethodInfoInvoker
    {
        private readonly Func<T, T1, T2, TResult> _func;

        public RefFuncMethodInfoInvoker(MethodDef methodDef, IndirectInvoker indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _func = (Func<T, T1, T2, TResult>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2>(P target, P1 p1, P2 p2)
        {
            return new RefMethodInfoInvokerParameters<T, T1, T2, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy,
                P, P1, P2, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1, p2);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IRefMethodInfoInvokerParameters<T, T1, T2, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            return _func(prms.Target, prms.Parameter1, prms.Parameter2);
        }
    }

    internal sealed class RefFuncMethodInfoInvoker<T, T1, T2, T3, TResult> : MethodInfoInvoker
    {
        private readonly Func<T, T1, T2, T3, TResult> _func;

        public RefFuncMethodInfoInvoker(MethodDef methodDef, IndirectInvoker indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _func = (Func<T, T1, T2, T3, TResult>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3>(P target, P1 p1, P2 p2, P3 p3)
        {
            return new RefMethodInfoInvokerParameters<T, T1, T2, T3, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy,
                P, P1, P2, P3, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1, p2, p3);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IRefMethodInfoInvokerParameters<T, T1, T2, T3, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            return _func(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3);
        }
    }

    internal sealed class RefFuncMethodInfoInvoker<T, T1, T2, T3, T4, TResult> : MethodInfoInvoker
    {
        private readonly Func<T, T1, T2, T3, T4, TResult> _func;

        public RefFuncMethodInfoInvoker(MethodDef methodDef, IndirectInvoker indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _func = (Func<T, T1, T2, T3, T4, TResult>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4>(P target, P1 p1, P2 p2, P3 p3, P4 p4)
        {
            return new RefMethodInfoInvokerParameters<T, T1, T2, T3, T4, Dummy, Dummy, Dummy, Dummy, Dummy,
                P, P1, P2, P3, P4, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1, p2, p3, p4);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IRefMethodInfoInvokerParameters<T, T1, T2, T3, T4, Dummy, Dummy, Dummy, Dummy, Dummy>;
            return _func(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4);
        }
    }

    internal sealed class RefFuncMethodInfoInvoker<T, T1, T2, T3, T4, T5, TResult> : MethodInfoInvoker
    {
        private readonly Func<T, T1, T2, T3, T4, T5, TResult> _func;

        public RefFuncMethodInfoInvoker(MethodDef methodDef, IndirectInvoker indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _func = (Func<T, T1, T2, T3, T4, T5, TResult>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
        {
            return new RefMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, Dummy, Dummy, Dummy, Dummy,
                P, P1, P2, P3, P4, P5, Dummy, Dummy, Dummy, Dummy>(target, p1, p2, p3, p4, p5);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IRefMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, Dummy, Dummy, Dummy, Dummy>;
            return _func(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5);
        }
    }

    internal sealed class RefFuncMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, TResult> : MethodInfoInvoker
    {
        private readonly Func<T, T1, T2, T3, T4, T5, T6, TResult> _func;

        public RefFuncMethodInfoInvoker(MethodDef methodDef, IndirectInvoker indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _func = (Func<T, T1, T2, T3, T4, T5, T6, TResult>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
        {
            return new RefMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, Dummy, Dummy, Dummy,
                P, P1, P2, P3, P4, P5, P6, Dummy, Dummy, Dummy>(target, p1, p2, p3, p4, p5, p6);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IRefMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, Dummy, Dummy, Dummy>;
            return _func(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6);
        }
    }

    internal sealed class RefFuncMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7, TResult> : MethodInfoInvoker
    {
        private readonly Func<T, T1, T2, T3, T4, T5, T6, T7, TResult> _func;

        public RefFuncMethodInfoInvoker(MethodDef methodDef, IndirectInvoker indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _func = (Func<T, T1, T2, T3, T4, T5, T6, T7, TResult>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
        {
            return new RefMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, Dummy, Dummy,
                P, P1, P2, P3, P4, P5, P6, P7, Dummy, Dummy>(target, p1, p2, p3, p4, p5, p6, p7);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IRefMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, Dummy, Dummy>;
            return _func(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6, prms.Parameter7);
        }
    }

    internal sealed class RefFuncMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7, T8, TResult> : MethodInfoInvoker
    {
        private readonly Func<T, T1, T2, T3, T4, T5, T6, T7, T8, TResult> _func;

        public RefFuncMethodInfoInvoker(MethodDef methodDef, IndirectInvoker indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _func = (Func<T, T1, T2, T3, T4, T5, T6, T7, T8, TResult>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7, P8>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
        {
            return new RefMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, Dummy,
                P, P1, P2, P3, P4, P5, P6, P7, P8, Dummy>(target, p1, p2, p3, p4, p5, p6, p7, p8);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IRefMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, Dummy>;
            return _func(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6, prms.Parameter7, prms.Parameter8);
        }
    }

    internal sealed class RefFuncMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> : MethodInfoInvoker
    {
        private readonly Func<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> _func;

        public RefFuncMethodInfoInvoker(MethodDef methodDef, IndirectInvoker indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _func = (Func<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7, P8, P9>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
        {
            return new RefMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, T9,
                P, P1, P2, P3, P4, P5, P6, P7, P8, P9>(target, p1, p2, p3, p4, p5, p6, p7, p8, p9);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as IRefMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>;
            return _func(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6, prms.Parameter7, prms.Parameter8, prms.Parameter9);
        }
    }
}
