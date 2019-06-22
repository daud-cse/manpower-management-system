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
    public class ApplicantTypeController : Controller
    {


        private readonly IApplicantTypeService _iApplicantTypeService;

        private readonly IUnitOfWork _unitOfWork;

        public ApplicantTypeController(IApplicantTypeService iApplicantTypeService, IUnitOfWork unitOfWork)
        {
            _iApplicantTypeService = iApplicantTypeService;
            _unitOfWork = unitOfWork;
        }
         [CustomActionFilter]
        public ActionResult Index()
        {
            var lstApplicantType = _iApplicantTypeService.GetAllApplicantType(GolbalSession.gblSession.GlobalCompanyId);
            return View(lstApplicantType);
        }

        //
        // GET: /ApplicantType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ApplicantType/Create
         [CustomActionFilter]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ApplicantType/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(ApplicantType objApplicantType)
        {
            try
            {
                var lstApplicantType = _iApplicantTypeService.GetAllApplicantType();

                if (lstApplicantType.Any())
                {
                    objApplicantType.ID = lstApplicantType.Max(x => x.ID)+1;
                }
                else
                {
                    objApplicantType.ID = 1;
                }
                objApplicantType.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                _iApplicantTypeService.Insert(objApplicantType);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Applicant Type Created Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /ApplicantType/Edit/5
         [CustomActionFilter]
        public ActionResult Edit(int id)
        {

            var objApplicantType = _iApplicantTypeService.Find(id);
            return View(objApplicantType);
        }

        //
        // POST: /ApplicantType/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(ApplicantType objApplicantType)
        {
            try
            {
                // TODO: Add update logic here
                objApplicantType.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                _iApplicantTypeService.Update(objApplicantType);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Applicant Type Updated Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /ApplicantType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ApplicantType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
