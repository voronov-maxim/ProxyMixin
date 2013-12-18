using System;

namespace ConsoleTest
{
	public interface ISample
	{
		void Virtual();
		void Final();
	}

	public class Sample : ISample
	{
		public virtual void Virtual()
		{
		}
		public void Final()
		{
		}
	}
	public 

    class Program
    {
        static void Main(String[] args)
        {
			RunSamples.InitConsoleTraceListener();
			Test();

			//RunSamples.LoggerSample();
        }

		private static void Test()
		{
			var sample = new Sample();
			var factory = new ProxyMixin.Factories.InterceptorFactory();
			var proxy = (ISample)factory.Create(sample, new ListLoggerMixin<ISample>());
			proxy.Virtual();
		}
    }
}
