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

    public interface IApplicantTypeService
    {
        ApplicantType Find(params object[] keyValues);
        void Insert(ApplicantType entity);
        void Update(ApplicantType entity);
        void Delete(object id);
        void Delete(ApplicantType entity);
        IEnumerable<ApplicantType> GetAllApplicantType(int GlobalCompanyId);
        IEnumerable<ApplicantType> GetAllApplicantType();
        IEnumerable<ApplicantType> GetActiveApplicantType(int GlobalCompanyId, bool IsActive);
        List<KeyValuePair<int, string>> GetKVPApplicantType(int GlobalCompanyId, bool IsActive);
        ApplicantType GetApplicantTypeById(int Id, int GlobalCompanyId);

    }
    public class ApplicantTypeService : Service<ApplicantType>, IApplicantTypeService
    {
        private readonly IRepositoryAsync<ApplicantType> _repository;

        public ApplicantTypeService(

              IRepositoryAsync<ApplicantType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<ApplicantType> GetAllApplicantType(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId).Select();
        }
        public IEnumerable<ApplicantType> GetAllApplicantType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<ApplicantType> GetActiveApplicantType(int GlobalCompanyId, bool IsActive)
        {
            return _repository
                   .Query(x => x.IsActive == true && x.GlobalCompanyId == GlobalCompanyId)
                   .Select();
        }
        public List<KeyValuePair<int, string>> GetKVPApplicantType(int GlobalCompanyId, bool IsActive)
        {
            var lstkvpApplicantType = new List<KeyValuePair<int, string>>();
            _repository.Query(x => x.IsActive == true && x.GlobalCompanyId == GlobalCompanyId).Select().ToList().ForEach(delegate(ApplicantType item)
            {
                lstkvpApplicantType.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });

            return lstkvpApplicantType;
        }
        public ApplicantType GetApplicantTypeById(int Id, int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.ID == Id && x.GlobalCompanyId == GlobalCompanyId)
                .Select().FirstOrDefault();
        }

    }
}
