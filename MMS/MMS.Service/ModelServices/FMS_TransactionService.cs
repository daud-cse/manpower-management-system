using MMS.Entities.Models;
using MMS.Entities.StoredProcedures;
using MMS.Entities.ViewModels;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ModelServices
{
   
    public interface IFMS_TransactionService
    {
        FMS_Transaction Find(params object[] keyValues);
        void Insert(FMS_Transaction entity);
        void Update(FMS_Transaction entity);
        void Delete(object id);
        void Delete(FMS_Transaction entity);
        IEnumerable<FMS_Transaction> GetAllFMS_Transaction(int GlobalCompanyId);

        IEnumerable<FMS_Transaction> UnApprovedTransactionList(int GlobalCompanyId,bool IsApproved , DateTime CurrentDate);
        IEnumerable<FMS_Transaction> GetActiveFMS_Transaction(int GlobalCompanyId,bool IsActive);
        FMS_Transaction GetFMS_TransactionById(int TransactionId, int GlobalCompanyId);
        string FMS_TransactionApproved(int GlobalCompanyId,int TransactionId,bool IsApproved,string UserId);
        IEnumerable<OpponentTransactionVM> FMS_GetOpponentTransactionByVoucherId(int GlobalCompanyId,string VoucherId, int GLAccountId,string SearchItem);

    }
    public class FMS_TransactionService : Service<FMS_Transaction>, IFMS_TransactionService
    {
        private readonly IRepositoryAsync<FMS_Transaction> _repository;
        private readonly IStoredProcedures _storedProcedures;
        public FMS_TransactionService(

              IRepositoryAsync<FMS_Transaction> repository
            ,IStoredProcedures storedProcedures

            )
            : base(repository)
        {
            _repository = repository;
            _storedProcedures = storedProcedures;

        }
       public IEnumerable<FMS_Transaction> UnApprovedTransactionList(int GlobalCompanyId,bool IsApproved, DateTime CurrentDate)
        {
            return _repository.Query(x => x.IsApproved == IsApproved && x.TransactionDate == CurrentDate && x.GlobalCompanyId == GlobalCompanyId)
                .Include(x => x.FMS_VoucherType)
                .Select();
        }
       public IEnumerable<FMS_Transaction> GetAllFMS_Transaction(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId)
                .Include(x=>x.FMS_VoucherType)
                .Select();
        }
        public IEnumerable<FMS_Transaction> GetActiveFMS_Transaction(int GlobalCompanyId,bool IsActive)
        {
            return _repository
                   .Query(x => x.GlobalCompanyId == GlobalCompanyId)
                   .Select();
        }
        public IEnumerable<OpponentTransactionVM> FMS_GetOpponentTransactionByVoucherId(int GlobalCompanyId,string VoucherId, int GLAccountId, string SearchItem)
        {
            return _storedProcedures.GetOpponentTransactionByVoucherId(GlobalCompanyId,VoucherId, GLAccountId, SearchItem);
        }

        public string FMS_TransactionApproved(int GlobalCompanyId,int TransactionId,bool IsApproved,string UserId)
        {
            return _storedProcedures.FMS_TransactionApproved(GlobalCompanyId,TransactionId, IsApproved,UserId); 
        }
        public FMS_Transaction GetFMS_TransactionById(int TransactionId, int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.TransactionId == TransactionId && x.GlobalCompanyId == GlobalCompanyId)
                .Include(x=>x.FMS_TransactionDet)
                .Select().FirstOrDefault();
        }

    }
}
