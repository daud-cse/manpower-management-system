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
using MMS.SECURITY.Models;
using MMS.Entities.Models;
using MMS.Service.ModelServices;
using MMS.SECURITY.Utility;
using System.Web.Security;

namespace MMS.SECURITY.Controllers
{

    public class AccountController : Controller
    {
        private readonly IGlobalCompanyService _iGlobalCompanyService;
        private readonly IUserService _iUserService;
        private readonly IModuleService _iModuleService;

        

        public AccountController(IGlobalCompanyService iGlobalCompanyService,IUserService iUserService, IModuleService iModuleService)
        {
            _iGlobalCompanyService = iGlobalCompanyService;
            _iUserService = iUserService;
            _iModuleService = iModuleService;
        }



        [HttpPost]

        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                //var rtUrl = "~/SECURITY";
                //return Redirect(rtUrl);


                string url = HttpContext.Request.Url.Host;
              var objGolbalCompany=  _iGlobalCompanyService.GetGlobalCompanyByURL(url);
              if (objGolbalCompany==null)
              {
                  TempData.Add("danger", "URL is not in Company Inforamtion.");
                  model.Password = "";
                  model.UserName = "";
                  return RedirectToAction("Index", "Home");

                }

              var objUser = _iUserService.GetUserById(objGolbalCompany.GlobalCompanyId, model.UserName, model.Password);


                if (objUser == null)
                {
                    TempData.Add("danger", "Invalid username or password.");
                    model.Password = "";
                    model.UserName = "";
                    return RedirectToAction("Index", "Home");
                }
                else if (objUser.UserModules.Count == 0 || objUser.UserModules == null)
                {
                    TempData.Add("danger", "No Module Assign this user.");
                    model.Password = "";
                    model.UserName = "";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (objUser.UserModules.Count > 1)
                    {
                        TempData.Add("danger", "Multiple Module Assign this user You can Assigne only one module.");
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else if (!objUser.UserModules.FirstOrDefault().IsActive)
                    {
                        TempData.Add("danger", "Module is Assign but User Module is not active.");
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        var module = _iModuleService.GetModuleById(objUser.UserModules.FirstOrDefault().ModuleId);

                        if (module == null)
                        {
                            TempData.Add("danger", "No Module Found.");
                            model.Password = "";
                            model.UserName = "";
                            return RedirectToAction("Index", "Home");
                        }
                        else if (module.RedirectURL == "Security")
                        {
                            SetSession(objUser);
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            objUser.UserModules.FirstOrDefault().Module = module;
                            SetAuthToken(objUser.ID);
                            string rtUrl = RedirectUrl(objUser);
                            if (rtUrl != "")
                            {
                                return Redirect(rtUrl);
                            }                            
                        }
                       
                    }
                }

            }
            TempData.Add("danger", "Invalid Model.");
            return View(model);
        }

        void SetAuthToken(int userId)
        {
            string url = HttpContext.Request.Url.Host;

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1
                , userId.ToString()
                , DateTime.Now
                , DateTime.Now.AddMinutes(5)
                , false
                , ""
                , url);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName
                , FormsAuthentication.Encrypt(ticket));

            Response.Cookies.Add(cookie);
        }
        public void SetSession(User objUser)
        {


            var roleRights = objUser.Role.RoleRightsses.Select(x => x.RightId).ToList();

            SECURITYMMSSession mmsSession = new SECURITYMMSSession();
            if (objUser != null)
            {
                mmsSession.GlobalCompanyName = objUser.GlobalCompany.GlobalCompanyName;
                mmsSession.GlobalCompanyId = objUser.GlobalCompanyId;
                mmsSession.UserId = objUser.UserAccountsId;
                mmsSession.Name = objUser.UserName;
                mmsSession.RoleId = objUser.RoleId.ToString();
                mmsSession.IsActive = objUser.IsActive;
                mmsSession.UserRights = roleRights.ToList();

            }
            GolbalSession.gblSession = mmsSession;

        }
        string RedirectUrl(User userInfo)
        {
            return userInfo.UserModules.FirstOrDefault().Module.RedirectURL;

        }
        public ActionResult Logout()
        {
            GolbalSession.gblSession = null;
            TempData.Add("success", "Logged out Successfully.");
            return RedirectToAction("Index", "Home");
        }

    }
}