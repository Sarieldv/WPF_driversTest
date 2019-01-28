using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Trainee : Person
    {
        public VehicleParams TraineeVehicle { get; set; }
        public string SchoolName { get; set; }
        public FullName Teacher { get; set; }
        private int _amountOfTests;
        public int AmountOfTests
        {
            get => _amountOfTests;
            set => _amountOfTests++;
        }
        public bool HaveTest { get; set; }
        public bool[] PassedByVehicleParams { get; set; }
        public int[] AmountOfClasses { get; set; }
        public Trainee()
        {
            PassedByVehicleParams = new bool[8];
            AmountOfClasses = new int[8];
        }
        public Trainee(string _ID, FullName _name, DateTime _birthDate, Gender _gender, PhoneNumber _phoneNumber, Address _address, VehicleParams _traineeVehicle, string _schoolName, FullName _teacher, int[] _amountOfClasses)
        {
            IDNumber = _ID;
            Name = _name;
            BirthDate = _birthDate;
            MyGender = _gender;
            MyPhoneNumber = _phoneNumber;
            MyAddress = _address;
            TraineeVehicle = _traineeVehicle;
            SchoolName = _schoolName;
            Teacher = _teacher;
            AmountOfClasses = _amountOfClasses;
        }


    }
}
