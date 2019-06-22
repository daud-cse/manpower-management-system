using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class CompanyContent
    {
        public int ID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> ContentID { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual Content Content { get; set; }
    }
}
