using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class CustomerType
    {
        public CustomerType()
        {
            this.Customers = new List<Customer>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime SetDate { get; set; }
        public string SetBy { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
