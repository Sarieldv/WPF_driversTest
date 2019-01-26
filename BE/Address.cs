using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Address
    {
        public string StreetName { get; set; }
        public int AddressNumber { get; set; }
        public string CityName { get; set; }
        public Address()
        {

        }
        public Address(string _streetName, int _addressNumber, string _cityName)
        {
            StreetName = _streetName;
            AddressNumber = _addressNumber;
            CityName = _cityName;
        }
        public override string ToString()
        {
            return "City: " + CityName + "    Street: " + StreetName + "    Address Number: " + AddressNumber;
        }
    }
}
