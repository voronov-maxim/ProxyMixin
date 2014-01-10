using ProxyMixin;
using ProxyMixin.Builders;
using ProxyMixin.Ctors;
using ProxyMixin.MethodInfoInvokers;
using ProxyMixin.Mixins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleTest
{
    public class LoggerMixin<T, I> : InterceptorMixin<T, I>
        where T : I
        where I : class
    {
        private readonly InterfaceMethodInfoMappingCollection _mappings;

        public LoggerMixin()
            : base()
        {
            _mappings = InterfaceMethodInfoMappingCollection.Create(typeof(I), typeof(T));
        }
        public LoggerMixin(IndirectInvoker indirectInvoker)
            : base(indirectInvoker)
        {
        }

        protected override Object GetIndexProperty(MethodInfoInvoker invoker, IMethodInfoInvokerParameters parameters)
        {
            Object result = invoker.Invoke(parameters);
            LogHelper.GetIndexProperty((PropertyInfo)_mappings.FindByTargetMethod(invoker.MethodInfo).ParentMember, parameters, result);
            return result;
        }
        protected override Object GetProperty(MethodInfoInvoker invoker, IMethodInfoInvokerParameters parameters)
        {
            Object result = invoker.Invoke(parameters);
            LogHelper.GetProperty((PropertyInfo)_mappings.FindByTargetMethod(invoker.MethodInfo).ParentMember, result);
            return result;
        }
        protected override Object Invoke(MethodInfoInvoker invoker, IMethodInfoInvokerParameters parameters)
        {
            Object result = invoker.Invoke(parameters);
            LogHelper.Invoke(invoker, parameters, result);
            return result;
        }
        protected override void SetIndexProperty(MethodInfoInvoker invoker, IMethodInfoInvokerParameters parameters)
        {
            invoker.Invoke(parameters);
            LogHelper.SetIndexProperty((PropertyInfo)_mappings.FindByTargetMethod(invoker.MethodInfo).ParentMember, parameters);
        }
        protected override void SetProperty(MethodInfoInvoker invoker, IMethodInfoInvokerParameters parameters)
        {
            invoker.Invoke(parameters);
            LogHelper.SetProperty((PropertyInfo)_mappings.FindByTargetMethod(invoker.MethodInfo).ParentMember, parameters);
        }
    }

    public static class LogHelper
    {
        private static void Args(StringBuilder sb, IMethodInfoInvokerParameters parameters)
        {
            for (int i = 0; i < parameters.ParameterCount; i++)
            {
                sb.Append(ToString(parameters.GetValueAsString(i)));
                if (i < parameters.ParameterCount - 1)
                    sb.Append(", ");
            }
        }
        public static void GetIndexProperty(PropertyInfo propertyInfo, IMethodInfoInvokerParameters parameters, Object result)
        {
            var sb = new StringBuilder(propertyInfo.Name);
            sb.Append('[');
            Args(sb, parameters);
            sb.Append("]: ");
            sb.Append(ToString(result));
            sb.Append(';');

            Trace.WriteLine(sb.ToString());
        }
        public static void GetProperty(PropertyInfo propertyInfo, Object result)
        {
            var sb = new StringBuilder(propertyInfo.Name);
            sb.Append(": ");
            sb.Append(ToString(result));
            sb.Append(';');

            Trace.WriteLine(sb.ToString());
        }
        public static void Invoke(MethodInfoInvoker invoker, IMethodInfoInvokerParameters parameters, Object result)
        {
            var sb = new StringBuilder(invoker.MethodInfo.Name);
            sb.Append('(');
            Args(sb, parameters);

            if (invoker.MethodInfo.ReturnType == typeof(void))
                sb.Append(')');
            else
            {
                sb.Append("): ");
                sb.Append(ToString(result));
            }
            sb.Append(';');

            Trace.WriteLine(sb.ToString());
        }
        public static void SetIndexProperty(PropertyInfo propertyInfo, IMethodInfoInvokerParameters parameters)
        {
            var sb = new StringBuilder(propertyInfo.Name);
            sb.Append('[');

            for (int i = 0; i < parameters.ParameterCount - 1; i++)
            {
                sb.Append(ToString(parameters.GetValueAsString(i)));
                if (i < parameters.ParameterCount - 2)
                    sb.Append(", ");
            }

            sb.Append("] = ");
            sb.Append(ToString(parameters.GetValueAsString(parameters.ParameterCount - 1)));
            sb.Append(';');

            Trace.WriteLine(sb.ToString());
        }
        public static void SetProperty(PropertyInfo propertyInfo, IMethodInfoInvokerParameters parameters)
        {
            var sb = new StringBuilder(propertyInfo.Name);
            sb.Append(" = ");
            sb.Append(ToString(parameters.GetValueAsString(0)));
            sb.Append(';');

            Trace.WriteLine(sb.ToString());
        }
        private static String ToString(Object value)
        {
            return value == null ? "null" : value.ToString();
        }
        private static String ToString(String value)
        {
            return value == null ? "null" : value;
        }
    }
}
