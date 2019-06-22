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
  
   public interface IApplicantFileLotService
   {
       ApplicantFileLot Find(params object[] keyValues);
       void Insert(ApplicantFileLot entity);
       void Update(ApplicantFileLot entity);
       void Delete(object id);
       void Delete(ApplicantFileLot entity);
       IEnumerable<ApplicantFileLot> GetAllApplicantFileLot();
       IEnumerable<ApplicantFileLot> GetActiveApplicantFileLot(bool IsActive);
       ApplicantFileLot GetApplicantFileLotById(int Id);

   }
   public class ApplicantFileLotService : Service<ApplicantFileLot>, IApplicantFileLotService
   {
       private readonly IRepositoryAsync<ApplicantFileLot> _repository;

       public ApplicantFileLotService(

             IRepositoryAsync<ApplicantFileLot> repository

           )
           : base(repository)
       {
           _repository = repository;

       }
       public IEnumerable<ApplicantFileLot> GetAllApplicantFileLot()
       {
           return _repository.Query().Select();
       }
       public IEnumerable<ApplicantFileLot> GetActiveApplicantFileLot(bool IsActive)
       {
           return _repository
                  .Query()
                  .Select();
       }

       public ApplicantFileLot GetApplicantFileLotById(int Id)
       {
           return _repository
               .Query(x => x.ID == Id)
               .Select().FirstOrDefault();
       }

   }
}
