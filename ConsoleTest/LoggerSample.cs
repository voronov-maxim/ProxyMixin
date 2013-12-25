using ProxyMixin;
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
    public class LoggerMixin<T, I> : InterceptorMixin<T, I> where T : I where I : class
    {
        protected override Object GetIndexProperty(PropertyInfo propertyInfo, Object[] args)
        {
            Object result = base.GetIndexProperty(propertyInfo, args);
            LogHelper.GetIndexProperty(propertyInfo, args, result);
            return result;
        }
        protected override Object GetProperty(PropertyInfo propertyInfo)
        {
            Object result = base.GetProperty(propertyInfo);
            LogHelper.GetProperty(propertyInfo, result);
            return result;
        }
		protected override Object Invoke(MethodInfoInvoker invoker, MethodInfoInvokerParameters parameters)
		{
			Object result = invoker.Invoke(parameters);
			//LogHelper.Invoke(methodInfo, args, result);
			return result;
		}
        protected override void SetIndexProperty(PropertyInfo propertyInfo, Object[] args)
        {
            base.SetIndexProperty(propertyInfo, args);
            LogHelper.SetIndexProperty(propertyInfo, args);
        }
        protected override void SetProperty(PropertyInfo propertyInfo, Object value)
        {
            base.SetProperty(propertyInfo, value);
            LogHelper.SetProperty(propertyInfo, value);
        }
    }

    public static class LogHelper
    {
        private static void Args(StringBuilder sb, Object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                Object arg = args[i];
                sb.Append(ToString(arg));
                if (i < args.Length - 1)
                    sb.Append(", ");
            }
        }
        public static void GetIndexProperty(PropertyInfo propertyInfo, Object[] args, Object result)
        {
            var sb = new StringBuilder(propertyInfo.Name);
            sb.Append('[');
            Args(sb, args);
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
        public static void Invoke(MethodInfo methodInfo, Object[] args, Object result)
        {
            var sb = new StringBuilder(methodInfo.Name);
            sb.Append('(');
            Args(sb, args);

            if (methodInfo.ReturnType == typeof(void))
                sb.Append(')');
            else
            {
                sb.Append("): ");
                sb.Append(ToString(result));
            }
            sb.Append(';');

            Trace.WriteLine(sb.ToString());
        }
        public static void SetIndexProperty(PropertyInfo propertyInfo, Object[] args)
        {
            var sb = new StringBuilder(propertyInfo.Name);
            sb.Append('[');

            for (int i = 0; i < args.Length - 1; i++)
            {
                Object arg = args[i];
                sb.Append(ToString(arg));
                if (i < args.Length - 2)
                    sb.Append(", ");
            }

            sb.Append("] = ");
            sb.Append(ToString(args[args.Length - 1]));
            sb.Append(';');

            Trace.WriteLine(sb.ToString());
        }
        public static void SetProperty(PropertyInfo propertyInfo, Object value)
        {
            var sb = new StringBuilder(propertyInfo.Name);
            sb.Append(" = ");
            sb.Append(ToString(value));
            sb.Append(';');

            Trace.WriteLine(sb.ToString());
        }
        private static String ToString(Object value)
        {
            return value == null ? "null" : value.ToString();
        }
    }
}
