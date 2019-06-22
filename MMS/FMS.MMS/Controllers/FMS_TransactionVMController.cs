using MMS.Entities.ViewModels;
using MMS.Service.ViewModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using MMS.Utility;
using MvcPaging;
using MMS.FMS.Utility;
using MMS.Entities.Models;
using MMS.Service.ModelServices;
using MMS.Utility;
namespace MMS.FMS.Controllers
{
    public class TransactionVMController : Controller
    {
        private readonly IFMS_TransactionVMService _ifMS_TransactionVMService;
        private readonly IFMS_SettingsService _iFMS_SettingsService;
        private readonly IFMS_DayOpenCloseService _iFMS_DayOpenCloseService;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionVMController(IFMS_TransactionVMService ifMS_TransactionVMService,
             IFMS_SettingsService iFMS_SettingsService
            , IFMS_DayOpenCloseService iFMS_DayOpenCloseService
            , IUnitOfWork unitOfWork)
        {
            _ifMS_TransactionVMService = ifMS_TransactionVMService;
            _iFMS_SettingsService = iFMS_SettingsService;
            _iFMS_DayOpenCloseService = iFMS_DayOpenCloseService;
            _unitOfWork = unitOfWork;
        }

        [CustomActionFilter]
        public ActionResult Index()
        {
            //int currentPageIndex = page.HasValue ? page.Value : 1;
            //const int defaultPageSize = 8;
            //IList<FMS_Transaction> TransactionList = new List<FMS_Transaction>();
            //var lstTransaction = _ifMS_TransactionVMService.
            //TransactionList = (IList<FMS_Transaction>)lstTransaction.ToList();
            //if (string.IsNullOrWhiteSpace(searchItem))
            //{
            //    TransactionList = TransactionList.ToPagedList(currentPageIndex, defaultPageSize);
            //}
            //else
            //{
            //    companyWiseApplicantList = companyWiseApplicantList.Where(p => p.Applicant.Name.ToLower() == searchItem.ToLower()
            //        || p.Applicant.PassportNo == searchItem.ToLower()
            //        || p.Company.Name == searchItem.ToLower()
            //        || Convert.ToString(p.ApplicantId) == searchItem.ToLower()
            //        ).ToPagedList(currentPageIndex, defaultPageSize);
            //}
            return View();
        }
        
        [CustomActionFilter]
        public ActionResult MultipleCashPaymentReceived(int VoucherTypeId)
        {
            try
            {
                var settings = _iFMS_SettingsService.GetFMS_Settings(GolbalSession.gblSession.GlobalCompanyId);

                if (settings.CurrentDate == null)
                {
                    GolbalSession.gblSession.IsDayOpened = false;
                    return View("MessageShow");
                }
                else
                {
                    GolbalSession.gblSession.IsDayOpened = true;
                }
                var TransactionVM = _ifMS_TransactionVMService.newFMS_TransactionVM(GolbalSession.gblSession.GlobalCompanyId,VoucherTypeId);
                TransactionVM.Transaction.VoucherTypeId = VoucherTypeId;
                TransactionVM.TransactionDet2.kvpSubsidyAccount = new List<KeyValuePair<int, string>>();


                if (TransactionVM.VoucherTypeWiseGLMap.GLAccountId == 0 && VoucherTypeId != 5)
                {
                    TempData.Add("danger", "Voucher Type Wise Default GL is not Mapped.");
                }

                return View(TransactionVM);
            }


            catch
            {
                return View();
            }

        }
       
