using MMS.Entities.Models;
using MMS.SECURITY;
using MMS.SECURITY.Utility;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SECURITY.MMS.Controllers
{
    public class GlobalCompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGlobalCompanyService _iGlobalCompanyService;
        public GlobalCompanyController(
            IUnitOfWork unitOfWork
            , IGlobalCompanyService iGlobalCompanyService)
        {

            _unitOfWork = unitOfWork;
            _iGlobalCompanyService = iGlobalCompanyService;
        }
        // GET: /GlobalCompany/
          [CustomActionFilter]
        public ActionResult Index()
        {
           var objGlobalCompany= _iGlobalCompanyService.GetGlobalCompanyById(GolbalSession.gblSession.GlobalCompanyId);
           return View(objGlobalCompany);
        }

        //
        // GET: /GlobalCompany/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /GlobalCompany/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GlobalCompany/Create
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
        // GET: /GlobalCompany/Edit/5

         [CustomActionFilter]
        public ActionResult Edit(int id)
        {
           var objGlobalCompany=  _iGlobalCompanyService.Find(id);
           return View(objGlobalCompany);
        }

        //
        // POST: /GlobalCompany/Edit/5
        [HttpPost]
        [CustomActionFilter]
         public ActionResult Edit(GlobalCompany objGlobalCompany)
        {
            try
            {
                // TODO: Add update logic here
                objGlobalCompany.PackageId = 1;
                _iGlobalCompanyService.Update(objGlobalCompany);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /GlobalCompany/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /GlobalCompany/Delete/5
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
