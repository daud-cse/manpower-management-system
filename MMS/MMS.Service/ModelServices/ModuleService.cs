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
   
   public interface IModuleService
   {
        Module Find(params object[] keyValues);
       void Insert( Module entity);
       void Update( Module entity);
       void Delete(object id);
       void Delete( Module entity);
       IEnumerable< Module> GetAllModule();
       IEnumerable< Module> GetActiveModule(bool IsActive);
        Module GetModuleById(int Id);

        List<KeyValuePair<int,string>> KvpGetActiveModule(bool IsActive);


   }
   public class  ModuleService : Service< Module>, IModuleService
   {
       private readonly IRepositoryAsync< Module> _repository;

       public  ModuleService(

             IRepositoryAsync< Module> repository

           )
           : base(repository)
       {
           _repository = repository;

       }
       public IEnumerable< Module> GetAllModule()
       {
           return _repository.Query().Select();
       }
       public IEnumerable< Module> GetActiveModule(bool IsActive)
       {
           return _repository
                  .Query()
                  .Select();
       }

       public  Module GetModuleById(int Id)
       {
           return _repository
               .Query(x => x.ModuleId == Id && x.IsActive==true)
               .Select().FirstOrDefault();
       }
       public List<KeyValuePair<int, string>> KvpGetActiveModule(bool IsActive)
       {
           var lstModule = new List<KeyValuePair<int, string>>();
           _repository.Query(x=>x.IsActive==true).Select().ToList().ForEach(delegate(Module item)
           {
               lstModule.Add(new KeyValuePair<int, string>(item.ModuleId, item.Name));
           });
           return lstModule;
       }


   }
}