        [CustomActionFilter]
        public ActionResult Create(int VoucherTypeId, bool IsMultipleCashPayRec = false)
        {
            try
            {
                var settings = _iFMS_SettingsService.GetFMS_Settings(GolbalSession.gblSession.GlobalCompanyId);


                if (settings.CurrentDate == null)
                {
                    GolbalSession.gblSession.IsDayOpened = false;
                    return View("MessageShow");
                }
                else
                {
                    GolbalSession.gblSession.IsDayOpened = true;
                }


                var TransactionVM = _ifMS_TransactionVMService.newFMS_TransactionVM(GolbalSession.gblSession.GlobalCompanyId,VoucherTypeId);
                TransactionVM.Transaction.VoucherTypeId = VoucherTypeId;
                TransactionVM.TransactionDet2.kvpSubsidyAccount = new List<KeyValuePair<int, string>>();


                if (TransactionVM.VoucherTypeWiseGLMap.GLAccountId == 0 && VoucherTypeId != 5)
                {
                    TempData.Add("danger", "Voucher Type Wise Default GL is not Mapped.");
                }
                if (VoucherTypeId == 1 && IsMultipleCashPayRec == false)
                {
                    return View("CashPayment", TransactionVM);
                }
                else if (VoucherTypeId == 1 && IsMultipleCashPayRec == true)
                {
                    return View("MultipleCashPaymentReceived", TransactionVM);
                }

                else if (VoucherTypeId == 2 && IsMultipleCashPayRec == false)
                {
                    return View("CashReceived", TransactionVM);
                }
                else if (VoucherTypeId == 2 && IsMultipleCashPayRec == true)
                {
                    return View("MultipleCashPaymentReceived", TransactionVM);
                }
                else if (VoucherTypeId == 3)
                {
                    return View("BankPayment", TransactionVM);
                }
                else if (VoucherTypeId == 4)
                {
                    return View("BankReceived", TransactionVM);
                }
                else if (VoucherTypeId == 5)
                {
                    return View("Journal", TransactionVM);
                }

                else if (VoucherTypeId == 6)
                {
                    return View("Contra", TransactionVM);
                }
                else if (VoucherTypeId == 7 && IsMultipleCashPayRec == false)
                {
                    TransactionVM.TransactionDet2.GLAccountId = TransactionVM.TransactionDet2.kvpGLAccount.FirstOrDefault().Key;                                          
                    return View("CashPaymentAgainstPayable", TransactionVM);
                }
                else if (VoucherTypeId == 8 && IsMultipleCashPayRec == false)
                {
                    TransactionVM.TransactionDet2.GLAccountId = TransactionVM.TransactionDet2.kvpGLAccount.FirstOrDefault().Key;
                    return View("CashReceivedAgainstReceivable", TransactionVM);
                }
                else if (VoucherTypeId == 9 && IsMultipleCashPayRec == false)
                {
                    TransactionVM.TransactionDet2.GLAccountId = TransactionVM.TransactionDet2.kvpGLAccount.FirstOrDefault().Key;
                    return View("BankPaymentAgainstPayable", TransactionVM);
                }
                else if (VoucherTypeId == 10 && IsMultipleCashPayRec == false)
                {
                    TransactionVM.TransactionDet2.GLAccountId = TransactionVM.TransactionDet2.kvpGLAccount.FirstOrDefault().Key;
                    return View("BankReceivedAgainstReceivable", TransactionVM);
                }
                else if (VoucherTypeId == 11 && IsMultipleCashPayRec == false)
                {
                    //TransactionVM.TransactionDet2.GLAccountId = TransactionVM.TransactionDet2.kvpGLAccount.FirstOrDefault().Key;
                    return View("PurchaseAgainstPayable", TransactionVM);
                }
                else if (VoucherTypeId == 12 && IsMultipleCashPayRec == false)
                {
                   // TransactionVM.TransactionDet2.GLAccountId = TransactionVM.TransactionDet2.kvpGLAccount.FirstOrDefault().Key;
                    return View("SalesAgainstReceivable", TransactionVM);
                }
                return View(TransactionVM);
            }


            catch
            {
                return View();
            }

        }

