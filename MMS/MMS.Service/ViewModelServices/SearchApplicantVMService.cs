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
    public interface ISearchApplicantVMService
    {
        SearchApplicantVM GetApplicant(SearchApplicantVM objApplicantVMVM);
        SearchApplicantVM newSearchApplicantVM(int GlobalCompanyId);
        SearchApplicantVM newSearchApplicantVMForAgentWiseApplicantReports(int GlobalCompanyId);
    }
    public class SearchApplicantVMService : ISearchApplicantVMService
    {
        private readonly IApplicantService _applicantService;
        private readonly IAgentService _iAgentService;
        private readonly IApplicantTypeService _iApplicantTypeService;
        private readonly ICustomerService _customerService;
        private readonly ICountryService _iCountryService;
        private readonly IDistrictService _iDistrictService;
        private readonly IDivisionService _iDivisionService;
        private readonly INationalityService _iNationalityService;
        private readonly IUpazilaService _iUpazilaService;
        private readonly ILocationService _iLocationService;
        public SearchApplicantVMService(IApplicantService applicantService
            , ICustomerService customerService
            , ICountryService iCountryService,
            IDivisionService iDivisionService,
            IDistrictService iDistrictService,
            INationalityService iNationalityService,
            IUpazilaService iUpazilaService
            ,IAgentService iAgentService
            ,IApplicantTypeService iApplicantTypeService
            ,ILocationService iLocationService)
        {
            _applicantService = applicantService;
            _customerService = customerService;
            _iCountryService = iCountryService;
            _iDivisionService = iDivisionService;
            _iDistrictService = iDistrictService;
            _iNationalityService = iNationalityService;
            _iUpazilaService = iUpazilaService;
            _iAgentService = iAgentService;
            _iApplicantTypeService = iApplicantTypeService;
            _iLocationService = iLocationService;
        }

        public SearchApplicantVM GetApplicant(SearchApplicantVM objSearchApplicantVM)
        {
            objSearchApplicantVM.lstApplicant = _applicantService.GetApplicantsByCriteria(objSearchApplicantVM).ToList();
            return objSearchApplicantVM;
        }
        public SearchApplicantVM newSearchApplicantVMForAgentWiseApplicantReports(int GlobalCompanyId)
        {
            SearchApplicantVM objSearchApplicantVM = new SearchApplicantVM();
            var lstAgent = new List<KeyValuePair<int, string>>();
            _iAgentService.GetActiveAgent(GlobalCompanyId,true).ToList().ForEach(delegate(Agent item)
            {
                lstAgent.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
           
            var lstLocation = new List<KeyValuePair<int, string>>();
            _iLocationService.GetActiveLocation(GlobalCompanyId,true).ToList().ForEach(delegate(Location item)
            {
                lstLocation.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            

            objSearchApplicantVM.kvpAgent = lstAgent;           
            objSearchApplicantVM.kvpLocation = lstLocation;


            return objSearchApplicantVM;
        }

          public SearchApplicantVM newSearchApplicantVM(int GlobalCompanyId)
        {
            SearchApplicantVM objSearchApplicantVM = new SearchApplicantVM();
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
            var lstLocation = new List<KeyValuePair<int, string>>();
            _iLocationService.GetActiveLocation(GlobalCompanyId,true).ToList().ForEach(delegate(Location item)
            {
                lstLocation.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            

            objSearchApplicantVM.kvpAgent = lstAgent;
            objSearchApplicantVM.kvpCountry = lstCountry;

            objSearchApplicantVM.kvpDivision = lstDivision;
            objSearchApplicantVM.kvpNationality = lstNationality;
            objSearchApplicantVM.kvpDistrict = lstDistrict;
            objSearchApplicantVM.kvpUpazila = lstUpazila;
            objSearchApplicantVM.kvpApplicantType = lstApplicantType;
            objSearchApplicantVM.kvpLocation = lstLocation;


            return objSearchApplicantVM;
        }
    }
}
