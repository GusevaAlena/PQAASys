using System;
using System.Collections.Generic;

#nullable disable

namespace PQAASys
{
    public partial class Condition
    {
        public Condition()
        {
            Requests = new HashSet<Request>();
        }

        public int ConditionId { get; set; }
        public int Temperature { get; set; }
        public string Environment { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
