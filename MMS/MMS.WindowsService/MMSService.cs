using MMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MMS.WindowsService
{
    public partial class MMSSERVICE : ServiceBase
    {
        Thread mainThreadObj;
        //private readonly IUnitOfWork _unitOfWork;
        //  private readonly IMessageSendVMService _IMessageSendVMService;
        //private readonly IAgentService _agentService;
        //private readonly IApplicantService _applicantService;
        //private readonly ILocationService _locationService;
        //private readonly IMessageInfoService _messageInfoService;
        DateTime currentDateTime;

        Thread objProcessThread;

        public MMSSERVICE()
        {
            InitializeComponent();
        }

        public void start()
        {

            string[] args = default(string[]);
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            currentDateTime = DateTime.Now;

            mainThreadObj = new Thread(ProcessThread);

            mainThreadObj.Start();

        }
        public void debug()
        {
            OnStart(null);
        }
        protected override void OnStop()
        {
        }
        public bool MessageSend(MessageInfo item)
        {

            try
            {



                string baseUrl = "https://powersms.banglaphone.net.bd";
                string userId = "shikkhaforall";
                string password = "Shikkhaforall123";


                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new FormUrlEncodedContent(new[] 
                                                     {
                                                        new KeyValuePair<string, string>("userId", userId)
                                                        ,new KeyValuePair<string, string>("password", password)
                                                        ,new KeyValuePair<string, string>("smsText",item.MessageBody) 
                                                        ,new KeyValuePair<string, string>("commaSeperatedReceiverNumbers", item.MobileNo)                    
                                                     });

                    var response = client.PostAsync("/httpapi/sendsms", content).Result;

                    return response.IsSuccessStatusCode;
                }

            }
            catch
            {
                return false;
            }



        }
        public string MessageSend()
        {


            string connectionString = ConfigurationManager.ConnectionStrings["MPMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            string vComTxt = @"SELECT MI.Id, MI.MobileNo                             
                              ,MI.MessageBody   
                              FROM  [MessageInfo] as MI "
                            + @" WHERE  MI.IsSent=0 ";
            List<MessageInfo> lstMessageInfo = new List<MessageInfo>();
            cmd.CommandText = vComTxt;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            using (IDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    MessageInfo objMessageInfo = new MessageInfo();
                    if (!String.IsNullOrEmpty(dr["Id"].ToString()))
                        objMessageInfo.Id = Convert.ToInt32(dr["Id"].ToString());
                    if (!String.IsNullOrEmpty(dr["MobileNo"].ToString()))
                        objMessageInfo.MobileNo = dr["MobileNo"].ToString();
                    if (!String.IsNullOrEmpty(dr["MessageBody"].ToString()))
                        objMessageInfo.MessageBody = dr["MessageBody"].ToString();
                    lstMessageInfo.Add(objMessageInfo);

                }
            }
            lstMessageInfo.ForEach(delegate(MessageInfo item)
            {
                var IsSent= MessageSend(item);
                if (IsSent)
                {
                    vComTxt = @"Update  MessageInfo set IsSent=1"
                         + @" WHERE  Id=" + item.Id;
                }
                else
                {
                    vComTxt = @"Update  MessageInfo set IsSent=0"
                         + @" WHERE  Id=" + item.Id;
                }
                
                cmd.CommandText = vComTxt;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            });

            string Msg = "";
            con.Close();
            con.Dispose();
            cmd.Dispose();
            return Msg;
        }
        private void ProcessThread()
        {



            if (objProcessThread == null ||
            objProcessThread.ThreadState == System.Threading.ThreadState.Stopped ||
            objProcessThread.ThreadState == System.Threading.ThreadState.Unstarted)
            {


                while (DateTime.Now >= currentDateTime.AddSeconds(10))//Every 10 Secs
                {
                    objProcessThread = new Thread(threadProcessMethod);

                    objProcessThread.IsBackground = true;                    
                    objProcessThread.Start();
                    MessageSend();                   
                    currentDateTime = currentDateTime.AddSeconds(10);

                    Thread.Sleep(1000 * 10);

                    ProcessThread();
                }

                Thread.Sleep(1000 * 10); //Every 10 Secs

                ProcessThread();
            }
        }

        private void threadProcessMethod()
        {
            Console.WriteLine(" This Method Will Call Every 10 Seconds : " + currentDateTime);
        }
    }
}
