using MMS.Entities.Models;
using MMS.Service.ModelServices;
using MMS.Utility;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
namespace MMS.Controllers
{
    public class GLSubsidyTypeMapController : Controller
    {
          private readonly IFMS_GLSubsidyTypeMapService _fMS_GLSubsidyTypeMapService;
        private readonly IUnitOfWork _unitOfWork;

        public GLSubsidyTypeMapController(IFMS_GLSubsidyTypeMapService iFMS_GLSubsidyTypeMapService, IUnitOfWork unitOfWork)
        {
            _fMS_GLSubsidyTypeMapService = iFMS_GLSubsidyTypeMapService;
            _unitOfWork = unitOfWork;
        }
        // GET: /FMS_GLSubsidyTypeMap/
         [CustomActionFilter]
        public ActionResult Index(string Search, int? page)
        {

              int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 8;
            IList<FMS_GLSubsidyTypeMap> lstGLSubsidyTypeMap = new List<FMS_GLSubsidyTypeMap>();
            var gLSubsidyTypeMap = _fMS_GLSubsidyTypeMapService.GetAllFMS_GLSubsidyTypeMap(GolbalSession.gblSession.GlobalCompanyId).ToList().OrderByDescending(x => x.SubsidyTypeId);
            lstGLSubsidyTypeMap = (IList<FMS_GLSubsidyTypeMap>)gLSubsidyTypeMap.ToList();
            if (string.IsNullOrWhiteSpace(Search))
            {
                lstGLSubsidyTypeMap = lstGLSubsidyTypeMap.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                lstGLSubsidyTypeMap = lstGLSubsidyTypeMap.Where(p => p.FMS_GLAccount.GLAccountName.ToLower() == Search.ToLower()
                    ||p.FMS_SubsidyType.SubsidyTypeName==Search.ToLower()                    
                    ).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(lstGLSubsidyTypeMap);
           
        }

        //
        // GET: /FMS_GLSubsidyTypeMap/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FMS_GLSubsidyTypeMap/Create
        public ActionResult Create()
        {
            var gLSubsidyTypeMap = _fMS_GLSubsidyTypeMapService.newFMS_GLSubsidyTypeMap(GolbalSession.gblSession.GlobalCompanyId,4, 2);
         return View(gLSubsidyTypeMap);
        }

        //
        // POST: /FMS_GLSubsidyTypeMap/Create
        [HttpPost]
        public ActionResult Create(FMS_GLSubsidyTypeMap objFMS_GLSubsidyTypeMap)
        {
            try
            {
                // TODO: Add insert logic here
                //objFMS_GLSubsidyTypeMap.GlSubsidyTpyeMapCode = Guid.NewGuid().ToString();
                objFMS_GLSubsidyTypeMap.SetDate = DateTime.Now;
                objFMS_GLSubsidyTypeMap.SetBy = GolbalSession.gblSession.UserId;
                _fMS_GLSubsidyTypeMapService.Insert(objFMS_GLSubsidyTypeMap);
                TempData.Add("success", "Map Created Successfully.");
                _unitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FMS_GLSubsidyTypeMap/Edit/5
        public ActionResult Edit(string id)
        {
           var gLSubsidyTypeMap= _fMS_GLSubsidyTypeMapService.Find(id);
           var newgLSubsidyTypeMap = _fMS_GLSubsidyTypeMapService.newFMS_GLSubsidyTypeMap(GolbalSession.gblSession.GlobalCompanyId,4, 2);
            gLSubsidyTypeMap.kvpPostedGLAccount = newgLSubsidyTypeMap.kvpPostedGLAccount;
            gLSubsidyTypeMap.kvpSubsidyType = newgLSubsidyTypeMap.kvpSubsidyType;
            return View(gLSubsidyTypeMap);
           
        }

        //
        // POST: /FMS_GLSubsidyTypeMap/Edit/5
        [HttpPost]
        public ActionResult Edit(FMS_GLSubsidyTypeMap objFMS_GLSubsidyTypeMap)
        {
            try
            {
                // TODO: Add insert logic here
               
                objFMS_GLSubsidyTypeMap.SetDate = DateTime.Now;
                objFMS_GLSubsidyTypeMap.SetBy = GolbalSession.gblSession.UserId;
                _fMS_GLSubsidyTypeMapService.Update(objFMS_GLSubsidyTypeMap);
                TempData.Add("success", "Map Update Successfully.");
                _unitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FMS_GLSubsidyTypeMap/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FMS_GLSubsidyTypeMap/Delete/5
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
