using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Role
    {
        public Role()
        {
            this.RoleRightsses = new List<RoleRightss>();
            this.Users = new List<User>();
        }

        public int RoleId { get; set; }
        public int ModuleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime SetDate { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Module Module { get; set; }
        public virtual ICollection<RoleRightss> RoleRightsses { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
