using MMS.Entities.Models;
using MMS.Entities.ViewModels;
using MMS.Service.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ViewModelServices
{
    public interface IAgentVMService
    {
        AgentVM GetAgentVM(int GlobalCompanyId);
    }
    public class AgentVMService : IAgentVMService
    {
        private readonly IAddressService _addressService;
        private readonly IAgentService _agentService;
        public AgentVMService(IAddressService addressService, IAgentService agentService)
        {
            _addressService = addressService;
            _agentService = agentService;
        }

        public AgentVM GetAgentVM(int GlobalCompanyId)
        {

            var agentList = _agentService.GetAllAgent( GlobalCompanyId);
            //agentList.ToList().ForEach(delegate (Agent item){
            //    _addressService.GetAddressById(item.ID);
                
            //})
            AgentVM agentVM = new AgentVM();
            agentVM.agentList = agentList.ToList();
            //agentVM.address = address;

            return agentVM;
        }

    }
}


