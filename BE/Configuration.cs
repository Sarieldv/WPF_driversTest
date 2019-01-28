using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BE
{
    public  static class Configuration
    {
        private static int maxTesterAge;
        private static int testId;
        private static int minTesterAge;
        private static int minimumTraineeAge;
        private const string CONFIGFILE = "..\\..\\..\\DAL\\DataBase\\config.xml";
        private static XElement configRoot;
        public static int MaximumTesterAge
        {
            get { return maxTesterAge; }
            private set { maxTesterAge = value; }
        }
        public static int MinimumTesterAge
        {
            get { return minTesterAge; }
            private set { minTesterAge = value; }
        }


        public static int TestId
        {
            get
            {
                testId++;
                XElement element = (from e in configRoot.Elements()
                                    where e.Name == "number"
                                    select e).FirstOrDefault();
                element.Value = testId.ToString();
                configRoot.Save(CONFIGFILE);
                return testId;
            }
            private set { testId = value; }
        }
        public static int MinimumTraineeAge
        {
            get
            { return minimumTraineeAge; }
            private set { minimumTraineeAge = value; }
        }
        private static int minimumClasses;

        public static int MinimumClasses
        {
            get { return minimumClasses; }
            set { minimumClasses = value; }
        }

        private static TimeSpan timeBetweenTests;

        public static TimeSpan TimeBetweenTests
        {
            get { return timeBetweenTests; }
            set { timeBetweenTests = value; }
        }
        static Configuration()
        {
            MaximumTesterAge = 80;
            MinimumTesterAge = 40;
            try
            {
                configRoot = XElement.Load(CONFIGFILE);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
            try
            {
                TestId = int.Parse(configRoot.Element("number").Value);     
            }
            catch 
            {
                throw new Exception("File upload problem"); ;
            }
            
            //TestId = 10000000;
            MinimumTraineeAge = 18;
            MinimumClasses = 20;
            TimeBetweenTests=new TimeSpan(7,0,0,0);
            // here we are going to change all this by loading a "configuration.xml" file
            // in those variables
        }

      }
}
