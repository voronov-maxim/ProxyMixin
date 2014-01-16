using InvokeMethodInfo;
using ProxyMixin;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleTest
{
    public class FooBase
    {
        public int i;
        public virtual void Test(int i)
        {
            this.i = i;
        }
    }
    public class Foo : FooBase
    {
        public override void Test(int i)
        {
            base.Test(i);
        }
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
        public string Ref(int i, ref FooBase foo, string s, ref ISample sample)
        {
            sample.Prop = i.ToString();
            return sample.Prop;
        }
        public virtual string Virtual(int i, string s)
        {
            return "";// s + i.ToString();
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

        delegate void ArgWarrior(RuntimeArgumentHandle argh);

        static void Main(String[] args)
        {
            //RunSamples.InitConsoleTraceListener();
            Test3();

            //RunSamples.LoggerSample();
        }

        private unsafe static void Test()
        {
            var ii = new IndirectInvoker(ProxyCtor.GetModuleBuilder());
            var d = (Action<IntPtr, Sample, IntPtr>)ii.Create(typeof(Sample), typeof(void), new Type[] { typeof(IntPtr) });

            int i = 1;
            TypedReference refi = __makeref(i);
            IntPtr ptr = new IntPtr(&i);

            var mi = typeof(Sample).GetMethod("Ref");
            var fptr = mi.MethodHandle.GetFunctionPointer();
            //d(fptr, new Sample(), ptr);

            for (int k = 0; k < 10000; k++)
            {
                var sample = new Sample();

                for (int j = 0; j < 1000; j++)
                    sample.Prop = j.ToString();

                var foo = new Foo() { i = 2 };
                var invoker = MethodInfoInvoker.Create(mi, ii);
                var prms = invoker.CreateParameters(sample, foo);
                invoker.Invoke(prms);

                var p1 = prms.GetValue<Foo>(0);
                Console.WriteLine(p1.i);
            }
        }

        private static void Test2()
        {
            var sample = new Sample2();
            var map = typeof(Sample).GetInterfaceMap(typeof(ISample));
            var mi = map.TargetMethods[1];
            var mii = MethodInfoInvoker.Create(mi, ProxyMixin.ProxyCtor.IndirectInvoker);
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
            var ii = new IndirectInvoker(ProxyCtor.GetModuleBuilder());
            var sample = new Sample2();
            var isample = (ISample)sample;
            var mixin = new ProxyMixin.Mixins.InterceptorMixin<Sample2, ISample>(ProxyMixin.ProxyCtor.IndirectInvoker);
            var proxy = ProxyMixin.Ctors.InterceptorCtor.Create<Sample2, ISample>(sample, mixin);
            var foo = new Foo();
            //var mi = typeof(Sample).GetInterfaceMap(typeof(ISample)).TargetMethods[0];
            var mi = typeof(Sample).GetMethod("Ref");

            var invoker = MethodInfoInvoker.Create(mi, ii);

            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                //proxy.Virtual(41, "qq");
                var args = invoker.CreateParameters((Sample)sample, 44, foo, "eee", isample);
                //var args = invoker.CreateParameters((Sample)sample, 41, "qqq");
                invoker.Invoke(args);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();
            for (int i = 0; i < 1000000; i++)
            {
                //isample.Virtual(41, "qq");
                var fooBase = (FooBase)foo;
                sample.Ref(44, ref fooBase, "eee", ref isample);
                //var args2 = new Object[] { 41, "qq" };
                //mi.Invoke(sample, args2);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
