using MMS.Entities.Models;
using MMS.Entities.UserDefineModels;
using MMS.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.StoredProcedures
{
    public interface IStoredProcedures
    {
        string GetAutoGenerateId(int GlobalCompanyId, int AutoIdType);
        string GetAutoTransactionId(int GlobalCompanyId, int VoucherTypeId);
        DashboardVM GetMPMSDashBoard(int GlobalCompanyId,int moduleId);
        FMSDashboardVM GetFMSDashBoard(int GlobalCompanyId,int moduleId);
        bool DeleteTableDataIByInlineSQL(string sql);
        IEnumerable<FMS_SubsidyAccount> GetSubsidyByCriteria(int GLAccountId, int SubsidyTypeId, int MappedId,string SearchItem);
        IEnumerable<FMS_SubSidyVM> FMS_SubsidyByCriteria(int GlobalCompanyId,int? SubsidyTypeId, int? SubsidyAccountId, string SearchItem);
        string FMS_TransactionApproved(int GlobalCompanyId, int TransactionId, bool IsApproved, string UserId);

        string FMS_DayCloseProcess(DateTime ProcessDate, int GlobalCompanyId, string userId);
        string FMS_DayOpenProcess(DateTime OpenDate,int GlobalCompanyId, string userId);
        string FMS_VerifyDayCloseCheckList(int GlobalCompanyId,string userId);
        IEnumerable<OpponentTransactionVM> GetOpponentTransactionByVoucherId(int GlobalCompanyId,string VoucherId, int GLAccountId, string SearchItem);
        List<ApplicantFind> ApplicantFind(string ApplicantFindText);

    }
}
