using MMS.SECURITY.Utility;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using MMS.Entities.Models;
using MMS.SECURITY;
namespace SECURITY.MMS.Controllers
{
    public class RoleController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleService _roleService;
        public RoleController(
            IUnitOfWork unitOfWork
            , IRoleService roleService)
        {

            _unitOfWork = unitOfWork;
            _roleService = roleService;
        }


        [CustomActionFilter]
        public ActionResult Index(string Search, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 8;
            IList<Role> roleList = new List<Role>();
            var lstrole = _roleService.GetAllRole(GolbalSession.gblSession.GlobalCompanyId).ToList().OrderByDescending(x => x.ModuleId);
            roleList = (IList<Role>)lstrole.ToList();
            if (string.IsNullOrWhiteSpace(Search))
            {
                roleList = roleList.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                roleList = roleList.Where(p => p.RoleName.ToLower() == Search.ToLower()
                    //|| p.Role.RoleName == Search.ToLower()
                    // || p.PhoneNo == Search.ToLower()
                    ).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(roleList);
        }
        public JsonResult GetRoleByModuleId(int moduleId)
        {
            var result = _roleService.GetRoleByModuleId(GolbalSession.gblSession.GlobalCompanyId, moduleId);
            return Json(result.kvpRole, JsonRequestBehavior.AllowGet);
        }
        // GET: /User/Details/5
        [CustomActionFilter]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /role/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            var role = _roleService.newRole();
            return View(role);
        }

        //
        // POST: /role/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(Role role)
        {
            try
            {
                role.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                role.SetDate = DateTime.Now;
                _roleService.Insert(role);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Role Created Successfully.");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(int id)
        {
            var role = _roleService.Find(id);
            var rolenew = _roleService.newRole();
            role.kvpModule = rolenew.kvpModule;
            return View(role);

        }

        //
        // POST: /User/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(Role role)
        {
            try
            {
                role.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                role.SetDate = DateTime.Now;
                _roleService.Update(role);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Role Update Successfully.");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5
        [CustomActionFilter]
        public ActionResult Delete(int id)
        {
            return View();
        }


    }
}