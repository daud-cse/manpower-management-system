using MMS.Entities.Models;
using MMS.SECURITY;
using MMS.SECURITY.Utility;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SECURITY.MMS.Controllers
{
    public class RoleRightsController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRightssService _roleRightService;
        private readonly IRoleService _roleService;

        public RoleRightsController(
            IUnitOfWork unitOfWork
            , IRoleRightssService roleRightService
            , IRoleService roleService
             )
        {

            _unitOfWork = unitOfWork;
            _roleRightService = roleRightService;
            _roleService = roleService;

        }


        //[CustomActionFilter]
        //public ActionResult Index(string Search, int? page)
        //{
        //    int currentPageIndex = page.HasValue ? page.Value : 1;
        //    const int defaultPageSize = 8;
        //    IList<RoleRightss> roleList = new List<RoleRightss>();
        //    var lstrole = _roleRightService.GetAllRoleRightss.ToList().OrderByDescending(x => x.ModuleId);
        //    roleList = (IList<Role>)lstrole.ToList();
        //    if (string.IsNullOrWhiteSpace(Search))
        //    {
        //        roleList = roleList.ToPagedList(currentPageIndex, defaultPageSize);
        //    }
        //    else
        //    {
        //        roleList = roleList.Where(p => p.RoleName.ToLower() == Search.ToLower()
        //            //|| p.Role.RoleName == Search.ToLower()
        //            // || p.PhoneNo == Search.ToLower()
        //            ).ToPagedList(currentPageIndex, defaultPageSize);
        //    }
        //    return View(roleList);
        //}

        [CustomActionFilter]
        public async Task<ActionResult> GetRoleRight(int moduleId, int RoleId)
        {
            var rolerights = new List<RoleRightss>();
            try
            {
                rolerights = _roleRightService.GetAllRoleRightss(moduleId, RoleId);
            }
            catch (Exception exp)
            {

            }

            return PartialView("_RoleRight", rolerights);
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
        public ActionResult Create(int moduleId = 0, int RoleId = 0)
        {
            var roleRight = _roleRightService.newRoleRightss();
            return View(roleRight);
        }

        //
        // POST: /role/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(List<RoleRightss> lstroleRight)
        {
            try
            {


                lstroleRight.ForEach(delegate(RoleRightss item)
                {
                    item.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                    item.SetBy = GolbalSession.gblSession.UserId;
                    if (item.ID > 0)
                    {
                        _roleRightService.Update(item);
                    }
                    else
                    {
                        _roleRightService.Insert(item);
                    }

                    _unitOfWork.SaveChanges();
                });

                var role = _roleService.GetRoleById(GolbalSession.gblSession.GlobalCompanyId, lstroleRight.FirstOrDefault().RoleId);
                TempData.Add("success", "Role Right Mapped Successfully.");
                // return RedirectToAction("Create", new { moduleId = role.ModuleId, RoleId = lstroleRight.FirstOrDefault().RoleId });
                return RedirectToAction("Create");
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
