using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            Worker = new BackgroundWorker();
            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
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
            thisTrainee = new Trainee();
        }
        BackgroundWorker Worker;
        Trainee thisTrainee;
        StackPanel stack;
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            stack = this.Parent as StackPanel;
            if((datePicker.SelectedDate == null))
            {
                Utilities.ErrorBox("You have not selected a date.");
                return;
            }
            if((datePicker.SelectedDate.Value - DateTime.Now).TotalDays < 0)
            {
                Utilities.ErrorBox("You cannot choose a date from the past.");
                return;
            }
            if (datePicker.SelectedDate.Value.DayOfWeek == DayOfWeek.Friday || datePicker.SelectedDate.Value.DayOfWeek == DayOfWeek.Saturday)
            {
                Utilities.ErrorBox("Fridays and Saturdays are unavailable for tests.");
                return;
            }
            DateTime chosenDate = new DateTime(datePicker.SelectedDate.Value.Year, datePicker.SelectedDate.Value.Month, datePicker.SelectedDate.Value.Day, timeChoice.SelectedIndex + 9, 0, 0);
            traineeAndDate instance = new traineeAndDate(thisTrainee, chosenDate);
            try
            {
                Worker.RunWorkerAsync(instance);
            }
            catch(Exception ex)
            {
                Utilities.ErrorBox(ex.Message);
                return;
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new TestOptions());
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

        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Exception result = e.Result as Exception;
            if (result != null)
            {
                Utilities.ErrorBox(result.Message);
                return;
            }
            Utilities.InformationBox("You have successfully added a test");
            stack.Children.Add(new TestOptions());
            stack.Children.Remove(this);
      
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FactoryBL.Instance.GetTest((e.Argument as traineeAndDate).trainee, (e.Argument as traineeAndDate).date);
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }
    }
}
