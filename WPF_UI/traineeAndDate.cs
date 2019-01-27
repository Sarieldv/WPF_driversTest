using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace WPF_UI
{
    public class traineeAndDate
    {
        public readonly Trainee trainee;
        public readonly DateTime date;
        public traineeAndDate(Trainee _trainee, DateTime _date)
        {
            trainee = _trainee;
            date = _date;
        }
    }
}
