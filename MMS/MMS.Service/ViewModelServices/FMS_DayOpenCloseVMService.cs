using MMS.Entities.Models;
using MMS.Entities.StoredProcedures;
using MMS.Entities.ViewModels;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MMS.Service.ViewModelServices
{

    public interface IFMS_DayOpenCloseVMService
    {
        FMS_DayOpenCloseVM newFMS_DayOpenCloseVM(int GlobalCompanyId);
        FMS_DayOpenCloseVM GetFMSDayOpenCloseVM(int GlobalCompanyId,string userId);
        string DayOpenCloseProcess(FMS_DayOpenCloseVM objFMS_DayOpenCloseVM, int GlobalCompanyId,string userId, IUnitOfWork _unitOfWork);
        bool IsCheckDayOpen(int GlobalCompanyId,DateTime openDate);
        string MonthEndProcess();
        string YearEndProcess();
    }
    public class FMS_DayOpenCloseVMService : IFMS_DayOpenCloseVMService
    {

        private readonly IStoredProcedures _storedProcedures;
        private readonly IFMS_DayWiseCheckListDetailsService _fMS_DayWiseCheckListDetailsService;
        private readonly IFMS_DayOpenCloseService _iFMS_DayOpenCloseService;
        private readonly IFMS_SettingsService _iFMS_SettingsService;



        public FMS_DayOpenCloseVMService(IStoredProcedures storedProcedures
            , IFMS_DayWiseCheckListDetailsService fMS_DayWiseCheckListDetailsService
            , IFMS_DayOpenCloseService iFMS_DayOpenCloseService
            , IFMS_SettingsService iFMS_SettingsService)
        {
            _storedProcedures = storedProcedures;
            _fMS_DayWiseCheckListDetailsService = fMS_DayWiseCheckListDetailsService;
            _iFMS_DayOpenCloseService = iFMS_DayOpenCloseService;
            _iFMS_SettingsService = iFMS_SettingsService;
        }
        public FMS_DayOpenCloseVM GetFMSDayOpenCloseVM(int GlobalCompanyId,string userId)
        {

            FMS_DayOpenCloseVM objFMS_DayOpenCloseVM = new FMS_DayOpenCloseVM();
            objFMS_DayOpenCloseVM.Settings = new FMS_Settings();
            objFMS_DayOpenCloseVM.Settings = _iFMS_SettingsService.GetFMS_Settings(GlobalCompanyId);
            objFMS_DayOpenCloseVM.lstDayWiseCheckListDetails = new List<FMS_DayWiseCheckListDetails>();
            if (!String.IsNullOrEmpty(objFMS_DayOpenCloseVM.Settings.CurrentDate.ToString()))
            {
                var msg = _storedProcedures.FMS_VerifyDayCloseCheckList(GlobalCompanyId,userId);
                var transactionDate = objFMS_DayOpenCloseVM.Settings.CurrentDate == null ? DateTime.Now : Convert.ToDateTime(objFMS_DayOpenCloseVM.Settings.CurrentDate);
                objFMS_DayOpenCloseVM.DayOpenClose = _iFMS_DayOpenCloseService.GetFMS_DayOpenClose(GlobalCompanyId,transactionDate);
                objFMS_DayOpenCloseVM.lstDayWiseCheckListDetails = _fMS_DayWiseCheckListDetailsService.GetFMS_DayWiseCheckListDetails(GlobalCompanyId,Convert.ToDateTime(objFMS_DayOpenCloseVM.Settings.CurrentDate)).ToList();
            }

            return objFMS_DayOpenCloseVM;
        }
        public FMS_DayOpenCloseVM newFMS_DayOpenCloseVM(int GlobalCompanyId)
        {
            FMS_DayOpenCloseVM objFMS_DayOpenCloseVMS = new FMS_DayOpenCloseVM();

            objFMS_DayOpenCloseVMS.DayOpenClose = new FMS_DayOpenClose();
            objFMS_DayOpenCloseVMS.lstDayWiseCheckListDetails = new List<FMS_DayWiseCheckListDetails>();
            return objFMS_DayOpenCloseVMS;
        }

        public bool IsCheckDayOpen(int GlobalCompanyId,DateTime openDate)
        {
            var dayOpenClose = _iFMS_DayOpenCloseService.GetFMS_DayOpenClose(GlobalCompanyId,openDate, true);
            if (dayOpenClose == null)
            {

                return false;
            }

            else
            {
                return true;
            }
        }
        public string DayOpenCloseProcess(FMS_DayOpenCloseVM objFMS_DayOpenCloseVM,int GlobalCompanyId,string userId, IUnitOfWork _unitOfWork)
        {
            objFMS_DayOpenCloseVM.Settings = new FMS_Settings();
            objFMS_DayOpenCloseVM.lstDayWiseCheckListDetails = new List<FMS_DayWiseCheckListDetails>();
            var setting = _iFMS_SettingsService.GetFMS_Settings(GlobalCompanyId);
            objFMS_DayOpenCloseVM.Settings = setting;
            string msg = "";
            if (objFMS_DayOpenCloseVM.DayOpenClose.OpenDate > DateTime.Now)
            {
                msg = "Open Date can not greater than Current Date.";
                return msg;
            }

            else if (IsCheckDayOpen(GlobalCompanyId,objFMS_DayOpenCloseVM.DayOpenClose.OpenDate))
            {
                msg = "Day clsoe Already done.";
                return msg;
            }
            else if (setting.LastClosedDate > objFMS_DayOpenCloseVM.DayOpenClose.OpenDate)
            {

                msg = "Open Date Can be  Greater Than Last Close Date.";
                return msg;
            }


            if (setting.CurrentDate == null && !objFMS_DayOpenCloseVM.DayOpenClose.IsDayClosed)
            {
                msg = _storedProcedures.FMS_DayOpenProcess(objFMS_DayOpenCloseVM.DayOpenClose.OpenDate,GlobalCompanyId,userId);
               

            }
            else if (setting.CurrentDate != null)
            {
                msg = _storedProcedures.FMS_DayCloseProcess(Convert.ToDateTime(setting.CurrentDate),GlobalCompanyId,userId);
            }

            return msg;

        }
        public string MonthEndProcess()
        {
            return "sfd";

        }
        public string YearEndProcess()
        {
            return "sfd";

        }
    }
}
