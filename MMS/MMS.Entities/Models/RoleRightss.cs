using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class RoleRightss
    {
        public int ID { get; set; }
        public int RoleId { get; set; }
        public int RightId { get; set; }
        public string SetBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsCreate { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsList { get; set; }
        public bool IsDetails { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Rightss Rightss { get; set; }
        public virtual Role Role { get; set; }
    }
}
