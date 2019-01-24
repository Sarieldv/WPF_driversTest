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
        public bool[,] MyWeekHours
        {
            get;/*=> MyWeekHours;*/
            set;
            //{
            //    MyWeekHours = value;
            //    string str = "";
            //    for (int i = 0; i < 5; i++)
            //    {
            //        for (int j = 0; j < 6; j++)
            //        {
            //            if(MyWeekHours[i,j])
            //            {
            //                str += 1;
            //            }
            //            else
            //            {
            //                str += 0;
            //            }
            //            str += ",";
            //        }
            //        str+=".";
            //    }
            //    if (WeeklyWorkHoursString!=str)
            //    {
            //        WeeklyWorkHoursString = str;
            //    }
                
            //}
        }
        public string WeeklyWorkHoursString
        {
            get => WeeklyWorkHoursString;
            set
            {
                WeeklyWorkHoursString = value;
                bool[,] arr = new bool[5, 6];
                int i = 0;
                int j = 0;
                for (int n = 0; n < WeeklyWorkHoursString.Length; n++)
                {
                    if (WeeklyWorkHoursString[n] == '1')
                    {
                        arr[i, j] = true;
                    }
                    else if(WeeklyWorkHoursString[n] == '0')
                    {
                        arr[i, j] = false;
                    }
                    else if(WeeklyWorkHoursString[n] == ',')
                    {
                        j++;
                    }
                    else if(WeeklyWorkHoursString[n] == '.')
                    {
                        i++;
                    }
                }
                for (i = 0; i < 5; i++)
                {
                    for (j = 0; j < 6; j++)
                    {
                        if(arr[i,j] != MyWeekHours[i,j])
                        {
                            MyWeekHours = arr;
                            return;
                        }
                    }
                }

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
            MyWeekHours = new bool[5, 6];
        }
    }
}
