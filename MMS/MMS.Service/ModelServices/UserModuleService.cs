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
  
     public interface IUserModuleService
   {
        UserModule Find(params object[] keyValues);
       void Insert( UserModule entity);
       void Update( UserModule entity);
       void Delete(object id);
       void Delete( UserModule entity);
       IEnumerable<UserModule> GetAllUserModule(int GlobalCompanyId);
       IEnumerable< UserModule> GetActiveUserModule(int GlobalCompanyId,bool IsActive);
       UserModule GetUserModuleById(int Id, int GlobalCompanyId);
       UserModule GetUserModuleByUserId(int userId, int GlobalCompanyId);

   }
   public class  UserModuleService : Service< UserModule>, IUserModuleService
   {
       private readonly IRepositoryAsync< UserModule> _repository;

       public  UserModuleService(

             IRepositoryAsync< UserModule> repository

           )
           : base(repository)
       {
           _repository = repository;

       }
       public IEnumerable<UserModule> GetAllUserModule(int GlobalCompanyId)
       {
           return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId)
               .Include(x=>x.User)
               .Include(x=>x.Module)
               .Select();
       }
       public IEnumerable< UserModule> GetActiveUserModule(int GlobalCompanyId,bool IsActive)
       {
           return _repository
                  .Query(x => x.GlobalCompanyId == GlobalCompanyId)
                  .Select();
       }

       public UserModule GetUserModuleById(int Id, int GlobalCompanyId)
       {
           return _repository
               .Query(x => x.ID == Id && x.GlobalCompanyId== GlobalCompanyId)
               .Select().FirstOrDefault();
       }
       public UserModule GetUserModuleByUserId(int userId, int GlobalCompanyId)
       {
           return _repository
              .Query(x => x.UserId == userId && x.GlobalCompanyId == GlobalCompanyId)
              .Select().FirstOrDefault();
       }

   }
}
