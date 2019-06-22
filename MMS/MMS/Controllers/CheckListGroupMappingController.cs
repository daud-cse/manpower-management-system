using MMS.Entities.Models;
using MMS.Entities.ViewModels;
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
    public class CheckListGroupMappingController : Controller
    {
        private readonly ICheckListGroupMappingService _iCheckListGroupMappingService;
        private readonly IApplicantCheckListService _iApplicantCheckListService;
        private readonly ICheckListService _iCheckListService;
        private readonly IUnitOfWork _unitOfWork;

        public CheckListGroupMappingController(ICheckListGroupMappingService iCheckListGroupMappingService, IApplicantCheckListService iApplicantCheckListService, IUnitOfWork unitOfWork, ICheckListService iCheckListService)
        {
            _iCheckListGroupMappingService = iCheckListGroupMappingService;
            _iApplicantCheckListService = iApplicantCheckListService;
            _iCheckListService = iCheckListService;
            _unitOfWork = unitOfWork;
        }
        // GET: /CheckListGroupMapping/

        [CustomActionFilter]
        public ActionResult Index()
        {
            var lstCheckListGroupMapping = _iCheckListGroupMappingService.GetAllCheckListGroupMapping(GolbalSession.gblSession.GlobalCompanyId).OrderBy(x => x.CheckListGroupID);
            return View(lstCheckListGroupMapping);
        }

        //
        // GET: /CheckListGroupMapping/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [CustomActionFilter]
        public JsonResult GetCheckListGroupMappingByTypeId(int CheckListGroupId)
        {

            var lstCheckListService = _iCheckListService.GetAllCheckList(GolbalSession.gblSession.GlobalCompanyId);
            var lstCheckListGroupMapping = _iCheckListGroupMappingService.GetCheckListGroupMappingByGroupId(GolbalSession.gblSession.GlobalCompanyId, CheckListGroupId);
            var lstCheckListGroupVM = new List<CheckListGroupVM>();
            if (lstCheckListGroupMapping.Any())
            {


                if (lstCheckListService.Any())
                {
                    lstCheckListService.ToList().ForEach(delegate(CheckList item)
                    {
                        var objCheckListGroupMap = lstCheckListGroupMapping.Where(x => x.CheckListID == item.ID && x.CheckListGroupID == CheckListGroupId).FirstOrDefault();
                        var objCheckListGroupVM = new CheckListGroupVM();
                        if (objCheckListGroupMap == null)
                        {
                            objCheckListGroupVM.ApplicantCheckListID = 0;
                            objCheckListGroupVM.CheckListGroupMapID = 0;
                            objCheckListGroupVM.CheckListID = item.ID;
                            objCheckListGroupVM.CheckListName = item.Name;
                            objCheckListGroupVM.IsCheckList = false;
                            lstCheckListGroupVM.Add(objCheckListGroupVM);
                        }
                        else
                        {
                            objCheckListGroupVM.ApplicantCheckListID = 0;
                            objCheckListGroupVM.CheckListGroupMapID = objCheckListGroupMap.ID;
                            objCheckListGroupVM.CheckListID = objCheckListGroupMap.CheckListID;
                            objCheckListGroupVM.CheckListName = objCheckListGroupMap.CheckList.Name;
                            objCheckListGroupVM.IsCheckList = objCheckListGroupMap.IsActive;
                            objCheckListGroupVM.Description = string.Empty;
                            lstCheckListGroupVM.Add(objCheckListGroupVM);
                        }

                    });
                }


            }
            else
            {
                lstCheckListService.ToList().ForEach(delegate(CheckList item)
                   {
                       var objCheckListGroupVM = new CheckListGroupVM();
                       objCheckListGroupVM.ApplicantCheckListID = 0;
                       objCheckListGroupVM.CheckListGroupMapID = 0;

                       objCheckListGroupVM.CheckListID = item.ID;
                       objCheckListGroupVM.CheckListName = item.Name;
                       objCheckListGroupVM.IsCheckList = false;
                       objCheckListGroupVM.Description = string.Empty;
                       lstCheckListGroupVM.Add(objCheckListGroupVM);
                   });

            }
            return new JsonResult()
            {
                Data = lstCheckListGroupVM,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        //
        // GET: /CheckListGroupMapping/Create

        [CustomActionFilter]
        public JsonResult GetCheckListGroupMapping(int CheckListGroupId, bool IsUpdate, int applicantID)
        {
            var lstCheckListGroupMapping = _iCheckListGroupMappingService.GetCheckListGroupMappingByGroupId(GolbalSession.gblSession.GlobalCompanyId, CheckListGroupId,true);
            var lstCheckListGroupVM = new List<CheckListGroupVM>();
            if (lstCheckListGroupMapping.Any())
            {


                if (IsUpdate)
                {
                    var lstApplicantCheckList = _iApplicantCheckListService.GetApplicantCheckListByApplicantId(GolbalSession.gblSession.GlobalCompanyId, applicantID);
                    if (lstCheckListGroupMapping.Count() > lstApplicantCheckList.Count())
                    {
                        if (lstApplicantCheckList.Any())
                        {
                            lstCheckListGroupMapping.ToList().ForEach(delegate(CheckListGroupMapping item)
                            {
                                var objApplicantCheckList = lstApplicantCheckList.Where(x => x.CheckListGroupMapID == item.ID && x.ApplicantID == applicantID).FirstOrDefault();
                                var objCheckListGroupVM = new CheckListGroupVM();
                                if (objApplicantCheckList == null)
                                {
                                    objCheckListGroupVM.ApplicantCheckListID = 0;
                                    objCheckListGroupVM.CheckListGroupMapID = item.ID;
                                    objCheckListGroupVM.CheckListID = item.CheckListID;
                                    objCheckListGroupVM.CheckListName = item.CheckList.Name;
                                    objCheckListGroupVM.IsCheckList = false;
                                    objCheckListGroupVM.Description = item.Description == null ? string.Empty : item.Description;
                                    lstCheckListGroupVM.Add(objCheckListGroupVM);
                                }

                                else
                                {

                                    objCheckListGroupVM.ApplicantCheckListID = objApplicantCheckList.ID;
                                    objCheckListGroupVM.CheckListGroupMapID = objApplicantCheckList.CheckListGroupMapping.ID;
                                    objCheckListGroupVM.CheckListGroupName = item.ApplicantType.Name;
                                    objCheckListGroupVM.CheckListID = item.CheckListID;
                                    objCheckListGroupVM.CheckListName = item.CheckList.Name;
                                    objCheckListGroupVM.IsCheckList = objApplicantCheckList.IsCompliant;
                                    objCheckListGroupVM.Description = item.Description == null ? string.Empty : item.Description;
                                    lstCheckListGroupVM.Add(objCheckListGroupVM);
                                }

                            });
                        }
                        else
                        {
                            lstCheckListGroupMapping.ToList().ForEach(delegate(CheckListGroupMapping item)
                            {
                                var objCheckListGroupVM = new CheckListGroupVM();
                                objCheckListGroupVM.ApplicantCheckListID = 0;
                                objCheckListGroupVM.CheckListGroupMapID = item.ID;
                                objCheckListGroupVM.CheckListGroupName = item.ApplicantType.Name;
                                objCheckListGroupVM.CheckListID = item.CheckListID;
                                objCheckListGroupVM.CheckListName = item.CheckList.Name;
                                objCheckListGroupVM.IsCheckList = false;
                                objCheckListGroupVM.Description = item.Description == null ? string.Empty : item.Description;
                                lstCheckListGroupVM.Add(objCheckListGroupVM);


                            });
                        }
                    }
                    else
                    {
                        lstApplicantCheckList.ToList().ForEach(delegate(ApplicantCheckList item)
                        {
                            var objCheckListGroupVM = new CheckListGroupVM();
                            objCheckListGroupVM.ApplicantCheckListID = item.ID;
                            objCheckListGroupVM.CheckListGroupMapID = item.CheckListGroupMapping.ID;

                            objCheckListGroupVM.CheckListID = item.CheckListGroupMapping.CheckListID;
                            objCheckListGroupVM.CheckListName = item.CheckListGroupMapping.CheckList.Name;
                            objCheckListGroupVM.IsCheckList = item.IsCompliant;
                            objCheckListGroupVM.Description = item.Description == null ? string.Empty : item.Description;
                            lstCheckListGroupVM.Add(objCheckListGroupVM);
                        });
                    }


                }
                else
                {
                    lstCheckListGroupMapping.ToList().ForEach(delegate(CheckListGroupMapping item)
                    {
                        var objCheckListGroupVM = new CheckListGroupVM();
                        objCheckListGroupVM.ApplicantCheckListID = 0;
                        objCheckListGroupVM.CheckListGroupMapID = item.ID;
                        objCheckListGroupVM.CheckListGroupName = item.ApplicantType.Name;
                        objCheckListGroupVM.CheckListID = item.CheckListID;
                        objCheckListGroupVM.CheckListName = item.CheckList.Name;
                        objCheckListGroupVM.IsCheckList = false;
                        objCheckListGroupVM.Description = string.Empty;
                        lstCheckListGroupVM.Add(objCheckListGroupVM);
                    });
                }
            }
            return new JsonResult()
            {
                Data = lstCheckListGroupVM,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [CustomActionFilter]
        public ActionResult Create()
        {


            var objCheckListGroupMapping = _iCheckListGroupMappingService.newCheckListGroupMapping(GolbalSession.gblSession.GlobalCompanyId);
            return View(objCheckListGroupMapping);
        }
        //
        // POST: /CheckListGroupMapping/Create
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Create(List<CheckListGroupVM> lstCheckListGroupVM)
        {
            try
            {

                lstCheckListGroupVM.ForEach(delegate(CheckListGroupVM item)
                {


                    var objCheckListGroupMapping = new CheckListGroupMapping();
                    objCheckListGroupMapping.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                    objCheckListGroupMapping.SetBy = GolbalSession.gblSession.UserId;
                    objCheckListGroupMapping.SetDate = DateTime.Now;
                    objCheckListGroupMapping.ID = item.CheckListGroupMapID;
                    objCheckListGroupMapping.CheckListGroupID = item.CheckListGroupID;
                    objCheckListGroupMapping.CheckListID = item.CheckListID;
                    objCheckListGroupMapping.IsActive = item.IsCheckList;
                    if (item.CheckListGroupMapID > 0)
                    {

                        _iCheckListGroupMappingService.Update(objCheckListGroupMapping);
                        _unitOfWork.SaveChanges();
                    }
                    else
                    {
                        if (item.IsCheckList)
                        {
                            _iCheckListGroupMappingService.Insert(objCheckListGroupMapping);
                            _unitOfWork.SaveChanges();
                        }

                    }


                });
                TempData.Add("success", "Check List Group Mapping Created Successfully.");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /CheckListGroupMapping/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(int id)
        {
            var objCheckListGroupMapping = _iCheckListGroupMappingService.Find(id);
            var objCheckListGroupMappingNew = _iCheckListGroupMappingService.newCheckListGroupMapping(GolbalSession.gblSession.GlobalCompanyId);


            objCheckListGroupMapping.kvpApplicantType = objCheckListGroupMappingNew.kvpApplicantType;
            return View(objCheckListGroupMapping);

        }

        //
        // POST: /CheckListGroupMapping/Edit/5
        [HttpPost]
        [CustomActionFilter]
        public ActionResult Edit(CheckListGroupMapping objCheckListGroupMapping)
        {
            try
            {
                // TODO: Add update logic here
                objCheckListGroupMapping.GlobalCompanyId = GolbalSession.gblSession.GlobalCompanyId;
                objCheckListGroupMapping.SetBy = GolbalSession.gblSession.UserId;
                objCheckListGroupMapping.SetDate = DateTime.Now;
                _iCheckListGroupMappingService.Update(objCheckListGroupMapping);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /CheckListGroupMapping/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /CheckListGroupMapping/Delete/5
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
