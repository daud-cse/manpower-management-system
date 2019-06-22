using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_GLAccountType
    {
        public int TypeID { get; set; }
        public Nullable<int> MasterTypeID { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public Nullable<int> AppearInReportTypeID { get; set; }
        public string DrCr { get; set; }
        public virtual FMS_AccountingReportType FMS_AccountingReportType { get; set; }
        public virtual FMS_GLAccountMasterType FMS_GLAccountMasterType { get; set; }
    }
}
