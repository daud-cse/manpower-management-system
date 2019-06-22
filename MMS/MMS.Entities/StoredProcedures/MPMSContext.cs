using MMS.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MMS.Entities.UserDefineModels;
using MMS.Entities.StoredProcedures;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace MMS.Entities.Models
{
    public partial class MPMSContext : IStoredProcedures
    {

        public string GetAutoGenerateId(int GlobalCompanyId, int AutoIdType)
        {
            using (var t = new MPMSContext())
            {
                object[] parameters = { GlobalCompanyId, AutoIdType };
                var list = t.Database.SqlQuery<string>("EXEC SpIdGenerate {0}, {1}", parameters).ToList();
                return list.FirstOrDefault();
            }
        }
        public string GetAutoTransactionId(int GlobalCompanyId, int VoucherTypeId)
        {
            using (var t = new MPMSContext())
            {
                object[] parameters = { GlobalCompanyId, VoucherTypeId };
                var list = t.Database.SqlQuery<string>("EXEC SpTransactionIdGenerate {0}, {1}", parameters).ToList();
                return list.FirstOrDefault();
            }
        }
        public DashboardVM GetMPMSDashBoard(int GlobalCompanyId,int moduleId)
        {
            DashboardVM dashboardVM = new DashboardVM();
            dashboardVM.lstApplicantLocationTotal = new List<ApplicantLocationTotal>();
            dashboardVM.lstApplicantTypeTotal = new List<ApplicantTypeTotal>();

            using (var multi = Database.Connection.QueryMultiple("[SpMPMSDashBoard]" + "'" + GlobalCompanyId + "'"))
            {
                dashboardVM.lstApplicantLocationTotal = multi.Read<ApplicantLocationTotal>().AsList();
                dashboardVM.lstApplicantTypeTotal = multi.Read<ApplicantTypeTotal>().AsList();
            }
            return dashboardVM;
        }

        public FMSDashboardVM GetFMSDashBoard(int GlobalCompanyId,int moduleId)
        {
            FMSDashboardVM objFMSDashboardVM = new FMSDashboardVM();
            objFMSDashboardVM.lstFMSDayOpenClose = new List<FMS_DayOpenClose>();
            objFMSDashboardVM.lstFMSCurrentLedgerSummary = new List<FMS_CurrentLedgerSummary>();
          

            using (var multi = Database.Connection.QueryMultiple("[SpFMSDashBoard]"+ "'"+GlobalCompanyId+"'"))
            {
                objFMSDashboardVM.lstFMSDayOpenClose = multi.Read<FMS_DayOpenClose>().AsList();
                objFMSDashboardVM.lstFMSCurrentLedgerSummary = multi.Read<FMS_CurrentLedgerSummary>().AsList();

            }
            if (objFMSDashboardVM.lstFMSDayOpenClose.Count == 0)
            {
                FMS_DayOpenClose objFMS_DayOpenClose = new FMS_DayOpenClose();
                objFMS_DayOpenClose.ClosingBankBalance = 0;
                objFMS_DayOpenClose.ClosingCashBalance = 0;
                objFMS_DayOpenClose.OpeningBankBalance = 0;
                objFMS_DayOpenClose.OpeningCashBalance = 0;
                objFMSDashboardVM.lstFMSDayOpenClose.Add(objFMS_DayOpenClose);
            }
            if (objFMSDashboardVM.lstFMSCurrentLedgerSummary.Count == 0)
            {
                FMS_CurrentLedgerSummary objFMSCurrentLedgerSummary = new FMS_CurrentLedgerSummary();
                objFMSCurrentLedgerSummary.ClosingBalance = 0;
                objFMSCurrentLedgerSummary.OpeningBalance = 0;
                objFMSCurrentLedgerSummary.DrAmount = 0;
                objFMSCurrentLedgerSummary.CrAmount = 0;
                objFMSDashboardVM.lstFMSCurrentLedgerSummary.Add(objFMSCurrentLedgerSummary);
            }

            return objFMSDashboardVM;
        }
        public bool DeleteTableDataIByInlineSQL(string sql)
        {
            bool IsSucess = false;
            try
            {
                Database.ExecuteSqlCommand(sql);
                IsSucess = true;
            }
            catch
            {
                IsSucess = false;
            }
            return IsSucess;
        }
        public string FMS_DayOpenProcess(DateTime OpenDate, int GlobalCompanyId, string userId)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "FMS_DayOpen";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter par = new SqlParameter("@OpenDate", SqlDbType.Date);
            SqlParameter par1 = new SqlParameter("@GlobalCompanyId", SqlDbType.Int);
            SqlParameter par2 = new SqlParameter("@userId", SqlDbType.VarChar);

            cmd.Parameters.Add(par).Value = OpenDate;
            cmd.Parameters.Add(par1).Value = GlobalCompanyId;
            cmd.Parameters.Add(par2).Value = userId;

            SqlParameter parOut = new SqlParameter("@Msg", SqlDbType.VarChar, 2000);
            parOut.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parOut);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            string Msg = "";
            Msg = parOut.Value.ToString();
            con.Close();
            con.Dispose();
            cmd.Dispose();
            return Msg;
        }
        public string FMS_DayCloseProcess(DateTime ProcessDate, int GlobalCompanyId, string userId)
        {

         
            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "FMS_DayClose";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par1 = new SqlParameter("@GlobalCompanyId", SqlDbType.Int);
            SqlParameter par2 = new SqlParameter("@userId", SqlDbType.VarChar);            
            cmd.Parameters.Add(par1).Value = GlobalCompanyId;
            cmd.Parameters.Add(par2).Value = userId;

            SqlParameter parOut = new SqlParameter("@Msg", SqlDbType.VarChar, 2000);
            parOut.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parOut);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            string Msg = "";
            Msg = parOut.Value.ToString();
            con.Close();
            con.Dispose();
            cmd.Dispose();
            //Database.ExecuteSqlCommand("FMS_DayClose @Msg out", Msg);
            //return (string)Msg.Value;
            return Msg;
        }
        public string FMS_VerifyDayCloseCheckList(int GlobalCompanyId,string userId)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "FMS_VerifyDayCloseCheckList";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par0 = new SqlParameter("@GlobalCompanyId", SqlDbType.Int);
            cmd.Parameters.Add(par0).Value = true;

            SqlParameter par = new SqlParameter("@CheckOnly", SqlDbType.Bit);
            cmd.Parameters.Add(par).Value = true;
            SqlParameter par1 = new SqlParameter("@Msg", SqlDbType.VarChar, 200);
            par1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par1);
            SqlParameter par2 = new SqlParameter("@CheckListID", SqlDbType.Int);
            par2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par2);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            string Msg = "";
            Msg = par1.Value.ToString();
            con.Close();
            con.Dispose();
            cmd.Dispose();
            return Msg;
        }
        public IEnumerable<FMS_SubsidyAccount> GetSubsidyByCriteria(int GLAccountId, int SubsidyTypeId, int MappedId, string SearchItem)
        {
            var GLAccountId_ = new SqlParameter("@GLAccountId", SqlDbType.Int);
            GLAccountId_.Value = GLAccountId;
            var SubsidyTypeId_ = new SqlParameter("@SubsidyTypeId", SqlDbType.Int);
            SubsidyTypeId_.Value = SubsidyTypeId;
            var MappedId_ = new SqlParameter("@MappedId", SqlDbType.Int);
            MappedId_.Value = MappedId;
            var SearchItem_ = new SqlParameter("@SearchItem", SqlDbType.VarChar);
            SearchItem_.Value = SearchItem;
            return Database.SqlQuery<FMS_SubsidyAccount>("GetSubsidyByCriteria @GLAccountId,@SubsidyTypeId,@MappedId,@SearchItem", GLAccountId_, SubsidyTypeId_, MappedId_, SearchItem_);
            //return fMS_SubsidyAccount;


        }
        public IEnumerable<OpponentTransactionVM> GetOpponentTransactionByVoucherId(int GlobalCompanyId,string VoucherId, int GLAccountId, string SearchItem)
        {

            var GlobalCompanyId_ = new SqlParameter("@GlobalCompanyId", SqlDbType.Int);
            GlobalCompanyId_.Value = GlobalCompanyId;


            var VoucherId_ = new SqlParameter("@VoucherId", SqlDbType.VarChar);
            if (VoucherId == null)
            {
                VoucherId_.Value =DBNull.Value;
            }
            else
            {
                VoucherId_.Value = VoucherId;
            }
            
            var GlAccountId_ = new SqlParameter("@GlAccountId", SqlDbType.Int);

            GlAccountId_.Value = GLAccountId;
            var SearchItem_ = new SqlParameter("@SearchItem", SqlDbType.VarChar);
            if (SearchItem == null)
            {
                SearchItem_.Value = DBNull.Value;
            }
            else
            {
                SearchItem_.Value = SearchItem;
            }

            return Database.SqlQuery<OpponentTransactionVM>("FMS_GetOpponentTransactionByVoucherId @GlobalCompanyId,@VoucherId,@GlAccountId,@SearchItem",GlobalCompanyId_, VoucherId_, GlAccountId_, SearchItem_);
          


        }
        public IEnumerable<FMS_SubSidyVM> FMS_SubsidyByCriteria(int GlobalCompanyId, int? SubsidyTypeId, int? SubsidyAccountId, string SearchItem)
        {


            var GlobalCompanyId_ = new SqlParameter("@GlobalCompanyId", SqlDbType.Int);
            GlobalCompanyId_.Value = GlobalCompanyId;

            var SubsidyTypeId_ = new SqlParameter("@SubsidyTypeId", SqlDbType.Int);
            SubsidyTypeId_.Value = SubsidyTypeId;
            var SubsidyAccountId_ = new SqlParameter("@SubsidyAccountId", SqlDbType.Int);

            SubsidyAccountId_.Value = SubsidyAccountId;
            var SearchItem_ = new SqlParameter("@SearchItem", SqlDbType.VarChar);
            SearchItem_.Value = SearchItem;
            return Database.SqlQuery<FMS_SubSidyVM>("FMS_SubsidyByCriteria @GlobalCompanyId,@SubsidyTypeId,@SubsidyAccountId,@SearchItem", GlobalCompanyId_, SubsidyTypeId_, SubsidyAccountId_, SearchItem_);


        }

        public string FMS_TransactionApproved(int GlobalCompanyId,int TransactionId, bool IsApproved,string UserId)
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "FMS_TransactionApprove";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter par = new SqlParameter("@GlobalCompanyId", SqlDbType.Int);
            cmd.Parameters.Add(par).Value = GlobalCompanyId;

            SqlParameter par1 = new SqlParameter("@TransactionId", SqlDbType.Int);
            cmd.Parameters.Add(par1).Value = TransactionId;

            SqlParameter par2 = new SqlParameter("@IsApproved", SqlDbType.Bit);
            cmd.Parameters.Add(par2).Value = IsApproved;

            SqlParameter par3 = new SqlParameter("@UserId", SqlDbType.VarChar);
            cmd.Parameters.Add(par3).Value = UserId;

            SqlParameter parOut = new SqlParameter("@Msg", SqlDbType.VarChar,2000);
            parOut.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parOut);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            string Msg = "";
            Msg = parOut.Value.ToString();
            con.Close();
            con.Dispose();
            cmd.Dispose();
            return Msg;

        }
        public List<ApplicantFind> ApplicantFind(string ApplicantFindText)
        {
           List< ApplicantFind> applicantFindList = new List< ApplicantFind>();


           //var ApplicantFindText_ = new SqlParameter("@ApplicantFindText", SqlDbType.VarChar);
           //ApplicantFindText_.Value = ApplicantFindText;
           //return Database.SqlQuery<ApplicantFind>("ApplicantFind @ApplicantFindText", ApplicantFindText_).ToList();

            using (var multi = Database.Connection.QueryMultiple("[ApplicantFind]" + "'"+ApplicantFindText+"'"))
            {
                applicantFindList = multi.Read<ApplicantFind>().AsList();
              
            }
            return applicantFindList;
        }
        public DashboardVM GetMaleFemaleRation(int CountryId, int DivisionId, int DistrictId, int UpaThanaId, int UnionId, int GenderType, int InstituteId, int BrancheId)
        {
            //var CountryId_ = new SqlParameter("@CountryId", SqlDbType.Int);
            //CountryId_.Value = CountryId;
            //var DivisionId_ = new SqlParameter("@DivisionId", SqlDbType.Int);
            //DivisionId_.Value = DivisionId;
            //var DistrictId_ = new SqlParameter("@DistrictId", SqlDbType.Int);
            //DistrictId_.Value = DistrictId;
            //var UpaThanaId_ = new SqlParameter("@UpaThanaId", SqlDbType.Int);
            //UpaThanaId_.Value = UpaThanaId;
            //var UnionId_ = new SqlParameter("@UnionId", SqlDbType.Int);
            //UnionId_.Value = UnionId;
            //var GenderType_ = new SqlParameter("@GenderType", SqlDbType.Int);
            //GenderType_.Value = GenderType;
            //var InstituteId_ = new SqlParameter("@InstituteId", SqlDbType.Int);
            //InstituteId_.Value = InstituteId;
            //var BrancheId_ = new SqlParameter("@BrancheId", SqlDbType.Int);
            //BrancheId_.Value = BrancheId;
            //IEnumerable<MaleFemaleRatio> maleFemaleRation;
            //List<cRatio> newcratio = new List<cRatio>();
            //Dashboard dashboard = new Dashboard();
            //dashboard.MaleFemaleRation = newcratio;
            using (var multi = Database.Connection.QueryMultiple("[SprMaleFemaleRatio]" + CountryId + "," + DivisionId + "," + DistrictId + "," + UpaThanaId + "," + UnionId + "," + GenderType + "," + InstituteId + "," + BrancheId))
            {
                var maleFemaleRation = multi.Read<string>().AsList();
                var DashboardHeader = multi.Read<string>();
            }

            //maleFemaleRation.AsList().ForEach(delegate(MaleFemaleRatio item)
            //{

            //    cRatio cratio = new cRatio();
            //    cratio.Name = item.InstituteName;
            //    cratio.Count = item.Total;
            //    cratio.Propertices = new List<KeyValuePair<string, decimal>>();
            //    cratio.Propertices.Add(new KeyValuePair<string, decimal>("Male", item.Male));
            //    cratio.Propertices.Add(new KeyValuePair<string, decimal>("FeMale", item.Female));
            //    dashboard.MaleFemaleRation.AsList().Add(cratio);

            //});
            var dashboard = new DashboardVM();
            return dashboard;
        }
    }
}
