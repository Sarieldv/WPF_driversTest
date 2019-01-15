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
        public DataPresentWindow(List<List<Trainee>> traineeList, bool? flag)
        {
            InitializeComponent();
            Label title = new Label();
            DataGrid data = new DataGrid();
            if (flag == null)
            {
                title.Content = "Trainees grouped by amount of tests done:";
            }
            else if (flag == true)
            {
                title.Content = "Trainees grouped by teacher:";
            }
            else if (flag == false)
            {
                title.Content = "Trainees grouped by school name:";
            }
            panel.Children.Add(title);
            foreach (var trainee in traineeList)
            {
                if (flag == null)
                {

                }
                else if (flag == true)
                {
                    
                }
                else if (flag == false)
                {
                    
                }
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
