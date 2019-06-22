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
    
    public interface IApplicantContentService
    {
        ApplicantContent Find(params object[] keyValues);
        void Insert(ApplicantContent entity);
        void Update(ApplicantContent entity);
        void Delete(object id);
        void Delete(ApplicantContent entity);
        IEnumerable<ApplicantContent> GetAllApplicantContent();
        IEnumerable<ApplicantContent> GetActiveApplicantContent(bool IsActive);
        ApplicantContent GetApplicantContentById(int Id);

    }
    public class ApplicantContentService : Service<ApplicantContent>, IApplicantContentService
    {
        private readonly IRepositoryAsync<ApplicantContent> _repository;

        public ApplicantContentService(

              IRepositoryAsync<ApplicantContent> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<ApplicantContent> GetAllApplicantContent()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<ApplicantContent> GetActiveApplicantContent(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public ApplicantContent GetApplicantContentById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
