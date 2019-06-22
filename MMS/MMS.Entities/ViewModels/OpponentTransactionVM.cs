using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.ViewModels
{
  public  class OpponentTransactionVM
    {
        public int TransactionId { get; set; }
        public string VoucherId { get; set; }
        public System.DateTime TransactionDate { get; set; }
       
     
        public bool IsPosted { get; set; }
       // public int PRTypeId { get; set; }
        public string ApprovedBy { get; set; }
      //  public bool IsAuto { get; set; }
        public bool IsApproved { get; set; }
       // public bool IsMultipleOrSingle { get; set; }
        public string Narration { get; set; }       
        public string ReferenceId { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
        public decimal TranactionAmount { get; set; }
        public int TransactionDetId { get; set; }        
        public decimal TransactionSLNo { get; set; }
        public int GLAccountId { get; set; }
        public string Particulars { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
       // public string ChequeNo { get; set; }             
        public int SubsidyTypeId { get; set; }
        public int SubsidyID { get; set; }
        public string SubsidyName { get; set; }

        public decimal CurrentBalance { get; set; }
      

    }
}