        //
        // POST: /FMS_TransactionVM/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(FMS_TransactionVM objFMS_TransactionVM)
        {
            try
            {
                var settings = _iFMS_SettingsService.GetFMS_Settings(GolbalSession.gblSession.GlobalCompanyId);
                if (settings.CurrentDate == null)
                {
                    TempData.Add("success", "Day Already Closed you ca'nt Transaction.");
                    return RedirectToAction("Index", "Transaction");
                }

                var opendate = settings.CurrentDate == null ? DateTime.Now: Convert.ToDateTime(settings.CurrentDate);
                var dayopenclose = _iFMS_DayOpenCloseService.GetFMS_DayOpenClose(GolbalSession.gblSession.GlobalCompanyId, opendate);
                if (dayopenclose == null)
                {
                    TempData.Add("success", "Day Not open.");
                    return RedirectToAction("Index", "Transaction");
                }
                else if (dayopenclose.IsDayClosed)
                {
                    TempData.Add("success", "Day is closed you can not tansaction.");
                    return RedirectToAction("Index", "Transaction");
                }
                if (objFMS_TransactionVM.Transaction.TransactionId > 0)
                {
                    _ifMS_TransactionVMService.UpdateFMS_TransactionVM(GolbalSession.gblSession.GlobalCompanyId,objFMS_TransactionVM, _unitOfWork, false, GolbalSession.gblSession.UserId);
                    TempData.Add("success", "Transaction Updated Successfully.");
                }
                else
                {
                    _ifMS_TransactionVMService.SaveFMS_TransactionVM(GolbalSession.gblSession.GlobalCompanyId,objFMS_TransactionVM, _unitOfWork, false, GolbalSession.gblSession.UserId);
                    TempData.Add("success", "Transaction Created Successfully.");
                }

                return RedirectToAction("Index", "Transaction");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message);
                return RedirectToAction("Index", "Transaction");
            }
        }
        [HttpPost]
        [CustomActionFilter]
        public ActionResult CreateOther(FMS_Transaction objTransaction, List<FMS_TransactionDet> objTransactionDetList)
        {
            try
            {
                var settings = _iFMS_SettingsService.GetFMS_Settings(GolbalSession.gblSession.GlobalCompanyId);
                if (settings.CurrentDate == null)
                {
                    TempData.Add("success", "Day Already Closed you ca'nt Transaction.");
                    return RedirectToAction("Index", "Transaction");
                }
                var opendate = settings.CurrentDate == null ? DateTime.Now : Convert.ToDateTime(settings.CurrentDate);
                var dayopenclose = _iFMS_DayOpenCloseService.GetFMS_DayOpenClose(GolbalSession.gblSession.GlobalCompanyId, opendate);
                if (dayopenclose == null)
                {
                    TempData.Add("success", "Day Not open.");
                    return RedirectToAction("Index", "Transaction");
                }
                else if (dayopenclose.IsDayClosed)
                {
                    TempData.Add("success", "Day is closed you can not tansaction.");
                    return RedirectToAction("Index", "Transaction");
                }

                var objFMS_TransactionVM = new FMS_TransactionVM();
                objFMS_TransactionVM.Transaction = new FMS_Transaction();
                objFMS_TransactionVM.TransactionDetList = new List<FMS_TransactionDet>();
                objFMS_TransactionVM.Transaction = objTransaction;
                objFMS_TransactionVM.TransactionDetList = objTransactionDetList;
                if (objFMS_TransactionVM.Transaction.TransactionId > 0)
                {
                    _ifMS_TransactionVMService.UpdateFMS_TransactionVM(GolbalSession.gblSession.GlobalCompanyId,objFMS_TransactionVM, _unitOfWork, true, GolbalSession.gblSession.UserId);
                    TempData.Add("success", "Transaction Updated Successfully.");
                }
                else
                {
                    _ifMS_TransactionVMService.SaveFMS_TransactionVM(GolbalSession.gblSession.GlobalCompanyId,objFMS_TransactionVM, _unitOfWork, true, GolbalSession.gblSession.UserId);
                    TempData.Add("success", "Transaction Created Successfully.");
                }

                return RedirectToAction("Index", "Transaction");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message);
                return RedirectToAction("Index", "Transaction");
            }
        }
        //
        // GET: /FMS_TransactionVM/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(string encrid)
        {
            encrid = Encryption.decrypt(encrid);
            int id = Convert.ToInt32(encrid);

            var objTransactionVM = _ifMS_TransactionVMService.GetTransactionVMByTransactionId(id,GolbalSession.gblSession.GlobalCompanyId);

            var TransactionVM = _ifMS_TransactionVMService.newFMS_TransactionVM(GolbalSession.gblSession.GlobalCompanyId,objTransactionVM.Transaction.VoucherTypeId);
            objTransactionVM.VoucherTypeWiseGLMap = TransactionVM.VoucherTypeWiseGLMap;
            objTransactionVM.Transaction.kvpVoucherType = TransactionVM.Transaction.kvpVoucherType;
            objTransactionVM.TransactionDet2.kvpGLAccount = TransactionVM.TransactionDet2.kvpGLAccount;
            TransactionVM.Transaction.ValueDate = DateTime.Now;
            TransactionVM.TransactionDet2.kvpSubsidyAccount = new List<KeyValuePair<int, string>>();
            if (objTransactionVM.Transaction.VoucherTypeId == 1 && objTransactionVM.Transaction.IsMultipleOrSingle == false)
            {
                return View("CashPayment", objTransactionVM);
            }
            if (objTransactionVM.Transaction.VoucherTypeId == 1 && objTransactionVM.Transaction.IsMultipleOrSingle == true)
            {
                return View("MultipleCashPaymentReceived", objTransactionVM);
            }

            else if (objTransactionVM.Transaction.VoucherTypeId == 2 && objTransactionVM.Transaction.IsMultipleOrSingle == false)
            {
                return View("CashReceived", objTransactionVM);
            }
            if (objTransactionVM.Transaction.VoucherTypeId == 2 && objTransactionVM.Transaction.IsMultipleOrSingle == true)
            {
                return View("MultipleCashPaymentReceived", objTransactionVM);
            }
            else if (objTransactionVM.Transaction.VoucherTypeId == 3)
            {
                return View("BankPayment", objTransactionVM);
            }
            else if (objTransactionVM.Transaction.VoucherTypeId == 4)
            {
                return View("BankReceived", objTransactionVM);
            }
            else if (objTransactionVM.Transaction.VoucherTypeId == 5)
            {
                return View("Journal", objTransactionVM);
            }
            else if (objTransactionVM.Transaction.VoucherTypeId == 6)
            {
                return View("Contra", objTransactionVM);
            }
            else if (objTransactionVM.Transaction.VoucherTypeId==7)
            {
                return View("CashPaymentAgainstPayable", objTransactionVM);
            }
            else if (objTransactionVM.Transaction.VoucherTypeId == 8)
            {
                return View("CashReceivedAgainstReceivable", objTransactionVM);
            }
            else if (objTransactionVM.Transaction.VoucherTypeId == 9)
            {
                return View("BankPaymentAgainstPayable", objTransactionVM);
            }
            else if (objTransactionVM.Transaction.VoucherTypeId == 10)
            {
                return View("BankReceivedAgainstReceivable", objTransactionVM);
            }
            else if (objTransactionVM.Transaction.VoucherTypeId == 11)
            {
                return View("PurchaseAgainstPayable", objTransactionVM);
            }
            else if (objTransactionVM.Transaction.VoucherTypeId == 12)
            {
                return View("SalesAgainstReceivable", objTransactionVM);
            }
            return View(objTransactionVM);
        }

        //
        // POST: /FMS_TransactionVM/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(FMS_TransactionVM objFMS_TransactionVM)
        {
            try
            {
                try
                {
                    _ifMS_TransactionVMService.SaveFMS_TransactionVM(GolbalSession.gblSession.GlobalCompanyId,objFMS_TransactionVM, _unitOfWork, false, GolbalSession.gblSession.UserId);
                    TempData.Add("success", "Transaction Updated Successfully.");
                    return RedirectToAction("Index", "Transaction");
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
        // [CustomActionFilter]
        public JsonResult Delete(int id)
        {

            var result = _ifMS_TransactionVMService.DeleteTransactionDet(id);
            return Json(result, JsonRequestBehavior.AllowGet);
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
