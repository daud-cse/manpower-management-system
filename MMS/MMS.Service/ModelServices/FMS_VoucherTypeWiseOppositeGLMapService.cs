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
   
    public interface IFMS_VoucherTypeWiseOppositeGLMapService
    {
        FMS_VoucherTypeWiseOppositeGLMap Find(params object[] keyValues);
        void Insert(FMS_VoucherTypeWiseOppositeGLMap entity);
        void Update(FMS_VoucherTypeWiseOppositeGLMap entity);
        void Delete(object id);
        void Delete(FMS_VoucherTypeWiseOppositeGLMap entity);
        IEnumerable<FMS_VoucherTypeWiseOppositeGLMap> GetAllFMS_VoucherTypeWiseOppositeGLMap(int GlobalCompanyId);
        IEnumerable<FMS_VoucherTypeWiseOppositeGLMap> GetActiveFMS_VoucherTypeWiseOppositeGLMap(int GlobalCompanyIdbool, bool IsActive);
        IEnumerable<FMS_VoucherTypeWiseOppositeGLMap> GetActiveFMS_VoucherTypeWiseOppositeGLMapById(int id,int GlobalCompanyId);
        IEnumerable<FMS_VoucherTypeWiseOppositeGLMap> GetOppositeGLMapByVoucherTypeId(int voucherTypeId, bool isActive,int GlobalCompanyId);
        FMS_VoucherTypeWiseOppositeGLMap newFMS_VoucherTypeWiseOppositeGLMap(int GlobalCompanyId,int VoucherTypeWiseOppositeGLMapId, int VoucherTpyeId);
        IEnumerable<FMS_VoucherTypeWiseOppositeGLMap> GetOppositeGLMapByVoucherTypeId(int voucherTypeId,int GlobalCompanyId);
        FMS_VoucherTypeWiseOppositeGLMap GetVoucherTypeWiseOppositeGLMap(int voucherId, int GLAccountId,int GlobalCompanyId);

    }
    public class FMS_VoucherTypeWiseOppositeGLMapService : Service<FMS_VoucherTypeWiseOppositeGLMap>, IFMS_VoucherTypeWiseOppositeGLMapService
    {
        private readonly IRepositoryAsync<FMS_VoucherTypeWiseOppositeGLMap> _repository;
        private readonly IFMS_VoucherTypeService _fMS_VoucherTypeService;
        private readonly IFMS_GLAccountService _ifMS_GLAccountService;
        public FMS_VoucherTypeWiseOppositeGLMapService(

              IRepositoryAsync<FMS_VoucherTypeWiseOppositeGLMap> repository
              ,IFMS_VoucherTypeService ifMS_VoucherTypeService
              ,IFMS_GLAccountService ifMS_GLAccountService
            )
            : base(repository)
        {
            _repository = repository;
        _ifMS_GLAccountService=ifMS_GLAccountService;
        _fMS_VoucherTypeService=ifMS_VoucherTypeService;

        }
        public FMS_VoucherTypeWiseOppositeGLMap newFMS_VoucherTypeWiseOppositeGLMap(int GlobalCompanyId,int VoucherTypeWiseOppositeGLMapId,int VoucherTpyeId)
        {
            FMS_VoucherTypeWiseOppositeGLMap objFMS_VoucherTypeWiseOppositeGLMap = new FMS_VoucherTypeWiseOppositeGLMap();
            var lstVoucherType = new List<KeyValuePair<int, string>>();
            _fMS_VoucherTypeService.GetActiveFMS_VoucherType(true).ToList().ForEach(delegate(FMS_VoucherType item)
            {
                lstVoucherType.Add(new KeyValuePair<int, string>(item.VoucherTypeId, item.VoucherTypeName));
            });
            var lstFMS_GLAccount = new List<KeyValuePair<int, string>>();
            _ifMS_GLAccountService.GetFMS_GLAccountList(GlobalCompanyId,3, false).ToList().ForEach(delegate(FMS_GLAccount item)
            {
                lstFMS_GLAccount.Add(new KeyValuePair<int, string>(item.GLAccountId, item.GLAccountName));
            });
            
            /////Alrady Map GL Remove From Dropdown

            //var lstGLMappedTypeWise = GetAllFMS_VoucherTypeWiseOppositeGLMap().ToList();
            //if (lstGLMappedTypeWise.Count > 1)
            //{
            //    lstGLMappedTypeWise.ForEach(delegate(FMS_VoucherTypeWiseOppositeGLMap item)
            //    {
            //        if (lstFMS_GLAccount.Count > 0)
            //        {
            //            if (VoucherTypeWiseOppositeGLMapId > 0)
            //            {
            //                if (VoucherTypeWiseOppositeGLMapId != item.VoucherTypeWiseOppositeGLMapId)
            //                {
            //                    lstFMS_GLAccount.RemoveAll(x => x.Key == item.GLAccountId);
            //                }

            //            }
            //            else
            //            {
            //                lstFMS_GLAccount.RemoveAll(x => x.Key == item.GLAccountId);
            //            }

            //        }
            //    });
            
            objFMS_VoucherTypeWiseOppositeGLMap.kvpGLAccount = lstFMS_GLAccount;
            objFMS_VoucherTypeWiseOppositeGLMap.kvpVoucherType = lstVoucherType;
            return objFMS_VoucherTypeWiseOppositeGLMap;
        }  
        public IEnumerable<FMS_VoucherTypeWiseOppositeGLMap> GetAllFMS_VoucherTypeWiseOppositeGLMap(int GlobalCompanyId)
        {
            return _repository.Query(x=>x.GlobalCompanyId==GlobalCompanyId)
                 .Include(x => x.FMS_VoucherType)
                 .Include(x => x.FMS_GLAccount)
                 .Select();
        }
        public IEnumerable<FMS_VoucherTypeWiseOppositeGLMap> GetActiveFMS_VoucherTypeWiseOppositeGLMap(int GlobalCompanyId,bool IsActive)
        {
            return _repository
                   .Query(x=>x.GlobalCompanyId==GlobalCompanyId && x.IsActive==IsActive)
                   .Select();
        }

        public IEnumerable<FMS_VoucherTypeWiseOppositeGLMap> GetOppositeGLMapByVoucherTypeId(int voucherTypeId, bool isActive,int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.VoucherTypeId == voucherTypeId && x.IsActive==isActive && x.GlobalCompanyId==GlobalCompanyId)
                .Include(x => x.FMS_GLAccount)
                .Select();
        }
        public IEnumerable<FMS_VoucherTypeWiseOppositeGLMap> GetOppositeGLMapByVoucherTypeId(int voucherTypeId,int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.VoucherTypeId == voucherTypeId && x.GlobalCompanyId==GlobalCompanyId)
                .Include(x => x.FMS_GLAccount)
                .Select();
        }
        public IEnumerable<FMS_VoucherTypeWiseOppositeGLMap> GetActiveFMS_VoucherTypeWiseOppositeGLMapById(int id,int GlobalCompanyId)
        {
            return _repository
               .Query(x => x.VoucherTypeWiseOppositeGLMapId == id && x.GlobalCompanyId==GlobalCompanyId)
               .Include(x => x.FMS_GLAccount)
               .Select();
        }

        public FMS_VoucherTypeWiseOppositeGLMap GetVoucherTypeWiseOppositeGLMap(int voucherId, int GLAccountId,int GlobalCompanyId)
         {
             return _repository
                .Query(x => x.VoucherTypeId == voucherId && x.GLAccountId == GLAccountId && x.GlobalCompanyId==GlobalCompanyId)
                .Include(x=>x.FMS_VoucherType)
                .Select().FirstOrDefault();
         }
    }
}
