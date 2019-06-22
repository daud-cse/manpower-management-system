﻿using MMS.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MMS.Controllers
{
    public class FileMovementController : Controller
    {
        //
        // GET: /FileMovement/
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            var echo = int.Parse(System.Web.HttpContext.Current.Request.Params["sEcho"]);
            var displayLength = int.Parse(System.Web.HttpContext.Current.Request.Params["iDisplayLength"]);
            var displayStart = int.Parse(System.Web.HttpContext.Current.Request.Params["iDisplayStart"]);
            var sortOrder = System.Web.HttpContext.Current.Request.Params["sSortDir_0"].ToString(CultureInfo.CurrentCulture);
            var roleId = System.Web.HttpContext.Current.Request.Params["roleId"].ToString(CultureInfo.CurrentCulture);

            var records = GetRecordsFromDatabaseWithFilter().ToList();

            if (!records.Any())
            {
               // return string.Empty;
            }

            var orderedResults = sortOrder == "asc"
                                 ? records.OrderBy(o => o.UserId)
                                 : records.OrderByDescending(o => o.UserId);
            var itemsToSkip = displayStart == 0
                              ? 0
                              : displayStart + 1;
            var pagedResults = orderedResults.Skip(itemsToSkip).Take(displayLength).ToList();
            var hasMoreRecords = false;

            var sb = new StringBuilder();
            sb.Append(@"{" + "\"sEcho\": " + echo + ",");
            sb.Append("\"recordsTotal\": " + records.Count + ",");
            sb.Append("\"recordsFiltered\": " + records.Count + ",");
            sb.Append("\"iTotalRecords\": " + records.Count + ",");
            sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
            sb.Append("\"aaData\": [");
            foreach (var result in pagedResults)
            {
                if (hasMoreRecords)
                {
                    sb.Append(",");
                }

                sb.Append("[");
                sb.Append("\"" + result.UserId + "\",");
                sb.Append("\"" + result.Name + "\",");
                sb.Append("\"" + result.Address + "\",");
                sb.Append("\"" + result.Age + "\",");
                sb.Append("\"<img class='image-details' src='content/details_open.png' runat='server' height='16' width='16' alt='View Details'/>\"");
                sb.Append("]");
                hasMoreRecords = true;
            }
            sb.Append("]}");
            var data=sb.ToString();
            return new JsonResult() { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return data;
           
        }
        //
        // GET: /FileMovement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        private static IEnumerable<CustomUser> GetRecordsFromDatabaseWithFilter()
        {
            // At this point you can call to your database to get the data
            // but I will just populate a sample collection in code
            return new List<CustomUser>
            {
                new CustomUser
                {
                    UserId = 1,
                    Address = "1 Newton Square, London",
                    Age=25,
                    Name="John Smith"
                },
                new CustomUser
                {
                    UserId = 2,
                    Address = "5 George Road, Manchester",
                    Age= 31,
                    Name = "Erica Keir"
                },
                new CustomUser
                {
                    UserId = 3,
                    Address = "32 Queen Mary St, Newcastle",
                    Age = 12,
                    Name = "Test McDermont"
                },
                  new CustomUser
                {
                    UserId = 7,
                    Address = "1 Newton Square, London",
                    Age=25,
                    Name="John Smith"
                },
                  new CustomUser
                {
                    UserId = 8,
                    Address = "1 Newton Square, London",
                    Age=25,
                    Name="John Smith"
                },
                  new CustomUser
                {
                    UserId = 9,
                    Address = "1 Newton Square, London",
                    Age=25,
                    Name="John Smith"
                }
            };
        }
        //
        // GET: /FileMovement/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FileMovement/Create
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
        // GET: /FileMovement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FileMovement/Edit/5
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
        // GET: /FileMovement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FileMovement/Delete/5
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
