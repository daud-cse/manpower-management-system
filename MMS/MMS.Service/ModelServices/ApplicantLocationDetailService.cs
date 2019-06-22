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
   
    public interface IApplicantLocationDetailService
    {
        ApplicantLocationDetail Find(params object[] keyValues);
        void Insert(ApplicantLocationDetail entity);
        void Update(ApplicantLocationDetail entity);
        void Delete(object id);
        void Delete(ApplicantLocationDetail entity);
        IEnumerable<ApplicantLocationDetail> GetAllApplicantLocationDetail(int ApplicantMovementID, int LocationID, int ApplicantID);
        IEnumerable<ApplicantLocationDetail> GetActiveApplicantLocationDetail(bool IsActive);
        ApplicantLocationDetail GetApplicantLocationDetailById(int Id);

    }
    public class ApplicantLocationDetailService : Service<ApplicantLocationDetail>, IApplicantLocationDetailService
    {
        private readonly IRepositoryAsync<ApplicantLocationDetail> _repository;

        public ApplicantLocationDetailService(

              IRepositoryAsync<ApplicantLocationDetail> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<ApplicantLocationDetail> GetAllApplicantLocationDetail(int ApplicantMovementID, int LocationID, int ApplicantID)
        {
            return _repository.
                Query(x => x.ApplicantMovementID == ApplicantMovementID && x.LocationID == LocationID && x.ApplicantID == ApplicantID)
                .Include(x=>x.LocationMapDetail)
                .Select();
        }
        public IEnumerable<ApplicantLocationDetail> GetActiveApplicantLocationDetail(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public ApplicantLocationDetail GetApplicantLocationDetailById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
