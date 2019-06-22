using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Country
    {
        public Country()
        {
            this.Addresses = new List<Address>();
            this.Agents = new List<Agent>();
            this.Applicants = new List<Applicant>();
            this.Companies = new List<Company>();
            this.Complains = new List<Complain>();
            this.Customers = new List<Customer>();
            this.EmployeeInfoes = new List<EmployeeInfo>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<Applicant> Applicants { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Complain> Complains { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<EmployeeInfo> EmployeeInfoes { get; set; }
    }
}
