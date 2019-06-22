using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_GLAccountMasterType
    {
        public FMS_GLAccountMasterType()
        {
            this.FMS_GLAccountType = new List<FMS_GLAccountType>();
        }

        public int MasterTypeID { get; set; }
        public string MasterTypeName { get; set; }
        public string Description { get; set; }
        public string DrCr { get; set; }
        public virtual ICollection<FMS_GLAccountType> FMS_GLAccountType { get; set; }
    }
}
