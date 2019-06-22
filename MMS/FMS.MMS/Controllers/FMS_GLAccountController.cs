using MMS.FMS.Utility;
using MMS.Entities.Models;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.FMS.Controllers
{
    public class GLAccountController : Controller
    {
        private readonly IFMS_GLAccountService _iFMS_GLAccountService;
        private readonly IUnitOfWork _unitOfWork;

        public GLAccountController(IFMS_GLAccountService iFMS_GLAccountService, IUnitOfWork unitOfWork)
        {
            _iFMS_GLAccountService = iFMS_GLAccountService;
            _unitOfWork = unitOfWork;
        }
         [CustomActionFilter]
        public ActionResult Index()
        {
          var GLAccount=  _iFMS_GLAccountService.GetAllFMS_GLAccount(GolbalSession.gblSession.GlobalCompanyId).OrderBy(x=>x.GLAccountCode);
            return View(GLAccount);
        }

        //
        // GET: /FMS_GLAccount/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FMS_GLAccount/Create
        public ActionResult Create(int id)
        {
            var glaccount = _iFMS_GLAccountService.Find(id);
            return View(glaccount);
        }

        //
        // POST: /FMS_GLAccount/Create
       
        [HttpPost]
        public ActionResult Create(FMS_GLAccount objGLAccount)
        {
            try
            {
                // TODO: Add update logic here
                objGLAccount.SetBy = GolbalSession.gblSession.UserId;
                objGLAccount.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                objGLAccount.SetDate = DateTime.Now;
                _iFMS_GLAccountService.Insert(objGLAccount);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //
        // GET: /FMS_GLAccount/Edit/5
        public ActionResult Edit(int id)
        {

            var glaccount=_iFMS_GLAccountService.Find(id);
            return View(glaccount);
        }

        //
        // POST: /FMS_GLAccount/Edit/5
        [HttpPost]
        public ActionResult Edit(FMS_GLAccount objGLAccount)
        {
            try
            {
                // TODO: Add update logic here
                objGLAccount.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                 objGLAccount.SetBy = GolbalSession.gblSession.UserId;
                objGLAccount.SetDate = DateTime.Now;
                _iFMS_GLAccountService.Update(objGLAccount);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FMS_GLAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FMS_GLAccount/Delete/5
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
