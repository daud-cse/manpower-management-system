using MMS.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ModelServices
{

    public interface ICompanyWiseApplicantService
    {
        CompanyWiseApplicant Find(params object[] keyValues);
        void Insert(CompanyWiseApplicant entity);
        void Update(CompanyWiseApplicant entity);
        void Delete(object id);
        void Delete(CompanyWiseApplicant entity);
        IEnumerable<CompanyWiseApplicant> GetAllCompanyWiseApplicant();
        IEnumerable<CompanyWiseApplicant> GetActiveCompanyWiseApplicant(bool IsActive);
        CompanyWiseApplicant GetCompanyWiseApplicantById(int Id);
        CompanyWiseApplicant newCompanyWiseApplicant();
        bool SaveCompanyWiseApplicant(List<CompanyWiseApplicant> lstCompanyWiseApplicant, IUnitOfWork _unitOfWork, string UserId);
        CompanyWiseApplicant GetCompanyWiseApplicantByApplicantId(int ApplicantId);

    }
    public class CompanyWiseApplicantService : Service<CompanyWiseApplicant>, ICompanyWiseApplicantService
    {
        private readonly IRepositoryAsync<CompanyWiseApplicant> _repository;
        private readonly ICompanyService _iCompanyService;
        public CompanyWiseApplicantService(

              IRepositoryAsync<CompanyWiseApplicant> repository,
               ICompanyService iCompanyService

            )
            : base(repository)
        {
            _iCompanyService = iCompanyService;
            _repository = repository;

        }
        public IEnumerable<CompanyWiseApplicant> GetAllCompanyWiseApplicant()
        {
            return _repository
                .Query()
                .Include(x=>x.Company)
                .Include(x => x.Applicant)
                .Select();
        }

        public bool SaveCompanyWiseApplicant(List<CompanyWiseApplicant> lstCompanyWiseApplicant, IUnitOfWork _unitOfWork, string UserId)
        {
            lstCompanyWiseApplicant.ToList().ForEach(delegate(CompanyWiseApplicant item)
            {
                item.IsActive = true;
                item.SetBy = UserId;
                item.SetDate = DateTime.Now;
                _repository.Insert(item);
                _unitOfWork.SaveChanges();
            });
            return true;
        }
        public CompanyWiseApplicant newCompanyWiseApplicant()
       {
           var lstCompany = new List<KeyValuePair<int, string>>();

           _iCompanyService.GetActiveCompany(true).ToList().ForEach(delegate(Company item)
           {
               lstCompany.Add(new KeyValuePair<int, string>(item.ID, item.Name));
           });
           CompanyWiseApplicant objCompanyWiseApplicant=new CompanyWiseApplicant();
           objCompanyWiseApplicant.kvpCompany=lstCompany;
           return objCompanyWiseApplicant;

       }
        public IEnumerable<CompanyWiseApplicant> GetActiveCompanyWiseApplicant(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public CompanyWiseApplicant GetCompanyWiseApplicantById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Include(x=>x.Company)
                .Include(x=>x.Applicant)
                .Select().FirstOrDefault();
        }
        public CompanyWiseApplicant GetCompanyWiseApplicantByApplicantId(int ApplicantId)
        {
            return _repository
                .Query(x => x.ApplicantId == ApplicantId)
                .Select().FirstOrDefault();
        }


    }
}
