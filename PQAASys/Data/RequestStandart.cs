using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace PQAASys
{
    public partial class RequestStandart
    {
        [Key,Column(Order=0)]
        public int RequestNumber { get; set; }
        [Key, Column(Order = 1)]
        public string StandartSeries { get; set; }
        [Key, Column(Order = 2)]
        public string StandartNumber { get; set; }

        public virtual Request RequestNumberNavigation { get; set; }
        public virtual Standart StandartNavigation { get; set; }
    }
}
