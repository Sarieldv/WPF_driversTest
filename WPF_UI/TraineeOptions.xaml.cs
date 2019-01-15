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
    /// Interaction logic for TraineeOptions.xaml
    /// </summary>
    public partial class TraineeOptions : UserControl
    {
        public TraineeOptions()
        {
            InitializeComponent();
        }

        private void addTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new AddTrainee());
            (this.Parent as StackPanel).Children.Remove(this); ;
        }

        private void updateTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new UpdateTrainee());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void eraseTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new EraseTrainee());
            (this.Parent as StackPanel).Children.Remove(this); ;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new openingWindow());
            (this.Parent as StackPanel).Children.Remove(this);
        }
    }
}
