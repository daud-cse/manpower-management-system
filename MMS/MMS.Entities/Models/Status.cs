using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Status
    {
        public Status()
        {
            this.Complains = new List<Complain>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Complain> Complains { get; set; }
    }
}
