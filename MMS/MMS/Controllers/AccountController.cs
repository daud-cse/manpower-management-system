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
using MMS.Models;
using MMS.Service.ModelServices;
using MMS.Entities.Models;
using PSU.Utility;
using PSU.web.Utility;
using MMS.Utility;

namespace MMS.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _iUserService;
        private readonly IRoleRightssService _iRoleRightssService;
        public AccountController(IUserService iUserService
            , IRoleRightssService iRoleRightssService)
        {
            _iUserService = iUserService;
            _iRoleRightssService = iRoleRightssService;
        }
      
        // POST: /Account/Login
        [HttpPost]       
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var objUser = _iUserService.GetUserById(GolbalSession.gblSession.GlobalCompanyId,model.UserName, model.Password);

                if(objUser==null){
                    TempData.Add("danger", "Invalid username or password.");
                    model.Password = "";
                    model.UserName = "";
                    return RedirectToAction("Index", "Home");
                }
                else{
                     SetSession(objUser);                                
                    return RedirectToAction("Index", "Dashboard");              
                }
               
            }
            TempData.Add("danger", "Invalid Model.");
            return View(model);
          
        }


        public void SetSession(User objUser)
        {


            var roleRights = _iRoleRightssService.GetRightssRoleId(objUser.RoleId);
            MMSSession mmsSession = new MMSSession();
            if (objUser != null)
            {
                mmsSession.UserId = objUser.UserAccountsId;
                mmsSession.Name = objUser.UserName;
                mmsSession.RoleId = objUser.RoleId.ToString();
                mmsSession.IsActive = objUser.IsActive;
                mmsSession.UserRights = roleRights.Select(x => x.Rightss.RightId).ToList();
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