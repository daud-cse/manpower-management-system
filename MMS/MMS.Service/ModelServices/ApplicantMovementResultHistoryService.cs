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
  
    public interface IApplicantMovementResultHistoryService
    {
        ApplicantMovementResultHistory Find(params object[] keyValues);
        void Insert(ApplicantMovementResultHistory entity);
        void Update(ApplicantMovementResultHistory entity);
        void Delete(object id);
        void Delete(ApplicantMovementResultHistory entity);
        IEnumerable<ApplicantMovementResultHistory> GetAllApplicantMovementResultHistory();
        IEnumerable<ApplicantMovementResultHistory> GetActiveApplicantMovementResultHistory(bool IsActive);
        ApplicantMovementResultHistory GetApplicantMovementResultHistoryById(int Id);

    }
    public class ApplicantMovementResultHistoryService : Service<ApplicantMovementResultHistory>, IApplicantMovementResultHistoryService
    {
        private readonly IRepositoryAsync<ApplicantMovementResultHistory> _repository;

        public ApplicantMovementResultHistoryService(

              IRepositoryAsync<ApplicantMovementResultHistory> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<ApplicantMovementResultHistory> GetAllApplicantMovementResultHistory()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<ApplicantMovementResultHistory> GetActiveApplicantMovementResultHistory(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public ApplicantMovementResultHistory GetApplicantMovementResultHistoryById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
