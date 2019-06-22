using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using MMS.Entities.Models;
using MMS.Utility;
namespace MMS.Controllers
{
    public class CountryController : Controller
    {
        private ICountryService _iCountryService;

        private readonly IUnitOfWork _unitOfWork;
        public CountryController(ICountryService iCountryService, IUnitOfWork unitOfWork)
        {
            _iCountryService = iCountryService;
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /Country/
         [CustomActionFilter]
        public ActionResult Index()
        {
            var countires = _iCountryService.GetAllCountry(GolbalSession.gblSession.GlobalCompanyId);
            return View(countires);
        }

        //
        // GET: /Country/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Country/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Country/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(Country objCountry)
        {
            try
            {

                var lstCountry = _iCountryService.GetAllCountry();

                if (lstCountry.Any())
                {
                  objCountry.ID =lstCountry.Max(x => x.ID)+1;
                }
                else
                {
                    objCountry.ID = 1;
                }
                objCountry.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                _iCountryService.Insert(objCountry);
                _unitOfWork.SaveChanges();
                
                TempData.Add("success", "Country Created Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Country/Edit/5
        public ActionResult Edit(int id)
        {
            var country=_iCountryService.Find(id);
            return View(country);
        }

        //
        // POST: /Country/Edit/5
        [HttpPost]
        public ActionResult Edit(Country objCountry)
        {
            try
            {
                objCountry.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                _iCountryService.Update(objCountry);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Country Update Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Country/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Country/Delete/5
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
