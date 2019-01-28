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
        private bool[,] privateMyWeekHours;
        [XmlIgnore]
        public bool[,] MyWeekHours
        {
            get => privateMyWeekHours;
            set
            {
                privateMyWeekHours = value;
                string str = "";
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (MyWeekHours[i, j])
                        {
                            str += "1";
                        }
                        else
                        {
                            str += "0";
                        }
                    }
                }
                privateWeeklyWorkHoursString = str;

            }
        }
        private string privateWeeklyWorkHoursString;
        public string WeeklyWorkHoursString
        {
            get => privateWeeklyWorkHoursString;
            set
            {
                privateWeeklyWorkHoursString = value;
                bool[,] arr = new bool[5, 6];
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if(value[6*i + j]=='1')
                        {
                            arr[i, j] = true;
                        }
                        else
                        {
                            arr[i, j] = false;
                        }
                    }
                }
                privateMyWeekHours = arr;
            }
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
                 MyWeekHours[(int)dateTime.DayOfWeek, (dateTime.Hour - 9)] = value;
            }
        }
        [XmlIgnore]
        public bool this[int num1, int num2]
        {
            get
            {
                return MyWeekHours[num1, num2];
            }
            set
            {
                 MyWeekHours[num1, num2] = value;
            }
        }
        public WeeklyWorkHours()
        {
            privateMyWeekHours = new bool[5, 6];
            privateWeeklyWorkHoursString = "000000000000000000000000000000";//string started with 30 zeros (a week with no test)
        }
    }
}
