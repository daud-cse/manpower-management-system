using MMS.Entities.ViewModels;
using MMS.Service.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ViewModelServices
{
   
   public interface ISubsidyMappedVMService
   {
       SubsidyMappedVM GetAgentVM(int GlobalCompanyId);
   }
   public class SubsidyMappedVMService : ISubsidyMappedVMService
   {
       private readonly IAddressService _addressService;
       private readonly IAgentService _agentService;
       public SubsidyMappedVMService(IAddressService addressService, IAgentService agentService)
       {
           _addressService = addressService;
           _agentService = agentService;
       }

       public SubsidyMappedVM GetAgentVM(int GlobalCompanyId)
       {

           var agentList = _agentService.GetAllAgent(GlobalCompanyId);
           //agentList.ToList().ForEach(delegate (Agent item){
           //    _addressService.GetAddressById(item.ID);

           //})
           SubsidyMappedVM agentVM = new SubsidyMappedVM();
           

           return agentVM;
       }

   }
}
