using MMS.FMS.Utility;
using MMS.Entities.Models;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
namespace MMS.FMS.Controllers
{
    public class VoucherTypeWiseOppositeGLMapController : Controller
    {
       private readonly IFMS_VoucherTypeWiseOppositeGLMapService _ifMS_VoucherTypeWiseOppositeGLMapService;
        private readonly IUnitOfWork _unitOfWork;

        public VoucherTypeWiseOppositeGLMapController(IFMS_VoucherTypeWiseOppositeGLMapService ifMS_VoucherTypeWiseOppositeGLMapService, IUnitOfWork unitOfWork)
        {
            _ifMS_VoucherTypeWiseOppositeGLMapService = ifMS_VoucherTypeWiseOppositeGLMapService;
            _unitOfWork = unitOfWork;
        }
        // GET: /FMS_GLSubsidyTypeMap/
        [CustomActionFilter]
        public ActionResult Index(string Search, int? page)
        {

            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 8;
            IList<FMS_VoucherTypeWiseOppositeGLMap> lstVoucherTypeWiseOppositeGLMap = new List<FMS_VoucherTypeWiseOppositeGLMap>();
            var voucherTypeWiseOppositeGLMap = _ifMS_VoucherTypeWiseOppositeGLMapService.GetAllFMS_VoucherTypeWiseOppositeGLMap(GolbalSession.gblSession.GlobalCompanyId).ToList().OrderByDescending(x => x.VoucherTypeId);
            lstVoucherTypeWiseOppositeGLMap = (IList<FMS_VoucherTypeWiseOppositeGLMap>)voucherTypeWiseOppositeGLMap.ToList();
            if (string.IsNullOrWhiteSpace(Search))
            {
                lstVoucherTypeWiseOppositeGLMap = lstVoucherTypeWiseOppositeGLMap.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                lstVoucherTypeWiseOppositeGLMap = lstVoucherTypeWiseOppositeGLMap.Where(p => p.FMS_GLAccount.GLAccountName.ToLower() == Search.ToLower()
                    || p.FMS_VoucherType.VoucherTypeName ==  Search.ToLower()
                    ).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(lstVoucherTypeWiseOppositeGLMap);

        }
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FMS_VoucherTypeWiseOppositeGLMap/Create
          [CustomActionFilter]
        public ActionResult Create()
        {

            var voucherTypeWiseOppositeGLMap = _ifMS_VoucherTypeWiseOppositeGLMapService.newFMS_VoucherTypeWiseOppositeGLMap(GolbalSession.gblSession.GlobalCompanyId,0, 0);
            return View(voucherTypeWiseOppositeGLMap);
        }

        //
        // POST: /FMS_VoucherTypeWiseOppositeGLMap/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(FMS_VoucherTypeWiseOppositeGLMap objFMS_VoucherTypeWiseOppositeGLMap)
        {
            try
            {
               var voucherTypeWiseOppositeGLMap=  _ifMS_VoucherTypeWiseOppositeGLMapService.GetVoucherTypeWiseOppositeGLMap(objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeId, objFMS_VoucherTypeWiseOppositeGLMap.GLAccountId,GolbalSession.gblSession.GlobalCompanyId);

             
               if (voucherTypeWiseOppositeGLMap!=null)
               {
                   TempData.Add("danger", "GL Account Already Mapped With " + voucherTypeWiseOppositeGLMap.FMS_VoucherType.VoucherTypeName + ".");
                   return RedirectToAction("Create");
                }
               if (objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeId == 7)
               {
                   var VoucherTypeWiseOppositeGLMap = _ifMS_VoucherTypeWiseOppositeGLMapService.GetOppositeGLMapByVoucherTypeId(objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeId,GolbalSession.gblSession.GlobalCompanyId);
                   if (VoucherTypeWiseOppositeGLMap != null)
                   {
                       if (VoucherTypeWiseOppositeGLMap.Count()==1)
                       {
                           TempData.Add("danger", "Cash payment Against Accounts Payable Voucher Only One GL Can be Mapped.");
                           return RedirectToAction("Create");
                       }
                   }
               }
               if (objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeId == 8)
               {
                   var VoucherTypeWiseOppositeGLMap = _ifMS_VoucherTypeWiseOppositeGLMapService.GetOppositeGLMapByVoucherTypeId(objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeId,GolbalSession.gblSession.GlobalCompanyId);
                   if (VoucherTypeWiseOppositeGLMap != null)
                   {
                       if (VoucherTypeWiseOppositeGLMap.Count() == 1)
                       {
                           TempData.Add("danger", "Cash Received Against Accounts Receivable Voucher Only One GL Can be Mapped.");
                           return RedirectToAction("Create");
                       }
                   }
               }
               if (objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeId == 9)
               {
                   var VoucherTypeWiseOppositeGLMap = _ifMS_VoucherTypeWiseOppositeGLMapService.GetOppositeGLMapByVoucherTypeId(objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeId,GolbalSession.gblSession.GlobalCompanyId);
                   if (VoucherTypeWiseOppositeGLMap != null)
                   {
                       if (VoucherTypeWiseOppositeGLMap.Count() == 1)
                       {
                           TempData.Add("danger", "Bank payment Against Accounts Payable Voucher Only One GL Can be Mapped.");
                           return RedirectToAction("Create");
                       }
                   }
               }
               if (objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeId == 10)
               {
                   var VoucherTypeWiseOppositeGLMap = _ifMS_VoucherTypeWiseOppositeGLMapService.GetOppositeGLMapByVoucherTypeId(objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeId,GolbalSession.gblSession.GlobalCompanyId);
                   if (VoucherTypeWiseOppositeGLMap != null)
                   {
                       if (VoucherTypeWiseOppositeGLMap.Count() == 1)
                       {
                           TempData.Add("danger", "Bank Received Against Accounts Receivable Voucher Only One GL Can be Mapped.");
                           return RedirectToAction("Create");
                       }
                   }
               }
               if (objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeId == 11)
               {
                   var VoucherTypeWiseOppositeGLMap = _ifMS_VoucherTypeWiseOppositeGLMapService.GetOppositeGLMapByVoucherTypeId(objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeId,GolbalSession.gblSession.GlobalCompanyId);
                   if (VoucherTypeWiseOppositeGLMap != null)
                   {
                       if (VoucherTypeWiseOppositeGLMap.Count() == 1)
                       {
                           TempData.Add("danger", "Purchase Against Accounts Payable Voucher Only One GL Can be Mapped.");
                           return RedirectToAction("Create");
                       }
                   }
               }
               if (objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeId == 12)
               {
                   var VoucherTypeWiseOppositeGLMap = _ifMS_VoucherTypeWiseOppositeGLMapService.GetOppositeGLMapByVoucherTypeId(objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeId,GolbalSession.gblSession.GlobalCompanyId);
                   if (VoucherTypeWiseOppositeGLMap != null)
                   {
                       if (VoucherTypeWiseOppositeGLMap.Count() == 1)
                       {
                           TempData.Add("danger", "Sales Against Accounts Receivable Voucher Only One GL Can be Mapped.");
                           return RedirectToAction("Create");
                       }
                   }
               }
               objFMS_VoucherTypeWiseOppositeGLMap.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                objFMS_VoucherTypeWiseOppositeGLMap.SetBy = GolbalSession.gblSession.UserId;
                objFMS_VoucherTypeWiseOppositeGLMap.SetDate = DateTime.Now;
                _ifMS_VoucherTypeWiseOppositeGLMapService.Insert(objFMS_VoucherTypeWiseOppositeGLMap);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Voucher Type Wise Opposite GL Map Created Successfully.");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //                
        // GET: /FMS_VoucherTypeWiseOppositeGLMap/Edit/5
          [CustomActionFilter]                        
        public ActionResult Edit(int id)
        {
            var VoucherTypeWiseOppositeGLMap=_ifMS_VoucherTypeWiseOppositeGLMapService.Find(id);
            var newvoucherTypeWiseOppositeGLMap = _ifMS_VoucherTypeWiseOppositeGLMapService.newFMS_VoucherTypeWiseOppositeGLMap(GolbalSession.gblSession.GlobalCompanyId,id, 0);
            VoucherTypeWiseOppositeGLMap.kvpVoucherType = newvoucherTypeWiseOppositeGLMap.kvpVoucherType;
            VoucherTypeWiseOppositeGLMap.kvpGLAccount = newvoucherTypeWiseOppositeGLMap.kvpGLAccount;
            return View(VoucherTypeWiseOppositeGLMap);
        
        }

        //
        // POST: /FMS_VoucherTypeWiseOppositeGLMap/Edit/5
        [HttpPost]
        [CustomActionFilter]                                             
        public ActionResult Edit(FMS_VoucherTypeWiseOppositeGLMap objFMS_VoucherTypeWiseOppositeGLMap)
        {
            try
            {
                var voucherTypeWiseOppositeGLMap = _ifMS_VoucherTypeWiseOppositeGLMapService.GetVoucherTypeWiseOppositeGLMap(objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeId, objFMS_VoucherTypeWiseOppositeGLMap.GLAccountId,GolbalSession.gblSession.GlobalCompanyId);
                if (voucherTypeWiseOppositeGLMap != null)
                {
                    TempData.Add("danger", "GL Account Already mapped with " + voucherTypeWiseOppositeGLMap.FMS_VoucherType.VoucherTypeName + ".");
                    return RedirectToAction("Edit", new { id = objFMS_VoucherTypeWiseOppositeGLMap.VoucherTypeWiseOppositeGLMapId });
                }
                objFMS_VoucherTypeWiseOppositeGLMap.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId; ;
                objFMS_VoucherTypeWiseOppositeGLMap.SetBy = GolbalSession.gblSession.UserId;
                objFMS_VoucherTypeWiseOppositeGLMap.SetDate = DateTime.Now;
                _ifMS_VoucherTypeWiseOppositeGLMapService.Update(objFMS_VoucherTypeWiseOppositeGLMap);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Voucher Type Wise Opposite GL Map Updated Successfully.");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FMS_VoucherTypeWiseOppositeGLMap/Delete/5
        public ActionResult Delete(int id)
        {
           
            _ifMS_VoucherTypeWiseOppositeGLMapService.Delete(id);
            _unitOfWork.SaveChanges();
            TempData.Add("success", "Voucher Type Wise Opposite GL Map Deleted Successfully.");
            return RedirectToAction("Index");
           
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
