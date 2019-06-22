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
   
    public interface IFMS_PaymentReceivedTypeService
    {
        FMS_PaymentReceivedType Find(params object[] keyValues);
        void Insert(FMS_PaymentReceivedType entity);
        void Update(FMS_PaymentReceivedType entity);
        void Delete(object id);
        void Delete(FMS_PaymentReceivedType entity);
        IEnumerable<FMS_PaymentReceivedType> GetAllFMS_PaymentReceivedType();
        IEnumerable<FMS_PaymentReceivedType> GetActiveFMS_PaymentReceivedType(bool IsActive);
        FMS_PaymentReceivedType GetFMS_PaymentReceivedTypeById(int Id);

    }
    public class FMS_PaymentReceivedTypeService : Service<FMS_PaymentReceivedType>, IFMS_PaymentReceivedTypeService
    {
        private readonly IRepositoryAsync<FMS_PaymentReceivedType> _repository;

        public FMS_PaymentReceivedTypeService(

              IRepositoryAsync<FMS_PaymentReceivedType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<FMS_PaymentReceivedType> GetAllFMS_PaymentReceivedType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<FMS_PaymentReceivedType> GetActiveFMS_PaymentReceivedType(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public FMS_PaymentReceivedType GetFMS_PaymentReceivedTypeById(int Id)
        {
            return _repository
                .Query(x => x.PRTypeId == Id)
                .Select().FirstOrDefault();
        }

    }
}
