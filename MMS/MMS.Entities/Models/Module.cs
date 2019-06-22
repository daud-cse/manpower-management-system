using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Module
    {
        public Module()
        {
            this.Rightsses = new List<Rightss>();
            this.Roles = new List<Role>();
            this.UserModules = new List<UserModule>();
        }

        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string RedirectURL { get; set; }
        public bool IsSecurityModule { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime SetDate { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<Rightss> Rightsses { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserModule> UserModules { get; set; }
    }
}
