using MMS.Entities.StoredProcedures;
using MMS.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ViewModelServices
{
  
     public interface IFMS_SubSidyVMService
    {
        IEnumerable<FMS_SubSidyVM> FMS_SubsidyByCriteria(int GlobalCompanyId,int SubsidyTypeId, int SubsidyAccountId, string SearchItem);
      
    }
     public class FMS_SubSidyVMService : IFMS_SubSidyVMService
     {
         private readonly IStoredProcedures _storedProcedures;
         public FMS_SubSidyVMService(IStoredProcedures storedProcedures
             )
         {
             _storedProcedures = storedProcedures;
         }

         public IEnumerable<FMS_SubSidyVM> FMS_SubsidyByCriteria(int GlobalCompanyId,int SubsidyTypeId, int SubsidyAccountId, string SearchItem)
         {
             return _storedProcedures.FMS_SubsidyByCriteria(GlobalCompanyId,SubsidyTypeId, SubsidyAccountId, SearchItem);
         }
     }
}
