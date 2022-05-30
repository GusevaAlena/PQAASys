using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

#nullable disable

namespace PQAASys
{
    public partial class Request
    {
        [Key]     
        public int requestNumber { get; set; }
        public int innerCustomer { get; set; }
        public int? outerCustomer { get; set; }
        public int typeOfTest { get; set; }
        public int InfoAboutSamples { get; set; }
        public int InfoAboutConditions { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }
        public int Standart { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Today;
        public Request()
        {
            
            Status = 1;
        }

        public virtual Condition InfoAboutConditionsNavigation { get; set; }
        public virtual SampleSet InfoAboutSamplesNavigation { get; set; }
        public virtual User InnerCustomerNavigation { get; set; }
        public virtual Organization OuterCustomerNavigation { get; set; }
        [ForeignKey("Status")]
        public virtual Status StatusNavigation { get; set; }
        public virtual Test TypeOfTestNavigation { get; set; }
        public virtual Standart StandartNavigation { get; set; }
        
    }
}
