using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Upazila
    {
        public Upazila()
        {
            this.Addresses = new List<Address>();
            this.Addresses1 = new List<Address>();
            this.Agents = new List<Agent>();
            this.Agents1 = new List<Agent>();
            this.Complains = new List<Complain>();
            this.Customers = new List<Customer>();
            this.Customers1 = new List<Customer>();
            this.EmployeeInfoes = new List<EmployeeInfo>();
            this.EmployeeInfoes1 = new List<EmployeeInfo>();
        }

        public Nullable<int> DistrictID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Address> Addresses1 { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<Agent> Agents1 { get; set; }
        public virtual ICollection<Complain> Complains { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Customer> Customers1 { get; set; }
        public virtual District District { get; set; }
        public virtual ICollection<EmployeeInfo> EmployeeInfoes { get; set; }
        public virtual ICollection<EmployeeInfo> EmployeeInfoes1 { get; set; }
    }
}
