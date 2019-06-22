using MMS.Entities.ViewModels;
using MMS.Service.ModelServices;
using MMS.Service.ViewModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Controllers
{
    public class TransactionVMController : Controller
    {
        private readonly IFMS_TransactionVMService _ifMS_TransactionVMService;      
        private readonly IUnitOfWork _unitOfWork;

        public TransactionVMController(IFMS_TransactionVMService ifMS_TransactionVMService,                    
            IUnitOfWork unitOfWork)
        {
            _ifMS_TransactionVMService = ifMS_TransactionVMService;
            _unitOfWork = unitOfWork;
        }


        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /FMS_TransactionVM/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FMS_TransactionVM/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FMS_TransactionVM/Create
        [HttpPost]
        public ActionResult Create(FMS_TransactionVM objFMS_TransactionVM)
        {
            try
            {
               // _ifMS_TransactionVMService.SaveFMS_TransactionVM(objFMS_TransactionVM,_unitOfWork,false,GolbalSession.gblSession.UserId);
                TempData.Add("success", "Transaction Created Successfully.");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FMS_TransactionVM/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FMS_TransactionVM/Edit/5
        [HttpPost]
        public ActionResult Edit(FMS_TransactionVM objFMS_TransactionVM)
        {
            try
            {
                try
                {
                  //  _ifMS_TransactionVMService.UpdateFMS_TransactionVM(GolbalSession.gblSession.,objFMS_TransactionVM, _unitOfWork,false,GolbalSession.gblSession.UserId);
                    TempData.Add("success", "Transaction Created Successfully.");
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FMS_TransactionVM/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FMS_TransactionVM/Delete/5
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
