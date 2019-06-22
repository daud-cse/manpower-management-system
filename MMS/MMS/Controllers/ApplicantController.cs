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
    public class ApplicantController : Controller
    {

        private readonly IApplicantService _applicantService;
        private readonly ISearchApplicantVMService _searchApplicantVMService;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicantController(IApplicantService applicantService, ISearchApplicantVMService searchApplicantVMService, IUnitOfWork unitOfWork)
        {
            _applicantService = applicantService;
            _searchApplicantVMService = searchApplicantVMService;
            _unitOfWork = unitOfWork;
        }

        [CustomActionFilter]
        public ActionResult Index(string searchItem, int? page)
        {

            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 15;
            IList<Applicant> applicantList = new List<Applicant>();
            var lstApplicant = _applicantService.GetAllApplicant(GolbalSession.gblSession.GlobalCompanyId).OrderByDescending(x => x.ID);
            applicantList = (IList<Applicant>)lstApplicant.ToList();
            if (string.IsNullOrWhiteSpace(searchItem))
            {
                applicantList = applicantList.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                applicantList = applicantList.Where(p => p.Name.ToLower().Contains(searchItem.ToLower())
                     || p.PassportNo.ToLower().Contains(searchItem.ToLower())
                     || (p.PreRefApplicantId == null ? "" : p.PreRefApplicantId).ToLower().Contains(searchItem.ToLower())
                     || Convert.ToString(p.ID) == searchItem.ToLower()
                     || p.ApplicantId.ToLower().Contains(searchItem.ToLower())
                     || p.Agent.Name.ToLower().Contains(searchItem.ToLower())
                     || (p.VisaNo == null ? "" : p.VisaNo).ToLower().Contains(searchItem.ToLower())
                     || (p.ApplicantPhoneNo == null ? "" : p.ApplicantPhoneNo).ToLower().Contains(searchItem.ToLower())
                     || p.Country.Name.ToLower().Contains(searchItem.ToLower())
                     || Convert.ToString(p.ApplicantId) == searchItem.ToLower()
                    ).ToPagedList(currentPageIndex, defaultPageSize);
            }
            ViewData["searchItem"] = searchItem;
            return View(applicantList);

        }
        [CustomActionFilter]
        public ActionResult GetApplicantByLocation(string searchItem, int? page, int? id)
        {
            //  var locationId=id==null?0;
            if (id != null)
            {
                Session["locationId"] = id;

            }
            else
            {
                id = 0;
            }
            var locationid = Session["locationId"];
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 15;
            IList<Applicant> applicantList = new List<Applicant>();
            var lstApplicant = _applicantService.GetAllApplicantByLocationId(Convert.ToInt32(locationid)).OrderByDescending(x => x.ID);
            applicantList = (IList<Applicant>)lstApplicant.ToList();
            if (string.IsNullOrWhiteSpace(searchItem))
            {
                applicantList = applicantList.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                applicantList = applicantList.Where(p => p.Name.ToLower() == searchItem.ToLower()
                     || p.PassportNo == searchItem.ToLower()
                     || p.PreRefApplicantId == searchItem.ToLower()
                     || Convert.ToString(p.ID) == searchItem.ToLower()
                     || p.ApplicantId == searchItem.ToLower()
                     || p.Agent.Name == searchItem.ToLower()
                     || p.VisaNo == searchItem.ToLower()
                     || p.ApplicantPhoneNo == searchItem.ToLower()
                     || p.Country.Name == searchItem.ToLower()
                     || Convert.ToString(p.ApplicantId) == searchItem.ToLower()
                    ).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(applicantList);

        }
        [CustomActionFilter]
        public JsonResult GetApplicantByPassportNo(string passportNo)
        {
            var applicant = _applicantService.GetApplicantByPassportNo(GolbalSession.gblSession.GlobalCompanyId, passportNo);
            if (applicant==null)
            {
                applicant = new Applicant();
            }
            return new JsonResult()
            {
                Data = applicant,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [CustomActionFilter]
        public JsonResult GetCompletedApplicantList(string userId, string searchItem)
        {
            var applicantList = _applicantService.GetCompletedApplicantsByCriteria(searchItem);
            return new JsonResult()
            {
                Data = applicantList.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [CustomActionFilter]
        public JsonResult GetCompletedApplicantListForCompanyMap(string userId, string searchItem)
        {
            var applicantList = _applicantService.GetCompletedApplicantListForCompanyMap(searchItem);
            return new JsonResult()
            {
                Data = applicantList.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [CustomActionFilter]
        public JsonResult GetApplicantList(string userId, string searchItem)
        {
            var applicantList = _applicantService.GetApplicantsByCriteria(searchItem);
            return new JsonResult()
            {
                Data = applicantList.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult CheckOnlineStatus(Applicant objApplicant)
        {

            var applicant = _applicantService.CheckOnlineStatus(objApplicant);
            return View(applicant);

        }
        [CustomActionFilter]
        public ActionResult createApplicantSearch(int? page, int? agentId)
        {

            var searchApplicantVM = _searchApplicantVMService.newSearchApplicantVM(GolbalSession.gblSession.GlobalCompanyId);
            searchApplicantVM.lstApplicant = new List<Applicant>();
            searchApplicantVM.iapplicantlist = (IList<Applicant>)searchApplicantVM.lstApplicant;
            searchApplicantVM.iapplicantlist = searchApplicantVM.iapplicantlist.ToPagedList(1, 1);
            return View(searchApplicantVM);
        }
        
        [CustomActionFilter]
        public ActionResult GetApplicantSearch(int? page, SearchApplicantVM objSearchApplicantVM)
        {
         
            if (page==null)
            {
                Session["objSearchApplicantVM"] = objSearchApplicantVM;
            }
            
            if (page>0)
            {
                
                objSearchApplicantVM = (SearchApplicantVM)Session["objSearchApplicantVM"];
            }
            

            const int defaultPageSize = 15;
            int currentPageIndex = page.HasValue ? page.Value : 1;
            objSearchApplicantVM.iapplicantlist = new List<Applicant>();
            objSearchApplicantVM.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
            _searchApplicantVMService.GetApplicant(objSearchApplicantVM);

            objSearchApplicantVM.lstApplicant = objSearchApplicantVM.lstApplicant.OrderByDescending(x => x.ID).ToList();

            objSearchApplicantVM.iapplicantlist = (IList<Applicant>)objSearchApplicantVM.lstApplicant.ToList();

            objSearchApplicantVM.iapplicantlist = objSearchApplicantVM.iapplicantlist.ToPagedList(currentPageIndex, defaultPageSize);
            var newSearchApplicantVM = _searchApplicantVMService.newSearchApplicantVM(GolbalSession.gblSession.GlobalCompanyId);
            objSearchApplicantVM.kvpAgent = newSearchApplicantVM.kvpAgent;
            objSearchApplicantVM.kvpCountry = newSearchApplicantVM.kvpCountry;
            objSearchApplicantVM.kvpDivision = newSearchApplicantVM.kvpDivision;
            objSearchApplicantVM.kvpNationality = newSearchApplicantVM.kvpNationality;
            objSearchApplicantVM.kvpDistrict = newSearchApplicantVM.kvpDistrict;
            objSearchApplicantVM.kvpUpazila = newSearchApplicantVM.kvpUpazila;
            objSearchApplicantVM.kvpLocation = newSearchApplicantVM.kvpLocation;

            if (Request.IsAjaxRequest())
                return PartialView("_ApplicantList", objSearchApplicantVM);
            else
                return View("createApplicantSearch", objSearchApplicantVM);
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
