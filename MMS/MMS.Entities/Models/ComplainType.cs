using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class ComplainType
    {
        public ComplainType()
        {
            this.Complains = new List<Complain>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public string SetBy { get; set; }
        public bool IsActive { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<Complain> Complains { get; set; }
    }
}
