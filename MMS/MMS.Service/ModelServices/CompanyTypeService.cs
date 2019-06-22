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
 

  public interface ICompanyTypeService
  {
      CompanyType Find(params object[] keyValues);
      void Insert(CompanyType entity);
      void Update(CompanyType entity);
      void Delete(object id);
      void Delete(CompanyType entity);
      IEnumerable<CompanyType> GetAllCompanyType();
      IEnumerable<CompanyType> GetActiveCompanyType(bool IsActive);
      CompanyType GetCompanyTypeById(int Id);

  }
  public class CompanyTypeService : Service<CompanyType>, ICompanyTypeService
  {
      private readonly IRepositoryAsync<CompanyType> _repository;

      public CompanyTypeService(

            IRepositoryAsync<CompanyType> repository

          )
          : base(repository)
      {
          _repository = repository;

      }
      public IEnumerable<CompanyType> GetAllCompanyType()
      {
          return _repository.Query().Select();
      }
      public IEnumerable<CompanyType> GetActiveCompanyType(bool IsActive)
      {
          return _repository
                 .Query(x => x.IsActive == IsActive)
                 .Select();
      }

      public CompanyType GetCompanyTypeById(int Id)
      {
          return _repository
              .Query(x => x.ID == Id)
              .Select().FirstOrDefault();
      }

  }
}
