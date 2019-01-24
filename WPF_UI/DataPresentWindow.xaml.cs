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
using System.Windows.Shapes;
using BE;
using BL;
namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for DataPresentWindow.xaml
    /// </summary>
    public partial class DataPresentWindow : Window
    {
        public DataPresentWindow(IEnumerable<List<Trainee>> traineeListList, int flag)
        {
            InitializeComponent();
            if (flag == 0)//by amount of tests
            {
                TitleLabel.Content = "Trainees grouped by amount of tests done:";
            }
            else if (flag == 1)//by teacher
            {
                TitleLabel.Content = "Trainees grouped by teacher:";
            }
            else if (flag == 2)//by school
            {
                TitleLabel.Content = "Trainees grouped by school name:";
            }
            else if (flag == 3)//all tests
            {
                TitleLabel.Content = "All future tests in the system:";
            }
            if (flag >= 0 && flag <= 2)
            {


                foreach (var traineeList in traineeListList)
                {
                    if (flag == 0)
                    {
                        Label cell = new Label();
                        cell.Content = traineeList.First().AmountOfTests.ToString() + " tests done:";
                        (TitleLabel.Parent as StackPanel).Children.Add(cell);
                        foreach (var trainee in traineeList)
                        {
                            cell = new Label();
                            cell.Content = trainee.ToString();
                            (TitleLabel.Parent as StackPanel).Children.Add(cell);
                        }
                    }
                    else if (flag == 1)
                    {
                        Label cell = new Label();
                        cell.Content = traineeList.First().Teacher.ToString() + " 's students:";
                        (TitleLabel.Parent as StackPanel).Children.Add(cell);
                        foreach (var trainee in traineeList)
                        {
                            cell = new Label();
                            cell.Content = trainee.ToString();
                            (TitleLabel.Parent as StackPanel).Children.Add(cell);
                        }
                    }
                    else if (flag == 2)
                    {
                        Label cell = new Label();
                        cell.Content = "Students that learnt at " + traineeList.First().SchoolName + ":";
                        (TitleLabel.Parent as StackPanel).Children.Add(cell);
                        foreach (var trainee in traineeList)
                        {
                            cell = new Label();
                            cell.Content = trainee.ToString();
                            (TitleLabel.Parent as StackPanel).Children.Add(cell);
                        }
                    }

                }
            }
            else if (flag == 3)
            {
                try
                {
                    Utilities.ReturnTests();
                }
                catch
                {
                    this.Close();
                }
                var tests = (from t in Utilities.ReturnTests()
                             where t.Grade == null
                             select t.ToString());
                Label cell = new Label();
                foreach (var test in tests)
                {
                    cell = new Label();
                    cell.Content = test;
                    (TitleLabel.Parent as StackPanel).Children.Add(cell);
                }
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
