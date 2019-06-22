using MMS.Entities.Models;
using MMS.Entities.ViewModels;
using MMS.Service.ModelServices;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Service.ViewModelServices
{

    public interface ILocationChangeVMService
    {

        string UpdateLocationVM(int GlobalCompanyId, LocationChangeVM objLocationChangeVM, IUnitOfWork _unitOfWork, string UserId);
        LocationChangeVM newLocationChangeVM();
        LocationChangeVM GetApplicantCurrentLocation(int GlobalCompanyId, string Id, bool IsApplicant);
    }
    public class LocationChangeVMService : ILocationChangeVMService
    {
        private readonly ICountryService _iCountryService;

        private readonly IAgentService _iAgentService;
        private readonly IApplicantTypeService _iApplicantTypeService;
        private readonly IApplicantMovementService _iApplicantMovementService;
        private readonly IApplicantService _iApplicantService;
        private readonly ICheckListGroupMappingService _iCheckListGroupMappingService;
        private readonly IApplicantFileLotService _iApplicantFileLotService;
        private readonly IApplicantCheckListService _iApplicantCheckListService;
        private readonly IMovementResultService _iMovementResultService;
        private readonly ILocationMapDetailService _iLocationMapDetailService;
        private readonly IApplicantLocationDetailService _iApplicantLocationDetailService;
        private readonly ILocationService _iLocationService;
        private readonly IApplicantMovementResultHistoryService _iApplicantMovementResultHistoryService;
        public LocationChangeVMService(
            IAgentService iAgentService,
            IApplicantMovementService iApplicantMovementService,
            IApplicantService iApplicantService,
            ICheckListGroupMappingService iCheckListGroupMappingService,
            IApplicantFileLotService iApplicantFileLotService,
            IApplicantCheckListService iApplicantCheckListService,
            IMovementResultService iMovementResultService,
            ILocationMapDetailService iLocationMapDetailService,
            IApplicantLocationDetailService iApplicantLocationDetailService,
            IApplicantMovementResultHistoryService iApplicantMovementResultHistoryService,
            ILocationService iLocationService

            )
        {
            _iAgentService = iAgentService;
            _iApplicantMovementService = iApplicantMovementService;
            _iApplicantService = iApplicantService;
            _iCheckListGroupMappingService = iCheckListGroupMappingService;
            _iApplicantCheckListService = iApplicantCheckListService;
            _iApplicantFileLotService = iApplicantFileLotService;
            _iMovementResultService = iMovementResultService;
            _iLocationMapDetailService = iLocationMapDetailService;
            _iApplicantLocationDetailService = iApplicantLocationDetailService;
            _iApplicantMovementResultHistoryService = iApplicantMovementResultHistoryService;
            _iLocationService = iLocationService;
        }
        public LocationChangeVM newLocationChangeVM()
        {
            LocationChangeVM locationChangeVM = new LocationChangeVM();
            locationChangeVM.lstApplicantLocationDetail = new List<ApplicantLocationDetail>();
            locationChangeVM.lstApplicantLocationDetail = new List<ApplicantLocationDetail>();
            locationChangeVM.applicant = new Applicant();
            locationChangeVM.applicant.Location = new Location();
            locationChangeVM.applicant.Location1 = new Location();
            locationChangeVM.applicantMovement = new ApplicantMovement();
            var lstMovementResult = new List<KeyValuePair<int, string>>();
            _iMovementResultService.GetActiveMovementResult(true).ToList().ForEach(delegate(MovementResult item)
            {
                lstMovementResult.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            locationChangeVM.kvpMovementResult = lstMovementResult;
            return locationChangeVM;
        }
        public LocationChangeVM GetApplicantCurrentLocation(int GlobalCompanyId, string Id, bool IsApplicant)
        {
            int numericpass = 0;
            int numericValueAssgin = 0;
            bool isNumeric = int.TryParse(Id, out numericpass);
            if (isNumeric)
            {
                numericValueAssgin = numericpass;
            }
            Applicant applicant = new Applicant();
            if (IsApplicant)
            {
                applicant = _iApplicantService.GetApplicantsById(numericValueAssgin, GlobalCompanyId);
            }
            else
            {
                applicant = _iApplicantService.GetApplicantsByPassPortNo(Convert.ToString(Id), GlobalCompanyId);
            }

            LocationChangeVM locationChangeVM = new LocationChangeVM();
            locationChangeVM.applicantMovement = new ApplicantMovement();
            locationChangeVM.lstLocationMapDetail = new List<LocationMapDetail>();
            locationChangeVM.lstApplicantLocationDetail = new List<ApplicantLocationDetail>();
            var lstMovementResult = new List<KeyValuePair<int, string>>();
            locationChangeVM.applicant = applicant;
            if (applicant != null && applicant.IsReceivedCompleted == true)
            {

                var lstLocationMapDetail = _iLocationMapDetailService.GetLocationMapDetailByLocationId(GlobalCompanyId, locationChangeVM.applicant.CurrentLocationID).ToList();
                var applicantMovement = _iApplicantMovementService.GetApplicantMovementByapplicantAndlocationId(GlobalCompanyId, applicant.ID, applicant.CurrentLocationID);
                if (applicantMovement == null)
                {
                    applicantMovement = new ApplicantMovement();
                }
                var lstApplicantLocationDetail = _iApplicantLocationDetailService.GetAllApplicantLocationDetail(applicantMovement.ID, applicant.CurrentLocationID, applicant.ID);
                locationChangeVM.applicantMovement = applicantMovement;
                if (lstApplicantLocationDetail.ToList().Count > 0)
                {
                    locationChangeVM.lstLocationMapDetail = lstLocationMapDetail.ToList();
                    if (lstApplicantLocationDetail.ToList()[0].LocationMapDetail.ControlTypeId == 1)//DropDown
                    {
                        locationChangeVM.ControlTypeId = lstLocationMapDetail.ToList().FirstOrDefault().ControlTypeId;
                        locationChangeVM.applicantLocationDetail = new ApplicantLocationDetail();
                        locationChangeVM.applicantLocationDetail.GlobalCompanyId = GlobalCompanyId;
                        locationChangeVM.applicantLocationDetail.ApplicantID = applicant.ID;
                        locationChangeVM.applicantLocationDetail.ID = lstApplicantLocationDetail.ToList()[0].ID;
                        locationChangeVM.applicantLocationDetail.ApplicantMovementID = applicantMovement.ID;
                        locationChangeVM.applicantLocationDetail.LocationID = applicant.CurrentLocationID;
                        locationChangeVM.applicantLocationDetail.LocationMapDetailID = lstApplicantLocationDetail.ToList()[0].LocationMapDetailID;
                        

                        var kvpLocationMapDetail = new List<KeyValuePair<int, string>>();
                        locationChangeVM.lstLocationMapDetail.ForEach(delegate(LocationMapDetail item)
                        {
                            kvpLocationMapDetail.Add(new KeyValuePair<int, string>(item.ID, item.Name));
                        });
                        locationChangeVM.kvpLocationMapDetail = kvpLocationMapDetail;
                    }
                    else if (lstApplicantLocationDetail.ToList()[0].LocationMapDetail.ControlTypeId == 2)//CheckBox
                    {
                        if (lstLocationMapDetail.Count > 0)
                        {
                            locationChangeVM.ControlTypeId = lstLocationMapDetail.ToList().FirstOrDefault().ControlTypeId;
                            //locationChangeVM.lstLocationMapDetail.ToList().ForEach(delegate(LocationMapDetail item)
                            //     {

                            //         ApplicantLocationDetail objApplicantLocationDetail = new ApplicantLocationDetail();
                            //         objApplicantLocationDetail.LocationMapDetail = new LocationMapDetail();
                            //         objApplicantLocationDetail.ApplicantID = applicant.ID;
                            //         objApplicantLocationDetail.ApplicantMovementID = applicantMovement.ID;
                            //         objApplicantLocationDetail.GlobalCompanyId = GlobalCompanyId;
                            //         objApplicantLocationDetail.LocationID = applicant.CurrentLocationID;
                            //         objApplicantLocationDetail.LocationMapDetailID = item.ID;
                            //         objApplicantLocationDetail.LocationMapDetail.IsMultiSelect = item.IsMultiSelect;
                            //         objApplicantLocationDetail.LocationMapDetail.Name = item.Name;
                            //         objApplicantLocationDetail.LocationMapDetail.ID = item.ID;
                            //         objApplicantLocationDetail.LocationMapDetailName = item.Name;                                     
                            //         locationChangeVM.lstApplicantLocationDetail.Add(objApplicantLocationDetail);
                            //     });

                            locationChangeVM.lstApplicantLocationDetail = lstApplicantLocationDetail.ToList();

                        }
                    }
                    else if (lstApplicantLocationDetail.ToList()[0].LocationMapDetail.ControlTypeId == 2)//Text Box
                    {
                        locationChangeVM.ControlTypeId = lstLocationMapDetail.ToList().FirstOrDefault().ControlTypeId;
                        locationChangeVM.applicantLocationDetail = new ApplicantLocationDetail();
                        locationChangeVM.applicantLocationDetail.GlobalCompanyId = GlobalCompanyId;
                        locationChangeVM.applicantLocationDetail.ApplicantID = applicant.ID;
                        locationChangeVM.applicantLocationDetail.ID = lstApplicantLocationDetail.ToList()[0].ID;
                        locationChangeVM.applicantLocationDetail.ApplicantMovementID = applicantMovement.ID;
                        locationChangeVM.applicantLocationDetail.LocationID = applicant.CurrentLocationID;
                        locationChangeVM.applicantLocationDetail.LocationMapDetailID = lstApplicantLocationDetail.ToList()[0].LocationMapDetailID;
                    }
                }
                else
                {
                    if (lstLocationMapDetail.Count > 0)
                    {
                        locationChangeVM.ControlTypeId = lstLocationMapDetail.FirstOrDefault().ControlTypeId;
                        locationChangeVM.lstLocationMapDetail = lstLocationMapDetail.ToList();
                        if (locationChangeVM.ControlTypeId == 2)//CheckBox
                        {
                            locationChangeVM.lstLocationMapDetail.ToList().ForEach(delegate(LocationMapDetail item)
                            {
                                ApplicantLocationDetail objApplicantLocationDetail = new ApplicantLocationDetail();
                                objApplicantLocationDetail.LocationMapDetail = new LocationMapDetail();
                                objApplicantLocationDetail.ApplicantID = applicant.ID;
                                objApplicantLocationDetail.ApplicantMovementID = applicantMovement.ID;
                                objApplicantLocationDetail.GlobalCompanyId = GlobalCompanyId;
                                objApplicantLocationDetail.LocationID = applicant.CurrentLocationID;
                                objApplicantLocationDetail.LocationMapDetailID = item.ID;
                                objApplicantLocationDetail.LocationMapDetail.IsMultiSelect = item.IsMultiSelect;
                                objApplicantLocationDetail.LocationMapDetail.Name = item.Name;
                                objApplicantLocationDetail.LocationMapDetail.ID = item.ID;
                                objApplicantLocationDetail.LocationMapDetailName = item.Name;
                                locationChangeVM.lstApplicantLocationDetail.Add(objApplicantLocationDetail);
                            });
                        }

                        else if (locationChangeVM.ControlTypeId == 1)//DropDown
                        {
                            locationChangeVM.applicantLocationDetail = new ApplicantLocationDetail();
                            locationChangeVM.applicantLocationDetail.ApplicantID = applicant.ID;
                            locationChangeVM.applicantLocationDetail.ApplicantMovementID = applicantMovement.ID;
                            locationChangeVM.applicantLocationDetail.LocationID = applicant.CurrentLocationID;
                            locationChangeVM.applicantLocationDetail.GlobalCompanyId = GlobalCompanyId;

                            var kvpLocationMapDetail = new List<KeyValuePair<int, string>>();
                            locationChangeVM.lstLocationMapDetail.ForEach(delegate(LocationMapDetail item)
                            {
                                kvpLocationMapDetail.Add(new KeyValuePair<int, string>(item.ID, item.Name));
                            });
                            locationChangeVM.kvpLocationMapDetail = kvpLocationMapDetail;
                        }
                        else if (locationChangeVM.ControlTypeId == 3)//Text Box
                        {
                            locationChangeVM.applicantLocationDetail = new ApplicantLocationDetail();
                            locationChangeVM.applicantLocationDetail.ApplicantID = applicant.ID;
                            locationChangeVM.applicantLocationDetail.ApplicantMovementID = applicantMovement.ID;
                            locationChangeVM.applicantLocationDetail.LocationID = applicant.CurrentLocationID;
                        }


                    }
                }
            }

            _iMovementResultService.GetActiveMovementResult(true).ToList().ForEach(delegate(MovementResult item)
            {
                lstMovementResult.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            locationChangeVM.kvpMovementResult = lstMovementResult;
            return locationChangeVM;
        }


        public string UpdateLocationVM(int GlobalCompanyId, LocationChangeVM objLocationChangeVM, IUnitOfWork _unitOfWork, string UserId)
        {
            objLocationChangeVM.applicantMovement.SetDate = DateTime.Now;
            objLocationChangeVM.applicantMovement.ApplicantID = objLocationChangeVM.applicant.ID;
            objLocationChangeVM.applicantMovement.UserID = UserId;

            if (objLocationChangeVM.applicantMovement.IsTaskCompleted)
            {
                //Check Next Location Exist
                var isNextLocationExist = _iLocationService.GetLocationById(objLocationChangeVM.applicant.NextLocationID, GlobalCompanyId).IsNextLocationExist;
                if (isNextLocationExist == false)
                {
                    return "1";
                }
            }



            if (objLocationChangeVM.applicantMovement.IsTaskCompleted)
            {
                // Added New CHecking
                var IsCheckNextLocationExist = _iApplicantMovementService.GetApplicantMovementByapplicantAndlocationId(GlobalCompanyId, objLocationChangeVM.applicant.ID, objLocationChangeVM.applicant.NextLocationID);


                if (IsCheckNextLocationExist != null)
                {
                    return "2";
                }
                var IsCheckCurrentLocationCompleted = _iApplicantMovementService.GetApplicantMovementByapplicantAndlocationId(GlobalCompanyId, objLocationChangeVM.applicant.ID, objLocationChangeVM.applicant.CurrentLocationID);

                if (IsCheckCurrentLocationCompleted == null)
                {
                    IsCheckCurrentLocationCompleted = new ApplicantMovement();
                }
                if (IsCheckCurrentLocationCompleted.IsTaskCompleted)
                {
                    return "2";
                }

                objLocationChangeVM.applicantMovement.IsTaskCompleted = true;
                //..........................Applicant Movement Data Insert if Applicant Current Location is not found in Applicant Movement Table......///
                if (objLocationChangeVM.applicantMovement.ID == 0)
                {
                    var locationCurrent = _iLocationService.GetLocationById(objLocationChangeVM.applicant.CurrentLocationID, GlobalCompanyId);

                    objLocationChangeVM.applicantMovement.GlobalCompanyId = GlobalCompanyId;
                    objLocationChangeVM.applicantMovement.LocationID = objLocationChangeVM.applicant.CurrentLocationID;
                    objLocationChangeVM.applicantMovement.UserID = UserId;
                    objLocationChangeVM.applicantMovement.SetDate = DateTime.Now;
                    objLocationChangeVM.applicantMovement.PercentageOfComplete = locationCurrent.SetPercentage;
                    objLocationChangeVM.applicantMovement.Description = "Done";
                    _iApplicantMovementService.Insert(objLocationChangeVM.applicantMovement);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    ///----------------------------Applicant Movement data Update--------------///   
                    var applicantMovementForUpdate = IsCheckCurrentLocationCompleted;
                    applicantMovementForUpdate.ID = objLocationChangeVM.applicantMovement.ID;
                    applicantMovementForUpdate.GlobalCompanyId = GlobalCompanyId;
                    applicantMovementForUpdate.IsTaskCompleted = objLocationChangeVM.applicantMovement.IsTaskCompleted;
                    applicantMovementForUpdate.IsSucceed = objLocationChangeVM.applicantMovement.IsSucceed;
                    applicantMovementForUpdate.LocationID = objLocationChangeVM.applicantMovement.LocationID;
                    applicantMovementForUpdate.SetDate = objLocationChangeVM.applicantMovement.SetDate;
                    applicantMovementForUpdate.PercentageOfComplete = objLocationChangeVM.applicantMovement.PercentageOfComplete;
                    applicantMovementForUpdate.MovementResultID = objLocationChangeVM.applicantMovement.MovementResultID;
                    applicantMovementForUpdate.ProbableMoveDate = objLocationChangeVM.applicantMovement.ProbableMoveDate;
                    applicantMovementForUpdate.ActualMoveDate = objLocationChangeVM.applicantMovement.ActualMoveDate;
                    applicantMovementForUpdate.ActualReceiveDate = objLocationChangeVM.applicantMovement.ActualReceiveDate;
                    applicantMovementForUpdate.ApplicantID = objLocationChangeVM.applicantMovement.ApplicantID;
                    applicantMovementForUpdate.ExpectedReceiveDate = objLocationChangeVM.applicantMovement.ExpectedReceiveDate;
                    applicantMovementForUpdate.UserID = objLocationChangeVM.applicantMovement.UserID;
                    applicantMovementForUpdate.Description = objLocationChangeVM.applicantMovement.Description;

                    _iApplicantMovementService.Update(applicantMovementForUpdate);
                    _unitOfWork.SaveChanges();
                }

                ///----------------------------Applicant Movement data Insert--------------///
                var applicantMovement = new ApplicantMovement();
                var location = _iLocationService.GetLocationById(objLocationChangeVM.applicant.NextLocationID, GlobalCompanyId);

                applicantMovement.GlobalCompanyId = GlobalCompanyId;
                applicantMovement.ApplicantID = objLocationChangeVM.applicant.ID;
                applicantMovement.PercentageOfComplete = location.SetPercentage;
                applicantMovement.LocationID = objLocationChangeVM.applicant.NextLocationID;
                applicantMovement.IsTaskCompleted = false;
                applicantMovement.MovementResultID = 0;//Not Start
                applicantMovement.UserID = UserId;
                applicantMovement.SetDate = DateTime.Now;
                _iApplicantMovementService.Insert(applicantMovement);
                _unitOfWork.SaveChanges();
                ///----------------------------History data Insert--------------///
                ApplicantMovementResultHistory objApplicantMovementResultHistory = new ApplicantMovementResultHistory();
                objApplicantMovementResultHistory.GlobalCompanyId = GlobalCompanyId;
                objApplicantMovementResultHistory.MovementResultID = objLocationChangeVM.applicantMovement.MovementResultID;
                objApplicantMovementResultHistory.ApplicantMovementID = objLocationChangeVM.applicantMovement.ID;
                objApplicantMovementResultHistory.ResultDate = DateTime.Now;
                objApplicantMovementResultHistory.Remarks = objLocationChangeVM.applicantMovement.Description;
                objApplicantMovementResultHistory.SetBy = UserId;
                objApplicantMovementResultHistory.SetDate = DateTime.Now;
                _iApplicantMovementResultHistoryService.Insert(objApplicantMovementResultHistory);
                _unitOfWork.SaveChanges();
                ///----------------------------Applicant Table Update--------------///
                objLocationChangeVM.applicantMovement.IsTaskCompleted = true;
                var applicant = _iApplicantService.Find(objLocationChangeVM.applicant.ID);
                applicant.PercentageOfComplete = location.SetPercentage + applicant.PercentageOfComplete;
                applicant.CurrentLocationID = objLocationChangeVM.applicant.NextLocationID;
                applicant.NextLocationID = Convert.ToInt32(location.NextLocationID);
                _iApplicantService.Update(applicant);
                _unitOfWork.SaveChanges();

            }
            else
            {
                var IsCheckCurrentLocationCompleted = _iApplicantMovementService.GetApplicantMovementByapplicantAndlocationId(GlobalCompanyId, objLocationChangeVM.applicant.ID, objLocationChangeVM.applicant.CurrentLocationID);

                if (IsCheckCurrentLocationCompleted == null)
                {
                    IsCheckCurrentLocationCompleted = new ApplicantMovement();
                }
                if (IsCheckCurrentLocationCompleted.IsTaskCompleted)
                {
                    return "2";
                }
                ///----------------------------Applicant Movement data Update--------------///  
                if (objLocationChangeVM.applicantMovement.ID == 0)
                {
                    var locationCurrent = _iLocationService.GetLocationById(objLocationChangeVM.applicant.CurrentLocationID, GlobalCompanyId);
                    objLocationChangeVM.applicantMovement.LocationID = objLocationChangeVM.applicant.CurrentLocationID;
                    objLocationChangeVM.applicantMovement.GlobalCompanyId = GlobalCompanyId;
                    objLocationChangeVM.applicantMovement.UserID = UserId;
                    objLocationChangeVM.applicantMovement.MovementResultID = 0;//Not Start
                    objLocationChangeVM.applicantMovement.SetDate = DateTime.Now;
                    objLocationChangeVM.applicantMovement.PercentageOfComplete = locationCurrent.SetPercentage;
                    _iApplicantMovementService.Insert(objLocationChangeVM.applicantMovement);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    var applicantMovementForUpdate = IsCheckCurrentLocationCompleted;
                    applicantMovementForUpdate.ID = objLocationChangeVM.applicantMovement.ID;
                    applicantMovementForUpdate.GlobalCompanyId = GlobalCompanyId;
                    applicantMovementForUpdate.IsTaskCompleted = objLocationChangeVM.applicantMovement.IsTaskCompleted;
                    applicantMovementForUpdate.IsSucceed = objLocationChangeVM.applicantMovement.IsSucceed;
                    applicantMovementForUpdate.LocationID = objLocationChangeVM.applicantMovement.LocationID;
                    applicantMovementForUpdate.SetDate = objLocationChangeVM.applicantMovement.SetDate;
                    applicantMovementForUpdate.PercentageOfComplete = objLocationChangeVM.applicantMovement.PercentageOfComplete;
                    applicantMovementForUpdate.MovementResultID = objLocationChangeVM.applicantMovement.MovementResultID;
                    applicantMovementForUpdate.ProbableMoveDate = objLocationChangeVM.applicantMovement.ProbableMoveDate;
                    applicantMovementForUpdate.ActualMoveDate = objLocationChangeVM.applicantMovement.ActualMoveDate;
                    applicantMovementForUpdate.ActualReceiveDate = objLocationChangeVM.applicantMovement.ActualReceiveDate;
                    applicantMovementForUpdate.ApplicantID = objLocationChangeVM.applicantMovement.ApplicantID;
                    applicantMovementForUpdate.ExpectedReceiveDate = objLocationChangeVM.applicantMovement.ExpectedReceiveDate;
                    applicantMovementForUpdate.UserID = objLocationChangeVM.applicantMovement.UserID;
                    applicantMovementForUpdate.Description = objLocationChangeVM.applicantMovement.Description;
                    _iApplicantMovementService.Update(applicantMovementForUpdate);
                    _unitOfWork.SaveChanges();
                }

                ///----------------------------History data Insert--------------////////////
                ApplicantMovementResultHistory objApplicantMovementResultHistory = new ApplicantMovementResultHistory();
                objApplicantMovementResultHistory.GlobalCompanyId = GlobalCompanyId;
                objApplicantMovementResultHistory.MovementResultID = objLocationChangeVM.applicantMovement.MovementResultID;
                objApplicantMovementResultHistory.ApplicantMovementID = objLocationChangeVM.applicantMovement.ID;
                objApplicantMovementResultHistory.ResultDate = DateTime.Now;
                objApplicantMovementResultHistory.Remarks = objLocationChangeVM.applicantMovement.Description;
                objApplicantMovementResultHistory.SetBy = UserId;
                objApplicantMovementResultHistory.SetDate = DateTime.Now;
                _iApplicantMovementResultHistoryService.Insert(objApplicantMovementResultHistory);
                _unitOfWork.SaveChanges();

            }
            if (objLocationChangeVM.lstApplicantLocationDetail != null)
            {

                objLocationChangeVM.lstApplicantLocationDetail.ForEach(delegate(ApplicantLocationDetail item)
                {

                    if (item.ID > 0)
                    {
                        if (objLocationChangeVM.applicantMovement.IsTaskCompleted)
                        {
                            item.ApplicantMovementID = objLocationChangeVM.applicantMovement.ID;
                        }
                        item.SetDate = DateTime.Now;
                        item.GlobalCompanyId = GlobalCompanyId;
                        _iApplicantLocationDetailService.Update(item);
                        _unitOfWork.SaveChanges();
                    }
                    else
                    {
                        if (objLocationChangeVM.applicantMovement.IsTaskCompleted)
                        {
                            item.ApplicantMovementID = objLocationChangeVM.applicantMovement.ID;
                        }
                        item.SetDate = DateTime.Now;
                        item.GlobalCompanyId = GlobalCompanyId;
                        _iApplicantLocationDetailService.Insert(item);
                        _unitOfWork.SaveChanges();
                    }

                });
            }
            else if (objLocationChangeVM.applicantLocationDetail != null)
            {

                if (objLocationChangeVM.applicantLocationDetail.ID > 0)
                {
                    objLocationChangeVM.applicantLocationDetail.IsSucceed = true;
                    objLocationChangeVM.applicantLocationDetail.GlobalCompanyId = GlobalCompanyId;
                    objLocationChangeVM.applicantLocationDetail.SetDate = DateTime.Now;
                    _iApplicantLocationDetailService.Update(objLocationChangeVM.applicantLocationDetail);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    objLocationChangeVM.applicantLocationDetail.IsSucceed = true;
                    objLocationChangeVM.applicantLocationDetail.GlobalCompanyId = GlobalCompanyId;
                    objLocationChangeVM.applicantLocationDetail.SetDate = DateTime.Now;
                    _iApplicantLocationDetailService.Insert(objLocationChangeVM.applicantLocationDetail);
                    _unitOfWork.SaveChanges();
                }
            }

            return "3";
        }
    }
}
