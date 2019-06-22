using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_MonthlyLedgerSummary
    {
        public int GLAccountId { get; set; }
        public int SummaryYM { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<decimal> DrAmount { get; set; }
        public Nullable<decimal> CrAmount { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public int GlobalCompanyId { get; set; }
    }
}
