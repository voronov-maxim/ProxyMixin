using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace InvokeMethodInfo
{
    internal sealed class RefMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, R, R1, R2, R3, R4, R5, R6, R7, R8, R9> :
        IMethodInfoInvokerParameters,
        IRefMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        private R1 _p1;
        private R2 _p2;
        private R3 _p3;
        private R4 _p4;
        private R5 _p5;
        private R6 _p6;
        private R7 _p7;
        private R8 _p8;
        private R9 _p9;
        private readonly int _parameterCount;
        private R _target;

        public RefMethodInfoInvokerParameters(R target)
        {
            _target = target;
            _parameterCount = 0;
        }
        public RefMethodInfoInvokerParameters(R target, R1 p1)
        {
            _target = target;
            _p1 = p1;
            _parameterCount = 1;
        }
        public RefMethodInfoInvokerParameters(R target, R1 p1, R2 p2)
        {
            _target = target;
            _p1 = p1;
            _p2 = p2;
            _parameterCount = 2;
        }
        public RefMethodInfoInvokerParameters(R target, R1 p1, R2 p2, R3 p3)
        {
            _target = target;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _parameterCount = 3;
        }
        public RefMethodInfoInvokerParameters(R target, R1 p1, R2 p2, R3 p3, R4 p4)
        {
            _target = target;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _p4 = p4;
            _parameterCount = 4;
        }
        public RefMethodInfoInvokerParameters(R target, R1 p1, R2 p2, R3 p3, R4 p4, R5 p5)
        {
            _target = target;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _p4 = p4;
            _p5 = p5;
            _parameterCount = 5;
        }
        public RefMethodInfoInvokerParameters(R target, R1 p1, R2 p2, R3 p3, R4 p4, R5 p5, R6 p6)
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
        public RefMethodInfoInvokerParameters(R target, R1 p1, R2 p2, R3 p3, R4 p4, R5 p5, R6 p6, R7 p7)
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
        public RefMethodInfoInvokerParameters(R target, R1 p1, R2 p2, R3 p3, R4 p4, R5 p5, R6 p6, R7 p7, R8 p8)
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
        public RefMethodInfoInvokerParameters(R target, R1 p1, R2 p2, R3 p3, R4 p4, R5 p5, R6 p6, R7 p7, R8 p8, R9 p9)
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

        private unsafe static IntPtr GetAddrOf<K>(ref K field)
        {
            TypedReference tr = __makeref(field);
            return *(IntPtr*)&tr;
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
                if (typeof(T1) == typeof(R1))
                    return __refvalue( __makeref(_p1),T1);

                if (typeof(T1) == typeof(IntPtr))
                {
                    IntPtr ptr = GetAddrOf(ref _p1);
                    return __refvalue( __makeref(ptr),T1);
                }

                return (T1)(Object)_p1;
            }
        }
        public T2 Parameter2
        {
            get
            {
                if (typeof(T2) == typeof(R2))
                    return __refvalue( __makeref(_p2),T2);

                if (typeof(T2) == typeof(IntPtr))
                {
                    IntPtr ptr = GetAddrOf(ref _p2);
                    return __refvalue( __makeref(ptr),T2);
                }

                return (T2)(Object)_p2;
            }
        }
        public T3 Parameter3
        {
            get
            {
                if (typeof(T3) == typeof(R3))
                    return __refvalue( __makeref(_p3),T3);

                if (typeof(T3) == typeof(IntPtr))
                {
                    IntPtr ptr = GetAddrOf(ref _p3);
                    return __refvalue( __makeref(ptr),T3);
                }

                return (T3)(Object)_p3;
            }
        }
        public T4 Parameter4
        {
            get
            {
                if (typeof(T4) == typeof(R4))
                    return __refvalue( __makeref(_p4),T4);

                if (typeof(T4) == typeof(IntPtr))
                {
                    IntPtr ptr = GetAddrOf(ref _p4);
                    return __refvalue( __makeref(ptr),T4);
                }

                return (T4)(Object)_p4;
            }
        }
        public T5 Parameter5
        {
            get
            {
                if (typeof(T5) == typeof(R5))
                    return __refvalue( __makeref(_p5),T5);

                if (typeof(T5) == typeof(IntPtr))
                {
                    IntPtr ptr = GetAddrOf(ref _p5);
                    return __refvalue( __makeref(ptr),T5);
                }

                return (T5)(Object)_p5;
            }
        }
        public T6 Parameter6
        {
            get
            {
                if (typeof(T6) == typeof(R6))
                    return __refvalue( __makeref(_p6),T6);

                if (typeof(T6) == typeof(IntPtr))
                {
                    IntPtr ptr = GetAddrOf(ref _p6);
                    return __refvalue( __makeref(ptr),T6);
                }

                return (T6)(Object)_p6;
            }
        }
        public T7 Parameter7
        {
            get
            {
                if (typeof(T7) == typeof(R7))
                    return __refvalue( __makeref(_p7),T7);

                if (typeof(T7) == typeof(IntPtr))
                {
                    IntPtr ptr = GetAddrOf(ref _p7);
                    return __refvalue( __makeref(ptr),T7);
                }

                return (T7)(Object)_p7;
            }
        }
        public T8 Parameter8
        {
            get
            {
                if (typeof(T8) == typeof(R8))
                    return __refvalue( __makeref(_p8),T8);

                if (typeof(T8) == typeof(IntPtr))
                {
                    IntPtr ptr = GetAddrOf(ref _p8);
                    return __refvalue( __makeref(ptr),T8);
                }

                return (T8)(Object)_p8;
            }
        }
        public T9 Parameter9
        {
            get
            {
                if (typeof(T9) == typeof(R9))
                    return __refvalue( __makeref(_p9),T9);

                if (typeof(T9) == typeof(IntPtr))
                {
                    IntPtr ptr = GetAddrOf(ref _p9);
                    return __refvalue( __makeref(ptr),T9);
                }

                return (T9)(Object)_p9;
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
                if (typeof(T) == typeof(R))
                    return __refvalue( __makeref(_target),T);

                return (T)(Object)_target;
            }
        }
    }
}
