using MMS.Entities.ViewModels;
using MMS.Service.ViewModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Entities;
using MMS.Entities.Models;
using MMS.FMS.Utility;
namespace MMS.FMS.Controllers
{
    public class DayOpenCloseVMController : Controller
    {
        private readonly IFMS_DayOpenCloseVMService _iFMS_DayOpenCloseVMService;
        private readonly IUnitOfWork _unitOfWork;

        public DayOpenCloseVMController(IFMS_DayOpenCloseVMService iFMS_DayOpenCloseVMService,
            IUnitOfWork unitOfWork)
        {
            _iFMS_DayOpenCloseVMService = iFMS_DayOpenCloseVMService;
            _unitOfWork = unitOfWork;
        }      
        [CustomActionFilter]
        public ActionResult DayOpenClose()
        {

            string userId = GolbalSession.gblSession.UserId;
            var objDayOpenCloseVM = _iFMS_DayOpenCloseVMService.GetFMSDayOpenCloseVM(GolbalSession.gblSession.GlobalCompanyId,userId);
            if (String.IsNullOrEmpty(objDayOpenCloseVM.Settings.CurrentDate.ToString()))
            {
                objDayOpenCloseVM.DayOpenClose = new FMS_DayOpenClose();
                //objDayOpenCloseVM.DayOpenClose.OpenDate = "";
                //objDayOpenCloseVM.DayOpenClose.OpenedOn = DateTime.Now;
               // objDayOpenCloseVM.DayOpenClose.ClosedOn = DateTime.Now;
            }
            if (objDayOpenCloseVM.DayOpenClose==null)
            {
                objDayOpenCloseVM.DayOpenClose = new FMS_DayOpenClose();
                objDayOpenCloseVM.DayOpenClose.OpenDate = objDayOpenCloseVM.Settings.CurrentDate == null ? DateTime.Now : Convert.ToDateTime(objDayOpenCloseVM.Settings.CurrentDate);
           }

            return View("DayOpenClose", objDayOpenCloseVM);
        }
        
        [HttpPost]
        [CustomActionFilter]
        public ActionResult DayOpenClose(FMS_DayOpenCloseVM objFMS_DayOpenCloseVM)
        {           
            var msg = _iFMS_DayOpenCloseVMService.DayOpenCloseProcess(objFMS_DayOpenCloseVM,GolbalSession.gblSession.GlobalCompanyId,GolbalSession.gblSession.UserId, _unitOfWork);
            try
            {                
                    TempData.Add("success", msg);
                    return RedirectToAction("Index", "DayOpenClose");           
            }
            catch(Exception exe)
            {
                TempData.Add("danger",exe.Message);               
                return RedirectToAction("DayOpenClose", "DayOpenCloseVM");
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
