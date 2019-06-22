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

    public interface IEmployeeInfoService
    {
        EmployeeInfo Find(params object[] keyValues);
        void Insert(EmployeeInfo entity);
        void Update(EmployeeInfo entity);
        void Delete(object id);
        void Delete(EmployeeInfo entity);
        IEnumerable<EmployeeInfo> GetAllEmployeeInfo(int GlobalCompanyId);
        IEnumerable<EmployeeInfo> GetActiveEmployeeInfo(int GlobalCompanyId, bool IsActive);
        EmployeeInfo GetEmployeeInfoById(int Id, int GlobalCompanyId);
        EmployeeInfo newEmployeeInfo(int GlobalCompanyId);
        EmployeeInfo newForEmployeeInfoEdit(EmployeeInfo EmployeeInfo, int GlobalCompanyId);

    }
    public class EmployeeInfoService : Service<EmployeeInfo>, IEmployeeInfoService
    {
        private readonly IRepositoryAsync<EmployeeInfo> _repository;   
        private readonly IContentService _iContentService;
        private readonly ICountryService _iCountryService;
        private readonly IDistrictService _iDistrictService;
        private readonly IDivisionService _iDivisionService;
        private readonly INationalityService _iNationalityService;
        private readonly IUpazilaService _iUpazilaService;
        private readonly IDesignationService _iDesignationService;
        
        public EmployeeInfoService(

              IRepositoryAsync<EmployeeInfo> repository,            
            IContentService iContentService,
            ICountryService iCountryService,
            IDistrictService iDistrictService,
            IDivisionService iDivisionService,
            INationalityService iNationalityService,
            IUpazilaService iUpazilaService,
            IDesignationService iDesignationService
            )
            : base(repository)
        {
            _repository = repository;          
            _iContentService = iContentService;
            _iCountryService = iCountryService;
            _iDistrictService = iDistrictService;
            _iNationalityService = iNationalityService;
            _iUpazilaService = iUpazilaService;
            _iDivisionService = iDivisionService;
            _iDesignationService = iDesignationService;
        }
        public IEnumerable<EmployeeInfo> GetAllEmployeeInfo(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId)
                .Include(x => x.District)
                .Include(x => x.Designation)
                .Include(x => x.Division)
                .Include(x => x.Upazila)
                .Select();
        }
        public IEnumerable<EmployeeInfo> GetActiveEmployeeInfo(int GlobalCompanyId,bool IsActive)
        {
            return _repository
                   .Query(x => x.IsActive == IsActive && x.GlobalCompanyId == GlobalCompanyId)
                   .Include(x => x.Country)
                   .Include(x => x.District)
                   .Select();
        }

        public EmployeeInfo GetEmployeeInfoById(int Id, int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.ID == Id && x.GlobalCompanyId == GlobalCompanyId)
                .Select().FirstOrDefault();
        }
        public EmployeeInfo newEmployeeInfo(int GlobalCompanyId)
        {
            var lstDesignation = new List<KeyValuePair<int, string>>();
            _iDesignationService.GetActiveDesignation(GlobalCompanyId,true).ToList().ForEach(delegate(Designation item)
            {
                lstDesignation.Add(new KeyValuePair<int, string>(item.ID, item.Name));
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
            EmployeeInfo objEmployeeInfo = new EmployeeInfo();
            objEmployeeInfo.kvpDestination = lstDesignation;
            objEmployeeInfo.kvpCountry = lstCountry;
            objEmployeeInfo.kvpDistrict = lstDistrict;
            objEmployeeInfo.kvpDivision = lstDivision;
            objEmployeeInfo.kvpNationality = lstNationality;
            objEmployeeInfo.kvpUpazila = lstUpazila;
            return objEmployeeInfo;
        }

        public EmployeeInfo newForEmployeeInfoEdit(EmployeeInfo EmployeeInfo, int GlobalCompanyId)
        {
            var lstDestination = new List<KeyValuePair<int, string>>();
            _iDesignationService.GetActiveDesignation(GlobalCompanyId, true).ToList().ForEach(delegate(Designation item)
            {
                lstDestination.Add(new KeyValuePair<int, string>(item.ID, item.Name));
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

            EmployeeInfo objEmployeeInfo = new EmployeeInfo();
            objEmployeeInfo.kvpDestination = lstDestination;
            objEmployeeInfo.kvpCountry = lstCountry;
            objEmployeeInfo.kvpDistrict = _iDistrictService.GetDistrictByDivisionID(EmployeeInfo.DivisionID).kvpDistrict;
            objEmployeeInfo.kvpDivision = lstDivision;
            objEmployeeInfo.kvpNationality = lstNationality;
            objEmployeeInfo.kvpUpazila = _iUpazilaService.GetUpazilaByDistrictID(EmployeeInfo.DistrictID).kvpUpazila;
            return objEmployeeInfo;
        }
    }
}
