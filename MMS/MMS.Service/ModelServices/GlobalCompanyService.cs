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
   

    public interface IGlobalCompanyService
    {
        GlobalCompany Find(params object[] keyValues);
        void Insert(GlobalCompany entity);
        void Update(GlobalCompany entity);
        void Delete(object id);
        void Delete(GlobalCompany entity);
        IEnumerable<GlobalCompany> GetAllGlobalCompany();
        IEnumerable<GlobalCompany> GetActiveGlobalCompany(bool IsActive);
        GlobalCompany GetGlobalCompanyById(int Id);
        GlobalCompany GetGlobalCompanyByURL(string url);

    }
    public class GlobalCompanyService : Service<GlobalCompany>, IGlobalCompanyService
    {
        private readonly IRepositoryAsync<GlobalCompany> _repository;

        public GlobalCompanyService(

              IRepositoryAsync<GlobalCompany> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<GlobalCompany> GetAllGlobalCompany()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<GlobalCompany> GetActiveGlobalCompany(bool IsActive)
        {
            return _repository
                   .Query(x => x.IsActive == IsActive)
                   .Select();
        }

        public GlobalCompany GetGlobalCompanyById(int Id)
        {
            return _repository
                .Query(x => x.GlobalCompanyId == Id)
                .Select().FirstOrDefault();
        }
        public GlobalCompany GetGlobalCompanyByURL(string url)
        {
            return _repository
               .Query(x => x.Url == url)
               .Select().FirstOrDefault();
        }
    }
}
