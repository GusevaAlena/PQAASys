using System;
using System.Collections.Generic;

#nullable disable

namespace PQAASys
{
    public partial class RoleUser
    {
        public int Role { get; set; }
        public int User { get; set; }

        public virtual Role RoleNavigation { get; set; }
        public virtual User UserNavigation { get; set; }
    }
}
