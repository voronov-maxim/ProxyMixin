using System;
using System.Collections.Generic;
using System.Reflection;

namespace InvokeMethodInfo
{
    internal sealed class ValActionMethodInfoInvoker<T> : MethodInfoInvoker
    {
        private readonly Action<T> _action;

        public ValActionMethodInfoInvoker(MethodDef methodDef, IndirectInvokerBuilder indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _action = (Action<T>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P>(P target)
        {
            return new ValMethodInfoInvokerParameters<P, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as ValMethodInfoInvokerParameters<T, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            if (prms == null)
            {
                var iprms = parameters as IValMethodInfoInvokerParameters<T, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
                _action(iprms.Target);
                return null;
            }
            _action(prms.Target);
            return null;
        }
    }

    internal sealed class ValActionMethodInfoInvoker<T, T1> : MethodInfoInvoker
    {
        private readonly Action<T, T1> _action;

        public ValActionMethodInfoInvoker(MethodDef methodDef, IndirectInvokerBuilder indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _action = (Action<T, T1>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1>(P target, P1 p1)
        {
            return new ValMethodInfoInvokerParameters<P, P1, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as ValMethodInfoInvokerParameters<T, T1, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            if (prms == null)
            {
                var iprms = parameters as IValMethodInfoInvokerParameters<T, T1, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
                _action(iprms.Target, iprms.Parameter1);
                return null;
            }
            _action(prms.Target, prms.Parameter1);
            return null;
        }
    }

    internal sealed class ValActionMethodInfoInvoker<T, T1, T2> : MethodInfoInvoker
    {
        private readonly Action<T, T1, T2> _action;

        public ValActionMethodInfoInvoker(MethodDef methodDef, IndirectInvokerBuilder indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _action = (Action<T, T1, T2>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2>(P target, P1 p1, P2 p2)
        {
            return new ValMethodInfoInvokerParameters<P, P1, P2, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1, p2);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as ValMethodInfoInvokerParameters<T, T1, T2, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            if (prms == null)
            {
                var iprms = parameters as IValMethodInfoInvokerParameters<T, T1, T2, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
                _action(iprms.Target, iprms.Parameter1, iprms.Parameter2);
                return null;
            }
            _action(prms.Target, prms.Parameter1, prms.Parameter2);
            return null;
        }
    }

    internal sealed class ValActionMethodInfoInvoker<T, T1, T2, T3> : MethodInfoInvoker
    {
        private readonly Action<T, T1, T2, T3> _action;

        public ValActionMethodInfoInvoker(MethodDef methodDef, IndirectInvokerBuilder indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _action = (Action<T, T1, T2, T3>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3>(P target, P1 p1, P2 p2, P3 p3)
        {
            return new ValMethodInfoInvokerParameters<P, P1, P2, P3, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1, p2, p3);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as ValMethodInfoInvokerParameters<T, T1, T2, T3, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
            if (prms == null)
            {
                var iprms = parameters as IValMethodInfoInvokerParameters<T, T1, T2, T3, Dummy, Dummy, Dummy, Dummy, Dummy, Dummy>;
                _action(iprms.Target, iprms.Parameter1, iprms.Parameter2, iprms.Parameter3);
                return null;
            }
            _action(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3);
            return null;
        }
    }

    internal sealed class ValActionMethodInfoInvoker<T, T1, T2, T3, T4> : MethodInfoInvoker
    {
        private readonly Action<T, T1, T2, T3, T4> _action;

        public ValActionMethodInfoInvoker(MethodDef methodDef, IndirectInvokerBuilder indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _action = (Action<T, T1, T2, T3, T4>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4>(P target, P1 p1, P2 p2, P3 p3, P4 p4)
        {
            return new ValMethodInfoInvokerParameters<P, P1, P2, P3, P4, Dummy, Dummy, Dummy, Dummy, Dummy>(target, p1, p2, p3, p4);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as ValMethodInfoInvokerParameters<T, T1, T2, T3, T4, Dummy, Dummy, Dummy, Dummy, Dummy>;
            if (prms == null)
            {
                var iprms = parameters as IValMethodInfoInvokerParameters<T, T1, T2, T3, T4, Dummy, Dummy, Dummy, Dummy, Dummy>;
                _action(iprms.Target, iprms.Parameter1, iprms.Parameter2, iprms.Parameter3, iprms.Parameter4);
                return null;
            }
            _action(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4);
            return null;
        }
    }

    internal sealed class ValActionMethodInfoInvoker<T, T1, T2, T3, T4, T5> : MethodInfoInvoker
    {
        private readonly Action<T, T1, T2, T3, T4, T5> _action;

        public ValActionMethodInfoInvoker(MethodDef methodDef, IndirectInvokerBuilder indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _action = (Action<T, T1, T2, T3, T4, T5>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
        {
            return new ValMethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, Dummy, Dummy, Dummy, Dummy>(target, p1, p2, p3, p4, p5);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as ValMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, Dummy, Dummy, Dummy, Dummy>;
            if (prms == null)
            {
                var iprms = parameters as IValMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, Dummy, Dummy, Dummy, Dummy>;
                _action(iprms.Target, iprms.Parameter1, iprms.Parameter2, iprms.Parameter3, iprms.Parameter4, iprms.Parameter5);
                return null;
            }
            _action(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5);
            return null;
        }
    }

    internal sealed class ValActionMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6> : MethodInfoInvoker
    {
        private readonly Action<T, T1, T2, T3, T4, T5, T6> _action;

        public ValActionMethodInfoInvoker(MethodDef methodDef, IndirectInvokerBuilder indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _action = (Action<T, T1, T2, T3, T4, T5, T6>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
        {
            return new ValMethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, P6, Dummy, Dummy, Dummy>(target, p1, p2, p3, p4, p5, p6);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as ValMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, Dummy, Dummy, Dummy>;
            if (prms == null)
            {
                var iprms = parameters as IValMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, Dummy, Dummy, Dummy>;
                _action(iprms.Target, iprms.Parameter1, iprms.Parameter2, iprms.Parameter3, iprms.Parameter4, iprms.Parameter5, iprms.Parameter6);
                return null;
            }
            _action(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6);
            return null;
        }
    }

    internal sealed class ValActionMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7> : MethodInfoInvoker
    {
        private readonly Action<T, T1, T2, T3, T4, T5, T6, T7> _action;

        public ValActionMethodInfoInvoker(MethodDef methodDef, IndirectInvokerBuilder indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _action = (Action<T, T1, T2, T3, T4, T5, T6, T7>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
        {
            return new ValMethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, P6, P7, Dummy, Dummy>(target, p1, p2, p3, p4, p5, p6, p7);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as ValMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, Dummy, Dummy>;
            if (prms == null)
            {
                var iprms = parameters as IValMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, Dummy, Dummy>;
                _action(iprms.Target, iprms.Parameter1, iprms.Parameter2, iprms.Parameter3, iprms.Parameter4, iprms.Parameter5, iprms.Parameter6, iprms.Parameter7);
                return null;
            }
            _action(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6, prms.Parameter7);
            return null;
        }
    }

    internal sealed class ValActionMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7, T8> : MethodInfoInvoker
    {
        private readonly Action<T, T1, T2, T3, T4, T5, T6, T7, T8> _action;

        public ValActionMethodInfoInvoker(MethodDef methodDef, IndirectInvokerBuilder indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _action = (Action<T, T1, T2, T3, T4, T5, T6, T7, T8>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7, P8>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
        {
            return new ValMethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, P6, P7, P8, Dummy>(target, p1, p2, p3, p4, p5, p6, p7, p8);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as ValMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, Dummy>;
            if (prms == null)
            {
                var iprms = parameters as IValMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, Dummy>;
                _action(iprms.Target, iprms.Parameter1, iprms.Parameter2, iprms.Parameter3, iprms.Parameter4, iprms.Parameter5, iprms.Parameter6, iprms.Parameter7, iprms.Parameter8);
                return null;
            }
            _action(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6, prms.Parameter7, prms.Parameter8);
            return null;
        }
    }

    internal sealed class ValActionMethodInfoInvoker<T, T1, T2, T3, T4, T5, T6, T7, T8, T9> : MethodInfoInvoker
    {
        private readonly Action<T, T1, T2, T3, T4, T5, T6, T7, T8, T9> _action;

        public ValActionMethodInfoInvoker(MethodDef methodDef, IndirectInvokerBuilder indirectInvoker)
            : base(methodDef.MethodInfo)
        {
            _action = (Action<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>)MethodInfoInvoker.CreateInvoker(methodDef, indirectInvoker);
        }

        public override IMethodInfoInvokerParameters CreateParameters<P, P1, P2, P3, P4, P5, P6, P7, P8, P9>(P target, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
        {
            return new ValMethodInfoInvokerParameters<P, P1, P2, P3, P4, P5, P6, P7, P8, P9>(target, p1, p2, p3, p4, p5, p6, p7, p8, p9);
        }
        public override Object Invoke(IMethodInfoInvokerParameters parameters)
        {
            var prms = parameters as ValMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>;
            if (prms == null)
            {
                var iprms = parameters as IValMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>;
                _action(iprms.Target, iprms.Parameter1, iprms.Parameter2, iprms.Parameter3, iprms.Parameter4, iprms.Parameter5, iprms.Parameter6, iprms.Parameter7, iprms.Parameter8, iprms.Parameter9);
                return null;
            }
            _action(prms.Target, prms.Parameter1, prms.Parameter2, prms.Parameter3, prms.Parameter4, prms.Parameter5, prms.Parameter6, prms.Parameter7, prms.Parameter8, prms.Parameter9);
            return null;
        }
    }
}
