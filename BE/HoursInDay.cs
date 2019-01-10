using System.Collections;

namespace BE
{
    public class HoursInDay : IEnumerable
    {
        public bool[] Hours = new bool[6];
        public IEnumerator GetEnumerator()
        {
            return Hours.GetEnumerator();
        }
    }
}