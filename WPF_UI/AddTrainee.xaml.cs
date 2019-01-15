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
using BE;
using BL;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for AddTrainee.xaml
    /// </summary>
    public partial class AddTrainee : UserControl
    {
        public AddTrainee()
        {
            InitializeComponent();
        }
        int[] classesArr = new int[8];
        int num;
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if ((BirthDate.SelectedDate == null))
            {
                Utilities.ErrorBox("You have not selected a date.");
                return;
            }
            if (!(bool)ManualHeavyTruck.IsChecked && !(bool)ManualMediumTruck.IsChecked && !(bool)ManualPrivateVehicle.IsChecked && !(bool)ManualTwoWheelVehicle.IsChecked && !(bool)AutomaticHeavyTruck.IsChecked && !(bool)AutomaticMediumTruck.IsChecked && !(bool)AutomaticPrivateVehicle.IsChecked && !(bool)AutomaticTwoWheelVehicle.IsChecked)
            {
                Utilities.ErrorBox("A vehicle must be selected!");
                return;
            }
            if (!Utilities.IsWords(FirstName.Text))
            {
                Utilities.ErrorBox("The first name contains characters that are not letters or spaces.");
                return;
            }
            if (!Utilities.IsWords(LastName.Text))
            {
                Utilities.ErrorBox("The last name contains characters that are not letters or spaces.");
                return;
            }
            if (!Utilities.IsStringNumbers(IDNumber.Text))
            {
                Utilities.ErrorBox("The ID number contains characters that are not numbers.");
                return;
            }
            if (!Utilities.IsStringNumbers(PhoneNumber.Text))
            {
                Utilities.ErrorBox("The phone number contains characters that are not numbers.");
                return;
            }
            if (HomeorMobile.SelectedItem.ToString() == "Home Phone")
            {
                if (PhoneNumber.Text.ToString().Length != 9)
                {
                    Utilities.ErrorBox("The phone number is an incorrect length.");
                    return;
                }
            }
            else
            {
                if (PhoneNumber.Text.ToString().Length != 10)
                {
                    Utilities.ErrorBox("The phone number is an incorrect length.");
                    return;
                }
            }
            if (!Utilities.IsStringNumbers(AddressNumber.Text.ToString()))
            {
                Utilities.ErrorBox("The address number contains characters that are not numbers.");
                return;
            }
            if (!Utilities.IsWords(teacherFirstName.Text.ToString()))
            {
                Utilities.ErrorBox("The teacher's first name contains characters that are not letters or spaces.");
                return;
            }
            if (!Utilities.IsWords(teacherLastName.Text.ToString()))
            {
                Utilities.ErrorBox("The teacher's last name contains characters that are not letters or spaces.");
                return;
            }
            VehicleParams myVehicle = new VehicleParams();
            if ((bool)AutomaticHeavyTruck.IsChecked)
            {
                myVehicle = (new VehicleParams(Vehicle.HeavyTruck, GearBox.Automatic));
            }
            else if ((bool)AutomaticMediumTruck.IsChecked)
            {
                myVehicle = (new VehicleParams(Vehicle.MediumTruck, GearBox.Automatic));
            }
            else if ((bool)AutomaticPrivateVehicle.IsChecked)
            {
                myVehicle = (new VehicleParams(Vehicle.PrivateVehicle, GearBox.Automatic));
            }
            else if ((bool)AutomaticTwoWheelVehicle.IsChecked)
            {
                myVehicle = (new VehicleParams(Vehicle.TwoWheelVehicle, GearBox.Automatic));
            }
            else if ((bool)ManualHeavyTruck.IsChecked)
            {
                myVehicle = (new VehicleParams(Vehicle.HeavyTruck, GearBox.Manual));
            }
            else if ((bool)ManualMediumTruck.IsChecked)
            {
                myVehicle = (new VehicleParams(Vehicle.MediumTruck, GearBox.Manual));
            }
            else if ((bool)ManualPrivateVehicle.IsChecked)
            {
                myVehicle = (new VehicleParams(Vehicle.PrivateVehicle, GearBox.Manual));
            }
            else if ((bool)ManualTwoWheelVehicle.IsChecked)
            {
                myVehicle = (new VehicleParams(Vehicle.TwoWheelVehicle, GearBox.Manual));
            }
            FirstName.Text = Utilities.ToProper(FirstName.Text.ToString());
            LastName.Text = Utilities.ToProper(LastName.Text.ToString());
            teacherFirstName.Text = Utilities.ToProper(teacherFirstName.Text.ToString());
            teacherLastName.Text = Utilities.ToProper(teacherLastName.Text.ToString());
            try
            {
                FactoryBL.Instance.AddTrainee(new Trainee(IDNumber.Text.ToString(), new FullName(FirstName.Text.ToString(), LastName.Text.ToString()), (DateTime)BirthDate.SelectedDate, (Gender)Gender.SelectedIndex, new PhoneNumber(PhoneNumber.Text.ToString(), (KindOfPhoneNumber)HomeorMobile.SelectedIndex), new Address(StreetName.Text.ToString(), int.Parse(AddressNumber.Text), CityName.Text), myVehicle, schoolName.Text.ToString(), new FullName(teacherFirstName.Text.ToString(), teacherLastName.Text.ToString()), classesArr ));
            }
            catch (Exception ex)
            {
                Utilities.ErrorBox(ex.Message);
                return;
            }
            Utilities.InformationBox(FirstName.Text.ToString() + " " + LastName.Text.ToString() + " was just added to the system as a trainee.");
            (this.Parent as StackPanel).Children.Add(new TraineeOptions());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void vehicleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            classes.Value = classesArr[(sender as ComboBox).SelectedIndex];
        }

        private void updateValue_Click(object sender, RoutedEventArgs e)
        {
            if (vehicleComboBox.SelectedIndex == -1)
                return;
            classesArr[vehicleComboBox.SelectedIndex] = (int)classes.Value;
        }
    }
}
