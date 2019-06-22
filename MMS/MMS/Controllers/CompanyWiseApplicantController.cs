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
namespace MMS.Controllers
{
    public class CompanyWiseApplicantController : Controller
    {
        private ICompanyWiseApplicantService _iCompanyWiseApplicantService;
        private readonly IUnitOfWork _unitOfWork;
        public CompanyWiseApplicantController(ICompanyWiseApplicantService iCompanyWiseApplicantService, IUnitOfWork unitOfWork)
        {
            _iCompanyWiseApplicantService = iCompanyWiseApplicantService;
            _unitOfWork = unitOfWork;
        }
      
       
           [CustomActionFilter]
        public ActionResult Index(string searchItem, int? page)
        {
          
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 8;
            IList<CompanyWiseApplicant> companyWiseApplicantList = new List<CompanyWiseApplicant>();
            var lstCompanyWiseApplicant = _iCompanyWiseApplicantService.GetAllCompanyWiseApplicant().OrderByDescending(x => x.ID);
            companyWiseApplicantList = (IList<CompanyWiseApplicant>)lstCompanyWiseApplicant.ToList();
            if (string.IsNullOrWhiteSpace(searchItem))
            {
                companyWiseApplicantList = companyWiseApplicantList.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                companyWiseApplicantList = companyWiseApplicantList.Where(p => p.Applicant.Name.ToLower() == searchItem.ToLower()
                    || p.Applicant.PassportNo == searchItem.ToLower()
                    || p.Company.Name == searchItem.ToLower()
                    || Convert.ToString(p.ApplicantId) == searchItem.ToLower()
                    ).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(companyWiseApplicantList);

        }


        //
        // GET: /CompanyWiseApplicant/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CompanyWiseApplicant/Create
        [CustomActionFilter]
        public ActionResult Create()
        {

            var objCompanyWiseApplicant = _iCompanyWiseApplicantService.newCompanyWiseApplicant();
            return View(objCompanyWiseApplicant);
        }

        //
        // POST: /CompanyWiseApplicant/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(List<CompanyWiseApplicant> lstCompanyWiseApplicant)
        {
            try
            {
                // TODO: Add insert logic here
                _iCompanyWiseApplicantService.SaveCompanyWiseApplicant(lstCompanyWiseApplicant, _unitOfWork, GolbalSession.gblSession.UserId);
                TempData.Add("success", "Company Applicant Mapped Save Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /CompanyWiseApplicant/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(int id)
        {
            _iCompanyWiseApplicantService.GetCompanyWiseApplicantById(id);
            return View();
        }

        //
        // POST: /CompanyWiseApplicant/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /CompanyWiseApplicant/Delete/5
        [CustomActionFilter]
        public ActionResult Delete(int id)
        {
          var  companyWiseApplicant= _iCompanyWiseApplicantService.GetCompanyWiseApplicantById(id);
          return View(companyWiseApplicant);
        }

        //
        // POST: /CompanyWiseApplicant/Delete/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Delete(CompanyWiseApplicant objCompanyWiseApplicant)
        {
            try
            {
                // TODO: Add delete logic here
                _iCompanyWiseApplicantService.Delete(objCompanyWiseApplicant.ID);
                _unitOfWork.SaveChanges();
                TempData.Add("danger", "Company Applicant Mapped delete Successfully.");
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
