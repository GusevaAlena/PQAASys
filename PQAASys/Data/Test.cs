using System;
using System.Collections.Generic;

#nullable disable

namespace PQAASys
{
    public partial class Test
    {
        public Test()
        {
            Requests = new HashSet<Request>();
            Characteristics = new HashSet<Characteristic>();
        }


        public int TestId { get; set; }
        public string TestName { get; set; }
        //public int DetermineCharacteristic { get; set; }

        //public virtual Characteristic DetermineCharacteristicNavigation { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Characteristic> Characteristics { get; set; }
    }
}
