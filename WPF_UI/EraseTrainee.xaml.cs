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
    /// Interaction logic for EraseTrainee.xaml
    /// </summary>
    public partial class EraseTrainee : UserControl
    {
        public EraseTrainee()
        {
            InitializeComponent();
            List<Trainee> trainees = Utilities.ReturnTrainees();
            if (trainees == null)
            {
                return;
            }
            foreach (var t in trainees)
            {
                ComboBoxItem boxItem = new ComboBoxItem();
                boxItem.Content = t.ToString();
                traineeOptions.Items.Add(boxItem);
            }

        }

        private void eraseButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Utilities.AreYouSureBox("erase this trainee"))
                return;
            ComboBoxItem TempBoxItem = new ComboBoxItem();
            Trainee k = new Trainee();
            foreach (var t in Utilities.ReturnTrainees())
            {
                TempBoxItem.Content = t.ToString();
                if (traineeOptions.SelectedItem.ToString() == TempBoxItem.ToString())
                {
                    k = t;
                    break;
                }
            }
            try
            {
                FactoryBL.Instance.EraseTrainee(k);
            }
            catch (Exception ex)
            {
                Utilities.ErrorBox(ex.Message);
            }
            Utilities.InformationBox("You have successfully erased a trainee.");
            (this.Parent as StackPanel).Children.Add(new TraineeOptions());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new TraineeOptions());
            (this.Parent as StackPanel).Children.Remove(this);
        }
    }
}
