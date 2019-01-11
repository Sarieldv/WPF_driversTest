using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TesterAndDate
    {
        public readonly Tester myTester;
        public readonly DateTime myDate;
        public TesterAndDate(Tester _tester, DateTime _date)
        {
            myTester = _tester;
            myDate = _date;
        }
    }
}
