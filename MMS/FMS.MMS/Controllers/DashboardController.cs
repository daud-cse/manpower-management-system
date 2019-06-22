using MMS.FMS.Utility;
using MMS.Service.ViewModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.FMS.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardVMService _dashboardVMService;
        public DashboardController(IDashboardVMService dashboardVMService)
        {
            _dashboardVMService = dashboardVMService;
        }
        [CustomActionFilter]
        public ActionResult GetDashboard(int moduleId = 0)
        {

            return View();

        }
        [CustomActionFilter]
        public ActionResult Index(int moduleId = 0)
        {
            var dashboardFMSVM = _dashboardVMService.GetFMSDashboard(GolbalSession.gblSession.GlobalCompanyId,moduleId);
            return View(dashboardFMSVM);
        }

      
    }
}
