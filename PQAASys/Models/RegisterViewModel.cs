using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PQAASys.Models
{
    public class RegisterViewModel
    {
        public User User { get; set; }
        public IEnumerable<SelectListItem> PositionSelectList { get; set; }
        public IEnumerable<SelectListItem> DepartmentSelectList { get; set; }
      
    }
}
