using MMS.Entities.Models;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using MMS.FMS.Utility;
namespace MMS.FMS.Controllers
{
    public class VoucherTypeWiseGLMapController : Controller
    {
       private readonly IFMS_VoucherTypeWiseGLMapService _ifMS_VoucherTypeWiseGLMapService;
        private readonly IUnitOfWork _unitOfWork;

        public VoucherTypeWiseGLMapController(IFMS_VoucherTypeWiseGLMapService ifMS_VoucherTypeWiseGLMapService, IUnitOfWork unitOfWork)
        {
            _ifMS_VoucherTypeWiseGLMapService = ifMS_VoucherTypeWiseGLMapService;
            _unitOfWork = unitOfWork;
        }
        // GET: /FMS_GLSubsidyTypeMap/
        [CustomActionFilter]
        public ActionResult Index(string Search, int? page)
        {

            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 8;
            IList<FMS_VoucherTypeWiseGLMap> lstVoucherTypeWiseGLMap = new List<FMS_VoucherTypeWiseGLMap>();
            var voucherTypeWiseGLMap = _ifMS_VoucherTypeWiseGLMapService.GetAllFMS_VoucherTypeWiseGLMap(GolbalSession.gblSession.GlobalCompanyId).ToList().OrderByDescending(x => x.VoucherTypeId);
            lstVoucherTypeWiseGLMap = (IList<FMS_VoucherTypeWiseGLMap>)voucherTypeWiseGLMap.ToList();
            if (string.IsNullOrWhiteSpace(Search))
            {
                lstVoucherTypeWiseGLMap = lstVoucherTypeWiseGLMap.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                lstVoucherTypeWiseGLMap = lstVoucherTypeWiseGLMap.Where(p => p.FMS_GLAccount.GLAccountName.ToLower() == Search.ToLower()
                    || p.FMS_VoucherType.VoucherTypeName ==  Search.ToLower()
                    ).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(lstVoucherTypeWiseGLMap);

        }
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FMS_VoucherTypeWiseOppositeGLMap/Create
        public ActionResult Create()
        {

            var voucherTypeWiseGLMap = _ifMS_VoucherTypeWiseGLMapService.newFMS_VoucherTypeWiseGLMap(GolbalSession.gblSession.GlobalCompanyId,0, 0);
            return View(voucherTypeWiseGLMap);
        }

        //
        // POST: /FMS_VoucherTypeWiseOppositeGLMap/Create
        [HttpPost]
        public ActionResult Create(FMS_VoucherTypeWiseGLMap objFMS_VoucherTypeWiseGLMap)
        {
            try
            {
                objFMS_VoucherTypeWiseGLMap.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                var voucherTypeWiseGLMap = _ifMS_VoucherTypeWiseGLMapService.GetVoucherTypeWiseGLMap(GolbalSession.gblSession.GlobalCompanyId,objFMS_VoucherTypeWiseGLMap.VoucherTypeId, objFMS_VoucherTypeWiseGLMap.GLAccountId);
               if (voucherTypeWiseGLMap!=null)
               {
                   TempData.Add("danger", "GL Account Already mapped with " + voucherTypeWiseGLMap.FMS_VoucherType.VoucherTypeName+".");
                   return RedirectToAction("Index");
                }
                objFMS_VoucherTypeWiseGLMap.SetBy = GolbalSession.gblSession.UserId;
                objFMS_VoucherTypeWiseGLMap.SetDate = DateTime.Now;
                objFMS_VoucherTypeWiseGLMap.ActionType ="Insert";
                _ifMS_VoucherTypeWiseGLMapService.Insert(objFMS_VoucherTypeWiseGLMap);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Voucher Type Wise Default GL Map Created Successfully.");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FMS_VoucherTypeWiseOppositeGLMap/Edit/5
        public ActionResult Edit(int id)
        {
            var VoucherTypeWiseGLMap=_ifMS_VoucherTypeWiseGLMapService.Find(id);
            var newvoucherTypeWiseGLMap = _ifMS_VoucherTypeWiseGLMapService.newFMS_VoucherTypeWiseGLMap(GolbalSession.gblSession.GlobalCompanyId,id, 0);
            VoucherTypeWiseGLMap.kvpVoucherType = newvoucherTypeWiseGLMap.kvpVoucherType;
            VoucherTypeWiseGLMap.kvpGLAccount = newvoucherTypeWiseGLMap.kvpGLAccount;
            return View(VoucherTypeWiseGLMap);
        
        }

        //
        // POST: /FMS_VoucherTypeWiseOppositeGLMap/Edit/5
        [HttpPost]
        public ActionResult Edit(FMS_VoucherTypeWiseGLMap objFMS_VoucherTypeWiseGLMap)
        {try
            {
                var voucherTypeWiseGLMap = _ifMS_VoucherTypeWiseGLMapService.GetVoucherTypeWiseGLMap(GolbalSession.gblSession.GlobalCompanyId,objFMS_VoucherTypeWiseGLMap.VoucherTypeId, objFMS_VoucherTypeWiseGLMap.GLAccountId);
               if (voucherTypeWiseGLMap!=null)
               {
                   TempData.Add("success", "GL Account Already mapped with " + voucherTypeWiseGLMap.FMS_VoucherType.VoucherTypeName+".");
                   return RedirectToAction("Create");
                }
               objFMS_VoucherTypeWiseGLMap.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                objFMS_VoucherTypeWiseGLMap.SetBy = GolbalSession.gblSession.UserId;
                objFMS_VoucherTypeWiseGLMap.SetDate = DateTime.Now;
                objFMS_VoucherTypeWiseGLMap.ActionType = "Update";
                _ifMS_VoucherTypeWiseGLMapService.Update(objFMS_VoucherTypeWiseGLMap);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Voucher Type Wise Default GL Map update Successfully.");
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
           
            _ifMS_VoucherTypeWiseGLMapService.Delete(id);
            _unitOfWork.SaveChanges();
            TempData.Add("success", "Voucher Type Wise Default GL Map Deleted Successfully.");
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
