using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class VendorType
    {
        public VendorType()
        {
            this.VendorInfoes = new List<VendorInfo>();
        }

        public int VendorTypeId { get; set; }
        public string VendorTypeName { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<VendorInfo> VendorInfoes { get; set; }
    }
}
