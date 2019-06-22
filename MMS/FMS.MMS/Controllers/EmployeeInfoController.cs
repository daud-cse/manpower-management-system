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
    public class EmployeeInfoController : Controller
    {
        private readonly IEmployeeInfoService _iEmployeeInfoService;               
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeInfoController(IEmployeeInfoService iEmployeeInfoService,                  
              IUnitOfWork unitOfWork)
        {
            _iEmployeeInfoService = iEmployeeInfoService;           
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /EmployeeInfo/
        public ActionResult Index(string searchItem, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 8;
            IList<EmployeeInfo> EmployeeInfolist = new List<EmployeeInfo>();
            var EmployeeInfo = _iEmployeeInfoService.GetAllEmployeeInfo(GolbalSession.gblSession.GlobalCompanyId).ToList().OrderByDescending(x => x.ID);
            EmployeeInfolist = (IList<EmployeeInfo>)EmployeeInfo.ToList();
            if (string.IsNullOrWhiteSpace(searchItem))
            {
                EmployeeInfolist = EmployeeInfolist.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                EmployeeInfolist = EmployeeInfolist.Where(p => p.Name.ToLower() == searchItem.ToLower()
                    || p.EmployeeAutoId.ToLower() == searchItem.ToLower()
                      || p.MobileNo.ToLower() == searchItem.ToLower()
                        || p.District.Name.ToLower() == searchItem.ToLower()
                         || p.Designation.Name.ToLower() == searchItem.ToLower()                      
                    ).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(EmployeeInfolist);
        }

        //
        // GET: /EmployeeInfo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /EmployeeInfo/Create
        [CustomActionFilter]
        public ActionResult Create()
        {

            var EmployeeInfo = _iEmployeeInfoService.newEmployeeInfo(GolbalSession.gblSession.GlobalCompanyId);
            EmployeeInfo.IsActive = true;
            EmployeeInfo.DateOfBirth = DateTime.Now;
            return View(EmployeeInfo);
        }

        //
        // POST: /EmployeeInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public ActionResult Create(EmployeeInfo objEmployeeInfo)
        {
            try
            {
                
                objEmployeeInfo.SetDate = DateTime.Now;
                objEmployeeInfo.SetBy = GolbalSession.gblSession.UserId;
                objEmployeeInfo.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                _iEmployeeInfoService.Insert(objEmployeeInfo);
                _unitOfWork.SaveChanges();
                var vCount=_iEmployeeInfoService.GetAllEmployeeInfo(GolbalSession.gblSession.GlobalCompanyId).Count();
                if (objEmployeeInfo.ID < 10)
                {
                    objEmployeeInfo.EmployeeAutoId = "EM0" + (vCount+1);
                }
                else
                {
                    objEmployeeInfo.EmployeeAutoId = "EM" + (vCount+1);
                }
                _iEmployeeInfoService.Update(objEmployeeInfo);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "EmployeeInfo Created Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /EmployeeInfo/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(int id)
        {
            var EmployeeInfo = _iEmployeeInfoService.Find(id);
            var EmployeeInfonew = _iEmployeeInfoService.newForEmployeeInfoEdit(EmployeeInfo,GolbalSession.gblSession.GlobalCompanyId);
            EmployeeInfo.kvpDestination = EmployeeInfonew.kvpDestination;
            EmployeeInfo.kvpCountry = EmployeeInfonew.kvpCountry;
            EmployeeInfo.kvpDistrict = EmployeeInfonew.kvpDistrict;
            EmployeeInfo.kvpDivision = EmployeeInfonew.kvpDivision;
            EmployeeInfo.kvpNationality = EmployeeInfonew.kvpNationality;
            EmployeeInfo.kvpUpazila = EmployeeInfonew.kvpUpazila;
            return View(EmployeeInfo);
        }

        //
        // POST: /EmployeeInfo/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(EmployeeInfo objEmployeeInfo)
        {
            try
            {

               
                objEmployeeInfo.SetBy = GolbalSession.gblSession.UserId;
                objEmployeeInfo.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                objEmployeeInfo.SetDate = DateTime.Now;              
                _iEmployeeInfoService.Update(objEmployeeInfo);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "EmployeeInfo Update Successfully.");
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData.Add("danger",ex.Message.ToString());
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
