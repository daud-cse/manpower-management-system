using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace MMS.Utility
{
    public class AutorizeActionFilter : System.Web.Mvc.ActionFilterAttribute
    {
        private int rights = 0;
        AutorizeActionFilter()
        {

        }
        public AutorizeActionFilter(int rights)
        {
            this.rights = rights;
        }

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.HttpContext.Session != null)// && GolbalSession.gblSession == null)
            {
                filterContext.HttpContext.Response.Redirect("~/Home/Index");
            }
            else
            {
                //if (GolbalSession.gblSession.UserRights.Exists(x => x.Equals(rights)))
                if(0==0)
                {

                    //  filterContext.HttpContext.Respon
                    // var page = Request.UrlReferrer.OriginalString.;
                    //  filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
                else
                {
                    //var page = filterContext.HttpContext.Request.Url.Authority;
                    string page = string.Empty;
                    if (filterContext.HttpContext.Request.UrlReferrer!=null)
                    {
                         page = filterContext.HttpContext.Request.UrlReferrer.OriginalString;

                        // filterContext.HttpContext.Response.Redirect("~" + filterContext.HttpContext.Request.UrlReferrer.LocalPath);
                         var abc = filterContext.HttpContext.Request.UrlReferrer.LocalPath.Split('/');
                         var controller = abc[1];
                         string action = "Index";
                         if (abc.Length > 2)
                         {
                             action = abc[2];
                         }
                         filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                        {
                            { "client", filterContext.RouteData.Values[ "client" ] },
                             { "controller",  controller},
                             { "action", action },
                             {"area",""}
                        });
                       //  TempData.Add("danger", "Model is not valid");
                    }
                    else
                    {
                        page = filterContext.HttpContext.Request.Url.AbsoluteUri;
                        var abc = filterContext.HttpContext.Request.Url.AbsoluteUri.Split('/');
                        var controller = abc[3];
                        string action = "";
                        //if (abc.Length > 2)
                        //{
                        //    action = abc[2];
                        //}
                        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                        {
                            { "client", filterContext.RouteData.Values[ "client" ] },
                             { "controller",  controller},
                             { "action", action },
                             {"area",""}
                        });
                    }

                    

                    //filterContext.HttpContext.Response.Redirect(page);
                    //return;
                };



                // TempData.Add("success", "Operation executed successfully");
                //  filterContext.HttpContext.Response.Redirect("~/Index");
            }

        }
    }
}
