using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Controllers
{
    public class MessageInfoController : Controller
    {
        private readonly IMessageInfoService _iMessageInfoService;
        private readonly IUnitOfWork _unitOfWork;
        public MessageInfoController(IMessageInfoService iMessageInfoService, IUnitOfWork unitOfWork)
        {
            _iMessageInfoService = iMessageInfoService;
            _unitOfWork = unitOfWork;
        }

        // GET: /MessageInfo/
        public ActionResult Index()
        {
            var lstMessageInfo = _iMessageInfoService.GetAllMessageInfo();
            return View(lstMessageInfo);
        }

        //
        // GET: /MessageInfo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MessageInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MessageInfo/Create
        [HttpPost]
        public ActionResult MessageMailSend(FormCollection collection)
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
        // GET: /MessageInfo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MessageInfo/Edit/5
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
        // GET: /MessageInfo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MessageInfo/Delete/5
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
