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
    public class AgentTypeController : Controller
    {
       private IAgentTypeService _iAgentTypeService;

        private readonly IUnitOfWork _unitOfWork;
        public AgentTypeController(IAgentTypeService iAgentTypeService, IUnitOfWork unitOfWork)
        {
            _iAgentTypeService = iAgentTypeService;
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {

            var lstAgentType=_iAgentTypeService.GetAllAgentType(GolbalSession.gblSession.GlobalCompanyId);
            return View(lstAgentType);
        }

        //
        // GET: /AgentType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /AgentType/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AgentType/Create
        [HttpPost]
        public ActionResult Create(AgentType objAgentType)
        {
            try
            {
                // TODO: Add insert logic here
                var lstAgentType = _iAgentTypeService.GetAllAgentType();
                if (lstAgentType.Any())
                {
                    objAgentType.ID = lstAgentType.Max(x => x.ID) + 1;
                    
                }
                else
                {
                    objAgentType.ID = 1;
                }
                objAgentType.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                _iAgentTypeService.Insert(objAgentType);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Agent Type Created Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /AgentType/Edit/5
        public ActionResult Edit(int id)
        {
            var objAgentType= _iAgentTypeService.Find(id);
            return View(objAgentType);
        }

        //
        // POST: /AgentType/Edit/5
        [HttpPost]
        public ActionResult Edit(AgentType objAgentType)
        {
            try
            {
                // TODO: Add update logic here
                objAgentType.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                _iAgentTypeService.Update(objAgentType);                
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Agent Type Update Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /AgentType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /AgentType/Delete/5
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
