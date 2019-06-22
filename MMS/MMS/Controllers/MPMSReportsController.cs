using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MMS.Entities.ViewModels;
using MMS.Service.ViewModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Controllers
{
    public class MPMSReportsController : Controller
    {
        private readonly ISearchApplicantVMService _searchApplicantVMService;
        private readonly IUnitOfWork _unitOfWork;
        public MPMSReportsController(ISearchApplicantVMService searchApplicantVMService, IUnitOfWork unitOfWork)
        {
          
            _searchApplicantVMService = searchApplicantVMService;
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /MPMSReports/Details/5
        public ActionResult AgentWiseApplicantList()
        {
            var searchApplicantVM = _searchApplicantVMService.newSearchApplicantVMForAgentWiseApplicantReports(GolbalSession.gblSession.GlobalCompanyId);          
            return View(searchApplicantVM);
           
        }
        [Route("Applicant Details")]

        public void GetAgentWiseApplicantList(SearchApplicantVM objSearchApplicantVM)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);

            try
            {
                bool isValid = true;

                string strReportName = "AgentWiseApplicantList.rpt";

                if (string.IsNullOrEmpty(strReportName))
                {
                    isValid = false;
                }

                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();

                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Views//MPMSReports//" + strReportName;
                    rd.Load(strRptPath);
                    rd.SetParameterValue("@AgentId", objSearchApplicantVM.AgentId);
                    rd.SetParameterValue("@ApplicantId", objSearchApplicantVM.ApplicantId);
                    rd.SetParameterValue("@LocationId", objSearchApplicantVM.LocationId);
                    rd.DataSourceConnections[0].IntegratedSecurity = false;
                    //strServer, strDatabase, strUserID, strPwd)
                    rd.DataSourceConnections[0].SetConnection(SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);
                 //   rd.SetDatabaseLogon(SConn.UserID, SConn.Password, SConn.DataSource, SConn.InitialCatalog);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "AgentWiseApplicant");
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
        [Route("Applicant Details")]
       
        public void GetApplicantDetails(int AgentId=0 ,int ApplicantID=0)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);

            try
            {
                bool isValid = true;

                string strReportName = "ApplicantDetails.rpt";

                if (string.IsNullOrEmpty(strReportName))
                {
                    isValid = false;
                }

                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();

                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Views//MPMSReports//" + strReportName;
                    rd.Load(strRptPath);
                    rd.SetParameterValue("@AgentId",AgentId);
                    rd.SetParameterValue("@ApplicantId",ApplicantID);                  
                    //rd.SetDatabaseLogon(SConn.UserID, SConn.Password, SConn.DataSource, SConn.InitialCatalog);
                    rd.DataSourceConnections[0].IntegratedSecurity = false;
                    //strServer, strDatabase, strUserID, strPwd)
                    rd.DataSourceConnections[0].SetConnection(SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "ApplicantReports" + "(" + ApplicantID + ")");
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


        [Route("Applicant Details Single Page")]

        public void GetApplicantDetailsSinglePage(int AgentId = 0, int ApplicantID = 0)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);

            try
            {
                bool isValid = true;

                string strReportName = "ApplicantDetailsSinglePage.rpt";

                if (string.IsNullOrEmpty(strReportName))
                {
                    isValid = false;
                }

                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();

                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Views//MPMSReports//" + strReportName;
                    rd.Load(strRptPath);
                    rd.SetParameterValue("@AgentId", AgentId);
                    rd.SetParameterValue("@ApplicantId", ApplicantID);                
                    //rd.SetDatabaseLogon(SConn.UserID, SConn.Password, SConn.DataSource, SConn.InitialCatalog);
                    rd.DataSourceConnections[0].IntegratedSecurity = false;
                    //strServer, strDatabase, strUserID, strPwd)
                    rd.DataSourceConnections[0].SetConnection(SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "ApplicantReports" + "(" + ApplicantID + ")");
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

        [Route("Passport Details")]

        public void GetPassportInfoDetails(int CustomerID = 0, int PasportInfoId = 0)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);

            try
            {
                bool isValid = true;

                string strReportName = "PassportInfoDetails.rpt";

                if (string.IsNullOrEmpty(strReportName))
                {
                    isValid = false;
                }

                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();

                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Views//MPMSReports//" + strReportName;
                    rd.Load(strRptPath);
                    rd.SetParameterValue("@CustomerId", CustomerID);
                    rd.SetParameterValue("@PassportInfoId", PasportInfoId);                   
                    //rd.SetDatabaseLogon(SConn.UserID, SConn.Password, SConn.DataSource, SConn.InitialCatalog);
                    rd.DataSourceConnections[0].IntegratedSecurity = false;
                    //strServer, strDatabase, strUserID, strPwd)
                    rd.DataSourceConnections[0].SetConnection(SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "CEPWorkShopTotal");
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
