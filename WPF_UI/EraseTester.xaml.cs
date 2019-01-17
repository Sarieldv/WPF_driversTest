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
            if(testers == null)
            {
                return;
            }
                foreach (var t in testers)
                {
                    ComboBoxItem boxItem = new ComboBoxItem();
                    boxItem.Content = t.ToString();
                    TesterOptions.Items.Add(boxItem);
                }
            
        }

        private void EraseButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Utilities.AreYouSureBox("erase this tester, even though any tests they are signed up to will be erased"))
                return;
            ComboBoxItem TempBoxItem = new ComboBoxItem();
            //Tester k = Utilities.ReturnTesters().Find(t => t.ToString() == TesterOptions.ToString());
            Tester k = new Tester();
            foreach (var t in Utilities.ReturnTesters())
            {
                TempBoxItem.Content = t.ToString();
                if(TesterOptions.SelectedItem.ToString() == TempBoxItem.ToString())
                {
                    k = t;           
                    break;
                }
            }
            try
            {
                FactoryBL.Instance.EraseTester(k);
            }
            catch(Exception ex)
            {
                Utilities.ErrorBox(ex.Message);
            }
            Utilities.InformationBox("You have successfully erased a tester");
            (this.Parent as StackPanel).Children.Add(new TesterOptions());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new TesterOptions());
            (this.Parent as StackPanel).Children.Remove(this);
        }
    }
}
