using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_DailySubsidyBalanceTransactionWise
    {
        public int Id { get; set; }
        public int TransactionDetId { get; set; }
        public int SubsidyTypeId { get; set; }
        public int SubsidyAccountId { get; set; }
        public Nullable<System.DateTime> OpenDate { get; set; }
        public Nullable<decimal> DrAmount { get; set; }
        public Nullable<decimal> CrAmount { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public int GlobalCompanyId { get; set; }
    }
}
