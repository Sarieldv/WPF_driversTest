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
    /// Interaction logic for AddTester.xaml
    /// </summary>
    public partial class AddTester : UserControl
    {
        public AddTester()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(bool)ManualHeavyTruck.IsChecked && !(bool)ManualMediumTruck.IsChecked && !(bool)ManualPrivateVehicle.IsChecked && !(bool)ManualTwoWheelVehicle.IsChecked && !(bool)AutomaticHeavyTruck.IsChecked && !(bool)AutomaticMediumTruck.IsChecked && !(bool)AutomaticPrivateVehicle.IsChecked && !(bool)AutomaticTwoWheelVehicle.IsChecked)
            {
                Utilities.ErrorBox("At least one vehicle must be selected!");
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
            List<VehicleParams> MyVehicles = new List<VehicleParams>();
            if ((bool)AutomaticHeavyTruck.IsChecked)
            {
                MyVehicles.Add(new VehicleParams(Vehicle.HeavyTruck, GearBox.Automatic));
            }
            if ((bool)AutomaticMediumTruck.IsChecked)
            {
                MyVehicles.Add(new VehicleParams(Vehicle.MediumTruck, GearBox.Automatic));
            }
            if ((bool)AutomaticPrivateVehicle.IsChecked)
            {
                MyVehicles.Add(new VehicleParams(Vehicle.PrivateVehicle, GearBox.Automatic));
            }
            if ((bool)AutomaticTwoWheelVehicle.IsChecked)
            {
                MyVehicles.Add(new VehicleParams(Vehicle.TwoWheelVehicle, GearBox.Automatic));
            }
            if ((bool)ManualHeavyTruck.IsChecked)
            {
                MyVehicles.Add(new VehicleParams(Vehicle.HeavyTruck, GearBox.Manual));
            }
            if ((bool)ManualMediumTruck.IsChecked)
            {
                MyVehicles.Add(new VehicleParams(Vehicle.MediumTruck, GearBox.Manual));
            }
            if ((bool)ManualPrivateVehicle.IsChecked)
            {
                MyVehicles.Add(new VehicleParams(Vehicle.PrivateVehicle, GearBox.Manual));
            }
            if ((bool)ManualTwoWheelVehicle.IsChecked)
            {
                MyVehicles.Add(new VehicleParams(Vehicle.TwoWheelVehicle, GearBox.Manual));
            }
            FirstName.Text = Utilities.ToProper(FirstName.Text.ToString());
            LastName.Text = Utilities.ToProper(LastName.Text.ToString());
            try
            {
                FactoryBL.Instance.AddTester(new Tester(IDNumber.Text.ToString(), new FullName(FirstName.Text.ToString(), LastName.Text.ToString()), (DateTime)BirthDate.SelectedDate, (Gender)Gender.SelectedIndex, new PhoneNumber(PhoneNumber.Text.ToString(), (KindOfPhoneNumber)HomeorMobile.SelectedIndex), new Address(StreetName.Text.ToString(), int.Parse(AddressNumber.Text),CityName.Text),(int) YearsOfExperience.Value,(int) MaxTests.Value, MyVehicles, (int)MaxDistance.Value));
                //for checking purposes
                //FactoryBL.Instance.AddTester(new Tester("212413165", new FullName("Sariel", "Segal"), (DateTime)BirthDate.SelectedDate, BE.Gender.Female, new PhoneNumber("0584719940", KindOfPhoneNumber.Mobile), new Address("Hanasi", 1, "Jerusalem"), 1, 1, MyVehicles, 1));
            }
            catch (Exception ex)
            {
                Utilities.ErrorBox(ex.Message);
                return;
            }
            Utilities.InformationBox(FirstName.Text.ToString() + " " + LastName.Text.ToString() + " was just added to the system as a tester.");
            this.Visibility = Visibility.Collapsed;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
