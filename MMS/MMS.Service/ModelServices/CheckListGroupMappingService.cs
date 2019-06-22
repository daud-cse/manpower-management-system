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
    public interface ICheckListGroupMappingService
    {
        CheckListGroupMapping Find(params object[] keyValues);
        void Insert(CheckListGroupMapping entity);
        void Update(CheckListGroupMapping entity);
        void Delete(object id);
        void Delete(CheckListGroupMapping entity);
        CheckListGroupMapping newCheckListGroupMapping(int GlobalGlobalCompanyId);
        IEnumerable<CheckListGroupMapping> GetAllCheckListGroupMapping(int GlobalGlobalCompanyId);
        IEnumerable<CheckListGroupMapping> GetActiveCheckListGroupMapping(bool IsActive);
        IEnumerable<CheckListGroupMapping> GetCheckListGroupMappingByGroupId(int GlobalCompanyId, int CheckListGroupId);
        IEnumerable<CheckListGroupMapping> GetCheckListGroupMappingByGroupId(int GlobalGlobalCompanyId, int CheckListGroupId,bool IsActive);

    }
    public class CheckListGroupMappingService : Service<CheckListGroupMapping>, ICheckListGroupMappingService
    {
        private readonly IRepositoryAsync<CheckListGroupMapping> _repository;
        private readonly IApplicantTypeService _iApplicantTypeService;
        public CheckListGroupMappingService(

              IRepositoryAsync<CheckListGroupMapping> repository,
              IApplicantTypeService iApplicantTypeService

            )
            : base(repository)
        {
            _repository = repository;
            _iApplicantTypeService = iApplicantTypeService;

        }

        public CheckListGroupMapping newCheckListGroupMapping(int GlobalGlobalCompanyId)
        {
            var objCheckListGroupMapping = new CheckListGroupMapping();
            objCheckListGroupMapping.kvpApplicantType = _iApplicantTypeService.GetKVPApplicantType(GlobalGlobalCompanyId, true);
            return objCheckListGroupMapping;
        }
        public IEnumerable<CheckListGroupMapping> GetAllCheckListGroupMapping(int GlobalGlobalCompanyId)
        {
            return _repository.Query(x => x.GlobalCompanyId == GlobalGlobalCompanyId).Include(x => x.ApplicantType)
                .Include(x => x.CheckList).Select();
        }
        public IEnumerable<CheckListGroupMapping> GetActiveCheckListGroupMapping(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public IEnumerable<CheckListGroupMapping> GetCheckListGroupMappingByGroupId(int GlobalCompanyId, int CheckListGroupId, bool IsActive)
        {
            return _repository
                .Query(x => x.CheckListGroupID == CheckListGroupId && x.GlobalCompanyId == GlobalCompanyId && x.IsActive == IsActive)
                .Include(x => x.CheckList)
                .Include(x => x.ApplicantType)
                .Select();
        }
        public IEnumerable<CheckListGroupMapping> GetCheckListGroupMappingByGroupId(int GlobalCompanyId, int CheckListGroupId)
        {
            return _repository
                .Query(x => x.CheckListGroupID == CheckListGroupId && x.GlobalCompanyId == GlobalCompanyId)
                .Include(x => x.CheckList)
                .Include(x => x.ApplicantType)
                .Select();
        }

    }
}
