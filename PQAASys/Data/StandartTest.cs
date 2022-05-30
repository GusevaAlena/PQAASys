using System;
using System.Collections.Generic;

#nullable disable

namespace PQAASys
{
    public partial class StandartTest
    {
        public int Test { get; set; }
        public int Standart { get; set; }

        public virtual Standart StandartNavigation { get; set; }
        public virtual Test TestNavigation { get; set; }
    }
}
