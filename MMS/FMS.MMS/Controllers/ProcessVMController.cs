using MMS.FMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.FMS.Controllers
{
    public class ProcessVMController : Controller
    {
        //
        // GET: /Process/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Process/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Process/Create
        public ActionResult DayEndProcess()
        {
            return View();
        }
        [HttpPost]
        [CustomActionFilter]
        public ActionResult DayEndProcess(FormCollection collection)
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
        public ActionResult MonthEndProcess()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MonthEndProcess(FormCollection collection)
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
        public ActionResult YearEndProcess()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YearEndProcess(FormCollection collection)
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
        // POST: /Process/Create
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
        // GET: /Process/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Process/Edit/5
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
        // GET: /Process/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Process/Delete/5
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
