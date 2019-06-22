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
 
   public interface ISexService
   {
        Sex Find(params object[] keyValues);
       void Insert( Sex entity);
       void Update( Sex entity);
       void Delete(object id);
       void Delete( Sex entity);
       IEnumerable<Sex> GetAllSex();
       IEnumerable<Sex> GetActiveSex(bool IsActive);
        Sex GetSexById(int Id);

   }
   public class  SexService : Service<Sex>, ISexService
   {
       private readonly IRepositoryAsync<Sex> _repository;

       public  SexService(

             IRepositoryAsync<Sex> repository

           )
           : base(repository)
       {
           _repository = repository;

       }
       public IEnumerable<Sex> GetAllSex()
       {
           return _repository.Query().Select();
       }
       public IEnumerable<Sex> GetActiveSex(bool IsActive)
       {
           return _repository
                  .Query()
                  .Select();
       }

       public  Sex GetSexById(int Id)
       {
           return _repository
               .Query(x => x.ID == Id)
               .Select().FirstOrDefault();
       }

   }
}
