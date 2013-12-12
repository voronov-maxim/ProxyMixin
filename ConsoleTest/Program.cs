using ProxyMixin;
using ProxyMixin.Mixins;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ConsoleTest
{
    public class Test : INotifyPropertyChanged
    {
        private String _stringProperty1;
        private PropertyChangedEventHandler _propertyChanged;

        public String StringProperty1
        {
            get
            {
                return _stringProperty1;
            }
            set
            {
                _stringProperty1 = value;
                if (_propertyChanged != null)
                    _propertyChanged(this, new PropertyChangedEventArgs("StringProperty2"));
            }
        }
        public String StringProperty2
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChanged += value; }
            remove { _propertyChanged -= value; }
        }
    }

    public interface ISimple
    {
        void Test(string svalue, int ivalue);
        //string STestProperty
        //{
        //    get;
        //    set;
        //}
    }
    public class BaseSimple : ISimple
    {
        void ISimple.Test(string svalue, int ivalue)
        {
        }

        //string ISimple.STestProperty
        //{
        //    get
        //    {
        //        return "baseSimple";
        //    }
        //    set
        //    {
        //    }
        //}
    }

    public class Simple : BaseSimple, ISimple
    {
        public void Test(string svalue, int ivalue)
        {
        }
        public string STestProperty
        {
            get;
            set;
        }
    }

    class Program
    {
        static void Main(String[] args)
        {
            var simple = new Simple();

            var builder = new InterceptorMixin<ISimple>();
            var mixin = builder.Create();
            mixin.Test("string value1", 444);
            //mixin.STestProperty = "ttt";

            //TestPerf();
        }


        /*private static void TestPerf()
        {
            var simple = new BaseSimple();

            var imap = typeof(BaseSimple).GetInterfaceMap(typeof(ISimple));
            var minfo = imap.TargetMethods[0];
            var isimple = (ISimple)simple;
            var dlgt = (Action<BaseSimple, string, object>)ProxyBuilderHelper.CreateDelegateFromMethodInfo(minfo);

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                var args = new Object[] { "string value1", 444 };
                minfo.Invoke(simple, args);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
                isimple.Test("string value1", 444);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                var args = new Object[] { simple, "string value1", 444 };
                //dlgt(simple, "string value1", 444);
                dlgt.DynamicInvoke(args);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }*/
    }
}
