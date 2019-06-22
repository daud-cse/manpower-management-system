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
  
    public interface IVendorInfoService
    {
        VendorInfo Find(params object[] keyValues);
        void Insert(VendorInfo entity);
        void Update(VendorInfo entity);
        void Delete(object id);
        void Delete(VendorInfo entity);
        IEnumerable<VendorInfo> GetAllVendorInfo(int GlobalCompanyId);
        IEnumerable<VendorInfo> GetActiveVendorInfo(bool IsActive);
        VendorInfo GetVendorInfoById(int Id, int GlobalCompanyId);
        VendorInfo newVendorInfo(int GlobalCompanyId);

    }
    public class VendorInfoService : Service<VendorInfo>, IVendorInfoService
    {
        private readonly IRepositoryAsync<VendorInfo> _repository;
        private readonly IVendorTypeService _iVendorTypeService;
        public VendorInfoService(

              IRepositoryAsync<VendorInfo> repository,
            IVendorTypeService iVendorTypeService

            )
            : base(repository)
        {
            _repository = repository;
            _iVendorTypeService = iVendorTypeService;

        }
        public IEnumerable<VendorInfo> GetAllVendorInfo(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId)
                              .Include(x=>x.VendorType)
                              .Select();
        }
        public VendorInfo newVendorInfo(int GlobalCompanyId)
        {

            VendorInfo objVendorInfo = new VendorInfo();


            var lstVendorType = new List<KeyValuePair<int, string>>();
            _iVendorTypeService.GetActiveVendorType(true).ToList().ForEach(delegate(VendorType item)
            {
                lstVendorType.Add(new KeyValuePair<int, string>(item.VendorTypeId, item.VendorTypeName));
            });
            objVendorInfo.kvpVendorType = lstVendorType;
            return objVendorInfo;
        }
        public IEnumerable<VendorInfo> GetActiveVendorInfo(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public VendorInfo GetVendorInfoById(int Id, int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.ID == Id && x.GlobalCompanyId==GlobalCompanyId)
                .Select().FirstOrDefault();
        }

    }
}
