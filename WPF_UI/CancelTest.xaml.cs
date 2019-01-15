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
using BE;
using BL;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for CancelTest.xaml
    /// </summary>
    public partial class CancelTest : UserControl
    {
        public CancelTest()
        {
            InitializeComponent();
            List<Test> tests = Utilities.ReturnTests();
            if(tests==null)
            {
                (this.Parent as StackPanel).Children.Remove(this);
            }
            foreach (var t in tests)
            {
                ListBoxItem boxItem = new ListBoxItem();
                boxItem.Content = t.ToString();
                testOptions.Items.Add(boxItem);
            }
            thisTest = new Test();
        }
        Test thisTest;
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(!Utilities.AreYouSureBox("cancel this test"))
            {
                (this.Parent as StackPanel).Children.Remove(this);
            }
            else
            {
                try
                {
                    FactoryBL.Instance.CancelTest(thisTest);
                }
                catch (Exception ex)
                {
                    Utilities.ErrorBox(ex.Message);
                }
            }
            Utilities.InformationBox("You have successfelly canceled a test.");
            (this.Parent as StackPanel).Children.Add(new TestOptions());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void testOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            thisTest = Utilities.ReturnTests().Find(t => t.ToString() == (sender as ComboBox).SelectedItem.ToString());
        }
    }
}
