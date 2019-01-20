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
    /// Interaction logic for bothUpdateOptions.xaml
    /// </summary>
    public partial class bothUpdateOptions : UserControl
    {
        public bothUpdateOptions()
        {
            InitializeComponent();
        }
        private void updateAndGradeButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new UpdateTestAfterTest());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new TestOptions());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void changeTheDate_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new UpdateTest());
            (this.Parent as StackPanel).Children.Remove(this);
        }
    }
}
