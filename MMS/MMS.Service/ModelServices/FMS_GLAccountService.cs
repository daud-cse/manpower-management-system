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
   
    public interface IFMS_GLAccountService
    {
        FMS_GLAccount Find(params object[] keyValues);
        void Insert(FMS_GLAccount entity);
        void Update(FMS_GLAccount entity);
        void Delete(object id);
        void Delete(FMS_GLAccount entity);
        IEnumerable<FMS_GLAccount> GetAllFMS_GLAccount(int GlobalCompanyId);
        IEnumerable<FMS_GLAccount> GetActiveFMS_GLAccount(bool IsActive);
        FMS_GLAccount GetFMS_GLAccountById(int Id);
        IEnumerable<FMS_GLAccount> GetFMS_GLAccountList(int GlobalCompanyId,int levelCode, bool hasChild);
        IEnumerable<FMS_GLAccount> GetFMS_GLAccountList(int GlobalCompanyId,bool hasChild);
        IEnumerable<FMS_GLAccount> GetFMS_GLAccountList(int GlobalCompanyId,int levelCode, bool hasChild,bool IsSubsidyExist);

    }
    public class FMS_GLAccountService : Service<FMS_GLAccount>, IFMS_GLAccountService
    {
        private readonly IRepositoryAsync<FMS_GLAccount> _repository;

        public FMS_GLAccountService(

              IRepositoryAsync<FMS_GLAccount> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<FMS_GLAccount> GetAllFMS_GLAccount(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId).Select();
        }
        public IEnumerable<FMS_GLAccount> GetActiveFMS_GLAccount(bool IsActive)
        {
            return _repository
                   .Query(x => x.IsActive == IsActive)
                   .Select();
        }

        public FMS_GLAccount GetFMS_GLAccountById(int Id)
        {
            return _repository
                .Query(x => x.GLAccountId == Id)
                .Select().FirstOrDefault();
        }

        public IEnumerable<FMS_GLAccount> GetFMS_GLAccountList(int GlobalCompanyId,int levelCode, bool hasChild)
        {
            return _repository
                   .Query(x => x.IsActive == true && x.HasChild == hasChild && x.GlobalCompanyId == GlobalCompanyId)
                   .Select();
        }
        public IEnumerable<FMS_GLAccount> GetFMS_GLAccountList(int GlobalCompanyId,bool hasChild)
        {
            return _repository
                   .Query(x => x.IsActive == true && x.HasChild == hasChild && x.GlobalCompanyId == GlobalCompanyId)
                   .Select();
        }
        public IEnumerable<FMS_GLAccount> GetFMS_GLAccountList(int GlobalCompanyId, int levelCode, bool hasChild, bool IsSubsidyExist)
        {
            return _repository
                  .Query(x => x.IsActive == true && x.HasChild == hasChild && x.IsSubsidyExist == IsSubsidyExist && x.GlobalCompanyId == GlobalCompanyId)
                  .Select();
        }

    }
}
