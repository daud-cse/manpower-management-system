using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using MMS.Service.ModelServices;
using MMS.Entities.ReportsModels;
using MMS.Entities.Models;
using MMS.FMS.Utility;
namespace MMS.FMS.Controllers
{
    public class FMSReportsController : Controller
    {

        private readonly IFMS_SubsidyTypeService _FMS_SubsidyTypeService;

        private readonly IAgentService _IAgentService;
        public FMSReportsController(IFMS_SubsidyTypeService FMS_SubsidyTypeService, IAgentService IAgentService)
        {
            _FMS_SubsidyTypeService = FMS_SubsidyTypeService;
            _IAgentService = IAgentService;
        }

        [Route("Voucher")]
        [CustomActionFilter]
        public void GetVoucherByTransactionId(int TransactionId=0)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);

            try
            {
                bool isValid = true;

                string strReportName = "FMSTransactionDetails.rpt";

                if (string.IsNullOrEmpty(strReportName))
                {
                    isValid = false;
                }

                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();

                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Views//FMSReports//" + strReportName;
                    rd.Load(strRptPath);
                    rd.SetParameterValue("@GlobalCompanyId", GolbalSession.gblSession.GlobalCompanyId);
                    rd.SetParameterValue("@TransactionId", TransactionId);                             
                    //rd.SetDatabaseLogon(SConn.UserID, SConn.Password, SConn.DataSource, SConn.InitialCatalog);
                    rd.DataSourceConnections[0].IntegratedSecurity = false;
                    //strServer, strDatabase, strUserID, strPwd)
                    rd.DataSourceConnections[0].SetConnection(SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "Voucher");
                    rd.DataSourceConnections.Clear();
                    rd.Refresh();
                    rd.Dispose();
                    rd.Clone();
                    rd.Close();
                }
                else
                {
                    Response.Write("Nothing Found; No Report name found");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
         [CustomActionFilter]
        public ActionResult LedgerStatement()
        {
            LedgerStatement objLedgerStatement = new LedgerStatement();
            objLedgerStatement.Todate = DateTime.Now;
            objLedgerStatement.Fromdate = DateTime.Now;
            var lstSubsidyType = new List<KeyValuePair<int, string>>();
            _FMS_SubsidyTypeService.GetActiveFMS_SubsidyType(true).ToList().ForEach(delegate(FMS_SubsidyType item)
            {
                lstSubsidyType.Add(new KeyValuePair<int, string>(item.SubsidyTypeId, item.SubsidyTypeName));
            });
            objLedgerStatement.kvpSubsidyType = lstSubsidyType;
            objLedgerStatement.IsApproved = true;
            return View(objLedgerStatement);

        }
        [HttpPost]
        [CustomActionFilter]
        public void LedgerStatement(LedgerStatement objLedgerStatement)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
            try
            {
                bool isValid = true;

                string strReportName = "LedgerStatement.rpt";

                if (string.IsNullOrEmpty(strReportName))
                {
                    isValid = false;
                }
                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Views//FMSReports//" + strReportName;
                    rd.Load(strRptPath);
                    rd.SetParameterValue("@GlobalCompanyId", GolbalSession.gblSession.GlobalCompanyId);
                    rd.SetParameterValue("@Fromdate",objLedgerStatement.Fromdate);
                    rd.SetParameterValue("@Todate", objLedgerStatement.Todate);
                    rd.SetParameterValue("@SubsidyTypeId", objLedgerStatement.SubsidyTypeId);
                    rd.SetParameterValue("@SubsidyId", objLedgerStatement.SubsidyId);
                    rd.SetParameterValue("@IsApproved", objLedgerStatement.IsApproved);  
                    //rd.SetDatabaseLogon(SConn.UserID, SConn.Password, SConn.DataSource, SConn.InitialCatalog);
                    rd.DataSourceConnections[0].IntegratedSecurity = false;
                    //strServer, strDatabase, strUserID, strPwd)
                    rd.DataSourceConnections[0].SetConnection(SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "LedgerStatement");
                    rd.DataSourceConnections.Clear();
                    rd.Refresh();
                    rd.Dispose();
                    rd.Clone();
                    rd.Close();
                }
                else
                {
                    Response.Write("Nothing Found; No Report name found");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        [CustomActionFilter]
        public ActionResult AgentLedgerStatement()
        {
            LedgerStatement objLedgerStatement = new LedgerStatement();
            objLedgerStatement.Todate = DateTime.Now;
            objLedgerStatement.Fromdate = DateTime.Now;
            objLedgerStatement.SubsidyTypeId=2;//For Agent
            var lstAgent = new List<KeyValuePair<int, string>>();
            _IAgentService. GetActiveAgent(GolbalSession.gblSession.GlobalCompanyId,true).ToList().ForEach(delegate(Agent item)
            {
                lstAgent.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            objLedgerStatement.IsApproved = true;
            objLedgerStatement.kvpAgent = lstAgent;
            return View(objLedgerStatement);

        }
        [HttpPost]
        [CustomActionFilter]
        public void AgentLedgerStatement(LedgerStatement objLedgerStatement)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
            try
            {
                bool isValid = true;

                string strReportName = "AgentLedgerStatement.rpt";

                if (string.IsNullOrEmpty(strReportName))
                {
                    isValid = false;
                }
                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Views//FMSReports//" + strReportName;
                    rd.Load(strRptPath);
                    rd.SetParameterValue("@GlobalCompanyId", GolbalSession.gblSession.GlobalCompanyId);
                    rd.SetParameterValue("@Fromdate", objLedgerStatement.Fromdate);
                    rd.SetParameterValue("@Todate", objLedgerStatement.Todate);
                    rd.SetParameterValue("@SubsidyTypeId", objLedgerStatement.SubsidyTypeId);//2 For Agent
                    rd.SetParameterValue("@AgentId", objLedgerStatement.AgentId);
                    rd.SetParameterValue("@SubsidyId", objLedgerStatement.SubsidyId);
                    rd.SetParameterValue("@IsApproved", objLedgerStatement.IsApproved);
                    //rd.SetDatabaseLogon(SConn.UserID, SConn.Password, SConn.DataSource, SConn.InitialCatalog);
                    rd.DataSourceConnections[0].IntegratedSecurity = false;
                    //strServer, strDatabase, strUserID, strPwd)
                    rd.DataSourceConnections[0].SetConnection(SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "LedgerStatement");
                    rd.DataSourceConnections.Clear();
                    rd.Refresh();
                    rd.Dispose();
                    rd.Clone();
                    rd.Close();
                }
                else
                {
                    Response.Write("Nothing Found; No Report name found");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        [CustomActionFilter]
        public ActionResult TransactionList()
        {
            LedgerStatement objLedgerStatement = new LedgerStatement();
            objLedgerStatement.Todate = DateTime.Now;
            objLedgerStatement.Fromdate = DateTime.Now;
            var lstSubsidyType = new List<KeyValuePair<int, string>>();
            _FMS_SubsidyTypeService.GetActiveFMS_SubsidyType(true).ToList().ForEach(delegate(FMS_SubsidyType item)
            {
                lstSubsidyType.Add(new KeyValuePair<int, string>(item.SubsidyTypeId, item.SubsidyTypeName));
            });
            objLedgerStatement.kvpSubsidyType = lstSubsidyType;
            objLedgerStatement.IsApproved = true;
            return View(objLedgerStatement);

        }
        [HttpPost]
        [CustomActionFilter]
        public void TransactionList(LedgerStatement objLedgerStatement)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
            try
            {
                bool isValid = true;

                string strReportName = "TransactionList.rpt";

                if (string.IsNullOrEmpty(strReportName))
                {
                    isValid = false;
                }
                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Views//FMSReports//" + strReportName;
                    rd.Load(strRptPath);
                    rd.SetParameterValue("@GlobalCompanyId", GolbalSession.gblSession.GlobalCompanyId);
                    rd.SetParameterValue("@Fromdate", objLedgerStatement.Fromdate);
                    rd.SetParameterValue("@Todate", objLedgerStatement.Todate);
                    rd.SetParameterValue("@SubsidyTypeId", objLedgerStatement.SubsidyTypeId);
                    rd.SetParameterValue("@SubsidyId", objLedgerStatement.SubsidyId);
                    rd.SetParameterValue("@IsApproved", objLedgerStatement.IsApproved);
                    //rd.SetDatabaseLogon(SConn.UserID, SConn.Password, SConn.DataSource, SConn.InitialCatalog);
                    rd.DataSourceConnections[0].IntegratedSecurity = false;
                    //strServer, strDatabase, strUserID, strPwd)
                    rd.DataSourceConnections[0].SetConnection(SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "LedgerStatement");
                    rd.DataSourceConnections.Clear();
                    rd.Refresh();
                    rd.Dispose();
                    rd.Clone();
                    rd.Close();
                }
                else
                {
                    Response.Write("Nothing Found; No Report name found");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
         [CustomActionFilter]
        public ActionResult BalanceSheet()
        {
            LedgerStatement objLedgerStatement = new LedgerStatement();
            objLedgerStatement.Fromdate = DateTime.Now;
            //objLedgerStatement.Fromdate = DateTime.Now;
            //var lstSubsidyType = new List<KeyValuePair<int, string>>();
            //_FMS_SubsidyTypeService.GetActiveFMS_SubsidyType(true).ToList().ForEach(delegate(FMS_SubsidyType item)
            //{
            //    lstSubsidyType.Add(new KeyValuePair<int, string>(item.SubsidyTypeId, item.SubsidyTypeName));
            //});
            //objLedgerStatement.kvpSubsidyType = lstSubsidyType;
            return View(objLedgerStatement);

        }
        [HttpPost]
        [CustomActionFilter]
        public void BalanceSheet(LedgerStatement objLedgerStatement)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
            try
            {
                bool isValid = true;

                string strReportName = "BalanceSheet.rpt";

                if (string.IsNullOrEmpty(strReportName))
                {
                    isValid = false;
                }
                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Views//FMSReports//" + strReportName;
                    rd.Load(strRptPath);
                    rd.SetParameterValue("@GlobalCompanyId", GolbalSession.gblSession.GlobalCompanyId);
                    rd.SetParameterValue("@Date", objLedgerStatement.Fromdate);
                    rd.SetParameterValue("@ReportViewTypeId", objLedgerStatement.SubsidyTypeId);                   
                    //rd.SetDatabaseLogon(SConn.UserID, SConn.Password, SConn.DataSource, SConn.InitialCatalog);
                    rd.DataSourceConnections[0].IntegratedSecurity = false;
                    //strServer, strDatabase, strUserID, strPwd)
                    rd.DataSourceConnections[0].SetConnection(SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "BalanceSheet");
                    rd.DataSourceConnections.Clear();
                    rd.Refresh();
                    rd.Dispose();
                    rd.Clone();
                    rd.Close();
                }
                else
                {
                    Response.Write("Nothing Found; No Report name found");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
         [CustomActionFilter]
        public ActionResult IncomeStatement()
        {
            LedgerStatement objLedgerStatement = new LedgerStatement();
            objLedgerStatement.Fromdate = DateTime.Now;
            //objLedgerStatement.Fromdate = DateTime.Now;
            //var lstSubsidyType = new List<KeyValuePair<int, string>>();
            //_FMS_SubsidyTypeService.GetActiveFMS_SubsidyType(true).ToList().ForEach(delegate(FMS_SubsidyType item)
            //{
            //    lstSubsidyType.Add(new KeyValuePair<int, string>(item.SubsidyTypeId, item.SubsidyTypeName));
            //});
            //objLedgerStatement.kvpSubsidyType = lstSubsidyType;
            return View(objLedgerStatement);

        }
        [HttpPost]
        [CustomActionFilter]
        public void IncomeStatement(LedgerStatement objLedgerStatement)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
            try
            {
                bool isValid = true;
                string strReportName = "IncomeStatement.rpt";
                if (string.IsNullOrEmpty(strReportName))
                {
                    isValid = false;
                }
                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Views//FMSReports//" + strReportName;
                    rd.Load(strRptPath);
                    rd.SetParameterValue("@GlobalCompanyId", GolbalSession.gblSession.GlobalCompanyId);
                    rd.SetParameterValue("@Date", objLedgerStatement.Fromdate);
                    rd.SetParameterValue("@ReportViewTypeId", objLedgerStatement.SubsidyTypeId);       
                    //rd.SetDatabaseLogon(SConn.UserID, SConn.Password, SConn.DataSource, SConn.InitialCatalog);
                    rd.DataSourceConnections[0].IntegratedSecurity = false;
                    //strServer, strDatabase, strUserID, strPwd)
                    rd.DataSourceConnections[0].SetConnection(SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "IncomeStatement");
                    rd.DataSourceConnections.Clear();
                    rd.Refresh();
                    rd.Dispose();
                    rd.Clone();
                    rd.Close();
                }
                else
                {
                    Response.Write("Nothing Found; No Report name found");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
    }
}
