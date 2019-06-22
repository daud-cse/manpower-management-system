using MMS.Entities.Models;
using MMS.Service.ModelServices;
using MMS.Utility;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Controllers
{
    public class LocationMapDetailController : Controller
    {
        private readonly ILocationMapDetailService _iLocationMapDetailService;

        private readonly IUnitOfWork _unitOfWork;

        public LocationMapDetailController(ILocationMapDetailService iLocationMapDetailService, IUnitOfWork unitOfWork)
        {
            _iLocationMapDetailService = iLocationMapDetailService;
            _unitOfWork = unitOfWork;
        }
        // GET: /LocationMapDetail/
        [CustomActionFilter]
        public ActionResult Index()
        {

            var lstLocationMapDetail = _iLocationMapDetailService.GetAllLocationMapDetail(GolbalSession.gblSession.GlobalCompanyId);
            return View(lstLocationMapDetail);
        }

        //
        // GET: /LocationMapDetail/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /LocationMapDetail/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            var objLocationMapDetail = _iLocationMapDetailService.newLocationMapDetail(GolbalSession.gblSession.GlobalCompanyId);
            return View(objLocationMapDetail);
        }

        //
        // POST: /LocationMapDetail/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(LocationMapDetail objLocationMapDetail)
        {
            try
            {


                var objLocationMapDetailExist = _iLocationMapDetailService.GetLocationMapDetailByLocationId(GolbalSession.gblSession.GlobalCompanyId, objLocationMapDetail.LocationId);

                if (objLocationMapDetailExist.Any())
                {
                    var obj = objLocationMapDetailExist.Where(x => x.ControlTypeId == objLocationMapDetail.ControlTypeId);
                    if (!obj.Any())
                    {
                        TempData.Add("danger", "You Can only Add Control as Same Control Type.");

                        return RedirectToAction("Create");
                    }
                }


                objLocationMapDetail.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                objLocationMapDetail.SetBy = GolbalSession.gblSession.UserId;
                objLocationMapDetail.SetDate = DateTime.Now;
                _iLocationMapDetailService.Insert(objLocationMapDetail);
                TempData.Add("success", "Map Loction Successfully.");
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /LocationMapDetail/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(int id)
        {
            var objLocationMapDetail = _iLocationMapDetailService.GetLocationMapDetailById(id, GolbalSession.gblSession.GlobalCompanyId);
            var objLocationMapDetailnew = _iLocationMapDetailService.newLocationMapDetail(GolbalSession.gblSession.GlobalCompanyId);

            objLocationMapDetail.kvpControlType = objLocationMapDetailnew.kvpControlType;
            objLocationMapDetail.kvpLocation = objLocationMapDetailnew.kvpLocation;

            return View(objLocationMapDetail);
        }

        //
        // POST: /LocationMapDetail/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(LocationMapDetail objLocationMapDetail)
        {

            try
            {
                var objLocationMapDetailExist = _iLocationMapDetailService.GetLocationMapDetailByLocationId(GolbalSession.gblSession.GlobalCompanyId, objLocationMapDetail.LocationId);

                if (objLocationMapDetailExist.ToList().Count>1)
                {
                    var obj = objLocationMapDetailExist.Where(x => x.ControlTypeId == objLocationMapDetail.ControlTypeId);
                    if (!obj.Any())
                    {
                        TempData.Add("danger", "You Can only Add Control as Same Control Type.");

                        return RedirectToAction("Edit", new { id = objLocationMapDetail.ID });
                    }
                }

                var objLocationMapDetailNew = _iLocationMapDetailService.Find(objLocationMapDetail.ID);
                objLocationMapDetailNew.SetBy = GolbalSession.gblSession.UserId;

                objLocationMapDetailNew.SetDate = DateTime.Now;
                objLocationMapDetailNew.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;                
                objLocationMapDetailNew.IsActive = objLocationMapDetail.IsActive;
                objLocationMapDetailNew.ControlTypeId = objLocationMapDetail.ControlTypeId;
                objLocationMapDetailNew.LocationId = objLocationMapDetail.LocationId;
                objLocationMapDetailNew.Name = objLocationMapDetail.Name;
                _iLocationMapDetailService.Update(objLocationMapDetailNew);
                TempData.Add("success", "Map Loction Update Successfully.");
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Add("danger", ex.Message.ToString());
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /LocationMapDetail/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /LocationMapDetail/Delete/5
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
