using MMS.Entities.Models;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Controllers
{
    public class CheckListController : Controller
    {
        private readonly ICheckListService _iCheckListService;

        private readonly IUnitOfWork _unitOfWork;

        public CheckListController(ICheckListService iCheckListService, IUnitOfWork unitOfWork)
        {
            _iCheckListService = iCheckListService;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var lstCheckList = _iCheckListService.GetAllCheckList(GolbalSession.gblSession.GlobalCompanyId);
            return View(lstCheckList);
        }

      
        //
        // GET: /ApplicantType/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ApplicantType/Create
        [HttpPost]
        public ActionResult Create(CheckList objCheckList)
        {
            try
            {
                var lstCheckList = _iCheckListService.GetAllCheckList();

                if (lstCheckList.Any())
                {
                    objCheckList.ID = lstCheckList.Max(x => x.ID) + 1;
                }
                else
                {
                    objCheckList.ID = 1;
                }
                objCheckList.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                _iCheckListService.Insert(objCheckList);
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
        public ActionResult Edit(int id)
        {

            var objCheckList = _iCheckListService.Find(id);
            return View(objCheckList);
        }

        //
        // POST: /ApplicantType/Edit/5
        [HttpPost]
        public ActionResult Edit(CheckList objCheckList)
        {
            try
            {
                // TODO: Add update logic here
                objCheckList.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                _iCheckListService.Update(objCheckList);
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
