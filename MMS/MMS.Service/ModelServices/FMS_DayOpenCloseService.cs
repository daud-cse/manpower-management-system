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
  

   public interface IFMS_DayOpenCloseService
   {
       FMS_DayOpenClose Find(params object[] keyValues);
       void Insert(FMS_DayOpenClose entity);
       void Update(FMS_DayOpenClose entity);
       void Delete(object id);
       void Delete(FMS_DayOpenClose entity);
       IEnumerable<FMS_DayOpenClose> GetAllFMS_DayOpenClose(int GlobalCompanyId);
       IEnumerable<FMS_DayOpenClose> GetActiveFMS_DayOpenClose(int GlobalCompanyId,bool IsActive);
       FMS_DayOpenClose GetFMS_DayOpenClose(int GlobalCompanyId,DateTime opendate);
       FMS_DayOpenClose GetFMS_DayOpenClose(int GlobalCompanyId,DateTime opendate,bool IsDayCloseDone);

   }
   public class FMS_DayOpenCloseService : Service<FMS_DayOpenClose>, IFMS_DayOpenCloseService
   {
       private readonly IRepositoryAsync<FMS_DayOpenClose> _repository;

       public FMS_DayOpenCloseService(

             IRepositoryAsync<FMS_DayOpenClose> repository

           )
           : base(repository)
       {
           _repository = repository;

       }
       public IEnumerable<FMS_DayOpenClose> GetAllFMS_DayOpenClose(int GlobalCompanyId)
       {
           return _repository.Query(x=>x.GlobalCompanyId==GlobalCompanyId).Select();
       }
       public IEnumerable<FMS_DayOpenClose> GetActiveFMS_DayOpenClose(int GlobalCompanyId,bool IsActive)
       {
           return _repository
                  .Query(x=>x.GlobalCompanyId==GlobalCompanyId)
                  .Select();
       }

       public FMS_DayOpenClose GetFMS_DayOpenClose(int GlobalCompanyId,DateTime opendate)
       {
           return _repository
               .Query(x => x.GlobalCompanyId == GlobalCompanyId && x.OpenDate == opendate.Date)
               .Select().FirstOrDefault();
       }
     public  FMS_DayOpenClose GetFMS_DayOpenClose(int GlobalCompanyId,DateTime opendate, bool IsDayCloseDone)
       {
           return _repository
               .Query(x => x.OpenDate == opendate.Date && x.IsDayClosed == IsDayCloseDone && x.GlobalCompanyId==GlobalCompanyId)
               .Select().FirstOrDefault();
       }

   }
}
