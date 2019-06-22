using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_Transaction
    {
        public FMS_Transaction()
        {
            this.FMS_TransactionDet = new List<FMS_TransactionDet>();
        }

        public int TransactionId { get; set; }
        public string VoucherId { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public System.DateTime ValueDate { get; set; }
        public int VoucherTypeId { get; set; }
        public bool IsPosted { get; set; }
        public int PRTypeId { get; set; }
        public string ApprovedBy { get; set; }
        public bool IsAuto { get; set; }
        public bool IsApproved { get; set; }
        public bool IsMultipleOrSingle { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> CompanyCode { get; set; }
        public string SetBy { get; set; }
        public string ActionType { get; set; }
        public System.DateTime SetDate { get; set; }
        public Nullable<decimal> VoucherSLNo { get; set; }
        public string ReferenceId { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
        public Nullable<decimal> TranactionAmount { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual FMS_PaymentReceivedType FMS_PaymentReceivedType { get; set; }
        public virtual FMS_VoucherType FMS_VoucherType { get; set; }
        public virtual ICollection<FMS_TransactionDet> FMS_TransactionDet { get; set; }
    }
}
