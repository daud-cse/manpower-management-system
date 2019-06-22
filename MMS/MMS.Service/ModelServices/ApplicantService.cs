using MMS.Entities.Models;
using MMS.Entities.ViewModels;
using MMS.Utility;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ModelServices
{

    public interface IApplicantService
    {
        Applicant Find(params object[] keyValues);
        void Insert(Applicant entity);
        void Update(Applicant entity);
        void Delete(object id);
        void Delete(Applicant entity);
        IEnumerable<Applicant> GetAllApplicant();
        IEnumerable<Applicant> GetAllApplicant(int GlobalCompanyId);
        IEnumerable<Applicant> GetAllApplicantByLocationId(int locationId);
        IEnumerable<Applicant> GetActiveApplicant(bool IsActive);
        Applicant CheckOnlineStatus(Applicant objApplicant);
        IEnumerable<Applicant> GetApplicantsByAgentId(int agentId);
        IEnumerable<Applicant> GetApplicantsByCriteria(SearchApplicantVM objSearchApplicantVM);
        List<Applicant> GetApplicantsByCriteria(string searchItem);
        Applicant GetApplicantsByPassPortNo(string passportNo,int GlobalCompanyId);
        Applicant GetApplicantsById(int Id,int GlobalCompanyId);
        Applicant newApplicant(int GlobalCompanyId);
        Applicant GetApplicantByPassportNo(int GlobalCompanyId, string passportNo);
        List<Applicant> GetCompletedApplicantsByCriteria(string searchItem);
        List<Applicant> GetCompletedApplicantListForCompanyMap(string searchItem);
        Applicant GetApplicantsByIdForApi(string Applicantinfo);

    }
    public class ApplicantService : Service<Applicant>, IApplicantService
    {
        private readonly IRepositoryAsync<Applicant> _repository;
        private readonly ICountryService _iCountryService;
        private readonly IAgentService _iAgentService;
        private readonly IApplicantTypeService _iApplicantTypeService;
        private readonly IDistrictService _iDistrictService;
        private readonly IDivisionService _iDivisionService;
        private readonly INationalityService _iNationalityService;
        private readonly IUpazilaService _iUpazilaService;
        private readonly ICompanyWiseApplicantService _iCompanyWiseApplicantService;


        public ApplicantService(
            IRepositoryAsync<Applicant> repository,
            IAgentService iAgentService,
            IApplicantTypeService iApplicantTypeService,
            ICountryService iCountryService,
            IDivisionService iDivisionService,
            IDistrictService iDistrictService,
            INationalityService iNationalityService,
            IUpazilaService iUpazilaService,
            ICompanyWiseApplicantService iCompanyWiseApplicantService

            )
            : base(repository)
        {
            _iAgentService = iAgentService;
            _iApplicantTypeService = iApplicantTypeService;
            _repository = repository;
            _iCountryService = iCountryService;
            _iDivisionService = iDivisionService;
            _iDistrictService = iDistrictService;
            _iNationalityService = iNationalityService;
            _iUpazilaService = iUpazilaService;
            _iCompanyWiseApplicantService = iCompanyWiseApplicantService;


        }
        public Applicant GetApplicantByPassportNo(int GlobalCompanyId,string passportNo)
        {
            return _repository.Query(x => x.PassportNo == passportNo && x.GlobalCompanyId == GlobalCompanyId).Select().FirstOrDefault();
        }
        public IEnumerable<Applicant> GetAllApplicantByLocationId(int locationId)
        {
            
            return  _repository.Query(x => x.CurrentLocationID == locationId)
                .Include(x=>x.Agent)
                .Include(x => x.Location)
                .Include(x => x.Country)
                .Select();
        }
        public Applicant CheckOnlineStatus(Applicant objApplicant)
        {
            Expression<Func<Applicant, bool>> predicate = PredicateBuilder.True<Applicant>();

            if (objApplicant.ID > 0)
            {
                predicate = predicate.And(p => p.ID == objApplicant.ID);
            }
            if (!String.IsNullOrEmpty(objApplicant.PassportNo))
            {
                predicate = predicate.Or(p => p.PassportNo == objApplicant.PassportNo);
            }
            return _repository.Query(predicate).Select().FirstOrDefault();
        }
        public List<Applicant> GetApplicantsByCriteria(string searchItem)
        {
            int outputofParse = 0;
            IEnumerable<Applicant> applicantListServer;
            if (Int32.TryParse(searchItem, out outputofParse))
            {
                 applicantListServer = _repository
                 .Query(p => p.ID==outputofParse)
                 .Include(x => x.Agent)
                 .Include(x=>x.Location1)
                 .Include(x => x.Country)
                 .Select().Take(20);
            }
            else
            {
                 applicantListServer = _repository
                 .Query(p => p.Name.ToLower().Contains(searchItem.ToLower()) || p.Country.Name.ToLower().Contains(searchItem.ToLower()))
                 .Include(x => x.Agent)
                 .Include(x => x.Location1)
                 .Include(x => x.Country)
                 .Select().Take(20);
            }
            

            List<Applicant> applicantList = new List<Applicant>();
            applicantListServer.ToList().ForEach(delegate(Applicant item)
            {
                Applicant applicant = new Applicant();
                applicant.ID = item.ID;
                applicant.Agent.Name = item.Agent.Name;
                applicant.FathersName = item.FathersName;
                applicant.MothersName = item.MothersName;
                applicant.IsAllTaskCompleted = item.Location1.IsNextLocationExist;
                applicant.Name = item.Name+" "+"("+item.Country.Name+")";
                applicant.ApplicantCountryName_vw = item.Country.Name;
                applicantList.Add(applicant);
            });
            return applicantList;
        }
        public List<Applicant> GetCompletedApplicantListForCompanyMap(string searchItem)
        {
            int outputofParse = 0;
            IEnumerable<Applicant> applicantListServer;
            if (Int32.TryParse(searchItem, out outputofParse))
            {
                applicantListServer = _repository
                .Query(p => p.ID == outputofParse && p.Location1.IsNextLocationExist == false)
                .Include(x => x.Agent)
                .Include(x => x.Location1)
                .Include(x => x.Country)
                .Select().Take(20);
            }
            else
            {
                applicantListServer = _repository
                .Query(p => p.Location1.IsNextLocationExist == false && (p.Name.ToLower().Contains(searchItem.ToLower()) || p.Country.Name.ToLower().Contains(searchItem.ToLower())))
                .Include(x => x.Agent)
                .Include(x => x.Location1)
                .Include(x => x.Country)
                .Select().Take(20);
            }                     
        
            List<Applicant> applicantList = new List<Applicant>();
            applicantListServer.ToList().ForEach(delegate(Applicant item)
            {
                Applicant applicant = new Applicant();
                var companyWiseApplicant = _iCompanyWiseApplicantService.GetCompanyWiseApplicantByApplicantId(item.ID);
                if (companyWiseApplicant != null)
                {
                    applicant.IsAlreadyMappedWithCompany = true;
                }
                applicant.Agent = new Agent();
                applicant.ID = item.ID;
                applicant.Agent.Name = item.Agent.Name;
                applicant.Agent.ID = item.Agent.ID; 
                applicant.FathersName = item.FathersName;
                applicant.MothersName = item.MothersName;
                applicant.IsAllTaskCompleted = item.Location1.IsNextLocationExist;
               // applicant.Name = item.Name + " " + "(" + item.Country.Name + ")";
                applicant.Name = item.Name;
                applicant.ApplicantCountryName_vw = item.Country.Name;
                applicantList.Add(applicant);
            });
            return applicantList;
        }
        public List<Applicant> GetCompletedApplicantsByCriteria(string searchItem)
        {
            int outputofParse = 0;
            IEnumerable<Applicant> applicantListServer;
            if (Int32.TryParse(searchItem, out outputofParse))
            {
                applicantListServer = _repository
                .Query(p => p.ID == outputofParse && p.Location1.IsNextLocationExist == false)
                .Include(x => x.Agent)
                .Include(x => x.Location1)
                .Include(x => x.Country)
                .Select().Take(20);
            }
            else
            {
                applicantListServer = _repository
                .Query(p => p.Location1.IsNextLocationExist == false && (p.Name.ToLower().Contains(searchItem.ToLower()) || p.Country.Name.ToLower().Contains(searchItem.ToLower())))
                .Include(x => x.Agent)
                .Include(x => x.Location1)
                .Include(x => x.Country)
                .Select().Take(20);
            }


            List<Applicant> applicantList = new List<Applicant>();
            applicantListServer.ToList().ForEach(delegate(Applicant item)
            {
                Applicant applicant = new Applicant();
                applicant.Agent = new Agent();
                applicant.ID = item.ID;
                applicant.Agent.Name = item.Agent.Name;
                applicant.Agent.ID = item.Agent.ID;
                applicant.FathersName = item.FathersName;
                applicant.MothersName = item.MothersName;
                applicant.IsAllTaskCompleted = item.Location1.IsNextLocationExist;
                // applicant.Name = item.Name + " " + "(" + item.Country.Name + ")";
                applicant.Name = item.Name;
                applicant.ApplicantCountryName_vw = item.Country.Name;
                applicantList.Add(applicant);
            });
            return applicantList;
        }
        public IEnumerable<Applicant> GetApplicantsByCriteria(SearchApplicantVM objSearchApplicantVM)
        {
            Expression<Func<Applicant, bool>> predicate = PredicateBuilder.True<Applicant>();
            predicate = predicate.And(p => p.GlobalCompanyId == objSearchApplicantVM.GlobalCompanyId);

            if (objSearchApplicantVM.AgentId > 0)
            {
                predicate = predicate.And(p => p.AgentID == objSearchApplicantVM.AgentId);
            }
            if (objSearchApplicantVM.LocationId > 0)
            {
                predicate = predicate.And(p => p.CurrentLocationID == objSearchApplicantVM.LocationId);
            }
            if (!String.IsNullOrEmpty(objSearchApplicantVM.PassportNo))
            {
                predicate = predicate.And(p => p.PassportNo.ToLower().Contains(objSearchApplicantVM.PassportNo));
            }
            if (objSearchApplicantVM.ApplicantId > 0)
            {
                predicate = predicate.And(p => p.ID == objSearchApplicantVM.ApplicantId);
            }
            if (!String.IsNullOrEmpty(objSearchApplicantVM.PreRefApplicantId))
            {
                predicate = predicate.And(p => p.PreRefApplicantId.ToLower().Contains(objSearchApplicantVM.PreRefApplicantId));
            }

            if (!String.IsNullOrEmpty(objSearchApplicantVM.AutoApplicantId))
            {
                predicate = predicate.And(p => p.PreRefApplicantId == objSearchApplicantVM.AutoApplicantId);
            }

            if (!String.IsNullOrEmpty(objSearchApplicantVM.ApplicantName))
            {
                predicate = predicate.And(p => p.Name.ToLower().Contains(objSearchApplicantVM.ApplicantName.ToLower()));
            }
            if (objSearchApplicantVM.CountryId > 0)
            {
                predicate = predicate.And(p => p.CountryID == objSearchApplicantVM.CountryId);
            }
            if (objSearchApplicantVM.DivisionId > 0)
            {
                predicate = predicate.And(p => p.DivisionID == objSearchApplicantVM.DivisionId);
            }
            if (objSearchApplicantVM.DistrictId > 0)
            {
                predicate = predicate.And(p => p.DistrictID == objSearchApplicantVM.DistrictId);
            }

            if (objSearchApplicantVM.UpazilaId > 0)
            {
                predicate = predicate.And(p => p.PermanentUpazilaID == objSearchApplicantVM.UpazilaId);
            }

            return _repository
                .Query(predicate)
                 .Include(x => x.Agent)
                .Include(x => x.Location)
                .Include(x => x.Country)
                .Include(x => x.Location1)
                .Select();
        }
        public IEnumerable<Applicant> GetAllApplicant()
        {
            return _repository.Query()
                .Include(x => x.Agent)
                .Include(x => x.Country)
                .Select();
        }
        public IEnumerable<Applicant> GetAllApplicant(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId)
                .Include(x=>x.Agent)
                .Include(x => x.ApplicantType)
                .Include(x => x.Country)
                .Include(x=>x.Location)
                .Include(x => x.Location1)
                .Select();
        }
        public IEnumerable<Applicant> GetActiveApplicant(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }
        public Applicant newApplicant(int GlobalCompanyId)
        {
            Applicant objApplicant = new Applicant();


            var lstAgent = new List<KeyValuePair<int, string>>();
            _iAgentService.GetActiveAgent(GlobalCompanyId,true).ToList().ForEach(delegate(Agent item)
            {
                lstAgent.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstApplicantType = new List<KeyValuePair<int, string>>();
            _iApplicantTypeService.GetActiveApplicantType(GlobalCompanyId,true).ToList().ForEach(delegate(ApplicantType item)
            {
                lstApplicantType.Add(new KeyValuePair<int, string>(item.ID, item.Name));
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
            _iDistrictService.GetActiveDistrict(true).ToList().ForEach(delegate(District item)
            {
                lstDistrict.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstUpazila = new List<KeyValuePair<int, string>>();
            _iUpazilaService.GetActiveUpazila(true).ToList().ForEach(delegate(Upazila item)
            {
                lstUpazila.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });


            objApplicant.kvpAgent = lstAgent;
            objApplicant.kvpCountry = lstCountry;

            objApplicant.kvpDivision = lstDivision;
            objApplicant.kvpNationality = lstNationality;
            objApplicant.kvpDistrict = lstDistrict;
            objApplicant.kvpUpazila = lstUpazila;

            objApplicant.kvpApplicantType = lstApplicantType;

            return objApplicant;
        }
        public IEnumerable<Applicant> GetApplicantsByAgentId(int agentId)
        {
            return _repository
                .Query(x => x.AgentID == agentId)
                 .Include(x => x.Agent)
                .Include(x => x.Location)
                .Include(x => x.Location1)
                .Select();
        }
        public Applicant GetApplicantsById(int Id,int GlobalCompanyId)
        {
            return _repository.Query(x => x.ID == Id && x.GlobalCompanyId==GlobalCompanyId)
                   .Include(x => x.ApplicantFileLot)
                   .Include(x=>x.ApplicantType)
                   .Include(x => x.ApplicantCheckLists)
                   .Include(x => x.Location)
                   .Include(x => x.Agent)
                   .Include(x => x.Location1)
                   .Select().FirstOrDefault();
        }
        public Applicant GetApplicantsByIdForApi(string Applicantinfo)
        {
            var objApplicant = _repository.Query(x => x.PreRefApplicantId == Applicantinfo)
                .Include(x=>x.Location)
                .Include(x=>x.Agent)
                .Include(x => x.Location1)                
                    .Select().SingleOrDefault();
           return objApplicant;
        }
        public Applicant GetApplicantsByPassPortNo(string passportNo,int GlobalCompanyId)
        {
            return _repository.Query(x => x.PassportNo == passportNo && x.GlobalCompanyId==GlobalCompanyId)
                   .Include(x => x.ApplicantFileLot)
                   .Include(x => x.ApplicantCheckLists)
                   .Include(x => x.Location)
                   .Include(x => x.Location1)
                   .Select().FirstOrDefault();
        }


    }
}
