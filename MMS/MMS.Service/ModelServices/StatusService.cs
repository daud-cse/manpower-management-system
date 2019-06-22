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
   
   public interface IStatusService
   {
       Status Find(params object[] keyValues);
       void Insert(Status entity);
       void Update(Status entity);
       void Delete(object id);
       void Delete(Status entity);
       IEnumerable<Status> GetAllStatus();
       IEnumerable<Status> GetActiveStatus(bool IsActive);
       Status GetStatusById(int Id);

   }
   public class StatusService : Service<Status>, IStatusService
   {
       private readonly IRepositoryAsync<Status> _repository;

       public StatusService(

             IRepositoryAsync<Status> repository

           )
           : base(repository)
       {
           _repository = repository;

       }
       public IEnumerable<Status> GetAllStatus()
       {
           return _repository.Query().Select();
       }
       public IEnumerable<Status> GetActiveStatus(bool IsActive)
       {
           return _repository
                  .Query()
                  .Select();
       }

       public Status GetStatusById(int Id)
       {
           return _repository
               .Query(x => x.ID == Id)
               .Select().FirstOrDefault();
       }

   }
}
