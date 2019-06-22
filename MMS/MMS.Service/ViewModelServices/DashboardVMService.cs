using MMS.Entities.Models;
using MMS.Entities.StoredProcedures;
using MMS.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ViewModelServices
{
   
    public interface IDashboardVMService
    {
        DashboardVM  GetMPMSDashboard(int GlobalCompanyId,int moduleId);
        FMSDashboardVM GetFMSDashboard(int GlobalCompanyId,int moduleId);

    }
    public class DashboardVMService : IDashboardVMService
    {

        private readonly IStoredProcedures _storedProcedures;

        public DashboardVMService(IStoredProcedures storedProcedures)
        {
            _storedProcedures = storedProcedures;
        }

        public DashboardVM GetMPMSDashboard(int GlobalCompanyId,int moduleId)
        {

            var objDashboardVM = _storedProcedures.GetMPMSDashBoard(GlobalCompanyId,moduleId);
            return objDashboardVM;
        }
        public FMSDashboardVM GetFMSDashboard(int GlobalCompanyId,int moduleId)
        {

            var objFMSDashboardVM = _storedProcedures.GetFMSDashBoard(GlobalCompanyId,moduleId);
            return objFMSDashboardVM;
        }
    }
}
