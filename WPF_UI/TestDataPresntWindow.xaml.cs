using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BE;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for TestDataPresntWindow.xaml
    /// </summary>
    public partial class TestDataPresntWindow : Window
    {
        public TestDataPresntWindow()
        {
            InitializeComponent();
            var tests = (from t in Utilities.ReturnTests()
                         where t.Grade == null
                         select t);
            if(tests == null)
            {
                Close();
            }
            TitleLabel.Content = "All future tests:";
            Label cell;
            Trainee myTrainee = new Trainee();
            foreach (var test in tests)
            {
                myTrainee = (from t in Utilities.ReturnTrainees()
                             where t.IDNumber == test.TraineeId
                             select t).FirstOrDefault();
                cell = new Label();
                cell.Content = "Test Number: " + test.Number;
                cell.FontWeight = FontWeights.DemiBold;
                (TitleLabel.Parent as StackPanel).Children.Add(cell);
                cell = new Label();
                cell.Content = "Trainee: " + myTrainee.ToString();
                (TitleLabel.Parent as StackPanel).Children.Add(cell);
                cell = new Label();
                cell.Content = "Date: " + test.DateAndTime.ToString();
                (TitleLabel.Parent as StackPanel).Children.Add(cell);
                cell = new Label();
                cell.Content =  test.AddressOfDeparture.ToString();
                (TitleLabel.Parent as StackPanel).Children.Add(cell);
                cell = new Label();
                (TitleLabel.Parent as StackPanel).Children.Add(cell);
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
