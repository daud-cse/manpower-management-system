using MMS.SECURITY;
using MMS.SECURITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.SECURITY.Utility
{
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.HttpContext.Session != null  && GolbalSession.gblSession== null)
            {
                filterContext.HttpContext.Response.Redirect("~/Home/Index");
            }
        }
    }
}