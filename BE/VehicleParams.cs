using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class VehicleParams
    {
        public Vehicle VehicleType;
        public GearBox GearBoxType;
        public int Index()
        {
            if((int) GearBoxType==0)
            {
                return (int)VehicleType;
            }
            return (int)VehicleType + 4;
        }
        public VehicleParams(Vehicle vehicle, GearBox gearBox)
        {
            VehicleType = vehicle;
            GearBoxType = gearBox;
        }
        public VehicleParams()
        {

        }
 
    }
}
