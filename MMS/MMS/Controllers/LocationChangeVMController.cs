using MMS.Entities.Models;
using MMS.Entities.ViewModels;
using MMS.Service.ModelServices;
using MMS.Service.ViewModelServices;
using MMS.Utility;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Controllers
{
    public class LocationChangeVMController : Controller
    {
        private readonly ILocationChangeVMService _locationChangeVMService;
        private readonly IMessageSendVMService _MessageSendVMService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageMailMasterService _MessageMailMasterService;
        public LocationChangeVMController(ILocationChangeVMService locationChangeVMService
            ,IMessageSendVMService MessageSendVMService
            , IUnitOfWork unitOfWork
            ,IMessageMailMasterService MessageMailMasterService)
        {
            _locationChangeVMService = locationChangeVMService;
            _MessageSendVMService = MessageSendVMService;
            _MessageMailMasterService = MessageMailMasterService;
            _unitOfWork = unitOfWork;
        }             
        [CustomActionFilter]
        public ActionResult CreateApplicantLocation()
        {
            var locationChangeVM = _locationChangeVMService.newLocationChangeVM();
            return View(locationChangeVM);
        }
          [CustomActionFilter]
        public ActionResult GetApplicantLocation(string Id, bool IsApplicant = false)
        {

            var locationChangeVM = _locationChangeVMService.GetApplicantCurrentLocation(GolbalSession.gblSession.GlobalCompanyId,Id, IsApplicant);
            if (locationChangeVM.applicant == null)
            {
                locationChangeVM.applicant = new Applicant();
                TempData.Add("danger", "Applicant not found.");
            }
            else if (locationChangeVM.applicant.IsReceivedCompleted == false)
            {
                locationChangeVM.applicant = new Applicant();
                TempData.Add("danger", "Applicant File not Received.");
            }
            return View("CreateApplicantLocation", locationChangeVM);
        }
       
        // POST: /LocationChangeVM/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult UpdateApplicantLocation(LocationChangeVM objLocationChangeVM)
        {
            try
            {

                if (objLocationChangeVM.applicant.ID == 0)
                {
                   
                    TempData.Add("danger", "Applicant not found.");
                    return RedirectToAction("CreateApplicantLocation");
                }
                var result = _locationChangeVMService.UpdateLocationVM(GolbalSession.gblSession.GlobalCompanyId,objLocationChangeVM, _unitOfWork, GolbalSession.gblSession.UserId);
                if (objLocationChangeVM.applicantMovement.IsTaskCompleted)
                {
                    if (result == "1")
                    {
                        TempData.Add("Success", "Applicant all task successfully completed.");
                    }
                    else
                    {
                        if (result == "2")
                        {
                            TempData.Add("danger", "Applicant Location change already completed please check Reports.");
                            return RedirectToAction("GetApplicantLocation", new { Id = Convert.ToString(objLocationChangeVM.applicant.ID), IsApplicant = true });
                        }
                        else if (result == "3")
                        {

                            ///This The Message send part
                            var message = "";                                                  
                            if (objLocationChangeVM.applicant.Location.IsSendApplicantMesasge || objLocationChangeVM.applicant.Location.IsSendAgentMesasge || objLocationChangeVM.applicant.Location.IsSendGeneralMesasge)
                            {
                                objLocationChangeVM.applicant = objLocationChangeVM.applicant;
                                var lstMessageMailVM = _MessageSendVMService.SendingMessageList(objLocationChangeVM.applicant,GolbalSession.gblSession.GlobalCompanyId);

                                MessageMailMaster objMessageMailMaster = new MessageMailMaster();
                                var  lstMailInfo=new List<MailInfo>();
                                objMessageMailMaster.MessageAreaTypeId = 1;
                                objMessageMailMaster.MessageBody = "Not Need";
                                objMessageMailMaster.MailBody = "Not Need";
                                objMessageMailMaster.SendingAreaTypeId = 1;//only send message                              
                                _MessageMailMasterService.VMMessageMailInsert(objMessageMailMaster, lstMessageMailVM,GolbalSession.gblSession.GlobalCompanyId, GolbalSession.gblSession.UserId, _unitOfWork);

                                //_MessageSendVMService.MessageSend(_unitOfWork);//Line Commets for Windows Service Desgin
                                message = " and Message sent successfully.";
                            }
                            TempData.Add("Success", "Applicant Location change successfully" + message + ".");
                        }
                    }
                }
                else if (result == "3")
                {
                    TempData.Add("Success", "Applicant Location Update successfully.");
                }
                else  if (result == "2")
                {
                    TempData.Add("danger", "Applicant Location change already completed please check Reports.");
                    return RedirectToAction("GetApplicantLocation", new { Id = Convert.ToString(objLocationChangeVM.applicant.ID), IsApplicant = true });
                }

                return RedirectToAction("GetApplicantLocation", new { Id = Convert.ToString(objLocationChangeVM.applicant.ID), IsApplicant = true });
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("GetApplicantLocation", new { Id = Convert.ToString(objLocationChangeVM.applicant.ID), IsApplicant = true });
            }
        }     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
