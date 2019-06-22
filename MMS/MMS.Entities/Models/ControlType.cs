using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class ControlType
    {
        public ControlType()
        {
            this.LocationMapDetails = new List<LocationMapDetail>();
        }

        public int Id { get; set; }
        public string ControlTypeName { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> Setdate { get; set; }
        public string SetBy { get; set; }
        public virtual ICollection<LocationMapDetail> LocationMapDetails { get; set; }
    }
}
