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
    
    public interface ICompanyService
    {
        Company Find(params object[] keyValues);
        void Insert(Company entity);
        void Update(Company entity);
        void Delete(object id);
        void Delete(Company entity);
        IEnumerable<Company> GetAllCompany();
        IEnumerable<Company> GetActiveCompany(bool IsActive);
        Company GetCompanyById(int Id);
        Company newCompany(int GlobalCompanyId);
    }
    public class CompanyService : Service<Company>, ICompanyService
    {
        private readonly IRepositoryAsync<Company> _repository;
         private readonly ICountryService _iCountryService;
        private readonly IAgentService _iAgentService;
        private readonly IApplicantTypeService _iApplicantTypeService;
        private readonly IDistrictService _iDistrictService;
        private readonly IDivisionService _iDivisionService;
        private readonly INationalityService _iNationalityService;
        private readonly IUpazilaService _iUpazilaService;
        private readonly ICompanyTypeService _iCompanyTypeService;
        

        public CompanyService(
            IRepositoryAsync<Company> repository,
          
            ICountryService iCountryService,
            IDivisionService iDivisionService,
            IDistrictService iDistrictService,
            INationalityService iNationalityService,
            IUpazilaService iUpazilaService,
            ICompanyTypeService iCompanyTypeService



            )
            : base(repository)
        {
           
            _repository = repository;
            _iCountryService = iCountryService;
            _iDivisionService = iDivisionService;
            _iDistrictService = iDistrictService;
            _iNationalityService = iNationalityService;
            _iUpazilaService = iUpazilaService;
            _iCompanyTypeService = iCompanyTypeService;


        }
        public Company newCompany(int GlobalCompanyId)
        {
            Company objComapny = new Company();


            var lstCompanyType = new List<KeyValuePair<int, string>>();
            _iCompanyTypeService.GetActiveCompanyType(true).ToList().ForEach(delegate(CompanyType item)
            {
                lstCompanyType.Add(new KeyValuePair<int, string>(item.ID, item.Name));
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
            //_iUpazilaService.GetActiveUpazila(true).ToList().ForEach(delegate(Upazila item)
            //{
            //    lstUpazila.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            //});



            objComapny.kvpCountry = lstCountry;
            objComapny.kvpComapnyType = lstCompanyType;
            objComapny.kvpDivision = lstDivision;
            objComapny.kvpNationality = lstNationality;
            objComapny.kvpDistrict = lstDistrict;
            objComapny.kvpUpazila = lstUpazila;
           

            return objComapny;
        }
        public IEnumerable<Company> GetAllCompany()
        {
            return _repository.
                Query()
                .Include(x=>x.Country)
                .Include(x => x.CompanyType)
               .Select();
        }
        public IEnumerable<Company> GetActiveCompany(bool IsActive)
        {
            return _repository
                   .Query(x=>x.IsActive==IsActive)
                   .Select();
        }

        public Company GetCompanyById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
