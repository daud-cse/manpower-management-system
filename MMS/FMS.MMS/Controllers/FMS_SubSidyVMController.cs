using MMS.FMS.Utility;
using MMS.Service.ViewModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.FMS.Controllers
{
    public class SubSidyVMController : Controller
    {
        private readonly IFMS_SubSidyVMService _iFMS_SubSidyVMService;

        public SubSidyVMController(
            IFMS_SubSidyVMService iFMS_SubSidyVMService
            )
        {
            _iFMS_SubSidyVMService = iFMS_SubSidyVMService;
           
        }
        // GET: /FMS_SubSidyVM/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /FMS_SubSidyVM/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FMS_SubSidyVM/Create
        public ActionResult Create()
        {
            return View();
        }
        [CustomActionFilter]
        public JsonResult GetSubsidyByCriteria(string searchItem,int SubsidyTypeId=0,int SubsidyAccountId=0)
        {
            var lstSubsidy = _iFMS_SubSidyVMService.FMS_SubsidyByCriteria(GolbalSession.gblSession.GlobalCompanyId, SubsidyTypeId, SubsidyAccountId, searchItem);
            return new JsonResult()
            {
                Data = lstSubsidy.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        //
        // POST: /FMS_SubSidyVM/Create
        [HttpPost]
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
        // GET: /FMS_SubSidyVM/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FMS_SubSidyVM/Edit/5
        [HttpPost]
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
        // GET: /FMS_SubSidyVM/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FMS_SubSidyVM/Delete/5
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
