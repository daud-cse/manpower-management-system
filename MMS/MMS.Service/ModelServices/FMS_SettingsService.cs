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
  
  public interface IFMS_SettingsService
  {
      FMS_Settings Find(params object[] keyValues);
      void Insert(FMS_Settings entity);
      void Update(FMS_Settings entity);
      void Delete(object id);
      void Delete(FMS_Settings entity);
      FMS_Settings GetFMS_Settings(int GlobalCompanyId);
      IEnumerable<FMS_Settings> GetActiveFMS_Settings(bool IsActive);
      FMS_Settings GetFMS_SettingsById(int Id);

  }
  public class FMS_SettingsService : Service<FMS_Settings>, IFMS_SettingsService
  {
      private readonly IRepositoryAsync<FMS_Settings> _repository;

      public FMS_SettingsService(

            IRepositoryAsync<FMS_Settings> repository

          )
          : base(repository)
      {
          _repository = repository;

      }
      public FMS_Settings GetFMS_Settings(int GlobalCompanyId)
      {
          return _repository.Query(x=>x.GlobalCompanyId==GlobalCompanyId).Select().FirstOrDefault();
      }
      public IEnumerable<FMS_Settings> GetActiveFMS_Settings(bool IsActive)
      {
          return _repository
                 .Query()
                 .Select();
      }

      public FMS_Settings GetFMS_SettingsById(int Id)
      {
          return _repository
              .Query(x => x.ID == Id)
              .Select().FirstOrDefault();
      }

  }
}
