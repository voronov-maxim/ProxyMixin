using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private readonly DynamicChangeTracking<TestViewModel> _dynamicViewModel;

        public MainWindow()
        {
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            InitializeComponent();

            var viewModel1 = new TestViewModel()
            {
                TestProperty1 = "test property",
                Rows = new List<Row>
                {
                    new Row() { Col1 = "R1C1", Col2 = "R1C2" },
                    new Row() { Col1 = "R2C1", Col2 = "R2C2" }
                }
            };

            var factory = new ProxyMixin.Factories.ChangeTrackingFactory();
            base.DataContext = factory.Create(viewModel1, "IsChanged");
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var zzz = dataGrid.Items;
            ((IRevertibleChangeTracking)base.DataContext).AcceptChanges();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ((IRevertibleChangeTracking)base.DataContext).RejectChanges();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            base.Close();
        }
    }

    public class TestViewModel : INotifyPropertyChanged
    {
        private string _testProperty1;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string DependentProperty
        {
            get
            {
                return "hello " + _testProperty1;
            }
        }
        public string TestProperty1
        {
            get
            {
                return _testProperty1;
            }
            set
            {
                _testProperty1 = value;
                OnPropertyChanged("DependentProperty");
            }
        }
        public List<Row> Rows
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class Row
    {
        public string Col1
        {
            get;
            set;
        }
        public string Col2
        {
            get;
            set;
        }
    }
}
