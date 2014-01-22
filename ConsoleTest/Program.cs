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

            //var ii = IndirectInvokerBuilder.Create(ProxyCtor.GetModuleBuilder());
            //for (int i = 0; i < 10; i++)
            //{
            //    ii.GetMethodInfo(typeof(void), i);
            //    ii.GetMethodInfo(typeof(object), i);
            //}
            //ProxyCtor._zzz.Save("CallMethodPointer.dll");
        }

        public static void Test3()
        {
            var ii = IndirectInvokerBuilder.Create();
            //var ii = IndirectInvokerBuilder.Create(ProxyCtor.GetModuleBuilder());
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
