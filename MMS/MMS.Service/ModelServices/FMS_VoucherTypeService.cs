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
   
    public interface IFMS_VoucherTypeService
    {
        FMS_VoucherType Find(params object[] keyValues);
        void Insert(FMS_VoucherType entity);
        void Update(FMS_VoucherType entity);
        void Delete(object id);
        void Delete(FMS_VoucherType entity);
        IEnumerable<FMS_VoucherType> GetAllFMS_VoucherType();
        IEnumerable<FMS_VoucherType> GetActiveFMS_VoucherType(bool IsActive);
        FMS_VoucherType GetFMS_VoucherTypeById(int Id);

    }
    public class FMS_VoucherTypeService : Service<FMS_VoucherType>, IFMS_VoucherTypeService
    {
        private readonly IRepositoryAsync<FMS_VoucherType> _repository;

        public FMS_VoucherTypeService(

              IRepositoryAsync<FMS_VoucherType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<FMS_VoucherType> GetAllFMS_VoucherType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<FMS_VoucherType> GetActiveFMS_VoucherType(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public FMS_VoucherType GetFMS_VoucherTypeById(int Id)
        {
            return _repository
                .Query(x => x.VoucherTypeId == Id)
                .Select().FirstOrDefault();
        }

    }
}
