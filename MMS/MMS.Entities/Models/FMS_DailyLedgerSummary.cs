using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_DailyLedgerSummary
    {
        public int GLAccountId { get; set; }
        public System.DateTime SummaryDate { get; set; }
        public Nullable<decimal> DrAmount { get; set; }
        public Nullable<decimal> CrAmount { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public int GlobalCompanyId { get; set; }
    }
}
