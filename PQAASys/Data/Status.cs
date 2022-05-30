﻿using System;
using System.Collections.Generic;

#nullable disable

namespace PQAASys
{
    public partial class Status
    {
        public Status()
        {
            Requests = new HashSet<Request>();
        }

        public int StatusId { get; set; }
        public string Status1 { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
