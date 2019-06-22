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
   
   public interface IComplainService
   {
       Complain Find(params object[] keyValues);
       void Insert(Complain entity);
       void Update(Complain entity);
       void Delete(object id);
       void Delete(Complain entity);
       IEnumerable<Complain> GetAllComplain();
       IEnumerable<Complain> GetActiveComplain(bool IsActive);
       Complain GetComplainById(int Id);
       Complain newComplain(int GlobalCompanyId);

   }
   public class ComplainService : Service<Complain>, IComplainService
   {
       private readonly IRepositoryAsync<Complain> _repository;
        private readonly IComplainTypeService _iComplainTypeService;
        private readonly IStatusService _iStatusService;
        private readonly IAgentService _iAgentService;
        private readonly ICountryService _iCountryService;
        private readonly IDistrictService _iDistrictService;
        private readonly IDivisionService _iDivisionService;
        private readonly INationalityService _iNationalityService;
        private readonly IUpazilaService _iUpazilaService;
        public ComplainService(

              IRepositoryAsync<Complain> repository,
          IComplainTypeService iComplainTypeService,
             IStatusService iStatusService,
            ICountryService iCountryService,
            IDistrictService iDistrictService,
            IDivisionService iDivisionService,
            INationalityService iNationalityService,
            IUpazilaService iUpazilaService,
            IAgentService iAgentService
            )
            : base(repository)
        {
            _repository = repository;
            _iAgentService = iAgentService;
            _iComplainTypeService = iComplainTypeService;
            _iStatusService = iStatusService;
            _iCountryService = iCountryService;
            _iDistrictService = iDistrictService;
            _iNationalityService = iNationalityService;
            _iUpazilaService = iUpazilaService;
            _iDivisionService = iDivisionService;
        }
       
       public IEnumerable<Complain> GetAllComplain()
       {
           return _repository.Query()
               .Include(x=>x.ComplainType)
               .Include(x => x.Applicant)
               .Include(x => x.Agent)
               .Include(x => x.Status)
               .Include(x => x.Country)
               .Include(x => x.District)
               .Select();
       }
       public IEnumerable<Complain> GetActiveComplain(bool IsActive)
       {
           return _repository
                  .Query()
                  .Select();
       }
       public Complain newComplain(int GlobalCompanyId)
       {
           var lstAgent = new List<KeyValuePair<int, string>>();

           _iAgentService.GetActiveAgent(GlobalCompanyId,true).ToList().ForEach(delegate(Agent item)
           {
               lstAgent.Add(new KeyValuePair<int, string>(item.ID, item.Name+" "+"("+item.District.Name+")"));
           });

           var lstComplainType = new List<KeyValuePair<int, string>>();
           _iComplainTypeService.GetActiveComplainType(true).ToList().ForEach(delegate(ComplainType item)
           {
               lstComplainType.Add(new KeyValuePair<int, string>(item.ID, item.Name));
           });

           var lstComplainStatus = new List<KeyValuePair<int, string>>();
           _iStatusService.GetActiveStatus(true).ToList().ForEach(delegate(Status item)
           {
               lstComplainStatus.Add(new KeyValuePair<int, string>(item.ID, item.Name));
           });


           var lstCountry = new List<KeyValuePair<int, string>>();
           _iCountryService.GetActiveCountry(GlobalCompanyId,true).ToList().ForEach(delegate(Country item)
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
           Complain objComplain = new Complain();
           objComplain.kvpAgent = lstAgent;
           objComplain.kvpComplainType = lstComplainType;
           objComplain.kvpComplainStatus = lstComplainStatus;
           objComplain.kvpCountry = lstCountry;
           objComplain.kvpDistrict = lstDistrict;
           objComplain.kvpDivision = lstDivision;
           objComplain.kvpNationality = lstNationality;
           objComplain.kvpUpazila = lstUpazila;
           return objComplain;
       }

       public Complain GetComplainById(int Id)
       {
           return _repository
               .Query(x => x.ID == Id)
               .Include(x=>x.Applicant)
               .Select().FirstOrDefault();
       }

   }
}
