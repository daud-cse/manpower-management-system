using MMS.Entities.Models;
using MMS.Entities.ViewModels;
using MMS.Service.ModelServices;
using MMS.Service.ViewModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Controllers
{
    public class MessageMailMasterController : Controller
    {
         private readonly IMessageMailMasterService _iMessageMailMasterService;
        private readonly IUnitOfWork _unitOfWork;
           private readonly IAgentService _iagentService;

           private readonly IMessageSendVMService _MessageSendVMService;
           public MessageMailMasterController(IMessageMailMasterService iMessageMailMasterService, IUnitOfWork unitOfWork
            ,IAgentService iagentService
               , IMessageSendVMService MessageSendVMService)
        {
            _iMessageMailMasterService = iMessageMailMasterService;
            _unitOfWork = unitOfWork;
             _iagentService = iagentService;
             _MessageSendVMService = MessageSendVMService;
        }


        // GET: /MessageMailMaster/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /MessageMailMaster/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MessageMailMaster/Create
        public ActionResult Create()
        {


            return View();
        }


        public ActionResult VmMessageMail()
        {
            var objMessageMailMaster= _iMessageMailMasterService.newMessageMailMaster();
            return View(objMessageMailMaster);
        }

        [HttpPost]
        public ActionResult VmMessageMail(MessageMailMaster objMessageMailMaster, List<MessageMailVM> lstMessageMailVM)
        {
            try
            {
                bool IsSucess = true;
                objMessageMailMaster.MessageAreaTypeId = 3;
               
               
            var   msg =_iMessageMailMasterService.VMMessageMailInsert(objMessageMailMaster, lstMessageMailVM,GolbalSession.gblSession.GlobalCompanyId, GolbalSession.gblSession.UserId, _unitOfWork);

                if(msg=="true"){
                   // _MessageSendVMService.MessageSend(_unitOfWork); //Line Commets for Windows Service Desgin
                    IsSucess = true;
                }
                else
                {
                    IsSucess = false;
                }
              
                return new JsonResult()
                {
                    Data = IsSucess,

                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch
            {
                return View();
            }
        }
         public ActionResult GetTypeWiseList(int MessageAreaTypeId)
        {
            var messageMailVMList = new List<MessageMailVM>();
            var agentList = _iagentService.GetActiveAgent(GolbalSession.gblSession.GlobalCompanyId, true);
            foreach (var item in agentList)
            {
                messageMailVMList.Add(new MessageMailVM
                {
                    Id = item.AgentId,
                    Name = item.Name,
                    MobileNo = item.Mobile,
                    EmailId = item.Email==null?"":item.Email,
                    IsSent = true
                    
                });
            }
          
            return new JsonResult()
            {
                Data = messageMailVMList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        //
        // POST: /MessageMailMaster/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MessageMailMaster/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MessageMailMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MessageMailMaster/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MessageMailMaster/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
