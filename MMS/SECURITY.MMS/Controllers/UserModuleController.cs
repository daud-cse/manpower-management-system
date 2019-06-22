using MMS.Entities.Models;
using MMS.SECURITY.Utility;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using MMS.SECURITY;
namespace SECURITY.MMS.Controllers
{
    public class UserModuleController : Controller
    {
         private readonly IUserModuleService _iUserModuleService;
        private readonly IUnitOfWork _unitOfWork;

        public UserModuleController(IUserModuleService iUserModuleService, IUnitOfWork unitOfWork)
        {
            _iUserModuleService = iUserModuleService;
            _unitOfWork = unitOfWork;
        }
        // GET: /User/
        [CustomActionFilter]
        public ActionResult Index(string Search, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 15;
            IList<UserModule> usermoduleList = new List<UserModule>();
            var lstuserModule = _iUserModuleService.GetAllUserModule(GolbalSession.gblSession.GlobalCompanyId).ToList().OrderByDescending(x => x.ID);
            usermoduleList = (IList<UserModule>)lstuserModule.ToList();
            if (string.IsNullOrWhiteSpace(Search))
            {
                usermoduleList = usermoduleList.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                usermoduleList = usermoduleList.Where(p => p.Module.Name.ToLower().Contains(Search.ToLower())
                    || p.User.UserName.ToLower().Contains(Search.ToLower())
                     || p.User.UserAccountsId.ToLower().Contains(Search.ToLower())
                    ).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(usermoduleList);
        }
        //
        //
        // GET: /UserModule/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /UserModule/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UserModule/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /UserModule/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /UserModule/Edit/5
        [HttpPost]
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
        // GET: /UserModule/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /UserModule/Delete/5
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
