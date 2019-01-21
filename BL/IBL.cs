using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BL
{
    public interface IBL
    {
        void AddTester(Tester NewTester);
    
        void EraseTester(Tester _tester);
        
        void UpdateTester(Tester updatedTester);
       
        void AddAnotherWeek(Tester tester);
        
        void RemoveFirstWeek(Tester tester);

        
        void AddTrainee(Trainee NewTrainee);
        
        void EraseTrainee(Trainee _trainee);
       
        void UpdateTrainee(Trainee updatedTrainee);
        
        void GetTest(Trainee trainee, DateTime dateTime);
        
        void AddTest(Test NewTest);
        
        void CancelTest(Test _test);
        
        void UpdateTest(Test updatedTest);

        List<Tester> ReturnTesters();
        
        List<Trainee> ReturnTrainees();
       
        List<Test> ReturnTests();

       
        List<Tester> TestersByDistance(int _distance, Address address);
        
        List<Tester> TestersBusyByTime(DateTime _dateTime);
        
        List<Tester> TestersFreeByTime(DateTime dateTime);
        
        List<Tester> TestersByCity(Address address);
        
        List<Tester> TestersBySpecialty(VehicleParams vehicle);
        List<Test>TestsByCondition (Func<Test, bool> _condition);
        Address BestTestAddress(Tester _tester, Trainee _trainee);
        int TestDistance(Tester _tester, Trainee _trainee);
        bool IsDateAvailable(Tester tester, DateTime dateTime);

        int TestsDone(Trainee _trainee);
        bool CanDrive(Trainee _trainee, VehicleParams vehicle);

        List<Test> TestsByDay(DateTime _dateTime);
        List<Test> TestsByMonth(DateTime _dateTime);
       
        int CalcDistance(Address address1, Address address2);

        List<List<List<Tester>>> TestersGroupedBySpecialty(bool _extraSorted);
        IEnumerable<List<Trainee>> TraineesGroupedBySchool(bool _extraSorted);
        IEnumerable<List<Trainee>> TraineesGroupedByTeacher(bool _extraSorted);
        IEnumerable<List<Trainee>> TraineesGroupedByTestAmount(bool _extraSorted);
    }
}
