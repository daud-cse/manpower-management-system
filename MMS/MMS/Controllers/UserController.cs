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
namespace MMS.Controllers
{
    public class UserController : Controller
    {
        
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
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
                    ||p.Role.RoleName==Search.ToLower()
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
           var objUser= _userService.Find(id);
           return View(objUser);
        }

        //
        // GET: /User/Create
          [CustomActionFilter]
        public ActionResult Create()
        {
            var user = _userService.newUser(GolbalSession.gblSession.GlobalCompanyId);
            return View(user);
        }

        //
        // POST: /User/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(User user)
        {
            try
            {
                user.SetBy = GolbalSession.gblSession.UserId;
                user.SetDate = DateTime.Now;
                _userService.Insert(user);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "User Created Successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /User/Edit/5
          [CustomActionFilter]
        public ActionResult Edit(int id)
        {
            var user = _userService.Find(id);
            var usernew = _userService.newUser(GolbalSession.gblSession.GlobalCompanyId);
            user.kvpRole = usernew.kvpRole;
            return View(user);
          
        }

        //
        // POST: /User/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(User user)
        {
            try
            {
                user.SetBy = GolbalSession.gblSession.UserId;
                user.SetDate = DateTime.Now;
                _userService.Update(user);
                _unitOfWork.SaveChanges();
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
