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
    public class ComplainTypeController : Controller
    {

         private readonly IComplainTypeService _iComplainTypeService;
       
        private readonly IUnitOfWork _unitOfWork;

        public ComplainTypeController(IComplainTypeService iComplainTypeService, IUnitOfWork unitOfWork)
        {
            _iComplainTypeService = iComplainTypeService;
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /ComplainType/
        public ActionResult Index()
        {
            var complainType = _iComplainTypeService.GetAllComplainType();
            return View(complainType);
        }

        //
        // GET: /ComplainType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ComplainType/Create
          [CustomActionFilter]
        public ActionResult Create()
        {
            //var complainType=
            return View();
        }

        //
        // POST: /ComplainType/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(ComplainType objComplainType)
        {
            try
            {
                objComplainType.SetBy = "Foysal";
                objComplainType.SetDate = DateTime.Now;
                _iComplainTypeService.Insert(objComplainType);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Complain Type Created Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /ComplainType/Edit/5
          [CustomActionFilter]
        public ActionResult Edit(int id)
        {
            var complainType = _iComplainTypeService.Find(id);
            return View(complainType);
        }

        //
        // POST: /ComplainType/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(int id, ComplainType objComplainType)
        {
            try
            {
                 objComplainType.SetBy = "Foysal";
                objComplainType.SetDate = DateTime.Now;
                _iComplainTypeService.Update(objComplainType);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Complain Type Updated Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /ComplainType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ComplainType/Delete/5
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
