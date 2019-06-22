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


    public interface IFMS_VoucherTypeWiseGLMapService
    {
        FMS_VoucherTypeWiseGLMap Find(params object[] keyValues);
        void Insert(FMS_VoucherTypeWiseGLMap entity);
        void Update(FMS_VoucherTypeWiseGLMap entity);
        void Delete(object id);
        void Delete(FMS_VoucherTypeWiseGLMap entity);
        IEnumerable<FMS_VoucherTypeWiseGLMap> GetAllFMS_VoucherTypeWiseGLMap(int GlobalCompanyId);
        IEnumerable<FMS_VoucherTypeWiseGLMap> GetActiveFMS_VoucherTypeWiseGLMap(bool IsActive);
        FMS_VoucherTypeWiseGLMap GetFMS_VoucherTypeWiseGLMapById(int Id, int GlobalCompanyId);
        FMS_VoucherTypeWiseGLMap GetFMSMapGLByVoucherTypeId(int GlobalCompanyId,int voucherTypeId);
        FMS_VoucherTypeWiseGLMap newFMS_VoucherTypeWiseGLMap(int GlobalCompanyId,int VoucherTypeWiseGLMapId, int VoucherTpyeId);
        FMS_VoucherTypeWiseGLMap GetVoucherTypeWiseGLMap(int GlobalCompanyId, int VoucherTypeId, int GLAccountId);

    }
    public class FMS_VoucherTypeWiseGLMapService : Service<FMS_VoucherTypeWiseGLMap>, IFMS_VoucherTypeWiseGLMapService
    {
        private readonly IRepositoryAsync<FMS_VoucherTypeWiseGLMap> _repository;
        private readonly IFMS_VoucherTypeService _fMS_VoucherTypeService;
        private readonly IFMS_GLAccountService _ifMS_GLAccountService;
        public FMS_VoucherTypeWiseGLMapService(

              IRepositoryAsync<FMS_VoucherTypeWiseGLMap> repository
                          ,IFMS_VoucherTypeService ifMS_VoucherTypeService
              ,IFMS_GLAccountService ifMS_GLAccountService

            )
            : base(repository)
        {
            _repository = repository;
            _ifMS_GLAccountService = ifMS_GLAccountService;
            _fMS_VoucherTypeService = ifMS_VoucherTypeService;

        }

        public FMS_VoucherTypeWiseGLMap GetVoucherTypeWiseGLMap(int GlobalCompanyId,int VoucherTypeId, int GLAccountId)
        {
            return _repository.Query(x => x.VoucherTypeId == VoucherTypeId && x.GlobalCompanyId == GlobalCompanyId)
                .Include(x=>x.FMS_VoucherType)
                .Select().FirstOrDefault();
        }
        public FMS_VoucherTypeWiseGLMap newFMS_VoucherTypeWiseGLMap(int GlobalCompanyId,int VoucherTypeWiseGLMapId, int VoucherTpyeId)
        {
            FMS_VoucherTypeWiseGLMap objFMS_VoucherTypeWiseGLMap = new FMS_VoucherTypeWiseGLMap();
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

            var lstGLMappedTypeWise = GetAllFMS_VoucherTypeWiseGLMap(GlobalCompanyId).ToList();
            if (lstGLMappedTypeWise.Count > 1)
            {
                lstGLMappedTypeWise.ForEach(delegate(FMS_VoucherTypeWiseGLMap item)
                {
                    if (lstFMS_GLAccount.Count > 0)
                    {
                        if (VoucherTpyeId > 0)
                        {
                            if (VoucherTpyeId != item.VoucherTypeId)
                            {
                                lstVoucherType.RemoveAll(x => x.Key == item.VoucherTypeId);
                            }

                        }
                        else
                        {
                            lstVoucherType.RemoveAll(x => x.Key == item.VoucherTypeId);
                        }

                    }
                });
            }

            objFMS_VoucherTypeWiseGLMap.kvpGLAccount = lstFMS_GLAccount;
            objFMS_VoucherTypeWiseGLMap.kvpVoucherType = lstVoucherType;
            return objFMS_VoucherTypeWiseGLMap;
        }
        public FMS_VoucherTypeWiseGLMap GetFMSMapGLByVoucherTypeId(int GlobalCompanyId,int voucherTypeId)
        {
            return _repository.Query(x => x.VoucherTypeId == voucherTypeId && x.GlobalCompanyId == GlobalCompanyId)
                .Include(x=>x.FMS_GLAccount)
                .Include(x => x.FMS_VoucherType)
                .Select().FirstOrDefault();
        }
        public IEnumerable<FMS_VoucherTypeWiseGLMap> GetAllFMS_VoucherTypeWiseGLMap(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId)
                .Include(x=>x.FMS_GLAccount)
                .Include(x => x.FMS_VoucherType)
                .Select();
        }
        public IEnumerable<FMS_VoucherTypeWiseGLMap> GetActiveFMS_VoucherTypeWiseGLMap(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public FMS_VoucherTypeWiseGLMap GetFMS_VoucherTypeWiseGLMapById(int Id, int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.VoucherTypeWiseGLMapId == Id && x.GlobalCompanyId == GlobalCompanyId)
                .Select().FirstOrDefault();
        }

    }
}
