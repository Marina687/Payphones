using System;
using System.Collections.Generic;
using System.Text;

namespace MoscowPayphones.DomainObjects
{
    public class Payphones : DomainObject 
    {

        public string DescriptionLocation { get; set; }

        public string PayWay { get; set; }

        public string IntercityConnectionPayment { get; set; }

        public string Name { get; set; }

        public string ValidUniversalServicesCard { get; set; }
        
    }
}
