using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PhoneNumber
    {
        public string number { get; set; }
        public KindOfPhoneNumber kind { get; set; }
       public PhoneNumber(string _number, KindOfPhoneNumber _kind)
       {
            number = _number;
            kind = _kind;
       }
        public PhoneNumber()
        {

        }
        public PhoneNumber(PhoneNumber phone)
        {
            this.kind = phone.kind;
            this.number = phone.number;
        }
    }
}
