using MMS.Service.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using Repository.Pattern.UnitOfWork;
using MMS.FMS.Utility;
using MMS.Entities.Models;
namespace MMS.FMS.Controllers
{
    public class BankAccountInfoController : Controller
    {
        private readonly IBankAccountInfoService _BankAccountInfoService;
        private readonly IUnitOfWork _unitOfWork;

        public BankAccountInfoController(IBankAccountInfoService BankAccountInfoService, IUnitOfWork unitOfWork)
        {
            _BankAccountInfoService = BankAccountInfoService;
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /BankAccountInfo/
        [CustomActionFilter]
        public ActionResult Index(string Search, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 8;
            IList<BankAccountInfo> BankAccountInfoList = new List<BankAccountInfo>();
            var lstBankAccountInfo = _BankAccountInfoService.GetAllBankAccountInfo(GolbalSession.gblSession.GlobalCompanyId).ToList().OrderByDescending(x => x.ID);
            BankAccountInfoList = (IList<BankAccountInfo>)lstBankAccountInfo.ToList();
            if (string.IsNullOrWhiteSpace(Search))
            {
                BankAccountInfoList = BankAccountInfoList.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                BankAccountInfoList = BankAccountInfoList.Where(p => p.Name.ToLower() == Search.ToLower()).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(BankAccountInfoList);
        }

        //
        // GET: /BankAccountInfo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /BankAccountInfo/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            var BankAccountInfo = _BankAccountInfoService.newBankAccountInfo(GolbalSession.gblSession.GlobalCompanyId);
            BankAccountInfo.AccountClosingDate = DateTime.Now;
            BankAccountInfo.AccountOpeningDate = DateTime.Now;
            return View(BankAccountInfo);
        }

        //
        // POST: /BankAccountInfo/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(BankAccountInfo objBankAccountInfo)
        {
            try
            {

                objBankAccountInfo.SetDate = DateTime.Now;
                objBankAccountInfo.BankAccountAutoId = "0";
                objBankAccountInfo.SetBy = GolbalSession.gblSession.UserId;
                objBankAccountInfo.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                _BankAccountInfoService.Insert(objBankAccountInfo);
                _unitOfWork.SaveChanges();
                if (objBankAccountInfo.ID < 10)
                {
                    objBankAccountInfo.BankAccountAutoId = "BANK0" + objBankAccountInfo.ID;
                }
                else
                {
                    objBankAccountInfo.BankAccountAutoId = "BANK" + objBankAccountInfo.ID;
                }
                _BankAccountInfoService.Update(objBankAccountInfo);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Bank Account Created Successfully.");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BankAccountInfo/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(int id)
        {
            var objBankAccountInfo = _BankAccountInfoService.Find(id);
            var newObjBankAccountInfo = _BankAccountInfoService.newBankAccountInfo(GolbalSession.gblSession.GlobalCompanyId);
            objBankAccountInfo.kvpBankAccountType = newObjBankAccountInfo.kvpBankAccountType;

            return View(objBankAccountInfo);
        }

        //
        // POST: /BankAccountInfo/Edit/5

        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(BankAccountInfo objBankAccountInfo)
        {
            try
            {
                // TODO: Add update logic here
                objBankAccountInfo.SetDate = DateTime.Now;
                objBankAccountInfo.SetBy = GolbalSession.gblSession.UserId;
                objBankAccountInfo.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                _BankAccountInfoService.Update(objBankAccountInfo);
                TempData.Add("success", "BankAccountInfo Update Successfully.");
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BankAccountInfo/Delete/5
        [CustomActionFilter]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BankAccountInfo/Delete/5
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
