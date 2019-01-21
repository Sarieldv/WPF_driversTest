using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class WeeklyWorkHours : IEnumerable
    {
        [XmlIgnore]
        public bool[,] MyWeekHours = new bool[5,6];
        public string WeeklyWorkHoursString
        {
            get
            {
                foreach (var a in MyWeekHours)
                {
                    
                }
            }
            private set;
        }
        
        public IEnumerator GetEnumerator()
        {
            return MyWeekHours.GetEnumerator();
        }
        [XmlIgnore]
        public bool this[DateTime dateTime]
        {
            get
            {
                return MyWeekHours[(int)dateTime.DayOfWeek, (dateTime.Hour - 9)];
            }
            set
            {
                value = MyWeekHours[(int)dateTime.DayOfWeek, (dateTime.Hour - 9)];
            }
        }
    }
}
