using BE;
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

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for TraineePassedWindow.xaml
    /// </summary>
    public partial class TraineePassedWindow : Window
    {
        public TraineePassedWindow()
        {
            InitializeComponent();
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
        Trainee thisTrainee;
        int vehicleIndex;
        private void vehicleOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vehicleIndex = vehicleOptions.SelectedIndex;
            if(traineeOptions.SelectedIndex != -1)
            {
                if(thisTrainee.PassedByVehicleParams[vehicleIndex])
                {
                    textOutput.Text = "Can Drive!";
                    textOutput.Background = Brushes.Green;
                    textOutput.Foreground = Brushes.White;
                }
                else
                {
                    textOutput.Text = "Can't Drive!";
                    textOutput.Background = Brushes.Red;
                    textOutput.Foreground = Brushes.White;
                }
            }
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
            if(vehicleOptions.SelectedIndex != -1)
            {
                if (thisTrainee.PassedByVehicleParams[vehicleIndex])
                {
                    textOutput.Text = "Can Drive!";
                    textOutput.Background = Brushes.Green;
                    textOutput.Foreground = Brushes.White;
                }
                else
                {
                    textOutput.Text = "Can't Drive!";
                    textOutput.Background = Brushes.Red;
                    textOutput.Foreground = Brushes.White;
                }
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
