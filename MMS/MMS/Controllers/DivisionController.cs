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
    public class DivisionController : Controller
    {
         private IDivisionService _iDivisionService;

        private readonly IUnitOfWork _unitOfWork;
        public DivisionController(IDivisionService iDivisionService, IUnitOfWork unitOfWork)
        {
            _iDivisionService = iDivisionService;
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /Division/
        public ActionResult Index()
        {

            var Division = _iDivisionService.GetAllDivision();
            return View(Division);
            
        }
       
        //
        // GET: /Division/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Division/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Division/Create
        [HttpPost]
        public ActionResult Create(Division division)
        {
            try
            {
                // TODO: Add insert logic here
                _iDivisionService.Insert(division);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Division/Edit/5
        public ActionResult Edit(int id)
        {
          
            var division = _iDivisionService.Find(id);
            return View(division);
        }

        //
        // POST: /Division/Edit/5
        [HttpPost]
        public ActionResult Edit(Division division)
        {
            _iDivisionService.Update(division);
            _unitOfWork.SaveChanges();
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Division/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Division/Delete/5
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
