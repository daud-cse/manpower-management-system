using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.FMS.Utility
{
    public class MMS : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.count = 20; //Add whatever
            base.OnActionExecuting(filterContext);
        }

    }
}