using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_YearlyLedgerSummary
    {
        public int GLAccountId { get; set; }
        public int SummaryYear { get; set; }
        public Nullable<decimal> DrAmount { get; set; }
        public Nullable<decimal> CrAmount { get; set; }
        public int GlobalCompanyId { get; set; }
    }
}
