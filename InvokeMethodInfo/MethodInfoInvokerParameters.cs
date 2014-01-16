using System;

namespace InvokeMethodInfo
{
    public interface IMethodInfoInvokerParameters
    {
        K GetValue<K>(int index);
        String GetValueAsString(int index);

        int ParameterCount
        {
            get;
        }
    }

    internal interface IRefMethodInfoInvokerParameters<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        T Target
        {
            get;
        }
        T1 Parameter1
        {
            get;
        }
        T2 Parameter2
        {
            get;
        }
        T3 Parameter3
        {
            get;
        }
        T4 Parameter4
        {
            get;
        }
        T5 Parameter5
        {
            get;
        }
        T6 Parameter6
        {
            get;
        }
        T7 Parameter7
        {
            get;
        }
        T8 Parameter8
        {
            get;
        }
        T9 Parameter9
        {
            get;
        }
    }

    internal interface IValMethodInfoInvokerParameters<out T, out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8, out T9>
    {
        T Target
        {
            get;
        }
        T1 Parameter1
        {
            get;
        }
        T2 Parameter2
        {
            get;
        }
        T3 Parameter3
        {
            get;
        }
        T4 Parameter4
        {
            get;
        }
        T5 Parameter5
        {
            get;
        }
        T6 Parameter6
        {
            get;
        }
        T7 Parameter7
        {
            get;
        }
        T8 Parameter8
        {
            get;
        }
        T9 Parameter9
        {
            get;
        }
    }
}
