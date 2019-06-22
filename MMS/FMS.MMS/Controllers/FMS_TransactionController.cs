using MMS.FMS.Utility;
using MMS.Service.ViewModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using Repository.Pattern.UnitOfWork;
using MMS.Entities.Models;
using MMS.Service.ModelServices;
using MMS.Entities.ViewModels;
namespace MMS.FMS.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IFMS_TransactionService _ifMS_TransactionService;
        private readonly IFMS_SettingsService _iFMS_SettingsService;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionController(IFMS_TransactionService ifMS_TransactionService,
            IFMS_SettingsService iFMS_SettingsService,
            IUnitOfWork unitOfWork)
        {
            _ifMS_TransactionService = ifMS_TransactionService;
            _iFMS_SettingsService = iFMS_SettingsService;
            _unitOfWork = unitOfWork;
        }
        [CustomActionFilter]
        public ActionResult Index(string searchItem1,string searchItem, int? page)
        {
            var settings = _iFMS_SettingsService.GetFMS_Settings(GolbalSession.gblSession.GlobalCompanyId);



            if (settings.CurrentDate == null)
            {
                GolbalSession.gblSession.IsDayOpened = false;
               // return View("../TransactionVM/MessageShow");
            }
            else
            {
                GolbalSession.gblSession.IsDayOpened = true;
                if (page == null && String.IsNullOrEmpty(searchItem))
                {
                    searchItem = settings.CurrentDate.ToString();
                }
            }
           
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 15;
            IList<FMS_Transaction> TransactionList = new List<FMS_Transaction>();
            var lstTransaction = _ifMS_TransactionService.GetAllFMS_Transaction(GolbalSession.gblSession.GlobalCompanyId).OrderByDescending(x => x.TransactionId);
            TransactionList = (IList<FMS_Transaction>)lstTransaction.ToList();
            if (string.IsNullOrWhiteSpace(searchItem))
            {
                TransactionList = TransactionList.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                if (!string.IsNullOrEmpty(searchItem1))
                {
                    TransactionList = TransactionList.Where(p => p.VoucherId.ToLower() == searchItem1.ToLower()

                      ).ToPagedList(currentPageIndex, defaultPageSize);
                   
                }
                else
                {
                    TransactionList = TransactionList.Where(p => (p.TransactionDate) == Convert.ToDateTime(searchItem)
                        ).ToPagedList(currentPageIndex, defaultPageSize);
                }
            }
            ViewData["searchItem"] = searchItem;
            ViewData["searchItem1"] = searchItem1;
            return View(TransactionList);
        }

        public JsonResult GetOpponentTransactionByVoucherId(string VoucherId, int GLAccountId, string SearchItem)
        {
            var opponentTransaction = _ifMS_TransactionService.FMS_GetOpponentTransactionByVoucherId(GolbalSession.gblSession.GlobalCompanyId, VoucherId, GLAccountId,SearchItem);
            var objopponentTransaction = new List<OpponentTransactionVM>();
           
            return new JsonResult()
            {
                Data = opponentTransaction.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [CustomActionFilter]
        public ActionResult UnApprovedTransactionList(int? page, DateTime CurrentDate, bool IsApproved = false)
        {


            var CurrentDateConvert = Convert.ToDateTime(CurrentDate);

            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 1200;
            IList<FMS_Transaction> TransactionList = new List<FMS_Transaction>();
            var lstTransaction = _ifMS_TransactionService.UnApprovedTransactionList(GolbalSession.gblSession.GlobalCompanyId,IsApproved, CurrentDate).OrderByDescending(x => x.TransactionId);
            TransactionList = (IList<FMS_Transaction>)lstTransaction.ToList();

            TransactionList = TransactionList.Where(p => p.IsApproved == IsApproved && p.TransactionDate == CurrentDate).ToPagedList(currentPageIndex, defaultPageSize);

            return View(TransactionList);
        }
        //
        // GET: /FMS_Transaction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [CustomActionFilter]
        public ActionResult TransactionApprovedForDayClose(int id, bool IsApproved)
        {
            var setting = _iFMS_SettingsService.GetFMS_Settings(GolbalSession.gblSession.GlobalCompanyId);
            try
            {


                if (setting == null)
                {
                    setting = new FMS_Settings();
                }
                var msg = _ifMS_TransactionService.FMS_TransactionApproved(GolbalSession.gblSession.GlobalCompanyId,id, IsApproved,GolbalSession.gblSession.UserId);
                TempData.Add("success", msg);
                return RedirectToAction("UnApprovedTransactionList", new { CurrentDate = setting.CurrentDate, IsApproved = false });
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("UnApprovedTransactionList", new { CurrentDate = setting.CurrentDate, IsApproved = false });
            }
        }
        [CustomActionFilter]
        public ActionResult TransactionApproved(int id, bool IsApproved)
        {
            try
            {
                var msg = _ifMS_TransactionService.FMS_TransactionApproved(GolbalSession.gblSession.GlobalCompanyId, id, IsApproved, GolbalSession.gblSession.UserId);
                TempData.Add("success", msg);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }
        [CustomActionFilter]
        public ActionResult TransactionDelete(int id)
        {
            try
            {
                _ifMS_TransactionService.Delete(id);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Transaction Deleted Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
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
