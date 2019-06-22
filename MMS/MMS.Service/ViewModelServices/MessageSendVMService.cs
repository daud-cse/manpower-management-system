using MMS.Entities.Models;
using MMS.Entities.ViewModels;
using MMS.Service.ModelServices;
using MMS.Utility;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ViewModelServices
{
    public interface IMessageSendVMService
    {
        bool MessageSend(IUnitOfWork _unitOfWork);
        List<MessageMailVM> SendingMessageList(Applicant objApplicant,int GlobalCompanyId);
    }
    public class MessageSendVMService : IMessageSendVMService
    {
        private readonly IRepositoryAsync<MessageInfo> _repository;
        private readonly IAgentService _agentService;
        private readonly IApplicantService _applicantService;
        private readonly ILocationService _locationService;
        private readonly IMessageInfoService _messageInfoService;



        public MessageSendVMService(IAgentService agentService
                                    , ILocationService locationService
                                    , IMessageInfoService messageInfoService
                                    , IApplicantService applicantService)
        {
            _applicantService = applicantService;
            _agentService = agentService;
            _locationService = locationService;
            _messageInfoService = messageInfoService;
        }
        public string MessageBody(Applicant objApplicant, string MessageBody)
        {
            string msg = string.Empty;
            msg = MessageBody.Replace("[AppName]", objApplicant.Name).Replace("[locName]", objApplicant.Location.Name).Replace("[NextlocName]", objApplicant.Location1.Name);
            return msg;

        }
        public List<MessageMailVM> SendingMessageList(Applicant objApplicant,int GlobalCompanyId)
        {
            string msg = string.Empty;
            string mobileNo = string.Empty;
            var applicant = _applicantService.GetApplicantsById(objApplicant.ID,GlobalCompanyId);
            if (objApplicant.Location.AgentMessageBody == null)
            {
                applicant.Location.AgentMessageBody = "Message body is not found!!";
            }
            if (objApplicant.Location.ApplicantMessageBody == null)
            {
                applicant.Location.ApplicantMessageBody = "Message body is not found!!";
            }
            if (objApplicant.Location.GeneralMessageBody == null)
            {
                applicant.Location.GeneralMessageBody = "Message body is not found!!";
            }
            var lstMessageMailVM = new List<MessageMailVM>();
            if (objApplicant.Location.IsSendAgentMesasge)
            {
                var objMessageInfo = new MessageMailVM();

                mobileNo = MobileNumber.Rectify(applicant.Agent.Mobile);
                msg = MessageBody(objApplicant, objApplicant.Location.AgentMessageBody);
                objMessageInfo.GlobalCompanyId = GlobalCompanyId;
                objMessageInfo.MobileNo = mobileNo;
                objMessageInfo.MessageBody = msg;
                objMessageInfo.AutoRefId = applicant.Agent.AgentId;
                objMessageInfo.Name = applicant.Agent.Name;
                lstMessageMailVM.Add(objMessageInfo);

            }
            if (objApplicant.Location.IsSendApplicantMesasge)
            {

                var objMessageInfo = new MessageMailVM();
                //For House Maid Female
                if (applicant.ApplicantTypeID == 1)
                {

                    msg = MessageBody(objApplicant, objApplicant.Location.AgentMessageBody);
                    
                    mobileNo = MobileNumber.Rectify(applicant.Agent.Mobile);
                    objMessageInfo.GlobalCompanyId = GlobalCompanyId;
                    objMessageInfo.MobileNo = mobileNo;
                    objMessageInfo.MessageBody = msg;
                    objMessageInfo.AutoRefId = applicant.Agent.AgentId;
                    objMessageInfo.Name = applicant.Agent.Name;
                    lstMessageMailVM.Add(objMessageInfo);

                }
                else
                {
                    msg = MessageBody(objApplicant, objApplicant.Location.ApplicantMessageBody);
                    objMessageInfo.GlobalCompanyId = GlobalCompanyId;
                    mobileNo = MobileNumber.Rectify(applicant.ApplicantPhoneNo);
                    objMessageInfo.MobileNo = mobileNo;
                    objMessageInfo.MessageBody = msg;
                    objMessageInfo.AutoRefId = applicant.ApplicantId;
                    objMessageInfo.Name = applicant.Name;
                    lstMessageMailVM.Add(objMessageInfo);
                }

            }
            if (objApplicant.Location.IsSendGeneralMesasge)
            {
                //Agent Part
                var objMessageInfo = new MessageMailVM();
                mobileNo = MobileNumber.Rectify(applicant.Agent.Mobile);
                msg = objApplicant.Location.AgentMessageBody;
                objMessageInfo.GlobalCompanyId = GlobalCompanyId;
                objMessageInfo.MobileNo = mobileNo;
                objMessageInfo.MessageBody = msg;
                objMessageInfo.AutoRefId = applicant.Agent.AgentId;
                objMessageInfo.Name = applicant.Agent.Name;
                lstMessageMailVM.Add(objMessageInfo);

                //Applicant Part
                var objMessageInfo1 = new MessageMailVM();
                mobileNo = MobileNumber.Rectify(applicant.Agent.Mobile);
                msg = objApplicant.Location.GeneralMessageBody;
                objMessageInfo1.GlobalCompanyId = GlobalCompanyId;
                objMessageInfo1.MobileNo = mobileNo;
                objMessageInfo1.MessageBody = msg;
                objMessageInfo1.AutoRefId = applicant.ApplicantId;
                objMessageInfo1.Name = applicant.Agent.Name;
                lstMessageMailVM.Add(objMessageInfo);

            }
            return lstMessageMailVM;
        }
        // string msg = "";
        // string mobileNo = "";
        //if (applicant.ApplicantTypeID == 1)
        //{    
        //    //For House Maid Female
        //    msg=applicant.Location.AgentMessageBody.Replace("AppName", applicant.Name).Replace("locName", objApplicant.Location.Name).Replace("NextlocName", objApplicant.Location1.Name);
        //    //msg = "আপনার যাত্রীর" + " " + applicant.Name + " এর " + objApplicant.Location.Name + " শেষ হয়েছে.আপনি এখন " + objApplicant.Location1.Name + " এর জন্য অফিস এ যোগাযোগ করুন ধন্যবাদ,Q.K QUICK EXPRESS LTD.";
        //    mobileNo = MobileNumber.Rectify(applicant.Agent.Mobile);
        //}
        //else
        //{
        //    //msg = "আপনার (" + applicant.Name + ") " + objApplicant.Location.Name + " শেষ হয়েছে.আপনি এখন " + objApplicant.Location1.Name + " এর জন্য অফিস এ যোগাযোগ করুন ধন্যবাদ,Q.K QUICK EXPRESS LTD.";
        //    msg = applicant.Location.ApplicantMessageBody.Replace("AppName", applicant.Name).Replace("locName", objApplicant.Location.Name).Replace("NextlocName", objApplicant.Location1.Name);
        //    mobileNo = MobileNumber.Rectify(applicant.ApplicantPhoneNo);
        //}
        public bool MessageSend(IUnitOfWork _unitOfWork)
        {

            try
            {
               
                 var objmessageInfoService = new MessageInfoService(_repository);
                string baseUrl = "https://powersms.banglaphone.net.bd";
                string userId = "shikkhaforall";
                string password = "Shikkhaforall123";

                var lstMessageInfo = objmessageInfoService.GetNotSentMessage(false);


                lstMessageInfo.ToList().ForEach(delegate(MessageInfo item)
                {
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);
                        client.DefaultRequestHeaders.ExpectContinue = false;
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var content = new FormUrlEncodedContent(new[] 
                                                     {
                                                        new KeyValuePair<string, string>("userId", userId)
                                                        ,new KeyValuePair<string, string>("password", password)
                                                        ,new KeyValuePair<string, string>("smsText",item.MessageBody) 
                                                        ,new KeyValuePair<string, string>("commaSeperatedReceiverNumbers", item.MobileNo)                    
                                                     });

                        var response = client.PostAsync("/httpapi/sendsms", content).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            item.IsSent = true;
                            item.SendDate = DateTime.Now;
                            _messageInfoService.Update(item);
                            _unitOfWork.SaveChanges();
                        }
                        else
                        {
                            item.IsSent = false;
                            _messageInfoService.Update(item);
                            _unitOfWork.SaveChanges();

                        }

                    }

                });

            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
