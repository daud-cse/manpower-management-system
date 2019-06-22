using MMS.Entities.Models;
using MMS.Entities.ViewModels;
using MMS.Utility;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ModelServices
{

    public interface IMessageMailMasterService
    {
        MessageMailMaster Find(params object[] keyValues);
        void Insert(MessageMailMaster entity);
        void Update(MessageMailMaster entity);
        void Delete(object id);
        void Delete(MessageMailMaster entity);
        IEnumerable<MessageMailMaster> GetAllMessageMailMaster();
        IEnumerable<MessageMailMaster> GetActiveMessageMailMaster(bool IsActive);
        MessageMailMaster GetMessageMailMasterById(int Id);
        string VMMessageMailInsert(MessageMailMaster objMessageMailMaster, List<MessageMailVM> lstMessageMailVM, int GlobalCompanyId, string UserId, IUnitOfWork _unitOfWork);
        MessageMailMaster newMessageMailMaster();
    }
    public class MessageMailMasterService : Service<MessageMailMaster>, IMessageMailMasterService
    {


        private readonly IRepositoryAsync<MessageMailMaster> _repository;
        private readonly IMessageInfoService _iMessageInfoService;
        private readonly IMailInfoService _iMailInfoService;
        private readonly IFMS_SubsidyTypeService _iFMS_SubsidyTypeService;

        private readonly ISendingAreaTypeService _iSendingAreaTypeService;
        public MessageMailMasterService(

              IRepositoryAsync<MessageMailMaster> repository
            , IMessageInfoService iMessageInfoService
            , IMailInfoService iMailInfoService
            , IFMS_SubsidyTypeService iFMS_SubsidyTypeService
            , ISendingAreaTypeService iSendingAreaTypeService

            )
            : base(repository)
        {
            _repository = repository;
            _iMessageInfoService = iMessageInfoService;
            _iMailInfoService = iMailInfoService;
            _iFMS_SubsidyTypeService = iFMS_SubsidyTypeService;
            _iSendingAreaTypeService = iSendingAreaTypeService;

        }
        public IEnumerable<MessageMailMaster> GetAllMessageMailMaster()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<MessageMailMaster> GetActiveMessageMailMaster(bool IsActive)
        {
            return _repository
                   .Query(x => x.IsCompleted == true)
                   .Select();
        }

        public MessageMailMaster newMessageMailMaster()
        {
            var objMessageMailMaster = new MessageMailMaster();

            objMessageMailMaster.kvpSubsidyType = _iFMS_SubsidyTypeService.GetKvpSubsidyType();
            objMessageMailMaster.kvpSendingAreaType = _iSendingAreaTypeService.KvpGetSendingAreaType(true);
            return objMessageMailMaster;
        }
        public MessageMailMaster GetMessageMailMasterById(int Id)
        {
            return _repository
                .Query(x => x.Id == Id)
                .Select().FirstOrDefault();
        }
        public string VMMessageMailInsert(MessageMailMaster objMessageMailMaster, List<MessageMailVM> lstMessageMailVM,int GlobalCompanyId, string UserId, IUnitOfWork _unitOfWork)
        {

            //_unitOfWork.BeginTransaction
            try
            {

                var objMessageMailMasterInsert = new MessageMailMaster();

                if (lstMessageMailVM.Count>0)
                {
                    objMessageMailMasterInsert.MailBody = objMessageMailMaster.MailBody == null ? "Not Found" : lstMessageMailVM.FirstOrDefault().MailBody == null ? "Not found" : lstMessageMailVM.FirstOrDefault().MailBody;
                    objMessageMailMasterInsert.MessageBody = objMessageMailMaster.MessageBody == null ? "Not Found" : lstMessageMailVM.FirstOrDefault().MessageBody;
                }
                objMessageMailMasterInsert.GlobalCompanyId = GlobalCompanyId;
                objMessageMailMasterInsert.SendDate = DateTime.Now;

                objMessageMailMasterInsert.SetBy = UserId;
                objMessageMailMasterInsert.SendDate = DateTime.Now;
                objMessageMailMasterInsert.IsCompleted = false;
             
                objMessageMailMasterInsert.MessageAreaTypeId = objMessageMailMaster.MessageAreaTypeId; ;
                objMessageMailMasterInsert.SendingAreaTypeId = objMessageMailMaster.SendingAreaTypeId;
                _repository.Insert(objMessageMailMasterInsert);
                _unitOfWork.SaveChanges();

                objMessageMailMasterInsert.AutoId = "MGS" + objMessageMailMasterInsert.Id;

                _repository.Update(objMessageMailMasterInsert);
                _unitOfWork.SaveChanges();

                if (objMessageMailMaster.SendingAreaTypeId == 1)//Message
                {
                    lstMessageMailVM.ToList().ForEach(delegate(MessageMailVM item)
                    {
                        var objMessageInfo = new MessageInfo();

                        objMessageInfo.GlobalCompanyId = GlobalCompanyId;
                        objMessageInfo.SendDate = DateTime.Now;
                        objMessageInfo.SetDate = DateTime.Now;
                        objMessageInfo.MessageMasterId = objMessageMailMasterInsert.Id;
                        objMessageInfo.IsSent = false;

                        objMessageInfo.MobileNo = MobileNumber.Rectify(item.MobileNo);
                        objMessageInfo.AutoRefId = item.AutoRefId;
                        objMessageInfo.Name = item.Name;

                        objMessageInfo.MessageBody = item.MessageBody;
                        objMessageInfo.SetBy = UserId;
                        _iMessageInfoService.Insert(objMessageInfo);
                        _unitOfWork.SaveChanges();
                    });
                }

                else if (objMessageMailMaster.SendingAreaTypeId == 2)//Mail
                {
                    lstMessageMailVM.ToList().ForEach(delegate(MessageMailVM item)
                    {
                        if (item.EmailId == null)
                        {

                        }
                        else
                        {
                            var objMailInfo = new MailInfo();
                            objMailInfo.IsSent = false;
                            objMailInfo.GlobalCompanyId = GlobalCompanyId;
                            objMailInfo.SendDate = DateTime.Now;
                            objMailInfo.SetDate = DateTime.Now;
                            objMailInfo.EmailId = item.EmailId;
                            objMailInfo.AutoRefId = item.AutoRefId;
                            objMailInfo.Name = item.Name;
                            objMailInfo.MailMasterId = objMessageMailMasterInsert.Id;
                            objMailInfo.MailBody = item.MailBody;
                            objMailInfo.SetBy = UserId;
                            _iMailInfoService.Insert(objMailInfo);
                            _unitOfWork.SaveChanges();
                        }
                    });
                }

                else if (objMessageMailMaster.SendingAreaTypeId == 3)//Both
                {
                    lstMessageMailVM.ToList().ForEach(delegate(MessageMailVM item)
                    {
                        var objMessageInfo = new MessageInfo();
                        objMessageInfo.GlobalCompanyId = GlobalCompanyId;
                        objMessageInfo.SendDate = DateTime.Now;
                        objMessageInfo.SetDate = DateTime.Now;

                        objMessageInfo.MobileNo = MobileNumber.Rectify(item.MobileNo);
                        objMessageInfo.AutoRefId = item.AutoRefId;
                        objMessageInfo.Name = item.Name;

                        objMessageInfo.MessageMasterId = objMessageMailMasterInsert.Id;
                        objMessageInfo.IsSent = false;
                        objMessageInfo.MessageBody = item.MessageBody;
                        objMessageInfo.SetBy = UserId;
                        _iMessageInfoService.Insert(objMessageInfo);
                        _unitOfWork.SaveChanges();
                    });
                    lstMessageMailVM.ToList().ForEach(delegate(MessageMailVM item)
                    {
                        if (item.EmailId==null)
                        {

                        }
                        else
                        {
                            var objMailInfo = new MailInfo();
                            objMailInfo.GlobalCompanyId = GlobalCompanyId;
                            objMailInfo.IsSent = false;
                            objMailInfo.SendDate = DateTime.Now;
                            objMailInfo.SetDate = DateTime.Now;
                            objMailInfo.EmailId = item.EmailId;
                            objMailInfo.AutoRefId = item.AutoRefId;
                            objMailInfo.Name = item.Name;
                            objMailInfo.MailMasterId = objMessageMailMasterInsert.Id;
                            objMailInfo.MailBody = item.MailBody;
                            objMailInfo.SetBy = UserId;
                            _iMailInfoService.Insert(objMailInfo);
                            _unitOfWork.SaveChanges();
                        }
                       
                    });

                }
                return "true";
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return ex.Message.ToString();
            }

        }
    }
}
