using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class Utilities
    {
        public static int FuncForID(int num)
        {
            if (num % 2 == 0)
            {
                return 2;
            }
            return 1;
        }
    }
}
