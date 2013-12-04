using ProxyMixin;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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

    class Program
    {
        static void Main(String[] args)
        {
            var test = new Test();
            test.PropertyChanged += test_PropertyChanged;
            var factory1 = new ProxyMixin.ChangeTrackingFactory();
            var proxy1 = factory1.Create(test, "IsChanged");
            test.PropertyChanged+=test_PropertyChanged2;
            ((IDynamicProxy)proxy1).MemberwiseMapToWrappedObject();

            ((INotifyPropertyChanged)proxy1).PropertyChanged += proxy1_PropertyChanged;
            proxy1.StringProperty1 = "test value";

            //var factory2 = new ProxyMixin.ChangeTrackingFactory();
            //var proxy2 = factory2.Create(test, "IsChanged");
        }

        static void test_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
        static void test_PropertyChanged2(object sender, PropertyChangedEventArgs e)
        {
        }

        static void proxy1_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
    }
}
