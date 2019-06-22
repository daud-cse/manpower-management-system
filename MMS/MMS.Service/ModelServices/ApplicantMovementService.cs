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
   
    public interface IApplicantMovementService
    {
        ApplicantMovement Find(params object[] keyValues);
        void Insert(ApplicantMovement entity);
        void Update(ApplicantMovement entity);
        void Delete(object id);
        void Delete(ApplicantMovement entity);
        IEnumerable<ApplicantMovement> GetAllApplicantMovement();
        IEnumerable<ApplicantMovement> GetActiveApplicantMovement(bool IsActive);
        ApplicantMovement GetApplicantMovementById(int Id);
        ApplicantMovement GetApplicantMovementByapplicantAndlocationId(int GlobalCompanyId,int applicantId, int locationId);

    }
    public class ApplicantMovementService : Service<ApplicantMovement>, IApplicantMovementService
    {
        private readonly IRepositoryAsync<ApplicantMovement> _repository;

        public ApplicantMovementService(

              IRepositoryAsync<ApplicantMovement> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<ApplicantMovement> GetAllApplicantMovement()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<ApplicantMovement> GetActiveApplicantMovement(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }
        public ApplicantMovement GetApplicantMovementByapplicantAndlocationId(int GlobalCompanyId,int applicantId, int locationId)
        {
            return _repository
                   .Query(x => x.ApplicantID == applicantId & x.LocationID == locationId && x.GlobalCompanyId == GlobalCompanyId)
                   .Select().FirstOrDefault();
        }
        public ApplicantMovement GetApplicantMovementById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
