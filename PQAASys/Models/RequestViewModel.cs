using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PQAASys.Models
{
    public class RequestViewModel
    {
        public Request request { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }
        public IEnumerable<SelectListItem> OrganizationList { get; set; }
        public IEnumerable<SelectListItem> TestList { get; set; }
        public IEnumerable<SelectListItem> ConditionList { get; set; }     
        public IEnumerable<SelectListItem> StandartList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
    }
}
