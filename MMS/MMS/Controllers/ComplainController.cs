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
    public class ComplainController : Controller
    {

        private readonly IComplainService _iComplainService;

        private readonly IUnitOfWork _unitOfWork;

        public ComplainController(IComplainService iComplainService, IUnitOfWork unitOfWork)
        {
            _iComplainService = iComplainService;
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /Complain/
          [CustomActionFilter]
        public ActionResult Index(string Search, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 8;
            IList<Complain> complainList = new List<Complain>();
            var complain = _iComplainService.GetAllComplain().ToList().OrderByDescending(x => x.ID);
            complainList = (IList<Complain>)complain.ToList();
            if (string.IsNullOrWhiteSpace(Search))
            {
                complainList = complainList.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                complainList = complainList.Where(p => p.ComplainPersonName.ToLower() == Search.ToLower()).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(complainList);
        }

        //
        // GET: /Complain/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Complain/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            var complain = _iComplainService.newComplain(GolbalSession.gblSession.GlobalCompanyId);
            complain.ComplainStartDate = DateTime.Now;
            return View(complain);
        }

        //
        // POST: /Complain/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(Complain objComplain)
        {
            try
            {
                objComplain.Applicant = new Applicant();
                objComplain.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                objComplain.SetBy = GolbalSession.gblSession.UserId;
                objComplain.SetDate = DateTime.Now;
                _iComplainService.Insert(objComplain);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Complain Created Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Complain/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(int id)
        {
            var complain = _iComplainService.GetComplainById(id);

            var newComplain = _iComplainService.newComplain(GolbalSession.gblSession.GlobalCompanyId);
            complain.kvpComplainStatus = newComplain.kvpComplainStatus;
            complain.kvpAgent = newComplain.kvpAgent;
            complain.kvpComplainType = newComplain.kvpComplainType;
            complain.kvpCountry = newComplain.kvpCountry;
            complain.kvpDistrict = newComplain.kvpDistrict;
            complain.kvpDivision = newComplain.kvpDivision;
            complain.kvpUpazila = newComplain.kvpUpazila;

            return View(complain);
        }

        //
        // POST: /Complain/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(int id, Complain objComplain)
        {
            try
            {
                objComplain.Applicant = new Applicant();
                objComplain.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                objComplain.SetBy = GolbalSession.gblSession.UserId;
                objComplain.SetDate = DateTime.Now;
                _iComplainService.Update(objComplain);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Complain Updated Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Complain/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Complain/Delete/5
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
    }
}
