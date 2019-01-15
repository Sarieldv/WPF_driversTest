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
    /// Interaction logic for DataWindow.xaml
    /// </summary>
    public partial class DataWindow : UserControl
    {
        public DataWindow()
        {
            InitializeComponent();
        }

        private void addTraineeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateTraineeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void eraseTraineeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new openingWindow());
            (this.Parent as StackPanel).Children.Remove(this);
        }
    }
}
