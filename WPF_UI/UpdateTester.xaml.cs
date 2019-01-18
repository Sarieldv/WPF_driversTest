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
    /// Interaction logic for UpdateTester.xaml
    /// </summary>
    public partial class UpdateTester : UserControl
    {
        public UpdateTester()
        {
            InitializeComponent();
            List<Tester> testers = Utilities.ReturnTesters();
            if (testers == null)
            {
                return;
            }
            foreach (var t in testers)
            {
                ComboBoxItem boxItem = new ComboBoxItem();
                boxItem.Content = t.ToString();
                TesterOptions.Items.Add(boxItem);
            }

            thisTester = new Tester();
            copyTester = new Tester();
        }
        Tester thisTester;
        Tester copyTester;
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
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
            if (!Utilities.IsStringNumbers(PhoneNumber.Text))
            {
                Utilities.ErrorBox("The phone number contains characters that are not numbers.");
                return;
            }
            if (HomeorMobile.SelectedIndex == 1)
            {
                if (PhoneNumber.Text.ToString().Length != 9)
                {
                    Utilities.ErrorBox("The phone number is an incorrect length.");
                    return;
                }
            }
            else
            {
                if (PhoneNumber.Text.ToString().Length != 10 && HomeorMobile.SelectedIndex != -1)
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
            thisTester.MyVehicles = MyVehicles;
            thisTester.MaxDistanceFromTest = (int)MaxDistance.Value;
            thisTester.MaximumWeeklyTests = (int)MaxDistance.Value;
            thisTester.YearsOfExperience = (int)MaxDistance.Value;
            try
            {
                FactoryBL.Instance.UpdateTester(thisTester);
            }
            catch (Exception ex)
            {
                Utilities.ErrorBox(ex.Message);
            }
            Utilities.InformationBox(thisTester.ToString() + " has been successfully updated.");
            (this.Parent as StackPanel).Children.Add(new TesterOptions());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new TesterOptions());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void TesterOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem TempBoxItem = new ComboBoxItem();
            foreach (var t in Utilities.ReturnTesters())
            {
                TempBoxItem.Content = t.ToString();
                if (TesterOptions.SelectedItem.ToString() == TempBoxItem.ToString())
                {
                    thisTester = t;
                    break;
                }
            }
                copyTester = thisTester;
                AutomaticPrivateVehicle.IsChecked = thisTester.hasVehicle(new VehicleParams(Vehicle.PrivateVehicle, GearBox.Automatic));
                AutomaticTwoWheelVehicle.IsChecked = thisTester.hasVehicle(new VehicleParams(Vehicle.TwoWheelVehicle, GearBox.Automatic));
                AutomaticMediumTruck.IsChecked = thisTester.hasVehicle(new VehicleParams(Vehicle.MediumTruck, GearBox.Automatic));
                AutomaticHeavyTruck.IsChecked = thisTester.hasVehicle(new VehicleParams(Vehicle.HeavyTruck, GearBox.Automatic));
                ManualPrivateVehicle.IsChecked = thisTester.hasVehicle(new VehicleParams(Vehicle.PrivateVehicle, GearBox.Manual));
                ManualTwoWheelVehicle.IsChecked = thisTester.hasVehicle(new VehicleParams(Vehicle.TwoWheelVehicle, GearBox.Manual));
                ManualMediumTruck.IsChecked = thisTester.hasVehicle(new VehicleParams(Vehicle.MediumTruck, GearBox.Manual));
                ManualHeavyTruck.IsChecked = thisTester.hasVehicle(new VehicleParams(Vehicle.HeavyTruck, GearBox.Manual));
                MaxTests.Value = thisTester.MaximumWeeklyTests;
                YearsOfExperience.Value = thisTester.YearsOfExperience;
                MaxDistance.Value = thisTester.MaxDistanceFromTest;
            }
            #region StuffIsChanged
            private void FirstName_TextChanged(object sender, TextChangedEventArgs e)
            {
                if ((sender as TextBox).Text.ToString() != "" && Utilities.IsWords((sender as TextBox).Text.ToString()))
                {
                    thisTester.Name = new FullName(Utilities.ToProper((sender as TextBox).Text.ToString()), thisTester.Name.LastName);
                }
                else
                {
                    thisTester.Name = new FullName(copyTester.Name.FirstName, thisTester.Name.LastName);
                }
            }
            private void LastName_TextChanged(object sender, TextChangedEventArgs e)
            {
                if ((sender as TextBox).Text.ToString() != "" && Utilities.IsWords((sender as TextBox).Text.ToString()))
                {
                    thisTester.Name = new FullName(thisTester.Name.FirstName, Utilities.ToProper((sender as TextBox).Text.ToString()));
                }
                else
                {
                    thisTester.Name = new FullName(thisTester.Name.FirstName, copyTester.Name.LastName);
                }
            }
            private void CityName_TextChanged(object sender, TextChangedEventArgs e)
            {
                if ((sender as TextBox).Text.ToString() != "")
                {
                    thisTester.MyAddress = new Address(thisTester.MyAddress.StreetName, thisTester.MyAddress.AddressNumber, (sender as TextBox).Text.ToString());
                }
                else
                {
                    thisTester.MyAddress = new Address(thisTester.MyAddress.StreetName, thisTester.MyAddress.AddressNumber, copyTester.MyAddress.CityName);
                }
            }
            private void AddressNumber_TextChanged(object sender, TextChangedEventArgs e)
            {
                if (Utilities.IsStringNumbers((sender as TextBox).Text.ToString()) && (sender as TextBox).Text.ToString() != "")
                {
                    thisTester.MyAddress = new Address(thisTester.MyAddress.StreetName, int.Parse((sender as TextBox).Text.ToString()), thisTester.MyAddress.CityName);
                }
                else
                {
                    thisTester.MyAddress = new Address(thisTester.MyAddress.StreetName, copyTester.MyAddress.AddressNumber, thisTester.MyAddress.CityName);
                }
            }
            private void StreetName_TextChanged(object sender, TextChangedEventArgs e)
            {
                if ((sender as TextBox).Text.ToString() != "")
                {
                    thisTester.MyAddress = new Address((sender as TextBox).Text.ToString(), thisTester.MyAddress.AddressNumber, thisTester.MyAddress.CityName);
                }
                else
                {
                    thisTester.MyAddress = new Address(copyTester.MyAddress.StreetName, thisTester.MyAddress.AddressNumber, thisTester.MyAddress.CityName);
                }
            }
            private void PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
            {
                if ((sender as TextBox).Text.ToString() != "")
                {
                    thisTester.MyPhoneNumber = new PhoneNumber((sender as TextBox).Text.ToString(), thisTester.MyPhoneNumber.kind);
                }
                else
                {
                    thisTester.MyPhoneNumber = new PhoneNumber(copyTester.MyPhoneNumber.number, thisTester.MyPhoneNumber.kind);
                }
            }
            private void HomeorMobile_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                thisTester.MyPhoneNumber = new PhoneNumber(thisTester.MyPhoneNumber.number, (KindOfPhoneNumber)HomeorMobile.SelectedIndex);
            }
            #endregion


        }
    }
