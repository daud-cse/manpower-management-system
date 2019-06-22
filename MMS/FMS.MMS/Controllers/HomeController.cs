using MMS.FMS.Utility;
using MMS.Entities.Models;
using MMS.Service.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace MMS.FMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _iUserService;
        private readonly IRoleRightssService _iRoleRightssService;
        private readonly IFMS_SettingsService _iFMS_SettingsService;
        public HomeController(IUserService iUserService
            ,IFMS_SettingsService iFMS_SettingsService
            , IRoleRightssService iRoleRightssService)
        {
            _iUserService = iUserService;
            _iFMS_SettingsService = iFMS_SettingsService;
            _iRoleRightssService = iRoleRightssService;

        }
        public ActionResult Index()
        {
        // var user = HttpContext.User.Identity.Name;
         //var user = "15";//15 mpl //HttpContext.User.Identity.Name;
         var user = "10";//10 qk //HttpContext.User.Identity.Name;

         
           if (string.IsNullOrEmpty(user))
           {
               return RedirectToAction("UnAuthenticated");
            }
            var id = int.Parse(user);
            var objUser = _iUserService.GetUserById(id);            
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


            var roleRights = _iRoleRightssService.GetRightssRoleId(objUser.RoleId);          
            MMSSession mmsSession = new MMSSession();
            if (objUser != null)
            {
                var settings = _iFMS_SettingsService.GetFMS_Settings(objUser.GlobalCompanyId);
                mmsSession.GlobalCompanyId = objUser.GlobalCompanyId;
                mmsSession.GlobalCompanyName = objUser.GlobalCompany.GlobalCompanyName;
                mmsSession.UserId = objUser.UserAccountsId;
                mmsSession.PId = objUser.ID;
                mmsSession.Name = objUser.UserName;
                mmsSession.RoleId = objUser.RoleId.ToString();
                mmsSession.IsActive = objUser.IsActive;
                mmsSession.UserRights =roleRights.Select(x => x.Rightss.RightId).ToList();
                if (settings.CurrentDate != null)
                {
                    mmsSession.IsDayOpened = true;
                }


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