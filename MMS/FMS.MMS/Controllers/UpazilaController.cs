using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.FMS.Controllers
{
    public class UpazilaController : Controller
    {
        private IUpazilaService _iUpazilaService;

        private readonly IUnitOfWork _unitOfWork;
        public UpazilaController(IUpazilaService iUpazilaService, IUnitOfWork unitOfWork)
        {
            _iUpazilaService = iUpazilaService;
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /Upazila/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Upazila/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public JsonResult GetUpazilaByDistrictId(int districtId)
        {
            var result = _iUpazilaService.GetUpazilaByDistrictID(districtId);
            return Json(result.kvpUpazila, JsonRequestBehavior.AllowGet);
        }
        //
        //
        // GET: /Upazila/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Upazila/Create
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
        // GET: /Upazila/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Upazila/Edit/5
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
        // GET: /Upazila/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Upazila/Delete/5
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
