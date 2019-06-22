using MMS.Entities.Models;
using MMS.Entities.ViewModels;
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
    public class ApplicantCheckListController : Controller
    {
        private readonly IApplicantCheckListService _iApplicantCheckListService;

        private readonly IUnitOfWork _unitOfWork;

        public ApplicantCheckListController(IApplicantCheckListService iApplicantCheckListService, IUnitOfWork unitOfWork)
        {
            _iApplicantCheckListService = iApplicantCheckListService;
            _unitOfWork = unitOfWork;
        }
        // GET: /CheckListGroupMapping/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /CheckListGroupMapping/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CheckListGroupMapping/Create
        public ActionResult Create()
        {
            return View();
        }
        [CustomActionFilter]
        public JsonResult GetApplicantCheckList(int CheckListGroupId)
        {
           // var lstApplicantCheckList = _iApplicantCheckListService.GetApplicantCheckList(GolbalSession.gblSession.GlobalCompanyId, CheckListGroupId);
            var lstCheckListGroupVM = new List<CheckListGroupVM>();

            //lstApplicantCheckList.ToList().ForEach(delegate(ApplicantCheckList item)
            //{
            //    var objCheckListGroupVM = new CheckListGroupVM();
            //    objCheckListGroupVM.CheckListGroupMapID = item.ID;
            //    objCheckListGroupVM.CheckListGroupID = item.CheckListGroupID;
               
            //    objCheckListGroupVM.CheckListID = item.CheckListID;
            //    objCheckListGroupVM.CheckListName = item.CheckList.Name;
            //    objCheckListGroupVM.IsCheckList = false;
            //    objCheckListGroupVM.Description = item.Description;
            //    lstCheckListGroupVM.Add(objCheckListGroupVM);
            //});
            return new JsonResult()
            {
                Data = lstCheckListGroupVM,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        //
        // POST: /CheckListGroupMapping/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /CheckListGroupMapping/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /CheckListGroupMapping/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /CheckListGroupMapping/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /CheckListGroupMapping/Delete/5
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
    }
}
