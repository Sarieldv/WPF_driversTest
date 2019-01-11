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
            var k = (from t in ReturnTests()
                     where t.TraineeId == trainee.IDNumber
                     select t);
            Test mostRecentTest = k.OrderByDescending(e => e.DateAndTime).FirstOrDefault();
            if (Configuration.TimeBetweenTests > (NewTest.DateAndTime - mostRecentTest.DateAndTime) && (mostRecentTest != null))
            {
                throw new Exception("Trainee had a test too recently.");
            }
            if ((from bool t in tester.MyWorkHours
                 where t
                 select t).Count() == tester.MaximumWeeklyTests)
            {
                throw new Exception("Tester can not test any more this week.");
            }
            if (!(tester.MyVehicles.Any(t => t == trainee.TraineeVehicle)))
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
            (tester.MyWorkHours[(int)((NewTest.DateAndTime.DayOfYear - DateTime.Now.DayOfYear) / 7)])[NewTest.DateAndTime] = true;
            tester.TestsSignedUpFor = 1;
            UpdateTester(tester);
            trainee.HaveTest = true;
            UpdateTrainee(trainee);
        }

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
                Sum += ((Temp % 10) * (2 ^ (i % 2))) / 10 + ((Temp % 10) * (2 ^ (i % 2))) % 10;
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

        public int CalcDistance(Address address1, Address address2)
        {
            Random r = new Random();
            return r.Next(0, 21);
        }

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
            (tester.MyWorkHours[(int)((_test.DateAndTime.DayOfYear - DateTime.Now.DayOfYear) / 7)])[_test.DateAndTime] = false;
            tester.TestsSignedUpFor = -1;
            UpdateTester(tester);
            trainee.HaveTest = false;
            UpdateTrainee(trainee);
        }

        public bool CanDrive(Trainee _trainee, VehicleParams vehicle)
        {
            return _trainee.PassedByVehicleParams[vehicle.Index()];
        }

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
            var k = (from t in ReturnTests()
                     where t.TraineeId == _trainee.IDNumber
                     select t);
            if (k != null)
            {
                CancelTest(k as Test);
            }
        }

        public void GetTest(Trainee trainee, DateTime dateTime)
        {
            if (trainee.HaveTest)
            {
                throw new Exception("Trainee already has a test.");
            }
            List<Tester> intersection = (List<Tester>)TestersBySpecialty(trainee.TraineeVehicle).Intersect(TestersFreeByTime(dateTime)).OrderBy(t => CalcDistance(t.MyAddress, trainee.MyAddress));
            //var k = (from t in intersection
            //         where t.MaxDistanceFromTest >= CalcDistance(t.MyAddress, trainee.MyAddress)
            //         select t).OrderByDescending(t=>CalcDistance(t.MyAddress, trainee.MyAddress));
            if(intersection.DefaultIfEmpty() == intersection)
            {
                throw new Exception("There are no testers that can test at that time with that kind of test.");
            }
            Tester tester = (from t in intersection
                             where CalcDistance(t.MyAddress, trainee.MyAddress) == CalcDistance(intersection.First().MyAddress, trainee.MyAddress)
                             select t).OrderByDescending(t => t.YearsOfExperience).First(); 
            AddTest(new Test(tester.IDNumber, trainee.TraineeVehicle, trainee.IDNumber, BestTestAddress(tester, trainee), dateTime));
        }
        public int TestDistance(Tester _tester, Trainee _trainee)
        {
            return CalcDistance(_trainee.MyAddress, BestTestAddress(_tester, _trainee));
        }
        public Address BestTestAddress(Tester _tester, Trainee _trainee)
        {
            if(CalcDistance(_tester.MyAddress, _trainee.MyAddress) <= _tester.MaxDistanceFromTest)
            {
                return _trainee.MyAddress;
            }
            //when real maps will be implemented this will return an address that is the closest to trainee that also is smaller then the maximum distance
            return _tester.MyAddress;
        }
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

        public List<Tester> TestersByDistance(int _distance, Address address)
        {
            var k = (from t in ReturnTesters()
                     where CalcDistance(address, t.MyAddress) >= _distance
                     select t);
            return k as List<Tester>;
        }

        public List<Tester> TestersBusyByTime(DateTime _dateTime)
        {
            var k = (from t in ReturnTesters()
                     where t.hasTestByDate(_dateTime)
                     select t);
            return k as List<Tester>;
        }

        public List<Tester> TestersByCity(Address address)
        {
            var k = (from t in ReturnTesters()
                     where t.MyAddress.CityName == address.CityName
                     select t);
            return k as List<Tester>;
        }

        public List<Tester> TestersBySpecialty(VehicleParams vehicle)
        {
            var k = (from t in ReturnTesters()
                     where t.hasVehicle(vehicle)
                     select t);
            return k as List<Tester>;
        }

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

        public List<Test> TestsByCondition(Func<Test, bool> _condition)
        {
            var k = (from t in ReturnTests()
                     where _condition(t)
                     select t);
            return k as List<Test>;
        }

        public List<Test> TestsByDay(DateTime _dateTime)
        {
            var k = (from t in TestsByMonth(_dateTime)
                     where t.DateAndTime.Day == _dateTime.Day
                     select t);
            return k as List<Test>;
        }
        public List<Test> TestsByMonth(DateTime _dateTime)
        {
            var k = (from t in ReturnTests()
                     where t.DateAndTime.Year == _dateTime.Year && t.DateAndTime.Month == _dateTime.Month
                     select t);
            return k as List<Test>;
        }
        public int TestsDone(Trainee _trainee)
        {
            return _trainee.AmountOfTests;
        }

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

            if (trainee.HaveTest)
            {
                throw new Exception("Trainee already has a test in the system.");
            }
            if (trainee.AmountOfClasses[updatedTest.TestVehicle.Index()] < Configuration.MinimumClasses)
            {
                throw new Exception("Not enough classes.");
            }
            var k = (from t in ReturnTests()
                     where t.TraineeId == trainee.IDNumber
                     select t);
            Test mostRecentTest = k.OrderByDescending(e => e.DateAndTime).FirstOrDefault();
            if (Configuration.TimeBetweenTests > (updatedTest.DateAndTime - mostRecentTest.DateAndTime) && (mostRecentTest != null))
            {
                throw new Exception("Trainee had a test too recently.");
            }
            if ((from bool t in tester.MyWorkHours
                 where t
                 select t).Count() == tester.MaximumWeeklyTests)
            {
                throw new Exception("Tester can not test any more this week.");
            }
            if (!(tester.MyVehicles.Any(t => t == trainee.TraineeVehicle)))
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

            Test originalTest = (from t in ReturnTests()
                                 where t.Number == updatedTest.Number
                                 select t).FirstOrDefault();
            if (updatedTest.TesterNote != null)
            {
                trainee.AmountOfTests = 1;
                trainee.PassedByVehicleParams[trainee.TraineeVehicle.Index()] = (bool)updatedTest.Grade;
                trainee.HaveTest = false;
               (tester.MyWorkHours[(int)((originalTest.DateAndTime.DayOfYear - DateTime.Now.DayOfYear) / 7)])[originalTest.DateAndTime] = false;

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
                catch(Exception ex)
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

        public void UpdateTester(Tester updatedTester)
        {
            Tester tester = (from t in ReturnTesters()
                             where t.IDNumber == updatedTester.IDNumber
                             select t).FirstOrDefault();
            var k = (from t in ReturnTests()
                     where t.TesterId == updatedTester.IDNumber
                     select t);
            if (tester == null)
            {
                throw new Exception("Tester does not exist.");
            }
            if (tester.TestsSignedUpFor > updatedTester.MaximumWeeklyTests)
            {
                throw new Exception("Tester is already signed up to more tests then will be possible. Please manually cancel those tests before changing the amount of tests possible.\n At least "+ (tester.TestsSignedUpFor - updatedTester.MaximumWeeklyTests).ToString() +" will need to be canceled to allow this action.");
            }
            if (tester.MyVehicles != updatedTester.MyVehicles && k.Any(t => updatedTester.hasVehicle(t.TestVehicle) == false))
            {
                throw new Exception("Tester is signed up to tests he will not be able to do because he will no longer specialize in the needed vehicle.\n Please manually cancel those tests before updating the tester.");
            }
            if ((tester.MaxDistanceFromTest > updatedTester.MaxDistanceFromTest || tester.MyAddress != updatedTester.MyAddress) && k.Any(t => CalcDistance(updatedTester.MyAddress, t.AddressOfDeparture) > updatedTester.MaxDistanceFromTest))
            {
                throw new Exception("Tester is signed up to tests he will not be able to do because his address will be too far from the test.\n Please manually cancel those tests before updating the tester.");
            }
            if (updatedTester.Age() > Configuration.MaximumTesterAge)
            {
                throw new Exception("Tester is too old.");
            }
            if (updatedTester.Age() < Configuration.MinimumTesterAge)
            {
                throw new Exception("Tester is too young.");
            }
            if (updatedTester.MyWorkHours != tester.MyWorkHours && k.Any(t => !updatedTester.hasTestByDate(t.DateAndTime)))
            {
                throw new Exception("Tester is signed up to Tests that need to be canceled in order to change his schedule.");
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

        public void UpdateTrainee(Trainee updatedTrainee)
        {
            Trainee trainee = (from t in ReturnTrainees()
                               where t.IDNumber == updatedTrainee.IDNumber
                               select t).FirstOrDefault();
            if (trainee == null)
            {
                throw new Exception("Trainee does not exist.");
            }
            if (updatedTrainee.Age() < Configuration.MinimumTraineeAge)
            {
                throw new Exception("Trainee is too young.");
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
                throw new Exception("Trainee has a test in the system.");
            }
            if (trainee.TraineeVehicle != updatedTrainee.TraineeVehicle && trainee.HaveTest)
            {
                throw new Exception("Trainee has a test in the system on a specific vehicle. In order to change the vehicle, please update the test.");
            }
            Test test = (from t in ReturnTests()
                         where t.TraineeId == trainee.IDNumber
                         select t).FirstOrDefault();
            if (test != null && updatedTrainee.PassedByVehicleParams[test.TestVehicle.Index()])
            {
                throw new Exception("Trainee has a test in the system that will become irrelevant.");
            }
            if (test != null && updatedTrainee.AmountOfClasses[test.TestVehicle.Index()] < Configuration.MinimumClasses)
            {
                throw new Exception("With this change, trainee will not have enough classes to do the test he has in the system.");
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

        public List<Tester> TestersFreeByTime(DateTime dateTime)
        {
            var k = (from t in ReturnTesters()
                     where !t.hasTestByDate(dateTime)
                     select t);
            return k as List<Tester>;
        }

        public List<TesterAndDate> GetAvailableDatesForTest(Trainee trainee)
        {
            if (trainee == null)
                return null;
            var k = TestersBySpecialty(trainee.TraineeVehicle).Intersect((from t in ReturnTesters()
                                                                          orderby CalcDistance(t.MyAddress, trainee.MyAddress)
                                                                          select t));
            List<TesterAndDate> returnList = new List<TesterAndDate>();
            foreach (var Tester in k)
            {
                foreach (var date in GetAvailableDates(Tester))
                {
                    returnList.Add(new TesterAndDate(Tester,date));
                }
            }
            return returnList;
        }

        public List<DateTime> GetAvailableDates(Tester tester)
        {
            List<DateTime> returnList = new List<DateTime>();
            DateTime date;
            for (int i = 0; i < tester.MyWorkHours.Length; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int h = 0; h < 6; h++)
                    {
                        date = new DateTime(DateTime.Now.AddDays((double)(7 * i  + j)).Year, DateTime.Now.AddDays((double)(7 * i + j)).Month, DateTime.Now.AddDays((double) (7 * i +j)).DayOfYear, h+9 , 0 , 0);
                        if(tester.MyWorkHours[i][date] == false)
                        {
                            returnList.Add(date);
                        }
                    }
                }
            }
            return returnList;        
        }
        //public void SendAMessage(string mySubject, string myBody, Person person)
        //{
        //    MailMessage mail = new MailMessage("you@yourcompany.com", "user@hotmail.com");
        //    SmtpClient client = new SmtpClient();
        //    client.Port = 25;
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    client.UseDefaultCredentials = false;
        //    client.Host = "smtp.gmail.com";
        //    mail.Subject = mySubject;
        //    mail.Body = myBody;
        //    client.Send(mail);
        //}
    }
}