using System;

namespace ConsoleTest
{
    public class FooBase
    {
    }
    public class Foo : FooBase
    {
    }

    public interface ISample
    {
        string Virtual(int i, string s);
        string Final(FooBase foo);
        string Prop
        {
            get;
            set;
        }
    }

    public class Sample : ISample
    {
        public string Virtual(int i, string s)
        {
            return s + i.ToString();
        }
        string ISample.Final(FooBase foo)
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
        public string Virtual2(int i, string s)
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

            var prms = ProxyMixin.MethodInfoInvokers.MethodInfoInvoker.CreateParameters(isample, 1, "qqq");
            var z = mii.Invoke(prms);
        }
        private static void Test2()
        {
            var sample = new Sample2();
            var map = typeof(Sample).GetInterfaceMap(typeof(ISample));
            var mi = map.TargetMethods[1];
            var mii = ProxyMixin.MethodInfoInvokers.MethodInfoInvoker.Create(mi, ProxyMixin.ProxyCtor.IndirectInvoker);
            var isample = (ISample)sample;
            var foo = new Foo();

            var sw = new System.Diagnostics.Stopwatch();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                isample.Final(foo);
                //isample.Final(1, 2);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                var prms = ProxyMixin.MethodInfoInvokers.MethodInfoInvoker.CreateParameters(isample, 1, "qqq");
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
            var foo = new Foo();
            var mi = typeof(Sample).GetInterfaceMap(typeof(ISample)).TargetMethods[0];
            //var ptr = mi.MethodHandle.GetFunctionPointer();

            var invoker = ProxyMixin.MethodInfoInvokers.MethodInfoInvoker.Create(mi, null);

            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                proxy.Virtual(41, "qq");
                //var args = ProxyMixin.MethodInfoInvokers.MethodInfoInvoker.CreateParameters(sample, 41, "qqq");
                //invoker.Invoke(args);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();
            for (int i = 0; i < 1000000; i++)
            {
                isample.Virtual(41, "qq");
                //var args2 = new Object[] { 41, "qq" };
                //mi.Invoke(sample, args2);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
