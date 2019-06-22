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
   
    public interface IApplicantLotStatusService
    {
        ApplicantLotStatu Find(params object[] keyValues);
        void Insert(ApplicantLotStatu entity);
        void Update(ApplicantLotStatu entity);
        void Delete(object id);
        void Delete(ApplicantLotStatu entity);
        IEnumerable<ApplicantLotStatu> GetAllApplicantLotStatu();
        IEnumerable<ApplicantLotStatu> GetActiveApplicantLotStatu(bool IsActive);
        ApplicantLotStatu GetApplicantLotStatuById(int Id);

    }
    public class ApplicantLotStatuService : Service<ApplicantLotStatu>, IApplicantLotStatusService
    {
        private readonly IRepositoryAsync<ApplicantLotStatu> _repository;

        public ApplicantLotStatuService(

              IRepositoryAsync<ApplicantLotStatu> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<ApplicantLotStatu> GetAllApplicantLotStatu()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<ApplicantLotStatu> GetActiveApplicantLotStatu(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public ApplicantLotStatu GetApplicantLotStatuById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
