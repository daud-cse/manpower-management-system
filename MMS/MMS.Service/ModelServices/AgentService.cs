using MMS.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ModelServices
{
  
    public interface IAgentService
    {
        Agent Find(params object[] keyValues);
        void Insert(Agent entity);
        void Update(Agent entity);
        void Delete(object id);
        void Delete(Agent entity);
        IEnumerable<Agent> GetAllAgent(int GlobalCompanyId);
        IEnumerable<Agent> GetActiveAgent(int GlobalCompanyId,bool IsActive);
        Agent GetAgentById(int Id, int GlobalCompanyId);
        Agent newAgent(int GlobalCompanyId);
        Agent newForAgentEdit(Agent agent, int GlobalCompanyId);
        
    }
    public class AgentService : Service<Agent>, IAgentService
    {
        private readonly IRepositoryAsync<Agent> _repository;
        private readonly IAgentTypeService _iAgentTypeService;
        private readonly IContentService _iContentService;
        private readonly ICountryService _iCountryService;
        private readonly IDistrictService _iDistrictService;
        private readonly IDivisionService _iDivisionService;
        private readonly INationalityService _iNationalityService;
        private readonly IUpazilaService _iUpazilaService;
        public AgentService(

              IRepositoryAsync<Agent> repository,
            IAgentTypeService iAgentTypeService,
            IContentService iContentService,
            ICountryService iCountryService,
            IDistrictService iDistrictService,
            IDivisionService iDivisionService,
            INationalityService iNationalityService,
            IUpazilaService iUpazilaService
            )
            : base(repository)
        {
            _repository = repository;
            _iAgentTypeService = iAgentTypeService;
            _iContentService = iContentService;
            _iCountryService = iCountryService;
            _iDistrictService = iDistrictService;
            _iNationalityService = iNationalityService;
            _iUpazilaService = iUpazilaService;
            _iDivisionService = iDivisionService;
        }
        public IEnumerable<Agent> GetAllAgent(int GlobalCompanyId)
        {
            return _repository.Query(x=>x.GlobalCompanyId==GlobalCompanyId)
                .Include(x=>x.District)
                .Include(x => x.AgentType)
                .Include(x => x.Division)
                .Include(x=>x.Upazila)
                .Select();
        }
        public IEnumerable<Agent> GetActiveAgent(int GlobalCompanyId,bool IsActive)
        {
            return _repository
                   .Query(x => x.IsActive == IsActive && x.GlobalCompanyId == GlobalCompanyId)
                   .Include(x => x.Country)
                   .Include(x=>x.District)
                   .Select();
        }

        public Agent GetAgentById(int Id, int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.ID == Id && x.GlobalCompanyId == GlobalCompanyId)
                .Select().FirstOrDefault();
        }
        public Agent newAgent(int GlobalCompanyId)
        {
            var lstAgentType = new List<KeyValuePair<int, string>>();
            _iAgentTypeService.GetActiveAgentType(GlobalCompanyId,true).ToList().ForEach(delegate(AgentType item)
            {
                lstAgentType.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstCountry = new List<KeyValuePair<int, string>>();
            _iCountryService.GetActiveCountry(GlobalCompanyId, true).ToList().ForEach(delegate(Country item)
            {
                lstCountry.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstDivision = new List<KeyValuePair<int, string>>();
            _iDivisionService.GetActiveDivision(true).ToList().ForEach(delegate(Division item)
            {
                lstDivision.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstNationality = new List<KeyValuePair<int, string>>();
            _iNationalityService.GetActiveNationality(true).ToList().ForEach(delegate(Nationality item)
            {
                lstNationality.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstDistrict = new List<KeyValuePair<int, string>>();
          
            var lstUpazila = new List<KeyValuePair<int, string>>();         
            Agent objAgent = new Agent();
            objAgent.kvpAgentType = lstAgentType;
            objAgent.kvpCountry = lstCountry;
            objAgent.kvpDistrict = lstDistrict;
            objAgent.kvpDivision = lstDivision;
            objAgent.kvpNationality = lstNationality;
            objAgent.kvpUpazila = lstUpazila;
            return objAgent;
        }

        public Agent newForAgentEdit(Agent agent, int GlobalCompanyId)
        {
            var lstAgentType = new List<KeyValuePair<int, string>>();
            _iAgentTypeService.GetActiveAgentType(GlobalCompanyId,true).ToList().ForEach(delegate(AgentType item)
            {
                lstAgentType.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstCountry = new List<KeyValuePair<int, string>>();
            _iCountryService.GetActiveCountry(GlobalCompanyId,true).ToList().ForEach(delegate(Country item)
            {
                lstCountry.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });

            var lstNationality = new List<KeyValuePair<int, string>>();
            _iNationalityService.GetActiveNationality(true).ToList().ForEach(delegate(Nationality item)
            {
                lstNationality.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstDivision = new List<KeyValuePair<int, string>>();
            _iDivisionService.GetActiveDivision(true).ToList().ForEach(delegate(Division item)
            {
                lstDivision.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
        
            Agent objAgent = new Agent();
            objAgent.kvpAgentType = lstAgentType;
            objAgent.kvpCountry = lstCountry;
            objAgent.kvpDistrict = _iDistrictService.GetDistrictByDivisionID(agent.DivisionID).kvpDistrict;
            objAgent.kvpDivision = lstDivision;
            objAgent.kvpNationality = lstNationality;
            objAgent.kvpUpazila = _iUpazilaService.GetUpazilaByDistrictID(agent.DistrictID).kvpUpazila;
            return objAgent;
        }
    }
}
