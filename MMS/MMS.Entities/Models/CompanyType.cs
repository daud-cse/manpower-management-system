using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class CompanyType
    {
        public CompanyType()
        {
            this.Companies = new List<Company>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}
