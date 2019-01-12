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
using BL;
using BE;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for UpdateTest.xaml
    /// </summary>
    public partial class UpdateTest : UserControl
    {
        public UpdateTest()
        {
            InitializeComponent();
            List<Trainee> trainees = Utilities.ReturnTrainees();
            if (trainees == null)
            {
                this.Visibility = Visibility.Collapsed;
            }
            foreach (var t in (from t in trainees where t.HaveTest select t))
            {
                ListBoxItem boxItem = new ListBoxItem();
                boxItem.Content = t.ToString();
                traineeOptions.Items.Add(boxItem);
            }
            thisTrainee = new Trainee();
            thisTest = new Test();
        }
        Trainee thisTrainee;
        Test thisTest;

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if(dateSelector.SelectedDate == null)
            {
                Utilities.ErrorBox("You must select a date.");
                return;
            }
            DateTime originalDate = thisTest.DateAndTime;
            if(!Utilities.AreYouSureBox("change the date of this test"))
            {
                return;
            }
            try
            {
                FactoryBL.Instance.CancelTest(thisTest);
            }
            catch(Exception ex)
            {
                Utilities.ErrorBox(ex.Message);
            }
            try
            {
                FactoryBL.Instance.GetTest(thisTrainee, dateSelector.SelectedDate.Value);
            }
            catch(Exception ex)
            {
                Utilities.ErrorBox(ex.Message);
                try
                {
                    FactoryBL.Instance.GetTest(thisTrainee, originalDate);
                }
                catch(Exception exc)
                {
                    Utilities.ErrorBox(exc.Message);
                }
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void traineeOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            thisTrainee = Utilities.ReturnTrainees().Find(t => t.ToString() == (sender as ComboBox).SelectedItem.ToString());
            thisTest = Utilities.ReturnTests().Find(t => t.TraineeId == thisTrainee.IDNumber);
        }
    }
}
