using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class Tester : Person
    {
        public int YearsOfExperience       { get; set;}
        public int MaximumWeeklyTests      { get; set;}
        public List <VehicleParams> MyVehicles     { get; set;}
        private WeeklyWorkHours[] PrivateMyWorkHours;
        [XmlIgnore]
        public WeeklyWorkHours[] MyWorkHours
        {
            get => PrivateMyWorkHours;
            set
            {
                //PrivateMyWorkHours = value;
                PrivateMyWorkHours = new WeeklyWorkHours[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    PrivateMyWorkHours[i] = value[i];
                }
                string str = "";
                string temp;
                foreach (var item in value)
                {
                    temp = item.WeeklyWorkHoursString;
                    str += temp;
                }
                PrivateMyWorkHoursString = str;
            }
        }
        private string PrivateMyWorkHoursString;
        public string MyWorkHoursString
        {
            get => PrivateMyWorkHoursString;
            set
            {
                PrivateMyWorkHoursString = value;
                WeeklyWorkHours[] arr = new WeeklyWorkHours[value.Length / 30];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = new WeeklyWorkHours();
                }
                for (int i = 0; i < arr.Length; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        for (int n = 0; n < 6; n++)
                        {
                            if (MyWorkHoursString[30 * i + 6 * j + n] == '1')
                            {
                                arr[i][j, n] = true;
                            }
                            else if (MyWorkHoursString[30 * i + 6 * j + n] == '0')
                            {
                                arr[i][j, n] = false;
                            }
                        }
                    }
                }
                PrivateMyWorkHours = arr;
            }
        }
        public int MaxDistanceFromTest     { get; set;}

        private int _testsSignedUpFor;
        public int TestsSignedUpFor
        {
            get => _testsSignedUpFor;
            set
            {
                if (value > 0)
                {
                    _testsSignedUpFor++;
                }
                if (value < 0)
                {
                    _testsSignedUpFor--;
                }
            }
        }
        public Tester()
        {
            
        }
        public Tester(string _ID, FullName _name, DateTime _birthDate, Gender _gender,PhoneNumber _phoneNumber, Address _address, int _yearsOfExperience, int _maximumWeeklyTests, List<VehicleParams> _myVehicles, WeeklyWorkHours[] _myWorkHours, int _maximumDistance)
        {
            IDNumber = _ID;
            Name = _name;
            BirthDate = _birthDate;
            MyGender = _gender;
            MyPhoneNumber = _phoneNumber;
            MyAddress = _address;
            YearsOfExperience = _yearsOfExperience;
            MaximumWeeklyTests = _maximumWeeklyTests;
            MyVehicles = _myVehicles;
            MyWorkHours = _myWorkHours;

            MaxDistanceFromTest = _maximumDistance;
        }
        public Tester(string _ID, FullName _name, DateTime _birthDate, Gender _gender, PhoneNumber _phoneNumber, Address _address, int _yearsOfExperience, int _maximumWeeklyTests, List<VehicleParams> _myVehicles, int _maximumDistance)
        {
            IDNumber = _ID;
            Name = _name;
            BirthDate = _birthDate;
            MyGender = _gender;
            MyPhoneNumber = _phoneNumber;
            MyAddress = _address;
            YearsOfExperience = _yearsOfExperience;
            MaximumWeeklyTests = _maximumWeeklyTests;
            MyVehicles = _myVehicles;
            PrivateMyWorkHours = new WeeklyWorkHours[1];
            PrivateMyWorkHours[0] = new WeeklyWorkHours();
            PrivateMyWorkHoursString = "000000000000000000000000000000";
            MaxDistanceFromTest = _maximumDistance;
        }
        public bool hasVehicle(VehicleParams vehicle)
        {
            foreach (var v in MyVehicles)
            {
                if(v.GearBoxType == vehicle.GearBoxType && v.VehicleType == vehicle.VehicleType)
                {
                    return true;
                }
            }
            return false;
        }
        public int getWeek(DateTime dateTime)
        {
            int weeks = (dateTime - DateTime.Now).Days - 6 + DateTime.Now.DayOfWeek -dateTime.DayOfWeek;
            weeks /= 7;
            return weeks + 1;
        }
        public bool hasTestByDate(DateTime dateTime)
        {
            if(MyWorkHours.Length <= getWeek(dateTime))
            {
                return false;
            }
            if(MyWorkHours[getWeek(dateTime)][dateTime])
            {
                return true;
            }
            return false;
        }
    }
}
