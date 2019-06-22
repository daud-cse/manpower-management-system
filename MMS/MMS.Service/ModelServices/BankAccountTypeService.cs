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
   
    public interface IBankAccountTypeService
    {
        BankAccountType Find(params object[] keyValues);
        void Insert(BankAccountType entity);
        void Update(BankAccountType entity);
        void Delete(object id);
        void Delete(BankAccountType entity);
        IEnumerable<BankAccountType> GetAllBankAccountType();
        IEnumerable<BankAccountType> GetAllBankAccountType(int GlobalCompanyId);
        IEnumerable<BankAccountType> GetActiveBankAccountType(int GlobalCompanyId,bool IsActive);
        BankAccountType GetBankAccountTypeById(int Id, int GlobalCompanyId);

    }
    public class BankAccountTypeService : Service<BankAccountType>, IBankAccountTypeService
    {
        private readonly IRepositoryAsync<BankAccountType> _repository;

        public BankAccountTypeService(

              IRepositoryAsync<BankAccountType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<BankAccountType> GetAllBankAccountType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<BankAccountType> GetAllBankAccountType(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId).Select();
        }
        public IEnumerable<BankAccountType> GetActiveBankAccountType(int GlobalCompanyId,bool IsActive)
        {
            return _repository
                   .Query(x => x.GlobalCompanyId == GlobalCompanyId)
                   .Select();
        }

        public BankAccountType GetBankAccountTypeById(int Id,int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.BankAccountTypeId == Id && x.GlobalCompanyId == GlobalCompanyId)
                .Select().FirstOrDefault();
        }

    }
}
