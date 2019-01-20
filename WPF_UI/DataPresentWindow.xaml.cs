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
        public DataPresentWindow(List<List<Trainee>> traineeListList, bool? flag)
        {
            InitializeComponent();
            if (flag == null)
            {
                TitleLabel.Content = "Trainees grouped by amount of tests done:";
            }
            else if (flag == true)
            {
                TitleLabel.Content = "Trainees grouped by teacher:";
            }
            else if (flag == false)
            {
                TitleLabel.Content = "Trainees grouped by school name:";
            }
            foreach (var traineeList in traineeListList)
            {
                if (flag == null)
                {
                    DataGridCell cell = new DataGridCell();
                    cell.Content = traineeList.First().AmountOfTests.ToString() + " tests done:";
                    data.Items.Add(cell);
                    foreach (var trainee in traineeList)
                    {
                        cell = new DataGridCell();
                        cell.Content = trainee.ToString();
                        data.Items.Add(cell);
                    }
                }
                else if (flag == true)
                {
                    DataGridCell cell = new DataGridCell();
                    cell.Content = traineeList.First().Teacher.ToString() + " 's students:";
                    data.Items.Add(cell);
                    foreach (var trainee in traineeList)
                    {
                        cell = new DataGridCell();
                        cell.Content = trainee.ToString();
                        data.Items.Add(cell);
                    }
                }
                else if (flag == false)
                {
                    DataGridCell cell = new DataGridCell();
                    cell.Content = "Students that learnt at " + traineeList.First().SchoolName;
                    data.Items.Add(cell);
                    foreach (var trainee in traineeList)
                    {
                        cell = new DataGridCell();
                        cell.Content = trainee.ToString();
                        data.Items.Add(cell);
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
