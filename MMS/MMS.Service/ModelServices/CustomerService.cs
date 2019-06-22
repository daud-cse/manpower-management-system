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
   
    public interface ICustomerService
    {
        Customer Find(params object[] keyValues);
        void Insert(Customer entity);
        void Update(Customer entity);
        void Delete(object id);
        void Delete(Customer entity);
        IEnumerable<Customer> GetAllCustomer(int GlobalCompanyId);
        IEnumerable<Customer> GetActiveCustomer(int GlobalCompanyId,bool IsActive);
        Customer GetCustomerById(int Id, int GlobalCompanyId);
        Customer newCustomer(int GlobalCompanyId);

    }
    public class CustomerService : Service<Customer>, ICustomerService
    {
        private readonly IRepositoryAsync<Customer> _repository;      
        private readonly IDistrictService _iDistrictService;
        private readonly IDivisionService _iDivisionService;       
        private readonly IUpazilaService _iUpazilaService;
        private readonly ICountryService _iCountryService;
        private readonly INationalityService _iNationalityService;
        private readonly ICustomerTypeService _iCustomerTypeService;

        public CustomerService(
                     
            IDivisionService iDivisionService,
            IDistrictService iDistrictService,           
            IUpazilaService iUpazilaService,
            INationalityService iNationalityService,
            ICountryService iCountryService,
              IRepositoryAsync<Customer> repository,
            ICustomerTypeService iCustomerTypeService
            )
            : base(repository)
        {
            _repository = repository;          
            _iDivisionService = iDivisionService;
            _iDistrictService = iDistrictService;           
            _iUpazilaService = iUpazilaService;
        _iNationalityService=iNationalityService;
        _iCountryService=iCountryService;
        _iCustomerTypeService = iCustomerTypeService;


        }

        public Customer newCustomer(int GlobalCompanyId)
        {
            var lstCustomerType = new List<KeyValuePair<int, string>>();
            _iCustomerTypeService.GetActiveCustomerType(true).ToList().ForEach(delegate(CustomerType item)
            {
                lstCustomerType.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });

            var lstNationality = new List<KeyValuePair<int, string>>();
            _iNationalityService.GetActiveNationality(true).ToList().ForEach(delegate(Nationality item)
            {
                lstNationality.Add(new KeyValuePair<int, string>(item.ID, item.Name));
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
            Customer objCustomer = new Customer();
            objCustomer.kvpDistrict = lstDistrict;
            objCustomer.kvpDivision = lstDivision;
            objCustomer.kvpUpazila = lstUpazila;
            objCustomer.kvpNationality = lstNationality;
            objCustomer.kvpCountry = lstCountry;
            objCustomer.kvpCustomerType = lstCustomerType;
            return objCustomer;

        }

        public IEnumerable<Customer> GetAllCustomer(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId).Select();
        }
        public IEnumerable<Customer> GetActiveCustomer(int GlobalCompanyId,bool IsActive)
        {
            return _repository
                   .Query(x => x.IsActive == IsActive && x.GlobalCompanyId == GlobalCompanyId)
                   .Include(x=>x.Division)
                   .Include(x => x.District)
                   .Include(x => x.Country)
                   .Select();
        }

        public Customer GetCustomerById(int Id, int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.ID == Id && x.GlobalCompanyId == GlobalCompanyId)
                .Select().FirstOrDefault();
        }

    }
}
