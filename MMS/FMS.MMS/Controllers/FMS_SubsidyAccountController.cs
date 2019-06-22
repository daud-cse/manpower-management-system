using MMS.FMS.Utility;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.FMS.Controllers
{
    public class SubsidyAccountController : Controller
    {
         private readonly IFMS_SubsidyAccountService _iFMS_SubsidyAccountService;
        private readonly IUnitOfWork _unitOfWork;

        public SubsidyAccountController(
            IFMS_SubsidyAccountService iFMS_SubsidyAccountService
            , IUnitOfWork unitOfWork)
        {
            _iFMS_SubsidyAccountService = iFMS_SubsidyAccountService;
            _unitOfWork = unitOfWork;
        }
        // GET: /FMS_SubsidyAccount/
        public ActionResult Index()
        {
            return View();
        }
       
        /// <summary>
        /// For Subsidy Dropdown
        /// </summary>
        /// <param name="GLAccountId"></param>
        /// <returns></returns>
         [CustomActionFilter]
        public JsonResult GetSubsidyByGLAccountId(int GLAccountId)
        {
            var result = _iFMS_SubsidyAccountService.GetSubsidyByGLAccountId(GolbalSession.gblSession.GlobalCompanyId,GLAccountId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
         public JsonResult GetSubsidyTypeWise(int GLAccountId, int subsidyTypeId, int MappedId=0)
         {
             var result = _iFMS_SubsidyAccountService.GetSubsidyTypeWise(GLAccountId, subsidyTypeId,MappedId,"");
             return Json(result, JsonRequestBehavior.AllowGet);
         }
         /// <summary>
         /// For Subsidy AutoCompleted
         /// </summary>
         /// <param name="GLAccountId"></param>
         /// <returns></returns>
         [CustomActionFilter]
         public JsonResult GetSubsidyByCriteria(int GLAccountId, string searchItem)
         {
             var lstSubsidy = _iFMS_SubsidyAccountService.GetSubsidyByCriteria(GLAccountId, searchItem);
              return new JsonResult()
            {
                Data = lstSubsidy.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
         }
        //
        // GET: /FMS_SubsidyAccount/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FMS_SubsidyAccount/Create
        public ActionResult SubsidyMapCreate()
        {
            var objSubsidyAccount = _iFMS_SubsidyAccountService.newSubsidyAccount();
            return View(objSubsidyAccount);
        }

        //
        // POST: /FMS_SubsidyAccount/Create
        [HttpPost]
        public ActionResult SubsidyMapCreate(FormCollection collection)
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
        // GET: /FMS_SubsidyAccount/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FMS_SubsidyAccount/Edit/5
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
        // GET: /FMS_SubsidyAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FMS_SubsidyAccount/Delete/5
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
