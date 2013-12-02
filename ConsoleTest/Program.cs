using System;
using System.Collections.Generic;

namespace ConsoleTest
{
    public class Test
    {
        public String StringProperty
        {
            get;
            set;
        }
    }

    class Program
    {
        static void Main(String[] args)
        {
            var test = new Test();
            var factory1 = new ProxyMixin.ChangeTrackingFactory();
            var proxy1 = factory1.Create(test, "IsChanged");

            var factory2 = new ProxyMixin.ChangeTrackingFactory();
            var proxy2 = factory2.Create(test, "IsChanged");

            try
            {
                Test1();
            }
            catch (Exception e)
            {
                var st = new System.Diagnostics.StackTrace(e);
                var frame = st.GetFrames()[0];
                var off = frame.GetILOffset();
            }
        }
        public static void Test1()
        {
            throw new Exception();
        }
    }
}
