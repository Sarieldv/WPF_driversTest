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
                return;
            }
            foreach (var t in (from t in trainees where t.HaveTest select t))
            {
                ComboBoxItem boxItem = new ComboBoxItem();
                boxItem.Content = t.ToString();
                traineeOptions.Items.Add(boxItem);
            }
            if(traineeOptions.Items.Count == 0)
            {
                Utilities.ErrorBox("There are no tests in the system.");
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
            DateTime chosenDate = new DateTime(dateSelector.SelectedDate.Value.Year, dateSelector.SelectedDate.Value.Month, dateSelector.SelectedDate.Value.Day, timeChoice.SelectedIndex + 9, 0, 0);

            try
            {
                FactoryBL.Instance.GetTest(thisTrainee, chosenDate);
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
                return;
            }
            Utilities.InformationBox("You have successfully updated this test");
            (this.Parent as StackPanel).Children.Add(new TestOptions());
            (this.Parent as StackPanel).Children.Remove(this);
        
    }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            (Parent as StackPanel).Children.Add(new TestOptions());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void traineeOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem TempBoxItem = new ComboBoxItem();
            foreach (var t in Utilities.ReturnTrainees())
            {
                TempBoxItem.Content = t.ToString();
                if (traineeOptions.SelectedItem.ToString() == TempBoxItem.ToString())
                {
                    thisTrainee = t;
                    break;
                }
            }
            thisTest = Utilities.ReturnTests().Find(t => t.TraineeId == thisTrainee.IDNumber);
        }
    }
}
