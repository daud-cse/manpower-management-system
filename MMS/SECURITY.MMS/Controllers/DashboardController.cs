using MMS.SECURITY.Utility;
using MMS.Service.ViewModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.SECURITY.Controllers
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

            if (moduleId == 2)
            {
                var dashboardVM = _dashboardVMService.GetMPMSDashboard(GolbalSession.gblSession.GlobalCompanyId, moduleId);
                return View("MMSDashboard", dashboardVM);
            }
            else if (moduleId == 3)
            {
                var dashboardFMSVM = _dashboardVMService.GetFMSDashboard(GolbalSession.gblSession.GlobalCompanyId,moduleId);

                return View("FMSDashboard", dashboardFMSVM);
            }
            return View();
        }
    }
}
