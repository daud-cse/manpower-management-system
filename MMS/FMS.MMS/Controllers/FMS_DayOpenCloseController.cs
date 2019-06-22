using MMS.Entities.Models;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using MMS.FMS.Utility;
namespace MMS.FMS.Controllers
{
    public class DayOpenCloseController : Controller
    {
         private readonly IFMS_DayOpenCloseService _iFMS_DayOpenCloseService;
        private readonly IUnitOfWork _unitOfWork;

        public DayOpenCloseController(IFMS_DayOpenCloseService iFMS_DayOpenCloseService, IUnitOfWork unitOfWork)
        {
            _iFMS_DayOpenCloseService = iFMS_DayOpenCloseService;
            _unitOfWork = unitOfWork;
        }
         [CustomActionFilter]
        public ActionResult Index(string searchItem, int? page)
        {

            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize =12 ;
            IList<FMS_DayOpenClose> DayOpenCloseList = new List<FMS_DayOpenClose>();
            var lstDayOpenClose = _iFMS_DayOpenCloseService.GetAllFMS_DayOpenClose(GolbalSession.gblSession.GlobalCompanyId).OrderByDescending(x=>x.OpenDate);
            DayOpenCloseList = (IList<FMS_DayOpenClose>)lstDayOpenClose.ToList();
            if (string.IsNullOrWhiteSpace(searchItem))
            {
                DayOpenCloseList = DayOpenCloseList.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                DayOpenCloseList = DayOpenCloseList.Where(p => Convert.ToString(p.IsDayClosed) == searchItem.ToLower()                   
                    ).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(DayOpenCloseList);
        }

        //
        // GET: /FMS_DayOpenClose/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FMS_DayOpenClose/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FMS_DayOpenClose/Create
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
        // GET: /FMS_DayOpenClose/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FMS_DayOpenClose/Edit/5
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
        // GET: /FMS_DayOpenClose/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FMS_DayOpenClose/Delete/5
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
