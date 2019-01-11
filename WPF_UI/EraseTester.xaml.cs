using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EraseTester.xaml
    /// </summary>
    public partial class EraseTester : UserControl
    {
        
        public EraseTester()
        {
            InitializeComponent();
            List<Tester> testers = Utilities.ReturnTesters();
            if (testers != null)
            {
                foreach (var t in Utilities.ReturnTesters())
                {
                    ListBoxItem boxItem = new ListBoxItem();
                    boxItem.Content = t.ToString();
                    TesterOptions.Items.Add(boxItem);
                }
            }
        }

        private void EraseButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Utilities.AreYouSureBox("erase this tester"))
                return;
            var k = Utilities.ReturnTesters().Find(t => t.ToString() == TesterOptions.ToString());
            FactoryBL.Instance.EraseTester(k);
            this.Visibility = Visibility.Collapsed;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
