using System;

namespace InvokeMethodInfo
{
    internal sealed class ValMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, T9> :
        IMethodInfoInvokerParameters,
        IValMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        public ValMethodInfoInvokerParameters()
        {
        }
        public void SetParameters(T target, T1 p1, T2 p2)
        {
            _target = target;
            _p1 = p1;
            _p2 = p2;
            _parameterCount = 2;
        }

        private T1 _p1;
        private T2 _p2;
        private T3 _p3;
        private T4 _p4;
        private T5 _p5;
        private T6 _p6;
        private T7 _p7;
        private T8 _p8;
        private T9 _p9;
        private int _parameterCount;
        private T _target;

        public ValMethodInfoInvokerParameters(T target)
        {
            _target = target;
            _parameterCount = 0;
        }
        public ValMethodInfoInvokerParameters(T target, T1 p1)
        {
            _target = target;
            _p1 = p1;
            _parameterCount = 1;
        }
        public ValMethodInfoInvokerParameters(T target, T1 p1, T2 p2)
        {
            _target = target;
            _p1 = p1;
            _p2 = p2;
            _parameterCount = 2;
        }
        public ValMethodInfoInvokerParameters(T target, T1 p1, T2 p2, T3 p3)
        {
            _target = target;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _parameterCount = 3;
        }
        public ValMethodInfoInvokerParameters(T target, T1 p1, T2 p2, T3 p3, T4 p4)
        {
            _target = target;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _p4 = p4;
            _parameterCount = 4;
        }
        public ValMethodInfoInvokerParameters(T target, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5)
        {
            _target = target;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _p4 = p4;
            _p5 = p5;
            _parameterCount = 5;
        }
        public ValMethodInfoInvokerParameters(T target, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6)
        {
            _target = target;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _p4 = p4;
            _p5 = p5;
            _p6 = p6;
            _parameterCount = 6;
        }
        public ValMethodInfoInvokerParameters(T target, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7)
        {
            _target = target;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _p4 = p4;
            _p5 = p5;
            _p6 = p6;
            _p7 = p7;
            _parameterCount = 7;
        }
        public ValMethodInfoInvokerParameters(T target, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8)
        {
            _target = target;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _p4 = p4;
            _p5 = p5;
            _p6 = p6;
            _p7 = p7;
            _p8 = p8;
            _parameterCount = 8;
        }
        public ValMethodInfoInvokerParameters(T target, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9)
        {
            _target = target;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _p4 = p4;
            _p5 = p5;
            _p6 = p6;
            _p7 = p7;
            _p8 = p8;
            _p9 = p9;
            _parameterCount = 9;
        }

        public K GetValue<K>(int index)
        {
            switch (index)
            {
                case 0:
                    return __refvalue( __makeref(_p1),K);
                case 1:
                    return __refvalue( __makeref(_p2),K);
                case 2:
                    return __refvalue( __makeref(_p3),K);
                case 3:
                    return __refvalue( __makeref(_p4),K);
                case 4:
                    return __refvalue( __makeref(_p5),K);
                case 5:
                    return __refvalue( __makeref(_p6),K);
                case 6:
                    return __refvalue( __makeref(_p7),K);
                case 7:
                    return __refvalue( __makeref(_p8),K);
                case 8:
                    return __refvalue( __makeref(_p9),K);
            }

            throw new ArgumentOutOfRangeException("index");
        }
        public String GetValueAsString(int index)
        {
            switch (index)
            {
                case 0:
                    return Parameter1 == null ? null : Parameter1.ToString();
                case 1:
                    return Parameter2 == null ? null : Parameter2.ToString();
                case 2:
                    return Parameter3 == null ? null : Parameter3.ToString();
                case 3:
                    return Parameter4 == null ? null : Parameter4.ToString();
                case 4:
                    return Parameter5 == null ? null : Parameter5.ToString();
                case 5:
                    return Parameter6 == null ? null : Parameter6.ToString();
                case 6:
                    return Parameter7 == null ? null : Parameter7.ToString();
                case 7:
                    return Parameter8 == null ? null : Parameter8.ToString();
                case 8:
                    return Parameter9 == null ? null : Parameter9.ToString();
            }

            throw new ArgumentOutOfRangeException("index");
        }

        public T1 Parameter1
        {
            get
            {
                return _p1;
            }
        }
        public T2 Parameter2
        {
            get
            {
                return _p2;
            }
        }
        public T3 Parameter3
        {
            get
            {
                return _p3;
            }
        }
        public T4 Parameter4
        {
            get
            {
                return _p4;
            }
        }
        public T5 Parameter5
        {
            get
            {
                return _p5;
            }
        }
        public T6 Parameter6
        {
            get
            {
                return _p6;
            }
        }
        public T7 Parameter7
        {
            get
            {
                return _p7;
            }
        }
        public T8 Parameter8
        {
            get
            {
                return _p8;
            }
        }
        public T9 Parameter9
        {
            get
            {
                return _p9;
            }
        }
        public int ParameterCount
        {
            get
            {
                return _parameterCount;
            }
        }
        public T Target
        {
            get
            {
                return _target;
            }
        }
    }
}
