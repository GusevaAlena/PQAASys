using System;
using System.Collections.Generic;

#nullable disable

namespace PQAASys
{
    public partial class Organization
    {
        public Organization()
        {
            Requests = new HashSet<Request>();
        }

        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationAddress { get; set; }
        public string OrganizationPhonenum { get; set; }
        public string OrganizationDescription { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
