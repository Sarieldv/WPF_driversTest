using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PhoneNumber
    {
        public readonly string number;
        public readonly KindOfPhoneNumber kind;
       public PhoneNumber(string _number, KindOfPhoneNumber _kind)
       {
            number = _number;
            kind = _kind;
       }
        public PhoneNumber(PhoneNumber phone)
        {
            this.kind = phone.kind;
            this.number = phone.number;
        }
    }
}
