using MMS.Entities.Models;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;

using MMS.Utility;
using MMS.Entities.StoredProcedures;
namespace MMS.Controllers
{
    public class CompanyController : Controller
    {

        private readonly ICompanyService _iCompanyService;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStoredProcedures _iStoredProcedures;
        public CompanyController(ICompanyService iCompanyService,IStoredProcedures iStoredProcedures, IUnitOfWork unitOfWork)
        {
            _iCompanyService = iCompanyService;
            _iStoredProcedures = iStoredProcedures;
            _unitOfWork = unitOfWork;
        }


        [CustomActionFilter]
        public ActionResult Index(string Search, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 8;
            IList<Company> companylist = new List<Company>();
            var company = _iCompanyService.GetAllCompany().ToList().OrderByDescending(x => x.ID);
            companylist = (IList<Company>)company.ToList();
            if (string.IsNullOrWhiteSpace(Search))
            {
                companylist = companylist.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                companylist = companylist.Where(p => p.Name.ToLower() == Search.ToLower()).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(companylist);
        }



        //
        // GET: /Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        [CustomActionFilter]
        // GET: /Company/Create
        public ActionResult Create()
        {
            var objCompany = _iCompanyService.newCompany(GolbalSession.gblSession.GlobalCompanyId);
            return View(objCompany);
        }

        //
        // POST: /Company/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(Company objComapny)
        {
            try
            {
                objComapny.SetDate = DateTime.Now;
                objComapny.SetBy = GolbalSession.gblSession.UserId;
                objComapny.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                objComapny.ComapnyId = _iStoredProcedures.GetAutoGenerateId(GolbalSession.gblSession.GlobalCompanyId,3);
                _iCompanyService.Insert(objComapny);
                _unitOfWork.SaveChanges();               
                TempData.Add("success", "Company Created Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Company/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(int id)
        {
            var objComapny = _iCompanyService.Find(id);
            var objCompanynew = _iCompanyService.newCompany(GolbalSession.gblSession.GlobalCompanyId);
            objComapny.kvpCountry = objCompanynew.kvpCountry;
            objComapny.kvpComapnyType = objCompanynew.kvpComapnyType;
            objComapny.kvpDivision = objCompanynew.kvpDivision;
            objComapny.kvpNationality = objCompanynew.kvpNationality;
            objComapny.kvpDistrict = objCompanynew.kvpDistrict;
            objComapny.kvpUpazila = objCompanynew.kvpUpazila;
            return View(objComapny);
        }

        //
        // POST: /Company/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(int id, Company objComapny)
        {
            try
            {
                objComapny.SetDate = DateTime.Now;
                objComapny.SetBy = GolbalSession.gblSession.UserId;
                objComapny.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                _iCompanyService.Update(objComapny);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Company Update Successfully.");
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
