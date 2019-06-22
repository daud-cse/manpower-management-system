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
    public interface IFMS_GLSubsidyTypeMapService
    {
        FMS_GLSubsidyTypeMap Find(params object[] keyValues);
        void Insert(FMS_GLSubsidyTypeMap entity);
        void Update(FMS_GLSubsidyTypeMap entity);
        void Delete(object id);
        void Delete(FMS_GLSubsidyTypeMap entity);
        IEnumerable<FMS_GLSubsidyTypeMap> GetAllFMS_GLSubsidyTypeMap(int GlobalCompanyId);
        IEnumerable<FMS_GLSubsidyTypeMap> GetActiveFMS_GLSubsidyTypeMap(int GlobalCompanyId, bool IsActive);
        FMS_GLSubsidyTypeMap GetFMS_GLSubsidyTypeMapById(int Id, int GlobalCompanyId);
        FMS_GLSubsidyTypeMap newFMS_GLSubsidyTypeMap(int GlobalCompanyId,int levelCode, int GlSubsidyTpyeMapId);
        FMS_GLSubsidyTypeMap GetGLSubsidyTypeMapByGLAccountId(int GlobalCompanyId,int GLAccountId);
        FMS_GLSubsidyTypeMap GetGLSubsidyTypeMap(int GlobalCompanyId,int GLAccountId,int SubsidyTypeMapId);
      List<KeyValuePair<int, string>> GetGLAccountBySubsidyType(int GlobalCompanyId,int SubsidyTypeId);
      List<KeyValuePair<int, string>> GetGLSubsidyTypeMap(int GlobalCompanyId,int GLAccountId);
    }
    public class FMS_GLSubsidyTypeMapService : Service<FMS_GLSubsidyTypeMap>, IFMS_GLSubsidyTypeMapService
    {
        private readonly IRepositoryAsync<FMS_GLSubsidyTypeMap> _repository;
        private readonly IFMS_GLAccountService _iFMS_GLAccountService;
        private readonly IFMS_SubsidyTypeService _iFMS_SubsidyTypeService;

        public FMS_GLSubsidyTypeMapService(

              IRepositoryAsync<FMS_GLSubsidyTypeMap> repository,
            IFMS_GLAccountService iFMS_GLAccountService,
            IFMS_SubsidyTypeService iFMS_SubsidyTypeService

            )
            : base(repository)
        {
            _repository = repository;
            _iFMS_SubsidyTypeService = iFMS_SubsidyTypeService;
            _iFMS_GLAccountService = iFMS_GLAccountService;

        }
        public List<KeyValuePair<int, string>> GetGLAccountBySubsidyType(int GlobalCompanyId,int SubsidyTypeId)
        {
             var lstGLAccount = new List<KeyValuePair<int, string>>();
             _repository.Query(x => x.SubsidyTypeId == SubsidyTypeId && x.GlobalCompanyId == GlobalCompanyId).Include(x => x.FMS_GLAccount).Select().ToList().ForEach(delegate(FMS_GLSubsidyTypeMap item)
            {
                lstGLAccount.Add(new KeyValuePair<int, string>(item.GLAccountId, item.FMS_GLAccount.GLAccountName));
            });
            return lstGLAccount;
        }
        public FMS_GLSubsidyTypeMap GetGLSubsidyTypeMapByGLAccountId(int GlobalCompanyId,int GLAccountId)
        {
            return _repository.Query(x => x.GLAccountId == GLAccountId && x.GlobalCompanyId == GlobalCompanyId).Select().FirstOrDefault();
        }

        public List<KeyValuePair<int, string>> GetGLSubsidyTypeMap(int GlobalCompanyId,int GLAccountId)
        {


            var lstSubsidyType = new List<KeyValuePair<int, string>>();

            _repository.Query(x => x.GLAccountId == GLAccountId && x.GlobalCompanyId == GlobalCompanyId).Include(x => x.FMS_SubsidyType).Select().OrderBy(x => x.SubsidyTypeId).ToList().ForEach(delegate(FMS_GLSubsidyTypeMap item)
            {
                lstSubsidyType.Add(new KeyValuePair<int, string>(item.SubsidyTypeId, item.FMS_SubsidyType.SubsidyTypeName));
            });

            return lstSubsidyType;
           
        }
        public FMS_GLSubsidyTypeMap GetGLSubsidyTypeMap(int GlobalCompanyId,int GLAccountId, int SubsidyTypeMapId)
        {
            return _repository.Query(x => x.GLAccountId == GLAccountId && x.SubsidyTypeId == SubsidyTypeMapId && x.GlobalCompanyId == GlobalCompanyId).Select().FirstOrDefault();
        }
        public FMS_GLSubsidyTypeMap newFMS_GLSubsidyTypeMap(int GlobalCompanyId,int levelCode, int GlSubsidyTpyeMapId)
        {
            FMS_GLSubsidyTypeMap objFMS_GLSubsidyTypeMap = new FMS_GLSubsidyTypeMap();
            var lstFMS_SubsidyType = new List<KeyValuePair<int, string>>();
            _iFMS_SubsidyTypeService.GetActiveFMS_SubsidyType(true).ToList().ForEach(delegate(FMS_SubsidyType item)
            {
                lstFMS_SubsidyType.Add(new KeyValuePair<int, string>(item.SubsidyTypeId, item.SubsidyTypeName));
            });

            var lstFMS_GLAccount = new List<KeyValuePair<int, string>>();

            _iFMS_GLAccountService.GetFMS_GLAccountList(GlobalCompanyId,levelCode, false, true).ToList().ForEach(delegate(FMS_GLAccount item)
            {
                lstFMS_GLAccount.Add(new KeyValuePair<int, string>(item.GLAccountId, item.GLAccountName));
            });

            ///Alrady Map GL Remove From Dropdown

            //var lstGLSubsidyTypeMap= GetAllFMS_GLSubsidyTypeMap().ToList();
            //if (lstGLSubsidyTypeMap.Count>1)
            //{
            //    lstGLSubsidyTypeMap.ForEach(delegate(FMS_GLSubsidyTypeMap item){
            //        if(lstFMS_GLAccount.Count>0){
            //            if (GlSubsidyTpyeMapId>0)
            //            {
            //                if (GlSubsidyTpyeMapId != item.GlSubsidyTpyeMapId)
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
            //}
            objFMS_GLSubsidyTypeMap.kvpSubsidyType = lstFMS_SubsidyType;
            objFMS_GLSubsidyTypeMap.kvpPostedGLAccount = lstFMS_GLAccount;
            return objFMS_GLSubsidyTypeMap;
        }
        public IEnumerable<FMS_GLSubsidyTypeMap> GetAllFMS_GLSubsidyTypeMap(int GlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalCompanyId)
                .Include(x=>x.FMS_GLAccount)
                .Include(x => x.FMS_SubsidyType)
                .Select();
        }
        public IEnumerable<FMS_GLSubsidyTypeMap> GetActiveFMS_GLSubsidyTypeMap(int GlobalCompanyId,bool IsActive)
        {
            return _repository
                   .Query(x => x.IsActive == IsActive && x.GlobalCompanyId == GlobalCompanyId)
                   .Select();
        }

        public FMS_GLSubsidyTypeMap GetFMS_GLSubsidyTypeMapById(int Id, int GlobalCompanyId)
        {
            return _repository
                .Query(x => x.GlSubsidyTypeMapId == Id && x.GlobalCompanyId == GlobalCompanyId)
                .Select().FirstOrDefault();
        }

    }
}
