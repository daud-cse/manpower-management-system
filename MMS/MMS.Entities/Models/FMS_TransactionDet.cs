using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_TransactionDet
    {
        public int TransactionDetId { get; set; }
        public int TransactionId { get; set; }
        public Nullable<decimal> TransactionSLNo { get; set; }
        public int GLAccountId { get; set; }
        public string Particulars { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        public string ChequeNo { get; set; }
        public string SetBy { get; set; }
        public System.DateTime SetDate { get; set; }
        public Nullable<int> SubsidyTypeId { get; set; }
        public Nullable<int> SubsidyAccountId { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual FMS_GLAccount FMS_GLAccount { get; set; }
        public virtual FMS_SubsidyType FMS_SubsidyType { get; set; }
        public virtual FMS_Transaction FMS_Transaction { get; set; }
    }
}
