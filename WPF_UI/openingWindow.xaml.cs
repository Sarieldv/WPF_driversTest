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
    /// Interaction logic for openingWindow.xaml
    /// </summary>
    public partial class openingWindow : UserControl
    {
        public openingWindow()
        {
            InitializeComponent();
        }
        private void TesterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TraineeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new TestOptions());

            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void PresentButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
