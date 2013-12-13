using ProxyMixin;
using ProxyMixin.Mixins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleTest
{
    public class ListLoggerMixin<T> : InterceptorMixin<IList<T>>
    {
        protected override Object Invoke(MethodInfo methodInfo, Object[] args)
        {
            Object result = base.Invoke(methodInfo, args);
            Log(methodInfo, args);
            return result;
        }
        private static void Log(MethodInfo methodInfo, Object[] args)
        {
            var sb = new StringBuilder(methodInfo.Name);
            sb.Append('(');
            for (int i = 0; i < args.Length; i++)
            {
                Object arg = args[i];
                sb.Append(arg == null ? "null" : arg.ToString());
                if (i < args.Length - 1)
                    sb.Append(", ");
            }
            sb.Append(");");
            Trace.WriteLine(sb.ToString());
        }
    }

    public class LoggerSample
    {
        public static void RunTest()
        {
            var list = new List<int>();

            var logger = new ListLoggerMixin<int>();
            IList<int> proxy = ProxyFactory.Create(list, logger.Create());

            Trace.Listeners.Add(new TextWriterTraceListener(@"C:\work\trace.log"));
            for (int i = 0; i < 10; i++)
                proxy.Add(i);
            var cnt = proxy.Count;
            while (proxy.Count > 0)
                proxy.RemoveAt(0);
            Trace.Flush();
        }
    }
}
