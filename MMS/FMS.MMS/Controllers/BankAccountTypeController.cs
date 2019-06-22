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
    public class BankAccountTypeController : Controller
    {
       private IBankAccountTypeService _iBankAccountTypeService;

        private readonly IUnitOfWork _unitOfWork;
        public BankAccountTypeController(IBankAccountTypeService iBankAccountTypeService, IUnitOfWork unitOfWork)
        {
            _iBankAccountTypeService = iBankAccountTypeService;
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {

            var lstBankAccountType=_iBankAccountTypeService.GetAllBankAccountType(GolbalSession.gblSession.GlobalCompanyId);
            return View(lstBankAccountType);
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
        public ActionResult Create(BankAccountType objBankAccountType)
        {
            try
            {
                // TODO: Add insert logic here
                  var lstBankAccountType=_iBankAccountTypeService.GetAllBankAccountType();
                if (lstBankAccountType.Any())
                {
                    objBankAccountType.BankAccountTypeId = lstBankAccountType.Max(x => x.BankAccountTypeId) + 1;
                    
                }
                else
                {
                    objBankAccountType.BankAccountTypeId = 1;
                }
                objBankAccountType.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                _iBankAccountTypeService.Insert(objBankAccountType);
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
            var objBankAccountType= _iBankAccountTypeService.Find(id);
            return View(objBankAccountType);
        }

        //
        // POST: /AgentType/Edit/5
        [HttpPost]
        public ActionResult Edit(BankAccountType objBankAccountType)
        {
            try
            {
                // TODO: Add update logic here
                objBankAccountType.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                _iBankAccountTypeService.Update(objBankAccountType);                
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
