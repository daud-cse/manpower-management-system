using MMS.Entities.Models;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MMS.Controllers
{
    public class ContentDetailsController : Controller
    {
       
         private readonly IContentDetailService _iContentDetailService;
        
        private readonly IUnitOfWork _unitOfWork;

        public ContentDetailsController(IContentDetailService iContentDetailService,            
            IUnitOfWork unitOfWork)
        {
            _iContentDetailService=iContentDetailService;
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /ContentDetails/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ContentDetails/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult GetImage(int id)
        {
           
            var img = _iContentDetailService.GetContentDetailByContentId(id);           
            if (img == null)
            {
                img = new ContentDetail();  
                img =_iContentDetailService.GetContentDefaultImage(2);//for default image
                   if(img==null){
                       img = new ContentDetail();  
                       img.FileExtension = "image/jpeg";
                       img.Object = img.Object == null ? new byte[] { } : new byte[] { };
                   }         
            
            }
           
            return File(img.Object,img.FileExtension);
           
        }
        //
        // GET: /ContentDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ContentDetails/Create
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
        // GET: /ContentDetails/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ContentDetails/Edit/5
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
        // GET: /ContentDetails/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ContentDetails/Delete/5
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
