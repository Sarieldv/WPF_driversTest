using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class WeeklyWorkHours : IEnumerable
    {
        public bool[,] MyWeekHours = new bool[5,6];
        public IEnumerator GetEnumerator()
        {
            return MyWeekHours.GetEnumerator();
        }
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
