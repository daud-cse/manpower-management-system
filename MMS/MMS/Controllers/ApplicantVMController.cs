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
    public class ApplicantVMController : Controller
    {
        private readonly IApplicantVMService _applicantVMService;
        private readonly ILocationService _locationVMService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContentService _iContentService;
        public ApplicantVMController(IApplicantVMService applicantVMService
          ,ILocationService locationVMService
          ,IUnitOfWork unitOfWork
          ,IContentService iContentService)
        {
            _applicantVMService = applicantVMService;
            _locationVMService = locationVMService;
            _iContentService = iContentService;
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /ApplicantVM/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ApplicantVM/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ApplicantVM/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            var applicantVM = _applicantVMService.newApplicantVM(GolbalSession.gblSession.GlobalCompanyId);

            var lstLocation = _locationVMService.GetAllLocation(GolbalSession.gblSession.GlobalCompanyId);
            if (lstLocation.Where(x => x.IsStartingCompanyWise == true).ToList().Count == 0)
            {
                TempData.Add("danger", "Applicant Location Starting Location is not set.");
                return RedirectToAction("Index", "Applicant");
            }
            if (lstLocation.Where(x => x.IsStartingCompanyWise == true).ToList().Count > 2)
            {
                TempData.Add("danger", "Applicant Location Starting Location set multiple times.");
                return RedirectToAction("Index", "Applicant");
            }
            var startingPoint = lstLocation.Where(x => x.IsStartingCompanyWise == true).FirstOrDefault();
            applicantVM.applicant.CurrentLocationID = startingPoint.ID;
            applicantVM.applicant.NextLocationID = startingPoint.NextLocationID == null ? 0 : Convert.ToInt32(startingPoint.NextLocationID);
            applicantVM.applicant.DateOfBirth = DateTime.Now;
            applicantVM.applicant.ReceivedDate = DateTime.Now;
            return View(applicantVM);
        }

        //
        // POST: /ApplicantVM/Create

        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(ApplicantVM objApplicantVM, List<CheckListGroupVM> lstCheckListGroupVM, IEnumerable<HttpPostedFileBase> uploadApplicantImage)
        {
            try
            {
                // TODO: Add insert logic here
                Content objContent = new Content();

                if (uploadApplicantImage != null)
                {
                    objContent = _iContentService.SaveContent(uploadApplicantImage, _unitOfWork, GolbalSession.gblSession.GlobalCompanyId, GolbalSession.gblSession.UserId);
                }


                objApplicantVM.applicant.ApplicantPhotoID = objContent.ID;
                if (lstCheckListGroupVM==null)
                {
                    lstCheckListGroupVM = new List<CheckListGroupVM>();
                }
                _applicantVMService.SaveApplicantVM(objApplicantVM, lstCheckListGroupVM,_unitOfWork, GolbalSession.gblSession.GlobalCompanyId, GolbalSession.gblSession.UserId);
                var applicantId = objApplicantVM.applicant.ID;
                
                TempData.Add("success", "Applicant Created Successfully.");
                return RedirectToAction("Index", "Applicant", new { agentId = objApplicantVM.applicantFileLot.AgentID });
                           
            }
            catch(Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index", "Applicant");
            }
        }

        //
        // GET: /ApplicantVM/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(int id)
        {
            var applicantVM = _applicantVMService.GetApplicantVMById(id, GolbalSession.gblSession.GlobalCompanyId);
         
            return View(applicantVM);
        }

        //
        // POST: /ApplicantVM/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(ApplicantVM objApplicantVM, List<CheckListGroupVM> lstCheckListGroupVM, IEnumerable<HttpPostedFileBase> uploadApplicantImage)
        {
            try
            {
                Content objContent = new Content();

                if (uploadApplicantImage != null)
                {
                    objContent = _iContentService.UpdateContent(uploadApplicantImage, _unitOfWork, GolbalSession.gblSession.GlobalCompanyId, objApplicantVM.applicant.ApplicantPhotoID);
                }
                objApplicantVM.applicant.ApplicantPhotoID = objContent.ID;

              var msg=  _applicantVMService.UpdateApplicantVM(objApplicantVM, lstCheckListGroupVM, _unitOfWork, GolbalSession.gblSession.GlobalCompanyId, GolbalSession.gblSession.UserId);
                var applicantId = objApplicantVM.applicant.ID;
                TempData.Add("success", "Applicant updated Successfully.");
                return RedirectToAction("Index", "Applicant", new { agentId = objApplicantVM.applicantFileLot.AgentID });
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index", "Applicant");
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
