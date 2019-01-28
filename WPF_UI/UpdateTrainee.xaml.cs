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
    /// Interaction logic for UpdateTrainee.xaml
    /// </summary>
    public partial class UpdateTrainee : UserControl
    {
        public UpdateTrainee()
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
                TraineeOptions.Items.Add(boxItem);
            }

            thisTrainee = new Trainee();
            copyTrainee = new Trainee();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Exception result = e.Result as Exception;
            if (result != null)
            {
                Utilities.ErrorBox(result.Message);
                return;
            }
            Utilities.InformationBox(thisTrainee.ToString() + " has been successfully updated.");
            stack.Children.Add(new TraineeOptions());
            stack.Children.Remove(this);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FactoryBL.Instance.UpdateTrainee(thisTrainee);
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }
        StackPanel stack;
        Trainee thisTrainee, copyTrainee;
        BackgroundWorker Worker;

        private void updateValue_Click(object sender, RoutedEventArgs e)
        {
            if (vehicleComboBox.SelectedIndex == -1)
                return;
            thisTrainee.AmountOfClasses[vehicleComboBox.SelectedIndex] = (int)classes.Value;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            stack = Parent as StackPanel;
            if (AutomaticPrivateVehicle.IsChecked.Value)
            {
                thisTrainee.TraineeVehicle = new VehicleParams(Vehicle.PrivateVehicle, GearBox.Automatic);
            }
            else if (AutomaticTwoWheelVehicle.IsChecked.Value)
            {
                thisTrainee.TraineeVehicle = new VehicleParams(Vehicle.TwoWheelVehicle, GearBox.Automatic);
            }
            else if (AutomaticMediumTruck.IsChecked.Value)
            {
                thisTrainee.TraineeVehicle = new VehicleParams(Vehicle.MediumTruck, GearBox.Automatic);
            }
            else if (AutomaticHeavyTruck.IsChecked.Value)
            {
                thisTrainee.TraineeVehicle = new VehicleParams(Vehicle.HeavyTruck, GearBox.Automatic);
            }
            else if (ManualPrivateVehicle.IsChecked.Value)
            {
                thisTrainee.TraineeVehicle = new VehicleParams(Vehicle.PrivateVehicle, GearBox.Manual);
            }
            else if (ManualTwoWheelVehicle.IsChecked.Value)
            {
                thisTrainee.TraineeVehicle = new VehicleParams(Vehicle.TwoWheelVehicle, GearBox.Manual);
            }
            else if (ManualMediumTruck.IsChecked.Value)
            {
                thisTrainee.TraineeVehicle = new VehicleParams(Vehicle.MediumTruck, GearBox.Manual);
            }
            else if (ManualHeavyTruck.IsChecked.Value)
            {
                thisTrainee.TraineeVehicle = new VehicleParams(Vehicle.HeavyTruck, GearBox.Manual);
            }
            try
            {
                Worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Utilities.ErrorBox(ex.Message);
            }

        }
        #region stuffIsChanged
        private void TraineeOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem TempBoxItem = new ComboBoxItem();
            foreach (var t in Utilities.ReturnTrainees())
            {
                TempBoxItem.Content = t.ToString();
                if (TraineeOptions.SelectedItem.ToString() == TempBoxItem.ToString())
                {
                    thisTrainee = t;
                    break;
                }
            }
            copyTrainee = thisTrainee;
        }
        private void FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.ToString() != "" && Utilities.IsWords((sender as TextBox).Text.ToString()))
            {
                thisTrainee.Name = new FullName(Utilities.ToProper((sender as TextBox).Text.ToString()), thisTrainee.Name.LastName);
            }
            else
            {
                thisTrainee.Name = new FullName(copyTrainee.Name.FirstName, thisTrainee.Name.LastName);
            }
        }

        private void LastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.ToString() != "" && Utilities.IsWords((sender as TextBox).Text.ToString()))
            {
                thisTrainee.Name = new FullName(thisTrainee.Name.FirstName, Utilities.ToProper((sender as TextBox).Text.ToString()));
            }
            else
            {
                thisTrainee.Name = new FullName(thisTrainee.Name.FirstName, copyTrainee.Name.LastName);
            }
        }

        private void CityName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.ToString() != "")
            {
                thisTrainee.MyAddress = new Address(thisTrainee.MyAddress.StreetName, thisTrainee.MyAddress.AddressNumber, (sender as TextBox).Text.ToString());
            }
            else
            {
                thisTrainee.MyAddress = new Address(thisTrainee.MyAddress.StreetName, thisTrainee.MyAddress.AddressNumber, copyTrainee.MyAddress.CityName);
            }
        }
        private void AddressNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Utilities.IsStringNumbers((sender as TextBox).Text.ToString()) && (sender as TextBox).Text.ToString() != "")
            {
                thisTrainee.MyAddress = new Address(thisTrainee.MyAddress.StreetName, int.Parse((sender as TextBox).Text.ToString()), thisTrainee.MyAddress.CityName);
            }
            else
            {
                thisTrainee.MyAddress = new Address(thisTrainee.MyAddress.StreetName, copyTrainee.MyAddress.AddressNumber, thisTrainee.MyAddress.CityName);
            }
        }
        private void StreetName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.ToString() != "")
            {
                thisTrainee.MyAddress = new Address((sender as TextBox).Text.ToString(), thisTrainee.MyAddress.AddressNumber, thisTrainee.MyAddress.CityName);
            }
            else
            {
                thisTrainee.MyAddress = new Address(copyTrainee.MyAddress.StreetName, thisTrainee.MyAddress.AddressNumber, thisTrainee.MyAddress.CityName);
            }
        }
        private void PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.ToString() != "")
            {
                thisTrainee.MyPhoneNumber = new PhoneNumber((sender as TextBox).Text.ToString(), thisTrainee.MyPhoneNumber.kind);
            }
            else
            {
                thisTrainee.MyPhoneNumber = new PhoneNumber(copyTrainee.MyPhoneNumber.number, thisTrainee.MyPhoneNumber.kind);
            }
        }
        private void HomeorMobile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            thisTrainee.MyPhoneNumber = new PhoneNumber(thisTrainee.MyPhoneNumber.number, (KindOfPhoneNumber)HomeorMobile.SelectedIndex);
        }

        private void vehicleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vehicleComboBox.SelectedIndex == -1)
                return;
            classes.Value = thisTrainee.AmountOfClasses[vehicleComboBox.SelectedIndex];
        }
        #endregion
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            (Parent as StackPanel).Children.Add(new TraineeOptions());
            (Parent as StackPanel).Children.Remove(this);
        }
        
    }
}
