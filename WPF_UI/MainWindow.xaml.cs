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
using BL;
using BE;
using System.Windows.Threading;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick_Hour);
            dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            dispatcherTimer.Start();
            InitializeComponent();
            #region Addition of 2 testers and 2 trainees
            //Valid ids:
            //242516987
            //314569245
            //015692486
            //234910768
            List<VehicleParams> vehicles = new List<VehicleParams>();
            vehicles.Add(new VehicleParams(Vehicle.PrivateVehicle, GearBox.Automatic));
            vehicles.Add(new VehicleParams(Vehicle.MediumTruck, GearBox.Automatic));
            vehicles.Add(new VehicleParams(Vehicle.HeavyTruck, GearBox.Automatic));
            vehicles.Add(new VehicleParams(Vehicle.TwoWheelVehicle, GearBox.Automatic));
            vehicles.Add(new VehicleParams(Vehicle.PrivateVehicle, GearBox.Manual));
            vehicles.Add(new VehicleParams(Vehicle.MediumTruck, GearBox.Manual));
            vehicles.Add(new VehicleParams(Vehicle.HeavyTruck, GearBox.Manual));
            vehicles.Add(new VehicleParams(Vehicle.TwoWheelVehicle, GearBox.Manual));
            try
            {
                FactoryBL.Instance.AddTester(new Tester("242516987", new FullName("John", "Doe"), new DateTime(1970, 1, 5), Gender.Male, new PhoneNumber("0546235582", KindOfPhoneNumber.Mobile), new Address("Hanasi", 7, "Jerusalem"), 3, 1, vehicles, 21));
            }
            catch (Exception ex)
            {
                Utilities.ErrorBox(ex.Message);
            }
            try
            {
                FactoryBL.Instance.AddTester(new Tester("314569245", new FullName("Margo", "Smith"), new DateTime(1972, 3, 22), Gender.Female, new PhoneNumber("0543846759", KindOfPhoneNumber.Mobile), new Address("HaErez", 8, "Jerusalem"), 4, 1, vehicles, 21));
            }
            catch (Exception ex)
            {
                Utilities.ErrorBox(ex.Message);
            }
            int[] classArr = { 22, 8, 12, 0, 24, 0, 0, 0 };
            try
            {
                FactoryBL.Instance.AddTrainee(new Trainee("015692486", new FullName("Naama", "Vazanna"), new DateTime(1990, 7, 23), Gender.Female, new PhoneNumber("086423751", KindOfPhoneNumber.Home), new Address("HaTamar", 19, "Jerusalem"), new VehicleParams(Vehicle.PrivateVehicle, GearBox.Automatic), "Horen Et Yosef", new FullName("Eli", "Jones"), classArr));
            }
            catch (Exception ex)
            {
                Utilities.ErrorBox(ex.Message);
            }
            try
            {
                FactoryBL.Instance.AddTrainee(new Trainee("234910768", new FullName("Moti", "Yaakovi"), new DateTime(1994, 8, 15), Gender.Female, new PhoneNumber("0506249371", KindOfPhoneNumber.Mobile), new Address("HaTnufa", 17, "Jerusalem"), new VehicleParams(Vehicle.PrivateVehicle, GearBox.Automatic), "Horen Et Yosef", new FullName("Adina", "Margi"), classArr));
            }
            catch (Exception ex)
            {
                Utilities.ErrorBox(ex.Message);
            }
            #endregion
            MainWindowStack.Children.Add(new openingWindow());
            
        }
        DispatcherTimer dispatcherTimer;
        void dispatcherTimer_Tick_Hour(object sender, EventArgs e)
        {
            if(DateTime.Now.Hour == 0)
            {
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick_Day);
                dispatcherTimer.Tick -= dispatcherTimer_Tick_Hour;
                dispatcherTimer.Interval = new TimeSpan(1, 0, 0);
            }
        }
        void dispatcherTimer_Tick_Day(object sender, EventArgs e)
        {
            if(DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick_Week);
                dispatcherTimer.Tick -= dispatcherTimer_Tick_Day;
                dispatcherTimer.Interval = new TimeSpan(7, 0, 0);
            }
        }
        void dispatcherTimer_Tick_Week(object sender, EventArgs e)
        {
            List<Tester> testers = Utilities.ReturnTesters();
            foreach (var tester in testers)
            {
                FactoryBL.Instance.RemoveFirstWeek(tester);
            }
        }
    }
}
