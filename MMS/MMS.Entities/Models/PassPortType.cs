using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class PassPortType
    {
        public PassPortType()
        {
            this.PassportInfoes = new List<PassportInfo>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime SetDate { get; set; }
        public string SetBy { get; set; }
        public virtual ICollection<PassportInfo> PassportInfoes { get; set; }
    }
}
