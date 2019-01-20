using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using BE;
using DAL;
namespace BL
{
    internal class BL_imp : IBL
    {
        /// <summary>
        /// Funtion to add a test to the system
        /// </summary>
        /// <param name="NewTest">Test to add</param>
        public void AddTest(Test NewTest)
        {
            Tester tester = (from t in ReturnTesters()
                             where t.IDNumber == NewTest.TesterId
                             select t).FirstOrDefault();
            if (tester == null)
            {
                throw new Exception("Tester does not exist.");
            }
            if (tester.hasTestByDate(NewTest.DateAndTime))
            {
                throw new Exception("That time is not available.");
            }
            Trainee trainee = (from t in ReturnTrainees()
                               where t.IDNumber == NewTest.TraineeId
                               select t).FirstOrDefault();
            if (trainee == null)
            {
                throw new Exception("Trainee does not exist.");
            }
            if (trainee.HaveTest)
            {
                throw new Exception("Trainee already has a test in the system.");
            }
            if (trainee.AmountOfClasses[NewTest.TestVehicle.Index()] < Configuration.MinimumClasses)
            {
                throw new Exception("Not enough classes.");
            }
            Test mostRecentTest;

            try
            {
                var k = (from t in ReturnTests()
                         where t.TraineeId == trainee.IDNumber
                         select t);
                mostRecentTest = k.OrderByDescending(e => e.DateAndTime).FirstOrDefault();
            }
            catch
            {
                mostRecentTest = null;
            }
            if (mostRecentTest != null && mostRecentTest.Grade != null)
            {
                if (Configuration.TimeBetweenTests > (NewTest.DateAndTime - mostRecentTest.DateAndTime))
                {
                    throw new Exception("Trainee had a test too recently.");
                }
            }
            int count = 0;
            if (tester.MyWorkHours.Length > tester.getWeek(NewTest.DateAndTime))
            {
                foreach (bool d in tester.MyWorkHours[tester.getWeek(NewTest.DateAndTime)])
                {
                    if (d)
                    {
                        count++;
                    }

                }
            }
            if (count == tester.MaximumWeeklyTests)
            {
                throw new Exception("Tester cannot test any more this week.");
            }
            if (!(tester.MyVehicles.Any(t => t.GearBoxType == trainee.TraineeVehicle.GearBoxType && t.VehicleType == trainee.TraineeVehicle.VehicleType)))
            {
                throw new Exception("Mismatch between trainee and tester");
            }
            if (CanDrive(trainee, NewTest.TestVehicle))
            {
                throw new Exception("Trainee already passed this test.");
            }
            if (CalcDistance(tester.MyAddress, NewTest.AddressOfDeparture) > tester.MaxDistanceFromTest)
            {
                throw new Exception("Test is too far for tester.");
            }
            try
            {
                FactoryDAL.Instance.AddTest(NewTest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //if ((NewTest.DateAndTime.DayOfWeek - DateTime.Now.DayOfWeek + 1) >= 0)
            //if((NewTest.DateAndTime - DateTime.Now).Days % 7 > 0)
            //{
                (tester.MyWorkHours[tester.getWeek(NewTest.DateAndTime)])[NewTest.DateAndTime] = true;
            //}
            //else
            //{
            //    (tester.MyWorkHours[(NewTest.DateAndTime - DateTime.Now).Days / 7 - 1])[NewTest.DateAndTime] = true;
            //}
            tester.TestsSignedUpFor = 1;
            UpdateTester(tester);
            trainee.HaveTest = true;
            UpdateTrainee(trainee);
        }
        /// <summary>
        /// Function that adds a given tester to the system
        /// </summary>
        /// <param name="NewTester">Tester to add to the system</param>
        public void AddTester(Tester NewTester)
        {
            if (NewTester.Age() > Configuration.MaximumTesterAge)
            {
                throw new Exception("Tester is too old.");
            }
            if (NewTester.Age() < Configuration.MinimumTesterAge)
            {
                throw new Exception("Tester is too young.");
            }
            int Sum = 0;
            int Temp = int.Parse(NewTester.IDNumber) / 10;
            for (int i = 0; i < 8; i++)
            {
                Sum += (Temp % 10) * (Utilities.FuncForID(i)) / 10 + ((Temp % 10) * Utilities.FuncForID(i)) % 10;
                Temp /= 10;
            }
            Temp = (((Sum / 10) * 10 + 10) - Sum) % 10;
            if (Temp != int.Parse(NewTester.IDNumber) % 10)
            {
                throw new Exception("The ID number is not valid.");
            }
            try
            {
                FactoryDAL.Instance.AddTester(NewTester);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Function to add a trainee to the system
        /// </summary>
        /// <param name="NewTrainee">Trainee to add</param>
        public void AddTrainee(Trainee NewTrainee)
        {
            if (NewTrainee.Age() < Configuration.MinimumTraineeAge)
            {
                throw new Exception("Trainee is too young.");
            }
            int Sum = 0;
            int Temp = int.Parse(NewTrainee.IDNumber) / 10;
            for (int i = 0; i < 8; i++)
            {
                Sum += (Temp % 10) * (Utilities.FuncForID(i)) / 10 + ((Temp % 10) * Utilities.FuncForID(i)) % 10;
                Temp /= 10;
            }
            Temp = (((Sum / 10) * 10 + 10) - Sum) % 10;
            if (Temp != int.Parse(NewTrainee.IDNumber) % 10)
            {
                throw new Exception("The ID number is not valid.");
            }
            try
            {
                FactoryDAL.Instance.AddTrainee(NewTrainee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Function to calculate the distance between two addresses
        /// </summary>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <returns></returns>
        public int CalcDistance(Address address1, Address address2)
        {
            Random r = new Random();
            return r.Next(0, 21);
        }
        /// <summary>
        /// Function to cancel a test
        /// </summary>
        /// <param name="_test">Test to cancel</param>
        public void CancelTest(Test _test)
        {

            try
            {
                FactoryDAL.Instance.CancelTest(_test);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Tester tester = (from t in ReturnTesters()
                             where t.IDNumber == _test.TesterId
                             select t).FirstOrDefault();
            Trainee trainee = (from t in ReturnTrainees()
                               where t.IDNumber == _test.TraineeId
                               select t).FirstOrDefault();
            (tester.MyWorkHours[tester.getWeek(_test.DateAndTime)])[_test.DateAndTime] = false;
            tester.TestsSignedUpFor = -1;
            UpdateTester(tester);
            trainee.HaveTest = false;
            UpdateTrainee(trainee);
        }
        /// <summary>
        /// Function the returns true if a given trainee can drive a given type of vehicle
        /// </summary>
        /// <param name="_trainee"></param>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public bool CanDrive(Trainee _trainee, VehicleParams vehicle)
        {
            return _trainee.PassedByVehicleParams[vehicle.Index()];
        }
        /// <summary>
        /// Function that erases a given tester from the system
        /// </summary>
        /// <param name="_tester">Tester to erase from the system</param>
        public void EraseTester(Tester _tester)
        {
            try
            {
                FactoryDAL.Instance.EraseTester(_tester);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                ReturnTests();
            }
            catch
            {
                return;
            }
            var k = (from t in ReturnTests()
                     where t.TesterId == _tester.IDNumber
                     select t);
            if (k != null)
            {
                foreach (Test item in k)
                {
                    CancelTest(item);
                }
            }
        }
        /// <summary>
        /// Function to erase a trainee from the system
        /// </summary>
        /// <param name="_trainee">Trainee to erase</param>
        public void EraseTrainee(Trainee _trainee)
        {
            try
            {
                FactoryDAL.Instance.EraseTrainee(_trainee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                ReturnTests();
            }
            catch
            {
                return;
            }
            var k = (from t in ReturnTests()
                     where t.TraineeId == _trainee.IDNumber
                     select t);
            if (k != null)
            {
                CancelTest(k as Test);
            }
        }
        /// <summary>
        /// Function to get a test
        /// </summary>
        /// <param name="trainee">Trainee the test is gotten for</param>
        /// <param name="dateTime">Date of the requested test</param>
        public void GetTest(Trainee trainee, DateTime dateTime)
        {
            if (trainee.HaveTest)
            {
                throw new Exception("Trainee already has a test.");
            }
            List<Tester> options = new List<Tester>();
            options = TestersBySpecialty(trainee.TraineeVehicle);
            if (options.DefaultIfEmpty() == options)
            {
                throw new Exception("There are no testers that can test with the needed vehicle.");
            }
            foreach (var tester in options)
            {
                if (IsDateAvailable(tester, dateTime))
                {
                    AddTest(new Test(tester.IDNumber, trainee.TraineeVehicle, trainee.IDNumber, BestTestAddress(tester, trainee), dateTime));
                    return;
                }
            }
            throw new Exception("All testers are busy during the selected time.");
        }
        /// <summary>
        /// Function that returns true if a given date is available for a test
        /// </summary>
        /// <param name="tester"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public bool IsDateAvailable(Tester tester, DateTime dateTime)
        {
            //TimeSpan span = dateTime - DateTime.Now; //opperation does not count today
            int weeks = tester.getWeek(dateTime);
            //if (dateTime.DayOfWeek-DateTime.Now.DayOfWeek < 0)
            //{
            //    weeks++;
            //}
            if (weeks < tester.MyWorkHours.Length)
            {
                if (tester.MyWorkHours[weeks][dateTime])
                {
                    return false;
                }
                return true;
            }
            for (int i = 0; i <= weeks - tester.MyWorkHours.Length + 1; i++)
            {
                AddAnotherWeek(tester);
            }
            return true;
        }
        /// <summary>
        /// Function to determine the distance from the optimal address for the test
        /// </summary>
        /// <param name="_tester"></param>
        /// <param name="_trainee"></param>
        /// <returns></returns>
        public int TestDistance(Tester _tester, Trainee _trainee)
        {
            return CalcDistance(_trainee.MyAddress, BestTestAddress(_tester, _trainee));
        }
        /// <summary>
        /// Function to determine the optimal address for a test
        /// </summary>
        /// <param name="_tester"></param>
        /// <param name="_trainee"></param>
        /// <returns></returns>
        public Address BestTestAddress(Tester _tester, Trainee _trainee)
        {
            if (CalcDistance(_tester.MyAddress, _trainee.MyAddress) <= _tester.MaxDistanceFromTest)
            {
                return _trainee.MyAddress;
            }
            //when real maps will be implemented this will return an address that is the closest to trainee that also is smaller then the maximum distance
            return _tester.MyAddress;
        }

        /// <summary>
        /// Function to return the list of testers
        /// </summary>
        /// <returns></returns>
        public List<Tester> ReturnTesters()
        {
            try
            {
                return FactoryDAL.Instance.ReturnTesters();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Function to return the list of tests
        /// </summary>
        /// <returns></returns>
        public List<Test> ReturnTests()
        {
            try
            {
                return FactoryDAL.Instance.ReturnTests();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Function to return the ist of trainees
        /// </summary>
        /// <returns></returns>
        public List<Trainee> ReturnTrainees()
        {
            try
            {
                return FactoryDAL.Instance.ReturnTrainees();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Function that returns all the testers who are a certain distance from an address
        /// </summary>
        /// <param name="_distance"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public List<Tester> TestersByDistance(int _distance, Address address)
        {
            var k = (from t in ReturnTesters()
                     where CalcDistance(address, t.MyAddress) >= _distance
                     select t);
            return k as List<Tester>;
        }
        /// <summary>
        /// Function that returns all the testers that are busy at a given time
        /// </summary>
        /// <param name="_dateTime"></param>
        /// <returns></returns>
        public List<Tester> TestersBusyByTime(DateTime _dateTime)
        {
            var k = (from t in ReturnTesters()
                     where t.hasTestByDate(_dateTime)
                     select t);
            return k as List<Tester>;
        }
        /// <summary>
        /// Function the returns all the testers who are in the same city as the givan address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public List<Tester> TestersByCity(Address address)
        {
            var k = (from t in ReturnTesters()
                     where t.MyAddress.CityName == address.CityName
                     select t);
            return k as List<Tester>;
        }
        /// <summary>
        /// Function the returns all the testers who specialize in a given vehicle
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public List<Tester> TestersBySpecialty(VehicleParams vehicle)
        {
            List<Tester> k = new List<Tester>();
            foreach (var t in ReturnTesters())
            {
                if (t.hasVehicle(vehicle))
                {
                    k.Add(t);
                }
            }
            return k;
        }
        /// <summary>
        /// Function to group the testers by their specialty (by vehicle then gearbox)
        /// </summary>
        /// <param name="_extraSorted"></param>
        /// <returns></returns>
        public List<List<List<Tester>>> TestersGroupedBySpecialty(bool _extraSorted)
        {
            var k = (from t in ReturnTesters()
                     group t by t.MyVehicles.Count);
            var g = (from t in k
                     group t by t.OrderByDescending(a => a.MyVehicles));
            if (_extraSorted)
            {
                foreach (var item in g)
                {
                    item.OrderByDescending(t => t.OrderByDescending(a => a.Name));
                }
            }
            return g as List<List<List<Tester>>>;
        }
        /// <summary>
        /// Function that returns all tests that apply for a given condition
        /// </summary>
        /// <param name="_condition"></param>
        /// <returns></returns>
        public List<Test> TestsByCondition(Func<Test, bool> _condition)
        {
            var k = (from t in ReturnTests()
                     where _condition(t)
                     select t);
            return k as List<Test>;
        }
        /// <summary>
        /// Function that returns all the tests that are happenning on a given day
        /// </summary>
        /// <param name="_dateTime"></param>
        /// <returns></returns>
        public List<Test> TestsByDay(DateTime _dateTime)
        {
            var k = (from t in TestsByMonth(_dateTime)
                     where t.DateAndTime.Day == _dateTime.Day
                     select t);
            return k as List<Test>;
        }
        /// <summary>
        /// Function that returns all the tests happening during a given month
        /// </summary>
        /// <param name="_dateTime"></param>
        /// <returns></returns>
        public List<Test> TestsByMonth(DateTime _dateTime)
        {
            var k = (from t in ReturnTests()
                     where t.DateAndTime.Year == _dateTime.Year && t.DateAndTime.Month == _dateTime.Month
                     select t);
            return k as List<Test>;
        }
        /// <summary>
        /// Function that returns the numer of tests a trainee has done
        /// </summary>
        /// <param name="_trainee"></param>
        /// <returns></returns>
        public int TestsDone(Trainee _trainee)
        {
            return _trainee.AmountOfTests;
        }
        /// <summary>
        /// Returns all the trainees grouped by the school they learnt in
        /// </summary>
        /// <param name="_extraSorted"></param>
        /// <returns></returns>
        public List<List<Trainee>> TraineesGroupedBySchool(bool _extraSorted)
        {
            var k = (from t in ReturnTrainees()
                     group t by t.SchoolName);
            if (_extraSorted)
            {
                foreach (var item in k)
                {
                    item.OrderByDescending(t => t.Name);
                }
            }
            return k as List<List<Trainee>>;

        }
        /// <summary>
        /// Returns all the trainees grouped by teacher
        /// </summary>
        /// <param name="_extraSorted"></param>
        /// <returns></returns>
        public List<List<Trainee>> TraineesGroupedByTeacher(bool _extraSorted)
        {
            var k = (from t in ReturnTrainees()
                     group t by t.Teacher);
            if (_extraSorted)
            {
                foreach (var item in k)
                {
                    item.OrderByDescending(t => t.Name);
                }
            }
            return k as List<List<Trainee>>;
        }
        /// <summary>
        /// Returns all the trainees grouped by test amount
        /// </summary>
        /// <param name="_extraSorted"></param>
        /// <returns></returns>
        public List<List<Trainee>> TraineesGroupedByTestAmount(bool _extraSorted)
        {
            var k = (from t in ReturnTrainees()
                     group t by t.AmountOfTests);
            if (_extraSorted)
            {
                foreach (var item in k)
                {
                    item.OrderByDescending(t => t.Name);
                }
            }
            return k as List<List<Trainee>>;
        }
        /// <summary>
        /// Function to update a test
        /// </summary>
        /// <param name="updatedTest">Test to update</param>
        public void UpdateTest(Test updatedTest)
        {
            if (!ReturnTests().Any(t => t.Number == updatedTest.Number))
            {
                throw new Exception("Test does not exist");
            }
            if (!ReturnTesters().Any(t => t.IDNumber == updatedTest.TesterId))
            {
                throw new Exception("Tester does not exist.");
            }
            if (!ReturnTrainees().Any(t => t.IDNumber == updatedTest.TraineeId))
            {
                throw new Exception("Trainee does not exist.");
            }
            #region counting fails
            int CountofFailed = 0;
            if (!(bool)updatedTest.DistanceKeep)
                CountofFailed++;
            if (!(bool)updatedTest.ReverseParking)
                CountofFailed++;
            if (!(bool)updatedTest.Parking)
                CountofFailed++;
            if (!(bool)updatedTest.LookingAtMirrors)
                CountofFailed++;
            if (!(bool)updatedTest.Junction)
                CountofFailed++;
            if (!(bool)updatedTest.Reversing)
                CountofFailed++;
            if (!(bool)updatedTest.Roundabout)
                CountofFailed++;
            if (!(bool)updatedTest.Overtaking)
                CountofFailed++;
            if (!(bool)updatedTest.Turning)
                CountofFailed++;
            #endregion
            if (CountofFailed > 4 && (bool)updatedTest.Grade)
            {
                throw new Exception("Failed too many parameters to pass the test.");
            }

            Tester tester = (from t in ReturnTesters()
                             where t.IDNumber == updatedTest.TesterId
                             select t).FirstOrDefault();

            if (tester.hasTestByDate(updatedTest.DateAndTime))
            {
                throw new Exception("That time is not available.");
            }
            Trainee trainee = (from t in ReturnTrainees()
                               where t.IDNumber == updatedTest.TraineeId
                               select t).FirstOrDefault();
            if (trainee.AmountOfClasses[updatedTest.TestVehicle.Index()] < Configuration.MinimumClasses)
            {
                throw new Exception("Not enough classes.");
            }
            var k = (from t in ReturnTests()
                     where t.TraineeId == trainee.IDNumber
                     select t);
            Test mostRecentTest = k.OrderByDescending(e => e.DateAndTime).FirstOrDefault();//gives the most recent (verified)
            if (mostRecentTest != null && mostRecentTest.Number != updatedTest.Number)
            {
                if (Configuration.TimeBetweenTests > (updatedTest.DateAndTime - mostRecentTest.DateAndTime))
                {
                    throw new Exception("Trainee had a test too recently.");
                }
            }
            int count = 0;
            //int week = (updatedTest.DateAndTime - DateTime.Now).Days / 7;
            //if(updatedTest.DateAndTime.DayOfWeek < DateTime.Now.DayOfWeek)
            //{
            //    week++;
            //}
            foreach (bool d in tester.MyWorkHours[tester.getWeek(updatedTest.DateAndTime)])
            {
                if (d)
                {
                    count++;
                }

            }
            if (count == tester.MaximumWeeklyTests)
            {
                throw new Exception("Tester cannot test any more this week.");
            }
            if (!(tester.MyVehicles.Any(t => t.GearBoxType == trainee.TraineeVehicle.GearBoxType && t.VehicleType == trainee.TraineeVehicle.VehicleType)))
            {
                throw new Exception("Mismatch between trainee and tester");
            }
            if (CanDrive(trainee, trainee.TraineeVehicle))
            {
                throw new Exception("Trainee already passed this test.");
            }
            if (CalcDistance(tester.MyAddress, updatedTest.AddressOfDeparture) > tester.MaxDistanceFromTest)
            {
                throw new Exception("Test is too far for tester.");
            }

            if (updatedTest.Grade != null)
            {
                trainee.AmountOfTests = 1;
                trainee.PassedByVehicleParams[trainee.TraineeVehicle.Index()] = (bool)updatedTest.Grade;
                trainee.HaveTest = false;
                (tester.MyWorkHours[tester.getWeek(mostRecentTest.DateAndTime)])[mostRecentTest.DateAndTime] = false;

                try
                {
                    UpdateTrainee(trainee);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                try
                {
                    UpdateTester(tester);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            try
            {
                FactoryDAL.Instance.UpdateTest(updatedTest);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Function to update a Tester in the system
        /// </summary>
        /// <param name="updatedTester">Tester to be updated</param>
        public void UpdateTester(Tester updatedTester)
        {
            Tester tester = (from t in ReturnTesters()
                             where t.IDNumber == updatedTester.IDNumber
                             select t).FirstOrDefault();
            List<Test> testList = new List<Test>();
            try
            {
                testList = ReturnTests();
            }
            catch
            {
                testList = new List<Test>();
            }
            var k = (from t in testList
                     where t.TesterId == updatedTester.IDNumber
                     select t);
            if (tester == null)
            {
                throw new Exception("Tester does not exist.");
            }
            if (tester.TestsSignedUpFor > updatedTester.MaximumWeeklyTests)
            {
                throw new Exception(tester.Name.ToString() + " is already signed up to more tests then will be possible. Please manually cancel those tests before changing the amount of tests possible.\n At least " + (tester.TestsSignedUpFor - updatedTester.MaximumWeeklyTests).ToString() + " will need to be canceled to allow this action.");
            }
            if (tester.MyVehicles != updatedTester.MyVehicles && k.Any(t => updatedTester.hasVehicle(t.TestVehicle) == false))
            {
                throw new Exception(tester.Name.ToString() + " is signed up to tests he will not be able to do because he will no longer specialize in the needed vehicle.\n Please manually cancel those tests before updating the tester.");
            }
            if ((tester.MaxDistanceFromTest > updatedTester.MaxDistanceFromTest || tester.MyAddress != updatedTester.MyAddress) && k.Any(t => CalcDistance(updatedTester.MyAddress, t.AddressOfDeparture) > updatedTester.MaxDistanceFromTest))
            {
                throw new Exception(tester.Name.ToString() + " is signed up to tests he will not be able to do because his address will be too far from the test.\n Please manually cancel those tests before updating the tester.");
            }
            if (updatedTester.Age() > Configuration.MaximumTesterAge)
            {
                throw new Exception(tester.Name.ToString() + " is too old.");
            }
            if (updatedTester.Age() < Configuration.MinimumTesterAge)
            {
                throw new Exception(tester.Name.ToString() + " is too young.");
            }
            if (updatedTester.MyWorkHours != tester.MyWorkHours && k.Any(t => !updatedTester.hasTestByDate(t.DateAndTime)))
            {
                throw new Exception(tester.Name.ToString() + " is signed up to Tests that need to be canceled in order to change his schedule.");
            }
            try
            {
                FactoryDAL.Instance.UpdateTester(updatedTester);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Function to update a trainee in the system
        /// </summary>
        /// <param name="updatedTrainee">Trainee to update</param>
        public void UpdateTrainee(Trainee updatedTrainee)
        {
            Trainee trainee = (from t in ReturnTrainees()
                               where t.IDNumber == updatedTrainee.IDNumber
                               select t).FirstOrDefault();
            if (trainee == null)
            {
                throw new Exception(trainee.Name.ToString() + " does not exist.");
            }
            if (updatedTrainee.Age() < Configuration.MinimumTraineeAge)
            {
                throw new Exception(trainee.Name.ToString() + " is too young.");
            }
            int Sum = 0;
            int Temp = int.Parse(updatedTrainee.IDNumber) / 10;
            for (int i = 0; i < 8; i++)
            {
                Sum += ((Temp % 10) * (2 ^ (i % 2))) / 10 + ((Temp % 10) * (2 ^ (i % 2))) % 10;
                Temp /= 10;
            }
            Temp = (((Sum / 10) * 10 + 10) - Sum) % 10;
            if (Temp != int.Parse(updatedTrainee.IDNumber) % 10)
            {
                throw new Exception("The ID number is not valid.");
            }
            if (trainee.HaveTest != updatedTrainee.HaveTest)
            {
                throw new Exception(trainee.Name.ToString() + " has a test in the system.");
            }
            if (trainee.TraineeVehicle != updatedTrainee.TraineeVehicle && trainee.HaveTest)
            {
                throw new Exception(trainee.Name.ToString() + " has a test in the system on a specific vehicle. In order to change the vehicle, please update the test.");
            }
            List<Test> testList = new List<Test>();
            try
            {
                testList = ReturnTests();
            }
            catch
            {
                testList = new List<Test>();
            }
            Test test = (from t in testList
                         where t.TraineeId == trainee.IDNumber
                         select t).FirstOrDefault();
            if (test != null && updatedTrainee.AmountOfClasses[test.TestVehicle.Index()] < Configuration.MinimumClasses)
            {
                throw new Exception("With this change, " + trainee.Name.ToString() + " will not have enough classes to do the test he has in the system.");
            }
            try
            {
                FactoryDAL.Instance.UpdateTrainee(updatedTrainee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Function to add a week to a given testers work hours
        /// </summary>
        /// <param name="tester">Tester to add a week for</param>
        public void AddAnotherWeek(Tester tester)
        {
            try
            {
                FactoryDAL.Instance.AddAnotherWeek(tester);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Function that removes the first week of a given testers work hours
        /// </summary>
        /// <param name="tester">Tester the week is removed for</param>
        public void RemoveFirstWeek(Tester tester)
        {
            try
            {
                FactoryDAL.Instance.RemoveFirstWeek(tester);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Function that returns all the testers that are free at a given time
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public List<Tester> TestersFreeByTime(DateTime dateTime)
        {
            var k = (from t in ReturnTesters()
                     where !t.hasTestByDate(dateTime)
                     select t);
            return k as List<Tester>;
        }
    }
}