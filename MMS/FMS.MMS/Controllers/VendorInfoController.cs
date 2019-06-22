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
    public class VendorInfoController : Controller
    {
         private readonly IVendorInfoService _VendorInfoService;
        private readonly IUnitOfWork _unitOfWork;

        public VendorInfoController(IVendorInfoService VendorInfoService, IUnitOfWork unitOfWork)
        {
            _VendorInfoService = VendorInfoService;
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /VendorInfo/
          [CustomActionFilter]
        public ActionResult Index(string Search, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 8;
            IList<VendorInfo> VendorInfoList = new List<VendorInfo>();
            var lstVendorInfo=_VendorInfoService.GetAllVendorInfo(GolbalSession.gblSession.GlobalCompanyId).ToList().OrderByDescending(x=>x.VendorTypeId);
            VendorInfoList = (IList<VendorInfo>)lstVendorInfo.ToList();
            if (string.IsNullOrWhiteSpace(Search))
            {
                VendorInfoList = VendorInfoList.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                VendorInfoList = VendorInfoList.Where(p => p.Name.ToLower() == Search.ToLower()).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(VendorInfoList);
        }

     
        // GET: /VendorInfo/Create
          [CustomActionFilter]
        public ActionResult Create()
        {
           var VendorInfo= _VendorInfoService.newVendorInfo(GolbalSession.gblSession.GlobalCompanyId);
           return View(VendorInfo);
        }

        //
        // POST: /VendorInfo/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(VendorInfo objVendorInfo)
        {
            try
            {
                objVendorInfo.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                objVendorInfo.SetDate = DateTime.Now;
                objVendorInfo.SetBy = GolbalSession.gblSession.UserId;
                objVendorInfo.VendorAutoId = "0";
                _VendorInfoService.Insert(objVendorInfo);
                _unitOfWork.SaveChanges();
                if (objVendorInfo.ID < 10)
                {
                    objVendorInfo.VendorAutoId = "VEN0" + objVendorInfo.ID;
                }
                else
                {
                    objVendorInfo.VendorAutoId = "VEN" + objVendorInfo.ID;
                }
                _VendorInfoService.Update(objVendorInfo);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "VendorInfo Created Successfully.");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /VendorInfo/Edit/5
          [CustomActionFilter]
        public ActionResult Edit(int id)
        {
           var objVendorInfo= _VendorInfoService.Find(id);
           var newObjVendorInfo=_VendorInfoService.newVendorInfo(GolbalSession.gblSession.GlobalCompanyId);
           objVendorInfo.kvpVendorType = newObjVendorInfo.kvpVendorType;          
           return View(objVendorInfo);
        }

        //
        // POST: /VendorInfo/Edit/5

        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(VendorInfo objVendorInfo)
        {
            try
            {
                // TODO: Add update logic here

                objVendorInfo.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                objVendorInfo.SetDate = DateTime.Now;
                objVendorInfo.SetBy = GolbalSession.gblSession.UserId;
                _VendorInfoService.Update(objVendorInfo);
                TempData.Add("success", "VendorInfo Update Successfully.");
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
               
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /VendorInfo/Delete/5
          [CustomActionFilter]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /VendorInfo/Delete/5
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
