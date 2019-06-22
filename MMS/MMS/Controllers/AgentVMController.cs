using MMS.Entities.ViewModels;
using MMS.Service.ViewModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Controllers
{
    public class AgentVMController : Controller
    {
        private readonly IAgentVMService _agentVMService;
        private readonly IUnitOfWork _unitOfWork;

        public AgentVMController(IAgentVMService agentVMService, IUnitOfWork unitOfWork)
        {
            _agentVMService = agentVMService;
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /AgentVM/
        public ActionResult Index()
        {
            var agentVM=_agentVMService.GetAgentVM(GolbalSession.gblSession.GlobalCompanyId);
            return View(agentVM);
        }

        //
        // GET: /AgentVM/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /AgentVM/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AgentVM/Create
        [HttpPost]
        public ActionResult Create(AgentVM objAgentVM)
        {
            try
            {
                // TODO: Add insert logic here
                TempData.Add("success", "Agent created successfully.");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /AgentVM/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /AgentVM/Edit/5
        [HttpPost]
        public ActionResult Edit(AgentVM objAgentVM)
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
        // GET: /AgentVM/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /AgentVM/Delete/5
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
