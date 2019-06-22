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
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(ICustomerService customerService, IUnitOfWork unitOfWork)
        {
            _customerService = customerService;
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /Customer/
          [CustomActionFilter]
        public ActionResult Index(string Search, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 8;
            IList<Customer> customerList = new List<Customer>();
            var lstcustomer = _customerService.GetAllCustomer(GolbalSession.gblSession.GlobalCompanyId).ToList().OrderByDescending(x => x.ID);
            customerList = (IList<Customer>)lstcustomer.ToList();
            if (string.IsNullOrWhiteSpace(Search))
            {
                customerList = customerList.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                customerList = customerList.Where(p => p.Name.ToLower() == Search.ToLower()).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(customerList);
        }

        //
        // GET: /Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Customer/Create
          [CustomActionFilter]
        public ActionResult Create()
        {
           var customer= _customerService.newCustomer(GolbalSession.gblSession.GlobalCompanyId);
           return View(customer);
        }

        //
        // POST: /Customer/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(Customer objCustomer)
        {
            try
            {

                objCustomer.SetDate = DateTime.Now;
                objCustomer.SetBy = GolbalSession.gblSession.UserId;
                _customerService.Insert(objCustomer);
                _unitOfWork.SaveChanges();
                if (objCustomer.ID < 10)
                {
                    objCustomer.CustomerId = "CUS0" + objCustomer.ID;
                }
                else
                {
                    objCustomer.CustomerId = "CUS" + objCustomer.ID;
                }
                _customerService.Update(objCustomer);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Customer Created Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Customer/Edit/5
          [CustomActionFilter]
        public ActionResult Edit(int id)
        {
           var objcustomer= _customerService.Find(id);
           var newObjcustomer = _customerService.newCustomer(GolbalSession.gblSession.GlobalCompanyId);
           objcustomer.kvpCustomerType = newObjcustomer.kvpCustomerType;
           objcustomer.kvpCountry = newObjcustomer.kvpCountry;
           objcustomer.kvpDistrict = newObjcustomer.kvpDistrict;
           objcustomer.kvpDivision = newObjcustomer.kvpDivision;
           objcustomer.kvpNationality = newObjcustomer.kvpNationality;
           objcustomer.kvpUpazila = newObjcustomer.kvpUpazila;
           return View(objcustomer);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(Customer objCustomer)
        {
            try
            {
                // TODO: Add update logic here
                objCustomer.SetBy = GolbalSession.gblSession.UserId;
                objCustomer.SetDate = DateTime.Now;
                _customerService.Update(objCustomer);
                TempData.Add("success", "Customer Update Successfully.");
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
               
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Customer/Delete/5
          [CustomActionFilter]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Customer/Delete/5
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
