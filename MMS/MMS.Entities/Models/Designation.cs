using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Designation
    {
        public Designation()
        {
            this.EmployeeInfoes = new List<EmployeeInfo>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<EmployeeInfo> EmployeeInfoes { get; set; }
    }
}
