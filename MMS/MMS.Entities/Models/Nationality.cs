using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Nationality
    {
        public Nationality()
        {
            this.Agents = new List<Agent>();
            this.Companies = new List<Company>();
            this.EmployeeInfoes = new List<EmployeeInfo>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<EmployeeInfo> EmployeeInfoes { get; set; }
    }
}
