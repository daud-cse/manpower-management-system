using MMS.Entities.Models;
using MMS.Service.ModelServices;
using MMS.Utility;
using PSU.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _iUserService;
        private readonly IRoleRightssService _iRoleRightssService;
        public HomeController(IUserService iUserService
            , IRoleRightssService iRoleRightssService)
        {

            _iUserService = iUserService;
            _iRoleRightssService = iRoleRightssService;


        }
        public ActionResult Index()
        {
         //   var user = HttpContext.User.Identity.Name;
             //var user = "15"; //Megawatt  HttpContext.User.Identity.Name;
            var user = "5"; //qk HttpContext.User.Identity.Name;
            if (string.IsNullOrEmpty(user))
            {
                return Redirect(Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/')) + "/");
            }

            var userId = int.Parse(user);
            var objUser = _iUserService.GetUserById(userId);
            if (!objUser.Role.RoleRightsses.Any())
            {
                return RedirectToAction("UnAuthenticated");
            }
            SetSession(objUser);
            return RedirectToAction("Index", "Dashboard");
        }
        public ActionResult UnAuthenticated()
        {
            return View();
        }
        public void SetSession(User objUser)
        {
            // var roleRights = objUser.Role.RoleRightsses.Select(x => x.RightId).ToList();
            var roleRights = _iRoleRightssService.GetRightssRoleId(objUser.RoleId);
            MMSSession mmsSession = new MMSSession();
            if (objUser != null)
            {
                mmsSession.GlobalCompanyId = objUser.GlobalCompanyId;
                mmsSession.GlobalCompanyName = objUser.GlobalCompany.GlobalCompanyName;
                mmsSession.UserId = objUser.UserAccountsId;
                mmsSession.PId = objUser.ID;
                mmsSession.Name = objUser.UserName;
                mmsSession.RoleId = objUser.RoleId.ToString();
                mmsSession.IsActive = objUser.IsActive;
                mmsSession.UserRights = roleRights.Select(x => x.Rightss.RightId).ToList();
            }
            GolbalSession.gblSession = mmsSession;

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}