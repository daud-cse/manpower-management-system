using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.ViewModels
{
    public class FMS_SubSidyVM
    {

        public int SubsidyAccountId { get; set; }
        public int SubsidyTypeId { get; set; }
        public decimal CurrentBalance { get; set; }
        public string SubsidyId { get; set; }
        public string SubsidyName { get; set; }
        public string BankName { get; set; }
        public string BankAccountNo { get; set; }
        public string BranchName { get; set; }

    }
}
