using System;
using System.Collections.Generic;

#nullable disable

namespace PQAASys
{
    public partial class Characteristic
    {
        //public Characteristic()
        //{
        //    Tests = new HashSet<Test>();
        //}

        public int CharacteristicId { get; set; }
        public string CharacteristicName { get; set; }
        public string Units { get; set; }
        public int Test { get; set; }

        //public virtual ICollection<Test> Tests { get; set; }
        public virtual Test TestNavigation { get; set; }
    }
}
