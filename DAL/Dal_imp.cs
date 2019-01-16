using System;
using System.Collections.Generic;
using System.Text;
using BE;
using DS;
using System.Linq;

namespace DAL
{
    internal class Dal_imp : IDAL
    {
        public void AddAnotherWeek(Tester tester)
        {
            if (!ReturnTesters().Any(t => t.IDNumber == tester.IDNumber))
            {
                throw new Exception("Tester does not exist");
            }
            WeeklyWorkHours[] temp = tester.MyWorkHours;
            tester.MyWorkHours = new WeeklyWorkHours[temp.Length + 1];
            for (int i = 0; i < temp.Length; i++)
            {
                tester.MyWorkHours[i] = temp[i];
            }
            tester.MyWorkHours[temp.Length + 1] = new WeeklyWorkHours();
            UpdateTester(tester);
        }
        public void RemoveFirstWeek(Tester tester)
        {
            if (!ReturnTesters().Any(t => t.IDNumber == tester.IDNumber))
            {
                throw new Exception("Tester does not exist");
            }
            WeeklyWorkHours[] temp = tester.MyWorkHours;
            tester.MyWorkHours = new WeeklyWorkHours[temp.Length - 1];
            for (int i = 1; i < temp.Length - 1; i++)
            {
                tester.MyWorkHours[i-1] = temp[i];
            }
            if(tester.MyWorkHours.Length == 0)
            {
                tester.MyWorkHours[1] = new WeeklyWorkHours();
            }
            UpdateTester(tester);
        }
        #region add
        public void AddTest(Test NewTest)
        {

            if (DataSource.TestsList.Any(t=>t.Number==NewTest.Number))
            {
                throw new Exception("New Test already exists.");
            }
            if(!DataSource.TraineesList.Any(t=>t.IDNumber==NewTest.TraineeId))
            {
                throw new Exception("Trainee does not exist.");
            }
            if(!DataSource.TestersList.Any(t=>t.IDNumber==NewTest.TesterId))
            {
                throw new Exception("Tester does not exist.");
            }
            DataSource.TestsList.Add(NewTest);


        }

        public void AddTester(Tester NewTester)
        {
            if (DataSource.TestersList.Any(t => t.IDNumber == NewTester.IDNumber) || DataSource.TraineesList.Any(t => t.IDNumber == NewTester.IDNumber))
            {
                throw new Exception("A person with this ID number already exists in the system.");
            }
            DataSource.TestersList.Add(NewTester);

        }

        public void AddTrainee(Trainee NewTrainee)
        {
            if (DataSource.TraineesList.Any(t => t.IDNumber == NewTrainee.IDNumber) || DataSource.TraineesList.Any(t => t.IDNumber == NewTrainee.IDNumber))
            {
                throw new Exception("A person with this ID number already exists in the system.");
            }
            DataSource.TraineesList.Add(NewTrainee);
        }
        #endregion
        #region erase
        public void CancelTest(Test _test)
        {
            if (!DataSource.TestsList.Any(t => _test.Number == t.Number))
            {
                throw new Exception("Test does not exist");
            }
            DataSource.TestsList.Remove(_test);
        }

        public void EraseTester(Tester tester)
        {
            if (!DataSource.TestersList.Any(t => tester.IDNumber == t.IDNumber))
            {
                throw new Exception("Tester does not exist.");
            }
            DataSource.TestersList.Remove(tester);
        }
        public void EraseTrainee(Trainee trainee)
        {
            if (!DataSource.TraineesList.Any(t => trainee.IDNumber == t.IDNumber))
            {
                throw new Exception("Trainee does not exist.");
            }
            DataSource.TraineesList.Remove(trainee);
        }

    
        #endregion
        #region returnFromDataSource
        public List<Tester> ReturnTesters()
        {
            if(DataSource.TestersList.FirstOrDefault()==null)
            {
                throw new Exception("There are no testers in the system.");
            }
            return DataSource.TestersList;
        }

        public List<Test> ReturnTests()
        {
            if (DataSource.TestsList.FirstOrDefault() == null)
            {
                throw new Exception("There are no tests in the system.");
            }
            return DataSource.TestsList;
        }

        public List<Trainee> ReturnTrainees()
        {
            if (DataSource.TraineesList.FirstOrDefault() == null)
            {
                throw new Exception("There are no trainees in the system.");
            }
            return DataSource.TraineesList;
        }
        #endregion
        #region update
        public void UpdateTest(Test updatedTest)

        {
            if (!DataSource.TraineesList.Any(t => t.IDNumber == updatedTest.TraineeId))
            {
                throw new Exception("Trainee does not exist.");
            }
            if (!DataSource.TestersList.Any(t => t.IDNumber == updatedTest.TesterId))
            {
                throw new Exception("Tester does not exist.");
            }
            var k = (from t in DataSource.TestsList
                     where updatedTest.Number == t.Number
                     select t).FirstOrDefault();
            if(k==null)
            {
                throw new Exception("Test does not exist.");
            }
            CancelTest(k);
            k = updatedTest.DeepClone();
            AddTest(k);
        }

        public void UpdateTester(Tester updatedTester)
        {
            var k = (from t in DataSource.TestersList
                     where t.IDNumber == updatedTester.IDNumber
                     select t).FirstOrDefault();
            if (k == null)
            {
                throw new Exception("Tester does not exist.");
            }
            EraseTester(k);
            k = updatedTester.DeepClone();
            AddTester(k);
        }

        public void UpdateTrainee(Trainee updatedTrainee)
        {
            var k = (from t in DataSource.TraineesList
                     where t.IDNumber == updatedTrainee.IDNumber
                     select t).FirstOrDefault();
            if (k == null)
            {
                throw new Exception("Trainee does not exist.");
            }
            EraseTrainee(k);
            k = updatedTrainee.DeepClone();
            AddTrainee(k);
        }
        #endregion
    }
}