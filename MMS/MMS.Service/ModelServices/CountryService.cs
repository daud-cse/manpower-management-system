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
    
    public interface ICountryService
    {
        Country Find(params object[] keyValues);
        void Insert(Country entity);
        void Update(Country entity);
        void Delete(object id);
        void Delete(Country entity);
        IEnumerable<Country> GetAllCountry(int GlobalCompanyId);
        IEnumerable<Country> GetAllCountry();
        IEnumerable<Country> GetActiveCountry(int GlobalCompanyId,bool IsActive);
        Country GetCountryById(int Id, int GlobalCompanyId);

    }
    public class CountryService : Service<Country>, ICountryService
    {
        private readonly IRepositoryAsync<Country> _repository;

        public CountryService(

              IRepositoryAsync<Country> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<Country> GetAllCountry(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId).Select();
        }
        public IEnumerable<Country> GetAllCountry()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<Country> GetActiveCountry(int GlobalCompanyId,bool IsActive)
        {
            return _repository
                   .Query(x => x.IsActive == IsActive && x.GlobalCompanyId == GlobalCompanyId)
                   .Select();
        }

        public Country GetCountryById(int Id, int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.ID == Id && x.GlobalCompanyId == GlobalCompanyId)
                .Select().FirstOrDefault();
        }

    }
}
