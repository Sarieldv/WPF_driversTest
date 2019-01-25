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
    public partial class TraineeDataPresentWindow : Window
    {
        public TraineeDataPresentWindow(IEnumerable<List<Trainee>> traineeListList, bool? flag)
        {
            InitializeComponent();
            if (flag == null)//by amount of tests
            {
                TitleLabel.Content = "Trainees grouped by amount of tests done:";
            }
            else if (flag == true)//by teacher
            {
                TitleLabel.Content = "Trainees grouped by teacher:";
            }
            else if (flag == false)//by school
            {
                TitleLabel.Content = "Trainees grouped by school name:";
            }
                foreach (var traineeList in traineeListList)
                {
                    if (flag == null)
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
                    else if (flag == true)
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
                    else if (flag == false)
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

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
