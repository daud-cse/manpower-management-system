using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MMS.Service.ModelServices;
using MMS.Entities.Models;
using MMS.FMS.Utility;
using MMS.FMS.Models;

namespace MMS.FMS.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {

        private readonly IUserService _iUserService;
        private readonly IRoleRightssService _iRoleRightssService;
        private readonly IFMS_SettingsService _iFMS_SettingsService;
        public AccountController(IUserService iUserService
            ,IFMS_SettingsService iFMS_SettingsService
            , IRoleRightssService iRoleRightssService)
        {
            _iUserService = iUserService;
            _iFMS_SettingsService = iFMS_SettingsService;
            _iRoleRightssService = iRoleRightssService;

        }


        //
        // GET: /Account/Login
        //[AllowAnonymous]
        //[CustomActionFilter]
        //public ActionResult Login(string returnUrl)
        //{
        //    RedirectToAction("Index", "Dashboard");
        //    ViewBag.ReturnUrl = returnUrl;
        //    return View();
        //}

        //
        // POST: /Account/Login
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //var rtUrl = "~/SECURITY";
                //return Redirect(rtUrl);

                var objUser = _iUserService.GetUserById(GolbalSession.gblSession.GlobalCompanyId,model.UserName, model.Password);

                if (objUser == null)
                {
                    TempData.Add("danger", "Invalid username or password.");
                    model.Password = "";
                    model.UserName = "";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    SetSession(objUser);
                    //  TempData.Add("success", "Successfully Login.");
                    return RedirectToAction("Index", "Dashboard");
                }

            }
            TempData.Add("danger", "Invalid Model.");
            return View(model);

            // If we got this far, something failed, redisplay form
            // return View(model);
        }


        public void SetSession(User objUser)
        {


          
            var roleRights = _iRoleRightssService.GetRightssRoleId(objUser.RoleId);
            
            MMSSession mmsSession = new MMSSession();
            if (objUser != null)
            {
                var settings = _iFMS_SettingsService.GetFMS_Settings(GolbalSession.gblSession.GlobalCompanyId);
                mmsSession.UserId = objUser.UserAccountsId;
                mmsSession.Name = objUser.UserName;
                mmsSession.RoleId = objUser.RoleId.ToString();
                mmsSession.IsActive = objUser.IsActive;
                mmsSession.UserRights = roleRights.Select(x => x.Rightss.RightId).ToList();
                if (settings.CurrentDate != null)
                {
                    mmsSession.IsDayOpened = true;
                }
                

            }
            GolbalSession.gblSession = mmsSession;

        }

        public ActionResult Logout()
        {
            GolbalSession.gblSession = null;
            TempData.Add("success", "Logged out Successfully.");
            return RedirectToAction("Index", "Home");
        }
      
    }
}