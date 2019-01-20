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
            (this.Parent as StackPanel).Children.Add(new AddTest());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void updateTestButton_Click(object sender, RoutedEventArgs e)
        {
            if(Utilities.ReturnTests() == null)
            {
                return;
            }
            (this.Parent as StackPanel).Children.Add(new bothUpdateOptions());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void cancelTestButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new CancelTest());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new openingWindow());
            (this.Parent as StackPanel).Children.Remove(this);
        }
    }
}
