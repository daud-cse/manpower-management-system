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
   
    public interface IFMS_TransactionDetService
    {
        FMS_TransactionDet Find(params object[] keyValues);
        void Insert(FMS_TransactionDet entity);
        void Update(FMS_TransactionDet entity);
        void Delete(object id);
        void Delete(FMS_TransactionDet entity);
        IEnumerable<FMS_TransactionDet> GetAllFMS_TransactionDet();
        IEnumerable<FMS_TransactionDet> GetTransactionDet(int TranasctionId);
        IEnumerable<FMS_TransactionDet> GetActiveFMS_TransactionDet(bool IsActive);
        FMS_TransactionDet GetFMS_TransactionDetById(int TransactionDetId);

    }
    public class FMS_TransactionDetService : Service<FMS_TransactionDet>, IFMS_TransactionDetService
    {
        private readonly IRepositoryAsync<FMS_TransactionDet> _repository;

        public FMS_TransactionDetService(

              IRepositoryAsync<FMS_TransactionDet> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<FMS_TransactionDet> GetAllFMS_TransactionDet()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<FMS_TransactionDet> GetActiveFMS_TransactionDet(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }
        public IEnumerable<FMS_TransactionDet> GetTransactionDet(int TranasctionId)
        {
            return _repository.Query(x => x.TransactionId == TranasctionId).Select();
        }
        public FMS_TransactionDet GetFMS_TransactionDetById(int TransactionDetId)
        {
            return _repository
                .Query(x => x.TransactionDetId == TransactionDetId)
                .Select().FirstOrDefault();
        }

    }
}
