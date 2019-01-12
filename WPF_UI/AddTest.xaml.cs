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
    /// Interaction logic for AddTest.xaml
    /// </summary>
    public partial class AddTest : UserControl
    {
        public AddTest()
        {
            InitializeComponent();
            List<Trainee> trainees = Utilities.ReturnTrainees();
            if (trainees == null)
            {
                //go back
            }
            foreach (var t in trainees)
            {
                ListBoxItem boxItem = new ListBoxItem();
                boxItem.Content = t.ToString();
                traineeOptions.Items.Add(boxItem);
            }
            thisTrainee = new Trainee();
        }
        Trainee thisTrainee;
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if((datePicker.SelectedDate == null))
            {
                Utilities.ErrorBox("You have not selected a date.");
                return;
            }
            DateTime chosenDate = new DateTime(datePicker.SelectedDate.Value.Year, datePicker.SelectedDate.Value.Month, datePicker.SelectedDate.Value.Day, timeChoice.SelectedIndex + 9, 0, 0);
            try
            {
                FactoryBL.Instance.GetTest(thisTrainee, chosenDate);
            }
            catch(Exception ex)
            {
                Utilities.ErrorBox(ex.Message);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            //go back
        }

        private void traineeOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            thisTrainee = Utilities.ReturnTrainees().Find(t => t.ToString() == (sender as ComboBox).SelectedItem.ToString());
              
        }
    }
}
