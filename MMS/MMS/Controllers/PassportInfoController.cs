using MMS.Entities.Models;
using MMS.Entities.ViewModels;
using MMS.Service.ModelServices;
using MMS.Service.ViewModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using MMS.Utility;
namespace MMS.Controllers
{
    public class PassportInfoController : Controller
    {
        private readonly IPassportInfoService _passportInfoService;

        private readonly ISearchPassportInfoVMService _searchPassportInfoVMService;
        private readonly IUnitOfWork _unitOfWork;

        public PassportInfoController(IPassportInfoService passportInfoService
            , IUnitOfWork unitOfWork
            , ISearchPassportInfoVMService searchPassportInfoVMService)
        {
            _passportInfoService = passportInfoService;
            _searchPassportInfoVMService = searchPassportInfoVMService;
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /PassportInfo/
          [CustomActionFilter]
        public ActionResult CreateSearchPassportInfo()
        {
            var searchPassportInfoVM = _searchPassportInfoVMService.newSearchPassportInfoVM(GolbalSession.gblSession.GlobalCompanyId);
            searchPassportInfoVM.lstpassportInfo = new List<PassportInfo>();
            return View(searchPassportInfoVM);
        }
          [CustomActionFilter]
        public ActionResult GetPassportInfo(SearchPassportInfoVM objSearchPassportInfoVM)
        {
            _searchPassportInfoVMService.GetPassportInfo(objSearchPassportInfoVM);
            var newSearchPassportInfoVM = _searchPassportInfoVMService.newSearchPassportInfoVM(GolbalSession.gblSession.GlobalCompanyId);
            objSearchPassportInfoVM.kvpSex = newSearchPassportInfoVM.kvpSex;
            objSearchPassportInfoVM.kvpRPOType = newSearchPassportInfoVM.kvpRPOType;
            objSearchPassportInfoVM.kvpPassPortType = newSearchPassportInfoVM.kvpPassPortType;
            objSearchPassportInfoVM.kvpMaritalStatus = newSearchPassportInfoVM.kvpMaritalStatus;
            objSearchPassportInfoVM.kvpCustomer = newSearchPassportInfoVM.kvpCustomer;
            //objSearchPassportInfoVM.kvpCustomer = newSearchPassportInfoVM.kvpCustomer;
            //objSearchPassportInfoVM.kvpCountry = newSearchPassportInfoVM.kvpCountry;
            //objSearchPassportInfoVM.kvpDivision = newSearchPassportInfoVM.kvpDivision;
            //objSearchPassportInfoVM.kvpNationality = newSearchPassportInfoVM.kvpNationality;
            //objSearchPassportInfoVM.kvpDistrict = newSearchPassportInfoVM.kvpDistrict;
            //objSearchPassportInfoVM.kvpUpazila = newSearchPassportInfoVM.kvpUpazila;
            return View("CreateSearchPassportInfo", objSearchPassportInfoVM);
        }
        
          [CustomActionFilter]
          public ActionResult Index(string searchItem, int? page)
          {

              int currentPageIndex = page.HasValue ? page.Value : 1;
              const int defaultPageSize = 8;
              IList<PassportInfo> passportInfoList = new List<PassportInfo>();
              var lstPassportInfo = _passportInfoService.GetAllPassportInfo().OrderByDescending(x => x.ID);
              passportInfoList = (IList<PassportInfo>)lstPassportInfo.ToList();
              if (string.IsNullOrWhiteSpace(searchItem))
              {
                  passportInfoList = passportInfoList.ToPagedList(currentPageIndex, defaultPageSize);
              }
              else
              {
                  passportInfoList = passportInfoList.Where(p => p.OwnerName.ToLower() == searchItem.ToLower()
                      || p.Customer.Name == searchItem.ToLower()
                      || p.OwerMobileNo == searchItem.ToLower()
                      || p.NewPassportNo == searchItem.ToLower()
                      || p.PreviousPassportNo == searchItem.ToLower()
                      || p.RPOType.Name == searchItem.ToLower()
                      ).ToPagedList(currentPageIndex, defaultPageSize);
              }
              return View(passportInfoList);

          }

        //
        // GET: /PassportInfo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /PassportInfo/Create
        [CustomActionFilter]
        public ActionResult Create()
        {

            var passportInfo = _passportInfoService.newPassportInfo(GolbalSession.gblSession.GlobalCompanyId);
            return View(passportInfo);
        }

        //
        // POST: /PassportInfo/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(PassportInfo objPassportInfo)
        {
            try
            {
                objPassportInfo.SetBy = GolbalSession.gblSession.UserId;
                objPassportInfo.SetDate = DateTime.Now;
                _passportInfoService.Insert(objPassportInfo);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Passport Info save Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /PassportInfo/Edit/5
          [CustomActionFilter]
        public ActionResult Edit(int id)
        {
            var objPassportinfo = _passportInfoService.Find(id);
            var newobjPassportinfo = _passportInfoService.newPassportInfo(GolbalSession.gblSession.GlobalCompanyId);
            objPassportinfo.kvpCustomer = newobjPassportinfo.kvpCustomer;
            objPassportinfo.kvpMaritalStatus = newobjPassportinfo.kvpMaritalStatus;
            objPassportinfo.kvpPassPortType = newobjPassportinfo.kvpPassPortType;
            objPassportinfo.kvpRPOType = newobjPassportinfo.kvpRPOType;
            objPassportinfo.kvpSex = newobjPassportinfo.kvpSex;
            return View(objPassportinfo);
        }

        //
        // POST: /PassportInfo/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(PassportInfo objPassportInfo)
        {
            try
            {
                objPassportInfo.SetBy = GolbalSession.gblSession.UserId;
                objPassportInfo.SetDate = DateTime.Now;
                _passportInfoService.Update(objPassportInfo);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Passport Info update Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /PassportInfo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PassportInfo/Delete/5
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
