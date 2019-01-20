using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class Utilities
    {
        /// <summary>
        /// Function that returns 2 ^ ( num % 2 + 1 )
        /// Is used while checking if id is valid
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
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
