using MMS.Service.ModelServices;
using MMS.Service.ViewModelServices;
using MMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Controllers
{
    public class DashboardController : Controller
    {

        private readonly IDashboardVMService _dashboardVMService;
        public DashboardController(IDashboardVMService dashboardVMService)
        {
            _dashboardVMService = dashboardVMService;
        }
        [CustomActionFilter]
        public ActionResult GetDashboard(int moduleId = 0)
        {

            return View();

        }
        [CustomActionFilter]
        public ActionResult Index(int moduleId = 0)
        {
            var dashboardVM = _dashboardVMService.GetMPMSDashboard(GolbalSession.gblSession.GlobalCompanyId,moduleId);
            return View(dashboardVM);
        }

        //
        // GET: /Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Dashboard/Create
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
        // GET: /Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Dashboard/Edit/5
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
        // GET: /Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Dashboard/Delete/5
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
