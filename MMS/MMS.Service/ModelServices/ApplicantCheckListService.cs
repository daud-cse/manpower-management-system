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

    public interface IApplicantCheckListService
  {
      ApplicantCheckList Find(params object[] keyValues);
      void Insert(ApplicantCheckList entity);
      void Update(ApplicantCheckList entity);
      void Delete(object id);
      void Delete(ApplicantCheckList entity);
      IEnumerable<ApplicantCheckList> GetAllApplicantCheckList();
      IEnumerable<ApplicantCheckList> GetActiveApplicantCheckList(bool IsActive);
      ApplicantCheckList GetApplicantCheckListById(int Id);
      IEnumerable<ApplicantCheckList> GetApplicantCheckListByApplicantId(int GlobalCompanyId,int ApplicantId);

  }
    public class ApplicantCheckListService : Service<ApplicantCheckList>, IApplicantCheckListService
  {
      private readonly IRepositoryAsync<ApplicantCheckList> _repository;

      public ApplicantCheckListService(

            IRepositoryAsync<ApplicantCheckList> repository

          )
          : base(repository)
      {
          _repository = repository;

      }
      public IEnumerable<ApplicantCheckList> GetAllApplicantCheckList()
      {
          return _repository.Query().Select();
      }
      public IEnumerable<ApplicantCheckList> GetActiveApplicantCheckList(bool IsActive)
      {
          return _repository
                 .Query()
                 .Select();
      }

      public ApplicantCheckList GetApplicantCheckListById(int Id)
      {
          return _repository
              .Query(x => x.ID == Id)
              .Select().FirstOrDefault();
      }

      public IEnumerable<ApplicantCheckList> GetApplicantCheckListByApplicantId(int GlobalCompanyId,int ApplicantId)
      {
          return _repository.Query(x => x.ApplicantID == ApplicantId && x.GlobalCompanyId == GlobalCompanyId)
              .Include(x=>x.CheckListGroupMapping)
              .Include(x => x.CheckListGroupMapping.CheckList).Select();
      }

  }
}
