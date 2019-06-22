using MMS.Entities.Models;
using MMS.Service.ModelServices;
using MMS.Utility;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Controllers
{
    public class LocationController : Controller
    {

        private readonly ILocationService _iLocationService;
        private readonly IUnitOfWork _unitOfWork;
        public LocationController(ILocationService iLocationService, IUnitOfWork unitOfWork)
        {
            _iLocationService = iLocationService;
            _unitOfWork = unitOfWork;
        }

        [CustomActionFilter]
        public ActionResult Index()
        {

            var lstLocation = _iLocationService.GetAllLocation(GolbalSession.gblSession.GlobalCompanyId);
            return View(lstLocation);
        }


        // GET: /Location/Edit/5
         [CustomActionFilter]
        public ActionResult Edit(int id)
        {

            var objLocation = _iLocationService.GetLocationById(id, GolbalSession.gblSession.GlobalCompanyId);
            return View(objLocation);
        }

        //
        // POST: /Location/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(int id, Location objLocation)
        {
            try
            {
                // TODO: Add update logic here
                
                var objLocationFromDataBase = _iLocationService.GetLocationById(id,GolbalSession.gblSession.GlobalCompanyId);

                objLocationFromDataBase.IsStartingCompanyWise = objLocation.IsStartingCompanyWise;

                objLocationFromDataBase.IsSendGeneralMesasge = objLocation.IsSendGeneralMesasge;
                objLocationFromDataBase.GeneralMessageBody = objLocation.GeneralMessageBody;

                objLocationFromDataBase.IsSendApplicantMesasge = objLocation.IsSendApplicantMesasge;
                objLocationFromDataBase.ApplicantMessageBody = objLocation.ApplicantMessageBody;

                objLocationFromDataBase.IsSendAgentMesasge = objLocation.IsSendAgentMesasge;
                objLocationFromDataBase.AgentMessageBody = objLocation.AgentMessageBody;

                _iLocationService.Update(objLocationFromDataBase);
                _unitOfWork.SaveChanges();
                TempData.Add("Success", "Location Update successfully");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
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
