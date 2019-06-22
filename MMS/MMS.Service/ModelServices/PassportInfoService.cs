using MMS.Entities.Models;
using MMS.Entities.ViewModels;
using MMS.Utility;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ModelServices
{

    public interface IPassportInfoService
    {
        PassportInfo Find(params object[] keyValues);
        void Insert(PassportInfo entity);
        void Update(PassportInfo entity);
        void Delete(object id);
        void Delete(PassportInfo entity);
        IEnumerable<PassportInfo> GetAllPassportInfo();
        IEnumerable<PassportInfo> GetActivePassportInfo(bool IsActive);
        PassportInfo GetPassportInfoById(int Id);
        PassportInfo newPassportInfo(int GlobalCompanyId);
        IEnumerable<PassportInfo> GetPassportInfoByCustomerId(int CustomerId);
        IEnumerable<PassportInfo> GetPassportInfoByCriteria(SearchPassportInfoVM objSearchPassportInfoVM);
    }
    public class PassportInfoService : Service<PassportInfo>, IPassportInfoService
    {
        private readonly IRepositoryAsync<PassportInfo> _repository;
        private readonly ICustomerService _iCustomerService;
        private readonly IPassPortTypeService _iPassPortTypeService;
        private readonly IRPOTypeService _iRPOTypeService;
        private readonly ISexService _iSexService;
        private readonly IMaritalStatusService _iMaritalStatusService;
        public PassportInfoService(

              IRepositoryAsync<PassportInfo> repository,
            ICustomerService iCustomerService,
            IPassPortTypeService iPassPortTypeService,
            IRPOTypeService iRPOTypeService,
            ISexService iSexService,
            IMaritalStatusService iMaritalStatusService


            )
            : base(repository)
        {
            _repository = repository;
            _iCustomerService = iCustomerService;
            _iPassPortTypeService = iPassPortTypeService;
            _iRPOTypeService = iRPOTypeService;
            _iSexService = iSexService;
            _iMaritalStatusService = iMaritalStatusService;


        }
        public IEnumerable<PassportInfo> GetPassportInfoByCriteria(SearchPassportInfoVM objSearchPassportInfoVM)
        {
            Expression<Func<PassportInfo, bool>> predicate = PredicateBuilder.True<PassportInfo>();
            if (objSearchPassportInfoVM.CustomerId > 0)
            {
                predicate = predicate.And(p => p.CustomerId == objSearchPassportInfoVM.CustomerId);
            }          

            if (!String.IsNullOrEmpty (objSearchPassportInfoVM.DeliverySlipNo))
            {
                predicate = predicate.And(p => p.DeliverySlipNo == objSearchPassportInfoVM.DeliverySlipNo);
            }
            if (!String.IsNullOrEmpty(objSearchPassportInfoVM.PassportNo))
            {
                predicate = predicate.Or(p => p.NewPassportNo == objSearchPassportInfoVM.PassportNo);
            }
            if (objSearchPassportInfoVM.SexId > 0)
            {
                predicate = predicate.And(p => p.SexTypeId == objSearchPassportInfoVM.SexId);
            }
            if (objSearchPassportInfoVM.RPOTypeId > 0)
            {
                predicate = predicate.And(p => p.RPOTypeId == objSearchPassportInfoVM.RPOTypeId);
            }
            if (objSearchPassportInfoVM.PassPortTypeId > 0)
            {
                predicate = predicate.And(p => p.PassPortTypeId == objSearchPassportInfoVM.PassPortTypeId);
            }

            if (objSearchPassportInfoVM.UpazilaId > 0)
            {
                predicate = predicate.And(p => p.MaritalStatusId == objSearchPassportInfoVM.MaritalStatusId);
            }

            return _repository
                .Query(predicate)
                 .Include(x => x.Customer)
                .Include(x => x.Sex)
                .Include(x => x.RPOType)
                .Include(x => x.PassPortType).Select();
        }
        public IEnumerable<PassportInfo> GetAllPassportInfo()
        {
            return _repository
                .Query()
                .Include(x=>x.Customer)
                .Include(x => x.Sex)
                .Include(x => x.RPOType)
                .Include(x => x.PassPortType)
                .Select();
        }
        public IEnumerable<PassportInfo> GetActivePassportInfo(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }
        public PassportInfo newPassportInfo(int GlobalCompanyId)
        {

            var lstMaritalStatus = new List<KeyValuePair<int, string>>();
            _iMaritalStatusService.GetActiveMaritalStatu(true).ToList().ForEach(delegate(MaritalStatu item)
            {
                lstMaritalStatus.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstCustomer = new List<KeyValuePair<int, string>>();
            _iCustomerService.GetActiveCustomer(GlobalCompanyId,true).ToList().ForEach(delegate(Customer item)
            {
                lstCustomer.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });

            var lstPassPortType = new List<KeyValuePair<int, string>>();
            _iPassPortTypeService.GetActivePassPortType(true).ToList().ForEach(delegate(PassPortType item)
            {
                lstPassPortType.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });

            var lstRPOType = new List<KeyValuePair<int, string>>();
            _iRPOTypeService.GetActiveRPOType(true).ToList().ForEach(delegate(RPOType item)
            {
                lstRPOType.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstSex = new List<KeyValuePair<int, string>>();
            _iSexService.GetActiveSex(true).ToList().ForEach(delegate(Sex item)
            {
                lstSex.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            PassportInfo objPassportInfo = new PassportInfo();
            objPassportInfo.kvpCustomer = lstCustomer;
            objPassportInfo.kvpPassPortType = lstPassPortType;
            objPassportInfo.kvpRPOType = lstRPOType;
            objPassportInfo.kvpSex = lstSex;
            objPassportInfo.kvpMaritalStatus = lstMaritalStatus;

            return objPassportInfo;

        }

        public IEnumerable<PassportInfo> GetPassportInfoByCustomerId(int CustomerId)
        {
            return _repository
                .Query(x => x.CustomerId==CustomerId)
                .Select();
        }
        public PassportInfo GetPassportInfoById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

    }
}
