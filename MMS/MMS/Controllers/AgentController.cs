using MMS.Entities.Models;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using MMS.Utility;
using MMS.Entities.StoredProcedures;
namespace MMS.Controllers
{
    public class AgentController : Controller
    {

        private readonly IAgentService _iagentService;
        private readonly IAgentContentService _iAgentContentService;
        private readonly IContentService _iContentService;
        private readonly IStoredProcedures _iStoredProcedures;
        private readonly IUnitOfWork _unitOfWork;

        public AgentController(IAgentService iagentService,
            IAgentContentService iAgentContentService,
            IContentService iContentService,
            IStoredProcedures iStoredProcedures,
            IUnitOfWork unitOfWork)
        {
            _iagentService = iagentService;
            _iContentService = iContentService;
            _iAgentContentService = iAgentContentService;
            _iStoredProcedures = iStoredProcedures;
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /Agent/
        public ActionResult Index(string searchItem, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 15;

            IList<Agent> agentlist = new List<Agent>();
            var agent = _iagentService.GetAllAgent(GolbalSession.gblSession.GlobalCompanyId).ToList().OrderByDescending(x => x.ID);
            agentlist = (IList<Agent>)agent.ToList();
            if (string.IsNullOrWhiteSpace(searchItem))
            {
                agentlist = agentlist.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                agentlist = agentlist.Where(p => p.Name.ToLower().Contains(searchItem.ToLower())
                    || p.AgentId.ToLower() == searchItem.ToLower()
                      || p.Mobile.ToLower().Contains(searchItem.ToLower())
                        || p.District.Name.ToLower().Contains(searchItem.ToLower())
                         || p.AgentType.Name.ToLower() == searchItem.ToLower()
                    ).ToPagedList(currentPageIndex, defaultPageSize);
            }
            ViewData["searchItem"] = searchItem;
            return View(agentlist);
        }

        //
        // GET: /Agent/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Agent/Create
        [CustomActionFilter]
        public ActionResult Create()
        {

            var agent = _iagentService.newAgent(GolbalSession.gblSession.GlobalCompanyId);
            agent.IsActive = true;
            return View(agent);
        }

        //
        // POST: /Agent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public ActionResult Create(Agent objAgent, IEnumerable<HttpPostedFileBase> uploadAgentImage)
        {
            try
            {
                Content objContent = new Content();

                if (uploadAgentImage != null)
                {
                    objContent = _iContentService.SaveContent(uploadAgentImage, _unitOfWork,GolbalSession.gblSession.GlobalCompanyId, GolbalSession.gblSession.UserId);
                }               
                var autoId=   _iStoredProcedures.GetAutoGenerateId(GolbalSession.gblSession.GlobalCompanyId,2);//2 for Agent
                objAgent.SetDate = DateTime.Now;
                objAgent.SetBy = GolbalSession.gblSession.UserId;
                objAgent.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                objAgent.AgentPhotoID = objContent.ID;
                objAgent.AgentId = autoId;
                _iagentService.Insert(objAgent);
                _unitOfWork.SaveChanges();                
                TempData.Add("success", "Agent Created Successfully.");
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }

        }

        //
        // GET: /Agent/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(int id)
        {
            var agent = _iagentService.Find(id);
            var agentnew = _iagentService.newForAgentEdit(agent, GolbalSession.gblSession.GlobalCompanyId);
            agent.kvpAgentType = agentnew.kvpAgentType;
            agent.kvpCountry = agentnew.kvpCountry;
            agent.kvpDistrict = agentnew.kvpDistrict;
            agent.kvpDivision = agentnew.kvpDivision;
            agent.kvpNationality = agentnew.kvpNationality;
            agent.kvpUpazila = agentnew.kvpUpazila;
            return View(agent);
        }

        //
        // POST: /Agent/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(Agent objAgent, IEnumerable<HttpPostedFileBase> uploadAgentImage)
        {
            try
            {

                Content objContent = new Content();

                if (uploadAgentImage != null)
                {
                    objContent = _iContentService.UpdateContent(uploadAgentImage, _unitOfWork,GolbalSession.gblSession.GlobalCompanyId, objAgent.AgentPhotoID);
                }
                objAgent.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                objAgent.SetBy = GolbalSession.gblSession.UserId;
                objAgent.SetDate = DateTime.Now;
                objAgent.AgentPhotoID = objContent.ID;
                _iagentService.Update(objAgent);
                _unitOfWork.SaveChanges();
                TempData.Add("success", "Agent Update Successfully.");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Agent/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Agent/Delete/5
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
