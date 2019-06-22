using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class BankAccountInfo
    {
        public int ID { get; set; }
        public string BankAccountAutoId { get; set; }
        public string BankAccountNo { get; set; }
        public string Name { get; set; }
        public int BankAccountTypeId { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public System.DateTime AccountOpeningDate { get; set; }
        public System.DateTime AccountClosingDate { get; set; }
        public bool IsActive { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public Nullable<decimal> DrAmount { get; set; }
        public Nullable<decimal> CrAmount { get; set; }
        public Nullable<decimal> CurrentBalance { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public string SetBy { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual BankAccountType BankAccountType { get; set; }
    }
}
