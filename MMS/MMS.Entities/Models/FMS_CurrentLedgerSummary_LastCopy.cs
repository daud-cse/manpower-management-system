using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_CurrentLedgerSummary_LastCopy
    {
        public int GLAccountId { get; set; }
        public string DrCr { get; set; }
        public System.DateTime OpenDate { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public Nullable<decimal> DrAmount { get; set; }
        public Nullable<decimal> CrAmount { get; set; }
        public Nullable<decimal> ClosingBalance { get; set; }
        public int GlobalCompanyId { get; set; }
    }
}
