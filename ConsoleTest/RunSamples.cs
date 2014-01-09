using ProxyMixin;
using ProxyMixin.Ctors;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleTest
{
    internal sealed class ConsoleTraceListener : TraceListener
    {
        public override void Write(String message)
        {
            Console.Write(message);
        }
        public override void WriteLine(String message)
        {
            Console.WriteLine(message);
        }
    }

    internal static class RunSamples
    {
        public static void InitConsoleTraceListener()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
        }
        public static void LoggerSample()
        {
            var list = new List<int>();
			var proxy = InterceptorCtor.Create(list, new LoggerMixin<List<int>, IList<int>>(ProxyCtor.IndirectInvoker));

            for (int i = 0; i < 10; i++)
                proxy.Add(i);

            proxy[4] = 42;
            int index = proxy.IndexOf(42);
            var value = proxy[index];

            while (proxy.Count > 0)
                proxy.RemoveAt(0);
            Trace.Flush();
        }
    }
}
