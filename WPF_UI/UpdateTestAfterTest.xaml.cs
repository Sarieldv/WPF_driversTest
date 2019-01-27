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
using System.ComponentModel;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for UpdateTestAfterTest.xaml
    /// </summary>
    public partial class UpdateTestAfterTest : UserControl
    {
        public UpdateTestAfterTest()
        {
            InitializeComponent();
            Worker = new BackgroundWorker();
            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            List<Test> tests = Utilities.ReturnTests();
            if (tests == null)
            {
                return;
            }
            foreach (var t in tests)
            {
                if(t.Grade == null)
                {
                    ComboBoxItem boxItem = new ComboBoxItem();
                    boxItem.Content = t.ToString();
                    testOptions.Items.Add(boxItem);
                }
            }
            thisTest = new Test();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Exception result = e.Result as Exception;
            if (result != null)
            {
                Utilities.ErrorBox(result.Message);
                return;
            }
            Utilities.InformationBox("You have successfully graded this test!");
            stack.Children.Add(new TestOptions());
            stack.Children.Remove(this);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FactoryBL.Instance.UpdateTest(e.Argument as Test);
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }

        BackgroundWorker Worker;
        Trainee thisTrainee;
        Test thisTest;
        StackPanel stack;

        private void gradeButton_Click(object sender, RoutedEventArgs e)
        {
            stack = Parent as StackPanel;
            thisTest.DistanceKeep = distanceKeep.IsChecked;
            thisTest.ReverseParking = reverseParking.IsChecked;
            thisTest.Parking = parking.IsChecked;
            thisTest.LookingAtMirrors = lookingAtMirrors.IsChecked;
            thisTest.Junction = junction.IsChecked;
            thisTest.Reversing = reversing.IsChecked;
            thisTest.Roundabout = roundAbout.IsChecked;
            thisTest.Overtaking = overTaking.IsChecked;
            thisTest.Turning = turning.IsChecked;
            thisTest.TesterNote = testersNote.Text.ToString();
            thisTest.Grade = passed.IsChecked;
            try
            {
                Worker.RunWorkerAsync(thisTest);
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

        private void testOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem TempBoxItem = new ComboBoxItem();
            foreach (var t in Utilities.ReturnTests())
            {
                TempBoxItem.Content = t.ToString();
                if (testOptions.SelectedItem.ToString() == TempBoxItem.ToString())
                {
                    thisTest = t;
                    break;
                }
            }
            thisTrainee = Utilities.ReturnTrainees().Find(t => t.IDNumber == thisTest.TraineeId);
        }
    }
}
