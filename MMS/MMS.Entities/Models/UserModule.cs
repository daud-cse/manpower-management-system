using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class UserModule
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int ModuleId { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime Setdate { get; set; }
        public string SetBy { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Module Module { get; set; }
        public virtual User User { get; set; }
    }
}
