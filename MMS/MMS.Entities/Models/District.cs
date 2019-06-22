using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class District
    {
        public District()
        {
            this.Agents = new List<Agent>();
            this.Companies = new List<Company>();
            this.Complains = new List<Complain>();
            this.Customers = new List<Customer>();
            this.EmployeeInfoes = new List<EmployeeInfo>();
            this.Upazilas = new List<Upazila>();
        }

        public Nullable<int> DivisionID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Complain> Complains { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual Division Division { get; set; }
        public virtual ICollection<EmployeeInfo> EmployeeInfoes { get; set; }
        public virtual ICollection<Upazila> Upazilas { get; set; }
    }
}
