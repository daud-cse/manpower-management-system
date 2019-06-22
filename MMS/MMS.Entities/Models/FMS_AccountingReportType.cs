using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_AccountingReportType
    {
        public FMS_AccountingReportType()
        {
            this.FMS_GLAccount = new List<FMS_GLAccount>();
            this.FMS_GLAccountType = new List<FMS_GLAccountType>();
        }

        public int ReportTypeID { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<FMS_GLAccount> FMS_GLAccount { get; set; }
        public virtual ICollection<FMS_GLAccountType> FMS_GLAccountType { get; set; }
    }
}
