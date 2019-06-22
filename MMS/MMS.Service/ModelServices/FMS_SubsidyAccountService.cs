using MMS.Entities.Models;
using MMS.Entities.StoredProcedures;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ModelServices
{

    public interface IFMS_SubsidyAccountService
    {
        FMS_SubsidyAccount Find(params object[] keyValues);
        void Insert(FMS_SubsidyAccount entity);
        void Update(FMS_SubsidyAccount entity);
        void Delete(object id);
        void Delete(FMS_SubsidyAccount entity);
        IEnumerable<FMS_SubsidyAccount> GetAllFMS_SubsidyAccount();
        IEnumerable<FMS_SubsidyAccount> GetActiveFMS_SubsidyAccount(bool IsActive);
        FMS_SubsidyAccount GetFMS_SubsidyAccountById(int Id);

        List<KeyValuePair<string, string>> GetSubsidyByGLAccountId(int GlobalCompanyId,int GlAcountId);
        FMS_SubsidyAccount newSubsidyAccount();
        List<FMS_SubsidyAccount> GetSubsidyByCriteria(int GLAccountId, string searchItem);
        List<FMS_SubsidyAccount> GetSubsidyTypeWise(int GLAccountId, int subsidyTypeId, int MappedId, string searchItem);

    }
    public class FMS_SubsidyAccountService : Service<FMS_SubsidyAccount>, IFMS_SubsidyAccountService
    {
        private readonly IRepositoryAsync<FMS_SubsidyAccount> _repository;
        private readonly IFMS_GLSubsidyTypeMapService _iFMS_GLSubsidyTypeMapService;
        private readonly IBankAccountInfoService _iBankAccountInfoService;
        private readonly IFMS_GLAccountService _iFMS_GLAccountService;
        private readonly IFMS_SubsidyTypeService _iFMS_SubsidyTypeService;
        private readonly IStoredProcedures _storedProcedures;
        public FMS_SubsidyAccountService(

              IRepositoryAsync<FMS_SubsidyAccount> repository,
            IFMS_GLSubsidyTypeMapService iFMS_GLSubsidyTypeMapService,
            IBankAccountInfoService iBankAccountInfoService,
            IFMS_GLAccountService iFMS_GLAccountService,
            IFMS_SubsidyTypeService iFMS_SubsidyTypeService,
            IStoredProcedures storedProcedures

            )
            : base(repository)
        {
            _repository = repository;
            _iFMS_GLSubsidyTypeMapService = iFMS_GLSubsidyTypeMapService;
            _iBankAccountInfoService = iBankAccountInfoService;
            _iFMS_SubsidyTypeService = iFMS_SubsidyTypeService;
            _iFMS_GLAccountService = iFMS_GLAccountService;
            _storedProcedures = storedProcedures;

        }

        public List<FMS_SubsidyAccount> GetSubsidyTypeWise(int GLAccountId, int subsidyTypeId, int MappedId, string searchItem)
        {
           // List<FMS_SubsidyAccount> lstSubsidyAccount = new List<FMS_SubsidyAccount>();
           //var lstBankInfo= _iBankAccountInfoService.GetActiveBankAccountInfo(true);

           //lstBankInfo.ToList().ForEach(delegate(BankAccountInfo item)
           //{
           //    FMS_SubsidyAccount objFMS_SubsidyAccount = new FMS_SubsidyAccount();
           //    objFMS_SubsidyAccount.SubsidyId = item.BankAccountAutoId;
           //    objFMS_SubsidyAccount.SubsidyName = item.BankAccountName;
           //    lstSubsidyAccount.Add(objFMS_SubsidyAccount);
           //});
            var lstSubsidyAccount = _storedProcedures.GetSubsidyByCriteria(GLAccountId, subsidyTypeId, MappedId, "").ToList();

           return lstSubsidyAccount;

        }
        public FMS_SubsidyAccount newSubsidyAccount()
        {
            var lstFMS_SubsidyType = new List<KeyValuePair<int, string>>();
            _iFMS_SubsidyTypeService.GetActiveFMS_SubsidyType(true).ToList().ForEach(delegate(FMS_SubsidyType item)
            {
                lstFMS_SubsidyType.Add(new KeyValuePair<int, string>(item.SubsidyTypeId, item.SubsidyTypeName));
            });

            var lstFMS_GLAccount = new List<KeyValuePair<string, string>>();
            //_iFMS_GLAccountService.GetFMS_GLAccountList(levelCode, true).ToList().ForEach(delegate(FMS_GLAccount item)
            //{
            //    lstFMS_GLAccount.Add(new KeyValuePair<string, string>(item.GLAccountId, item.GLAccountName));
            //});

            FMS_SubsidyAccount objFMS_SubsidyAccount = new FMS_SubsidyAccount();
            objFMS_SubsidyAccount.kvpGLAccount = lstFMS_GLAccount;
            objFMS_SubsidyAccount.kvpSubsidyType = lstFMS_SubsidyType;
            return objFMS_SubsidyAccount;
        }
        public List<FMS_SubsidyAccount> GetSubsidyByCriteria(int GLAccountId, string searchItem)
        {
            //var glSubsidyTpyeMap = _iFMS_GLSubsidyTypeMapService.GetGLSubsidyTypeMapByGLAccountId(GLAccountId);
            //var lstSubsidy = _repository
            //    .Query(x => x.GlSubsidyTypeMapId == glSubsidyTpyeMap.GlSubsidyTypeMapId
            //    && (x.SubsidyId.ToLower().Contains(searchItem.ToLower())
            //    || x.SubsidyName.ToLower().Contains(searchItem.ToLower()))
            //    )
            //    .Include(x => x.FMS_GLSubsidyTypeMap.FMS_SubsidyType)
            //    .Select().Take(20);
            List<FMS_SubsidyAccount> subsidyList = new List<FMS_SubsidyAccount>();
            //lstSubsidy.ToList().ForEach(delegate(FMS_SubsidyAccount item)
            //{
            //    FMS_SubsidyAccount objSubsidy = new FMS_SubsidyAccount();
            //    objSubsidy.FMS_GLSubsidyTypeMap = new FMS_GLSubsidyTypeMap();
            //    objSubsidy.FMS_GLSubsidyTypeMap.FMS_SubsidyType = new FMS_SubsidyType();
            //    objSubsidy.GlSubsidyTypeMapId = item.GlSubsidyTypeMapId;
            //    objSubsidy.SubsidyAccountId = item.SubsidyAccountId;
            //    objSubsidy.SubsidyName = item.SubsidyName;
            //    objSubsidy.SubsidyId = item.SubsidyId;
            //   // objSubsidy.SubsidyTypeId = item.SubsidyTypeId;
            //    //if (item.SubsidyTypeId == 4)//Bank Type
            //    //{
            //    //    var bankInfo = _iBankAccountInfoService.GetBankAccountInfoByAutoId(item.SubsidyId);
            //    //    if (bankInfo == null)
            //    //    {
            //    //        bankInfo = new BankAccountInfo();
            //    //    }
            //    objSubsidy.BankName = item.BankName;
            //    objSubsidy.BankAccountNo = item.BankAccountNo;
            //    objSubsidy.BranchName = item.BranchName;
            //   // }
            //    // objSubsidy.FMS_GLSubsidyTypeMap.FMS_SubsidyType = item.FMS_GLSubsidyTypeMap.FMS_SubsidyType;           
            //    subsidyList.Add(objSubsidy);
            //});
            return subsidyList;
        }
        public List<KeyValuePair<string, string>> GetSubsidyByGLAccountId(int GlobalCompanyId, int GlAcountId)
        {

            var glSubsidyTpyeMap = _iFMS_GLSubsidyTypeMapService.GetGLSubsidyTypeMapByGLAccountId(GlobalCompanyId,GlAcountId);
            var lstSubsidy = new List<KeyValuePair<string, string>>();
            if (glSubsidyTpyeMap == null)
            {
                return lstSubsidy;
            }
            //_repository.Query(x => x.GlSubsidyTypeMapId == glSubsidyTpyeMap.GlSubsidyTypeMapId).Select().ToList().ForEach(delegate(FMS_SubsidyAccount item)
            //{
            //    lstSubsidy.Add(new KeyValuePair<string, string>(item.SubsidyId, item.SubsidyName));
            //});

            return lstSubsidy;
        }
        public IEnumerable<FMS_SubsidyAccount> GetAllFMS_SubsidyAccount()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<FMS_SubsidyAccount> GetActiveFMS_SubsidyAccount(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public FMS_SubsidyAccount GetFMS_SubsidyAccountById(int Id)
        {
            return _repository
                .Query(x => x.SubsidyAccountId == Id)
                .Select().FirstOrDefault();
        }

    }
}
