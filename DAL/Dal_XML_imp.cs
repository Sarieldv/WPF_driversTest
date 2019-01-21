using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BE;

namespace DAL
{
    class Dal_XML_imp : IDAL
    {
        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }

        public static T LoadFromXML<T>(string path) {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(file); file.Close();
            return result;
        }
        public void AddAnotherWeek(Tester tester)
        {
            throw new NotImplementedException();
        }

        public void AddTest(Test NewTest)
        {
            throw new NotImplementedException();
        }

        public void AddTester(Tester NewTester)
        {
            throw new NotImplementedException();
        }

        public void AddTrainee(Trainee NewTrainee)
        {
            throw new NotImplementedException();
        }

        public void CancelTest(Test _test)
        {
            throw new NotImplementedException();
        }

        public void EraseTester(Tester _tester)
        {
            throw new NotImplementedException();
        }

        public void EraseTrainee(Trainee _trainee)
        {
            throw new NotImplementedException();
        }

        public void RemoveFirstWeek(Tester tester)
        {
            throw new NotImplementedException();
        }

        public List<Tester> ReturnTesters()
        {
            throw new NotImplementedException();
        }

        public List<Test> ReturnTests()
        {
            throw new NotImplementedException();
        }

        public List<Trainee> ReturnTrainees()
        {
            throw new NotImplementedException();
        }

        public void UpdateTest(Test updatedTest)
        {
            throw new NotImplementedException();
        }

        public void UpdateTester(Tester updatedTester)
        {
            throw new NotImplementedException();
        }

        public void UpdateTrainee(Trainee updatedTrainee)
        {
            throw new NotImplementedException();
        }
    }
}
