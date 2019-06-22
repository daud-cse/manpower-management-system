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
    public interface ISearchPassportInfoVMService
    {
        SearchPassportInfoVM GetPassportInfo(SearchPassportInfoVM objSearchPassportInfoVM);
        SearchPassportInfoVM newSearchPassportInfoVM(int GlobalCompanyId);
    }
    public class SearchPassportInfoVMService : ISearchPassportInfoVMService
    {
        private readonly IPassportInfoService _passportInfoService;
        private readonly ICustomerService _customerService;
        private readonly ICountryService _iCountryService;    
        private readonly IDistrictService _iDistrictService;
        private readonly IDivisionService _iDivisionService;
        private readonly INationalityService _iNationalityService;
        private readonly IUpazilaService _iUpazilaService;
        private readonly IPassPortTypeService _iPassPortTypeService;
        private readonly IRPOTypeService _iRPOTypeService;
        private readonly ISexService _iSexService;
        private readonly IMaritalStatusService _iMaritalStatusService;
        public SearchPassportInfoVMService(IPassportInfoService passportInfoService
            ,ICustomerService customerService
            ,ICountryService iCountryService,
            IDivisionService iDivisionService,
            IDistrictService iDistrictService,
            INationalityService iNationalityService,
            IUpazilaService iUpazilaService,
             IPassPortTypeService iPassPortTypeService,
            IRPOTypeService iRPOTypeService,
            ISexService iSexService,
            IMaritalStatusService iMaritalStatusService)
        {
            _passportInfoService = passportInfoService;
            _customerService = customerService;
            _iCountryService = iCountryService;
            _iDivisionService = iDivisionService;
            _iDistrictService = iDistrictService;
            _iNationalityService = iNationalityService;
            _iUpazilaService = iUpazilaService;
            _iPassPortTypeService = iPassPortTypeService;
            _iRPOTypeService = iRPOTypeService;
            _iSexService = iSexService;
            _iMaritalStatusService = iMaritalStatusService;
        }

        public SearchPassportInfoVM GetPassportInfo(SearchPassportInfoVM objSearchPassportInfoVM)
        {
            objSearchPassportInfoVM.lstpassportInfo = _passportInfoService.GetPassportInfoByCriteria(objSearchPassportInfoVM).ToList();
            return objSearchPassportInfoVM;
        }
        public SearchPassportInfoVM newSearchPassportInfoVM(int GlobalCompanyId)
        {
            SearchPassportInfoVM objSearchPassportInfoVM = new SearchPassportInfoVM();
            var lstcustomer = new List<KeyValuePair<int, string>>();
            _customerService.GetActiveCustomer(GlobalCompanyId,true).ToList().ForEach(delegate(Customer item)
            {
                lstcustomer.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
           
            //var lstCountry = new List<KeyValuePair<int, string>>();
            //_iCountryService.GetActiveCountry(true).ToList().ForEach(delegate(Country item)
            //{
            //    lstCountry.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            //});
            //var lstDivision = new List<KeyValuePair<int, string>>();
            //_iDivisionService.GetActiveDivision(true).ToList().ForEach(delegate(Division item)
            //{
            //    lstDivision.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            //});
            //var lstNationality = new List<KeyValuePair<int, string>>();
            //_iNationalityService.GetActiveNationality(true).ToList().ForEach(delegate(Nationality item)
            //{
            //    lstNationality.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            //});
            //var lstDistrict = new List<KeyValuePair<int, string>>();
            //_iDistrictService.GetActiveDistrict(true).ToList().ForEach(delegate(District item)
            //{
            //    lstDistrict.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            //});
            //var lstUpazila = new List<KeyValuePair<int, string>>();
            //_iUpazilaService.GetActiveUpazila(true).ToList().ForEach(delegate(Upazila item)
            //{
            //    lstUpazila.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            //});
            var lstMaritalStatus = new List<KeyValuePair<int, string>>();
            _iMaritalStatusService.GetActiveMaritalStatu(true).ToList().ForEach(delegate(MaritalStatu item)
            {
                lstMaritalStatus.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });           

            var lstPassPortType = new List<KeyValuePair<int, string>>();
            _iPassPortTypeService.GetActivePassPortType(true).ToList().ForEach(delegate(PassPortType item)
            {
                lstPassPortType.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });

            var lstRPOType = new List<KeyValuePair<int, string>>();
            _iRPOTypeService.GetActiveRPOType(true).ToList().ForEach(delegate(RPOType item)
            {
                lstRPOType.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstSex = new List<KeyValuePair<int, string>>();
            _iSexService.GetActiveSex(true).ToList().ForEach(delegate(Sex item)
            {
                lstSex.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            objSearchPassportInfoVM.kvpSex = lstSex;
            objSearchPassportInfoVM.kvpRPOType = lstRPOType;
            objSearchPassportInfoVM.kvpPassPortType = lstPassPortType;
            objSearchPassportInfoVM.kvpMaritalStatus = lstMaritalStatus;
            objSearchPassportInfoVM.kvpCustomer = lstcustomer;
         
            return objSearchPassportInfoVM;
        }
    }
}
