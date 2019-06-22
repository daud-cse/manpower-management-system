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
   
    public interface ICompanyContentService
    {
        CompanyContent Find(params object[] keyValues);
        void Insert(CompanyContent entity);
        void Update(CompanyContent entity);
        void Delete(object id);
        void Delete(CompanyContent entity);
        IEnumerable<CompanyContent> GetAllCompanyContent();
        IEnumerable<CompanyContent> GetActiveCompanyContent(bool IsActive);
        CompanyContent GetCompanyContentById(int Id);

    }
    public class CompanyContentService : Service<CompanyContent>, ICompanyContentService
    {
        private readonly IRepositoryAsync<CompanyContent> _repository;

        public CompanyContentService(

              IRepositoryAsync<CompanyContent> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<CompanyContent> GetAllCompanyContent()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<CompanyContent> GetActiveCompanyContent(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public CompanyContent GetCompanyContentById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
