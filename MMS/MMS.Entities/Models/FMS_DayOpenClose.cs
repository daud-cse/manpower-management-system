using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_DayOpenClose
    {
        public int Id { get; set; }
        public System.DateTime OpenDate { get; set; }
        public Nullable<System.DateTime> OpenedOn { get; set; }
        public bool IsDayClosed { get; set; }
        public Nullable<System.DateTime> ClosedOn { get; set; }
        public Nullable<decimal> OpeningBankBalance { get; set; }
        public Nullable<decimal> OpeningCashBalance { get; set; }
        public Nullable<decimal> ClosingBankBalance { get; set; }
        public Nullable<decimal> ClosingCashBalance { get; set; }
        public Nullable<decimal> Revenue { get; set; }
        public Nullable<decimal> Expenses { get; set; }
        public Nullable<decimal> GrossProfitOrLoss { get; set; }
        public int GlobalCompanyId { get; set; }
    }
}
