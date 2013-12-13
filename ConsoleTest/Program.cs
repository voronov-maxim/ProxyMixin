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
        int TestString(string svalue, int ivalue);
        void TestVoid();
        string STestProperty
        {
            get;
            set;
        }
        Simple this[int i]
        {
            get;
            set;
        }
    }
    public class BaseSimple : ISimple
    {
        int ISimple.TestString(string svalue, int ivalue)
        {
            //return "base " + svalue + " = " + ivalue;
            return ivalue * 10;
        }
        void ISimple.TestVoid()
        {
        }

        string ISimple.STestProperty
        {
            get
            {
                return "baseSimple";
            }
            set
            {
            }
        }

        Simple ISimple.this[int i]
        {
            get
            {
                return new Simple();
            }
            set
            {
            }
        }
    }

    public class Simple : BaseSimple, ISimple
    {
        int ISimple.TestString(string svalue, int ivalue)
        {
            //return svalue + " = " + ivalue;
            return ivalue * 20;
        }
        void ISimple.TestVoid()
        {
        }

        private string _STestProperty;
        public string STestProperty
        {
            get
            {
                return _STestProperty;
            }
            set
            {
                _STestProperty = value;
            }
        }
        Simple ISimple.this[int i]
        {
            get
            {
                return new Simple();
            }
            set
            {
            }
        }
    }

    class Program
    {
        static void Main(String[] args)
        {
            LoggerSample.RunTest();

            //var simple = new Simple();

            //var builder = new InterceptorMixin<ISimple>();
            //var interceptorMixin = builder.Create();
            //var proxy = ProxyFactory.Create(simple, interceptorMixin);
            //var isimple = (ISimple)proxy;
            //var result = isimple.TestString("test value", 33);
            //isimple.TestVoid();
            //isimple.STestProperty = "qqq";
            //var sprop = isimple.STestProperty;
            //isimple[0] = new Simple();
            //var ind = isimple[0];
            
            //TestPerf();
        }


        private static void TestPerf()
        {
            var simple = new Simple();
            var isimple = (ISimple)simple;

            var builder = new InterceptorMixin<ISimple>();
            var interceptorMixin = builder.Create();
            var proxy = ProxyFactory.Create(simple, interceptorMixin);
            var iproxy = (ISimple)proxy;

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                //iproxy.TestString("string value1", 444);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                //isimple.TestString("string value1", 444);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
