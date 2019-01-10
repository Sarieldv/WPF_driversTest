using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DAL
{
    public interface IDAL
    {
        void AddTester(Tester NewTester );
        void EraseTester(Tester _tester);
        void UpdateTester(Tester updatedTester);
        void AddAnotherWeek(Tester tester);
        void RemoveFirstWeek(Tester tester);

        void AddTrainee(Trainee NewTrainee);
        void EraseTrainee(Trainee _trainee);
        void UpdateTrainee(Trainee updatedTrainee);
        

        void AddTest(Test NewTest);
        void CancelTest(Test _test);
        void UpdateTest(Test updatedTest);
        
        List<Tester> ReturnTesters();
        List<Trainee> ReturnTrainees();
        List<Test> ReturnTests();
    }
}
