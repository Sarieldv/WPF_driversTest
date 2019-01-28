using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BE
{
    public class Test
    {
        public int Number { get; set; }
        public VehicleParams TestVehicle { get; set; }
        public string TesterId { get; set; }
        public string TraineeId { get; set; }
        public Address AddressOfDeparture { get; set; }
        public bool? DistanceKeep { get; set; }
        public bool? ReverseParking { get; set; }
        public bool? Parking { get; set; }
        public bool? LookingAtMirrors { get; set; }
        public bool? Junction { get; set; }
        public bool? Reversing { get; set; }
        public bool? Roundabout { get; set; }
        public bool? Overtaking { get; set; }
        public bool? Turning { get; set; }
        public string TesterNote { get; set; }
        public DateTime DateAndTime { get; set; }
        [XmlIgnore]
        public bool? Grade
        {
            get=>PrivateGrade;
            set
            {
                PrivateGrade = value;
                if (value == null)
                {
                    PrivateGradeAccessor = 0;
                }
                if (value == true)
                {
                    PrivateGradeAccessor = 1;
                }
                if (value == false)
                {
                    PrivateGradeAccessor = 2;
                }
            }
        }
        private bool? PrivateGrade { get; set; }
        private int PrivateGradeAccessor { get; set; }
        public int GradeAccessor
        {
            get => PrivateGradeAccessor;
            set
            {
                PrivateGradeAccessor = value;
                if(value == 0)
                {
                    PrivateGrade = null;
                }
                if(value == 1)
                {
                    PrivateGrade = true;
                }
                if(value == 2)
                {
                    PrivateGrade = false;
                }
            }
        }
        public Test(string _testerId, VehicleParams vehicle, string _traineeId, Address _addressOfDeparture, DateTime _dateAndTime)
        {
            Number = Configuration.TestId;
            TestVehicle = vehicle;
            TesterId = _testerId;
            TraineeId = _traineeId;
            AddressOfDeparture = _addressOfDeparture;
            DateAndTime = _dateAndTime;
            DistanceKeep = null;
            ReverseParking = null;
            Parking = null;
            LookingAtMirrors = null;
            Junction = null;
            Reversing = null;
            Roundabout = null;
            Overtaking = null;
            Turning = null;
            TesterNote = "";
            Grade = null;
        }
        public Test(Test t, bool _distanceKeep, bool _reverseParking, bool _parking, bool _lookingAtMirrors, bool _junction, bool _reversing, bool _roundabout, bool _overtaking, bool _turning, string _testerNote, bool _grade)
        {
            Number = t.Number;
            TesterId = t.TesterId;
            TraineeId = t.TraineeId;
            AddressOfDeparture = t.AddressOfDeparture;
            DateAndTime = t.DateAndTime;
            DistanceKeep = _distanceKeep;
            ReverseParking = _reverseParking;
            Parking = _parking;
            LookingAtMirrors = _lookingAtMirrors;
            Junction = _junction;
            Reversing = _reversing;
            Roundabout = _roundabout;
            Overtaking = _overtaking;
            Turning = _turning;
            TesterNote = _testerNote;
            Grade = _grade;
        }
        public Test()
        {

        }

        public override string ToString()
        {
            return "Number: " + Number + " TesterId: " + TesterId + " TraineeId: " + TraineeId + " Date: " + DateAndTime;
        }
    }
}
