using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace PQAASys
{
    public partial class Standart
    {
        [Key]
        public int Standart_id { get; set; }
        public string StandartSeries { get; set; }
   
        public string StandartNumber { get; set; }
        public string StandartName { get; set; }
        public string YearOfIssue { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public Standart()
        {
            Requests = new HashSet<Request>();
        }
    }
}
