using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Tester : Person
    {
        public int YearsOfExperience       { get; set;}
        public int MaximumWeeklyTests      { get; set;}
        public List <VehicleParams> MyVehicles     { get; set;}
        public WeeklyWorkHours[] MyWorkHours { get; set;}
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
            MyWorkHours = new WeeklyWorkHours[1];
            MyWorkHours[0] = new WeeklyWorkHours();
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
            //if (MyVehicles.Any(t => t == vehicle))
            //    return true;
            return false;
        }
        public bool hasTestByDate(DateTime dateTime)
        {
            if(MyWorkHours.Length <= ((dateTime.DayOfYear - DateTime.Now.DayOfYear) / 7 + 1))
            {
                return false;
            }
            if(MyWorkHours[(int)((dateTime.DayOfYear - DateTime.Now.DayOfYear) / 7)][dateTime])
            {
                return true;
            }
            return false;
        }
    }
}
