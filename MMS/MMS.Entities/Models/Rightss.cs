using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Rightss
    {
        public Rightss()
        {
            this.RoleRightsses = new List<RoleRightss>();
        }

        public int ID { get; set; }
        public string RightId { get; set; }
        public int ModuleId { get; set; }
        public string RightsName { get; set; }
        public bool IsActive { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Module Module { get; set; }
        public virtual ICollection<RoleRightss> RoleRightsses { get; set; }
    }
}
