using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.FMS.Controllers
{
    public class DistrictController : Controller
    {
        private IDistrictService _iDistrictService;

        private readonly IUnitOfWork _unitOfWork;
        public DistrictController(IDistrictService iDistrictService, IUnitOfWork unitOfWork)
        {
            _iDistrictService = iDistrictService;
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /District/
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetDistrictByDivisionId(int divisionId)
        {
            var result = _iDistrictService.GetDistrictByDivisionID(divisionId);
            return Json(result.kvpDistrict, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /District/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /District/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /District/Create
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
        // GET: /District/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /District/Edit/5
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
        // GET: /District/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /District/Delete/5
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
