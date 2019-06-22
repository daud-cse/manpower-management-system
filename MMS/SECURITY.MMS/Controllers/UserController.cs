using MMS.Entities.Models;
using MMS.Service.ModelServices;
using MMS.Utility;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using MMS.SECURITY.Utility;
using MMS.SECURITY;

namespace SECURITY.MMS.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly IUserModuleService _userModuleService;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUserService userService, IUnitOfWork unitOfWork, IUserModuleService userModuleService)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _userModuleService = userModuleService;
        }
        // GET: /User/
        [CustomActionFilter]
        public ActionResult Index(string Search, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 8;
            IList<User> userList = new List<User>();
            var lstuser = _userService.GetAllUser(GolbalSession.gblSession.GlobalCompanyId).ToList().OrderByDescending(x => x.ID);
            userList = (IList<User>)lstuser.ToList();
            if (string.IsNullOrWhiteSpace(Search))
            {
                userList = userList.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                userList = userList.Where(p => p.UserName.ToLower() == Search.ToLower()
                    || p.Role.RoleName == Search.ToLower()
                     || p.PhoneNo == Search.ToLower()
                    ).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(userList);
        }
        //
        // GET: /User/Details/5
        [CustomActionFilter]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            var user = _userService.newUser(GolbalSession.gblSession.GlobalCompanyId);
            user.kvpRole = new List<KeyValuePair<int, string>>();
            return View(user);
        }

        //
        // POST: /User/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(User user, int ModuleId)
        {
            try
            {
                var objuserExist = _userService.GetUserInfoByUserId(user.UserAccountsId, GolbalSession.gblSession.GlobalCompanyId);
                if (objuserExist != null)
                {
                    TempData.Add("danger", "User Id Already Exist.");
                    return RedirectToAction("Create");
                }


                user.SetBy = GolbalSession.gblSession.UserId;
                user.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                user.SetDate = DateTime.Now;
                _userService.Insert(user);
                _unitOfWork.SaveChanges();


                UserModule objUserModule = new UserModule();
                objUserModule.UserId = user.ID;
                objUserModule.ModuleId = ModuleId;
                objUserModule.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                objUserModule.SetBy = GolbalSession.gblSession.UserId;
                objUserModule.Setdate = DateTime.Now;
                objUserModule.IsActive = user.IsActive;
                _userModuleService.Insert(objUserModule);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "User Created Successfully.");
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
            var user = _userService.Find(id);
            var objusermodule = _userModuleService.GetUserModuleByUserId(user.ID, GolbalSession.gblSession.GlobalCompanyId);
            if (objusermodule == null)
            {
                objusermodule = new UserModule();
            }
            var usernew = _userService.newUser(objusermodule.ModuleId);
            user.kvpRole = usernew.kvpRole;
            user.kvpModule = usernew.kvpModule;
            user.ModuleId = objusermodule.ModuleId;
            return View(user);

        }

        //
        // POST: /User/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(User user, int ModuleId)
        {
            try
            {
                var userIdexist = _userService.Find(user.ID);
                
                if (userIdexist.UserAccountsId != user.UserAccountsId)
                {
                    var lstuser = _userService.GetAllUser(GolbalSession.gblSession.GlobalCompanyId).ToList();
                    var vIndex = lstuser.FindIndex(x => x.UserAccountsId == userIdexist.UserAccountsId);
                    lstuser.RemoveAt(vIndex);
                    if (lstuser.Count > 0)
                    {
                        lstuser = lstuser.FindAll(x => x.UserAccountsId == user.UserAccountsId);
                    }
                    if (lstuser.Count > 0)
                    {
                        TempData.Add("danger", "User Id Already Exist.");
                        return RedirectToAction("Edit", new { id = user.ID });
                    }
                }
                //userIdexist = new User();
                userIdexist.ID = user.ID;
                userIdexist.RoleId = user.RoleId;
                userIdexist.ModuleId = user.ModuleId;
                userIdexist.IsActive = user.IsActive;

                userIdexist.Password = user.Password;
                userIdexist.PhoneNo = user.PhoneNo;
                userIdexist.UserName = user.UserName;
                userIdexist.UserAccountsId = user.UserAccountsId;
                userIdexist.UserDescription = user.UserDescription;
                userIdexist.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                userIdexist.SetBy = GolbalSession.gblSession.UserId;
                userIdexist.SetDate = DateTime.Now;
                _userService.Update(userIdexist);
                _unitOfWork.SaveChanges();

                var objUserModule = _userModuleService.GetUserModuleByUserId(user.ID, GolbalSession.gblSession.GlobalCompanyId);
                if (objUserModule == null)
                {
                    UserModule objUserModule1 = new UserModule();
                    objUserModule1.UserId = user.ID;
                    objUserModule1.ModuleId = ModuleId;
                    objUserModule.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                    objUserModule1.SetBy = GolbalSession.gblSession.UserId;
                    objUserModule1.Setdate = DateTime.Now;
                    objUserModule1.IsActive = user.IsActive;
                    _userModuleService.Insert(objUserModule1);
                    _unitOfWork.SaveChanges();

                }
                else
                {

                    objUserModule.ModuleId = ModuleId;
                    objUserModule.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                    objUserModule.SetBy = GolbalSession.gblSession.UserId;
                    objUserModule.Setdate = DateTime.Now;
                    objUserModule.IsActive = user.IsActive;
                    _userModuleService.Update(objUserModule);
                    _unitOfWork.SaveChanges();

                }


                TempData.Add("success", "User Update Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /User/Delete/5
        [CustomActionFilter]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5
        [HttpPost]
        [CustomActionFilter]
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
