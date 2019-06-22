using MMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.ViewModels
{
   public class FMS_TransactionVM
    {
       public FMS_Transaction Transaction { get; set; }
       public FMS_TransactionDet TransactionDet1 { get; set; }
       public FMS_TransactionDet TransactionDet2 { get; set; }
       public List<FMS_TransactionDet> TransactionDetList { get; set; }
       public FMS_VoucherTypeWiseGLMap VoucherTypeWiseGLMap { get; set; }
       public FMS_SubsidyAccount SubsidyAccount { get; set; }
       public List<FMS_SubsidyAccount> lstSubsidyAccount { get; set; }
       public int? SubsidyAccountId1 { get; set; }
       public int UITypeId { get; set;}
    }
}
