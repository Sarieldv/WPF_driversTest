using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class Person
    {
        public string IDNumber { get; set; }
        public FullName Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender MyGender { get; set; }
        public PhoneNumber MyPhoneNumber { get; set; }
        public Address MyAddress { get; set; }

        public int Age()
        {
            return (int)(DateTime.Now - BirthDate).TotalDays / 365;
        }
        public override string ToString()
        {
            return "Name: " + Name.ToString() + " ID: " + IDNumber;
        }
   
    }
}
