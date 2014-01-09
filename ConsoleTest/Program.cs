using System;

namespace ConsoleTest
{
	public interface ISample
	{
        string Virtual(int i, string s);
		string Final(int i, string s);
        string Prop
        {
            get;
            set;
        }
	}

	public class Sample : ISample
	{
        public virtual string Virtual(int i, string s)
		{
            return "q";
		}
		string ISample.Final(int i, string s)
		{
			return "final";
		}
        private string _prop;
        public virtual string Prop
        {
            get
            {
                return _prop;
            }
            set
            {
                _prop = value;
            }
        }
	}

	public interface ISample2
	{
		void Virtual();
	}
	public class Sample2 : Sample
	{
        public override string Virtual(int i, string s)
		{
            return "w";
		}
        public override string Prop
        {
            get
            {
                return base.Prop;
            }
            set
            {
                base.Prop = value;
            }
        }
	}

	class Program
	{
		static void Main(String[] args)
		{
			//RunSamples.InitConsoleTraceListener();
			Test3();

			//RunSamples.LoggerSample();
		}

		private static void Test()
		{
			var sample = new Sample2();
			var map = typeof(Sample).GetInterfaceMap(typeof(ISample));
			var mi = map.TargetMethods[1];
			var mii = ProxyMixin.MethodInfoInvokers.MethodInfoInvoker.Create(mi, ProxyMixin.ProxyCtor.IndirectInvoker);
			var isample = (ISample)sample;

			var mi2 = ProxyMixin.MethodInfoInvokers.MethodInfoInvoker.GetCreateParametersMethodInfo(mii.MethodInfo, typeof(Sample2));

			var prms = mii.CreateParameters(isample, 1, "qqq");
			var z = mii.Invoke(prms);
		}
		private static void Test2()
		{
			var sample = new Sample2();
			var map = typeof(Sample).GetInterfaceMap(typeof(ISample));
			var mi = map.TargetMethods[1];
            var mii = ProxyMixin.MethodInfoInvokers.MethodInfoInvoker.Create(mi, ProxyMixin.ProxyCtor.IndirectInvoker);
			var isample = (ISample)sample;

			var sw = new System.Diagnostics.Stopwatch();

			sw.Start();
			for (int i = 0; i < 1000000; i++)
			{
				isample.Final(1, "qqq");
				//isample.Final(1, 2);
			}
			sw.Stop();
			Console.WriteLine(sw.ElapsedMilliseconds);

			sw.Start();
			for (int i = 0; i < 1000000; i++)
			{
				var prms = mii.CreateParameters(isample, 1, "qqq");
				//var prms = mii.CreateParameters(sample, 1, 2);
				mii.Invoke(prms);
			}
			sw.Stop();
			Console.WriteLine(sw.ElapsedMilliseconds);

			sw.Start();
			for (int i = 0; i < 1000000; i++)
			{
				object[] prms = new object[] { 1, "qqq" };
				//object[] prms = new object[] { 1, 2 };
				mi.Invoke(isample, prms);
			}
			sw.Stop();
			Console.WriteLine(sw.ElapsedMilliseconds);
		}
		public static void Test3()
		{
			var sample = new Sample2();
            var isample = (ISample)sample;
            var mixin = new ProxyMixin.Mixins.InterceptorMixin<Sample2, ISample>(ProxyMixin.ProxyCtor.IndirectInvoker);
			var proxy = ProxyMixin.Ctors.InterceptorCtor.Create<Sample2, ISample>(sample, mixin);

            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000000; i++)
                proxy.Final(42, "qq");
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Start();
            for (int i = 0; i < 1000000; i++)
                isample.Final(42, "qq");
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
	}
}
