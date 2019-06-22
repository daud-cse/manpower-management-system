using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class BankAccountType
    {
        public BankAccountType()
        {
            this.BankAccountInfoes = new List<BankAccountInfo>();
        }

        public int BankAccountTypeId { get; set; }
        public string BankAccountTypeName { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<BankAccountInfo> BankAccountInfoes { get; set; }
    }
}
