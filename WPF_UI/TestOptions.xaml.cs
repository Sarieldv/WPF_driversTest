using System;
using System.Collections.Generic;
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

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for TestOptions.xaml
    /// </summary>
    public partial class TestOptions : UserControl
    {
        public TestOptions()
        {
            InitializeComponent();
        }

        private void addTestButton_Click(object sender, RoutedEventArgs e)
        {
            AddTest addTest = new AddTest(); 
            this.mainGrid.Children.Add(addTest)
        }

        private void updateTestButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancelTestButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
