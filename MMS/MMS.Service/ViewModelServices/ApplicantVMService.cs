using MMS.Entities.Models;
using MMS.Entities.StoredProcedures;
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
    public interface IApplicantVMService
    {
        ApplicantVM newApplicantVM(int GlobalCompanyId);
        bool SaveApplicantVM(ApplicantVM objApplicantVM, List<CheckListGroupVM> lstCheckListGroupVM, IUnitOfWork _unitOfWork, int GlobalCompanyId, string UserId);
        string UpdateApplicantVM(ApplicantVM objApplicantVM, List<CheckListGroupVM> lstCheckListGroupVM, IUnitOfWork _unitOfWork, int GlobalCompanyId, string UserId);
        ApplicantVM GetApplicantVMById(int applicantId, int GlobalCompanyId);
        ApplicantVM newForApplicantEdit(ApplicantVM objApplicantVM, int GlobalCompanyId);
    }
    public class ApplicantVMService : IApplicantVMService
    {
        private readonly ICountryService _iCountryService;

        private readonly IAgentService _iAgentService;
        private readonly IApplicantTypeService _iApplicantTypeService;
        private readonly ILocationService _iLocationService;
        private readonly IApplicantMovementService _iApplicantMovementService;
        private readonly IApplicantService _iApplicantService;
        private readonly ICheckListGroupMappingService _iCheckListGroupMappingService;
        private readonly IApplicantFileLotService _iApplicantFileLotService;
        private readonly IApplicantCheckListService _iApplicantCheckListService;
        private readonly IDistrictService _iDistrictService;
        private readonly IDivisionService _iDivisionService;
        private readonly INationalityService _iNationalityService;
        private readonly IUpazilaService _iUpazilaService;
        private readonly IStoredProcedures _iStoredProcedures;

        public ApplicantVMService(
            IAgentService iAgentService,
            IApplicantTypeService iApplicantTypeService,
            IApplicantMovementService iApplicantMovementService,
            IApplicantService iApplicantService,
            ICheckListGroupMappingService iCheckListGroupMappingService,
            IApplicantFileLotService iApplicantFileLotService,
            IApplicantCheckListService iApplicantCheckListService,
            ICountryService iCountryService,
            IDivisionService iDivisionService,
            IDistrictService iDistrictService,
            INationalityService iNationalityService,
            IUpazilaService iUpazilaService,
            ILocationService iLocationService,
            IStoredProcedures iStoredProcedures



            )
        {
            _iAgentService = iAgentService;
            _iApplicantTypeService = iApplicantTypeService;
            _iApplicantMovementService = iApplicantMovementService;
            _iApplicantService = iApplicantService;
            _iCheckListGroupMappingService = iCheckListGroupMappingService;
            _iApplicantFileLotService = iApplicantFileLotService;
            _iApplicantCheckListService = iApplicantCheckListService;
            _iApplicantFileLotService = iApplicantFileLotService;
            _iCountryService = iCountryService;
            _iDivisionService = iDivisionService;
            _iDistrictService = iDistrictService;
            _iNationalityService = iNationalityService;
            _iUpazilaService = iUpazilaService;
            _iLocationService = iLocationService;
            _iStoredProcedures = iStoredProcedures;


        }

        public ApplicantVM newApplicantVM(int GlobalCompanyId)
        {
            var lstAgent = new List<KeyValuePair<int, string>>();
            _iAgentService.GetActiveAgent(GlobalCompanyId, true).ToList().ForEach(delegate(Agent item)
            {
                lstAgent.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstApplicantType = new List<KeyValuePair<int, string>>();
            _iApplicantTypeService.GetActiveApplicantType(GlobalCompanyId, true).ToList().ForEach(delegate(ApplicantType item)
            {
                lstApplicantType.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstCountry = new List<KeyValuePair<int, string>>();
            _iCountryService.GetActiveCountry(GlobalCompanyId, true).ToList().ForEach(delegate(Country item)
            {
                lstCountry.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstDivision = new List<KeyValuePair<int, string>>();
            _iDivisionService.GetActiveDivision(true).ToList().ForEach(delegate(Division item)
            {
                lstDivision.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstNationality = new List<KeyValuePair<int, string>>();
            _iNationalityService.GetActiveNationality(true).ToList().ForEach(delegate(Nationality item)
            {
                lstNationality.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstDistrict = new List<KeyValuePair<int, string>>();
            //_iDistrictService.GetActiveDistrict(true).ToList().ForEach(delegate(District item)
            //{
            //    lstDistrict.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            //});
            var lstUpazila = new List<KeyValuePair<int, string>>();
            //_iUpazilaService.GetActiveUpazila(true).ToList().ForEach(delegate(Upazila item)
            //{
            //    lstUpazila.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            //});
         
            ApplicantVM objApplicantVM = new ApplicantVM();
            objApplicantVM.kvpAgent = lstAgent;
            objApplicantVM.kvpCountry = lstCountry;

            objApplicantVM.kvpDivision = lstDivision;
            objApplicantVM.kvpNationality = lstNationality;
            objApplicantVM.kvpDistrict = lstDistrict;
            objApplicantVM.kvpUpazila = lstUpazila;

            objApplicantVM.kvpApplicantType = lstApplicantType;
            objApplicantVM.applicantFileLot = new ApplicantFileLot();
            objApplicantVM.applicant = new Applicant();
           
       
          
            return objApplicantVM;

        }
        public ApplicantVM newForApplicantEdit(ApplicantVM objApplicantVMnew, int GlobalCompanyId)
        {
            //var lstAgent = new List<KeyValuePair<int, string>>();
            //_iAgentService.GetActiveAgent(GlobalCompanyId, true).ToList().ForEach(delegate(Agent item)
            //{
            //    lstAgent.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            //});
            //var lstApplicantType = new List<KeyValuePair<int, string>>();
            //_iApplicantTypeService.GetActiveApplicantType(GlobalCompanyId, true).ToList().ForEach(delegate(ApplicantType item)
            //{
            //    lstApplicantType.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            //});
            //var lstCountry = new List<KeyValuePair<int, string>>();
            //_iCountryService.GetActiveCountry(GlobalCompanyId, true).ToList().ForEach(delegate(Country item)
            //{
            //    lstCountry.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            //});
            //var lstDivision = new List<KeyValuePair<int, string>>();
            //_iDivisionService.GetActiveDivision(true).ToList().ForEach(delegate(Division item)
            //{
            //    lstDivision.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            //});
            //var lstNationality = new List<KeyValuePair<int, string>>();
            //_iNationalityService.GetActiveNationality(true).ToList().ForEach(delegate(Nationality item)
            //{
            //    lstNationality.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            //});
            ////var lstDistrict = new List<KeyValuePair<int, string>>();
            ////_iDistrictService.GetActiveDistrict(true).ToList().ForEach(delegate(District item)
            ////{
            ////    lstDistrict.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            ////});
            ////var lstUpazila = new List<KeyValuePair<int, string>>();
            ////_iUpazilaService.GetActiveUpazila(true).ToList().ForEach(delegate(Upazila item)
            ////{
            ////    lstUpazila.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            ////});
            //var lstchecklistgroupmapping = _iCheckListGroupMappingService.GetCheckListGroupMappingByGroupId(GlobalCompanyId, 1);
            //ApplicantVM objApplicantVM = new ApplicantVM();
            //objApplicantVM.kvpAgent = lstAgent;
            //objApplicantVM.kvpCountry = lstCountry;

            //objApplicantVM.kvpDivision = lstDivision;
            //objApplicantVM.kvpNationality = lstNationality;
            ////objApplicantVM.kvpDistrict = lstDistrict;
            ////objApplicantVM.kvpUpazila = lstUpazila;
            //objApplicantVM.kvpDistrict = _iDistrictService.GetDistrictByDivisionID(objApplicantVMnew.applicant.DivisionID).kvpDistrict;
            //objApplicantVM.kvpUpazila = _iUpazilaService.GetUpazilaByDistrictID(objApplicantVMnew.applicant.DistrictID).kvpUpazila;
            //objApplicantVM.kvpApplicantType = lstApplicantType;
            //objApplicantVM.applicantFileLot = new ApplicantFileLot();
            //objApplicantVM.applicant = new Applicant();
            //objApplicantVM.lstCheckListGroupMapping = new List<CheckListGroupMapping>();
            //objApplicantVM.lstCheckListGroupMapping = lstchecklistgroupmapping.ToList();
            //objApplicantVM.applicantCheckList = new List<ApplicantCheckList>();
            //objApplicantVM.lstCheckListGroupMapping.ForEach(delegate(CheckListGroupMapping item)
            //{
            //    ApplicantCheckList objApplicantCheckList = new ApplicantCheckList();

            //    // objApplicantCheckList.CheckListID = item.CheckListID;
            //    // objApplicantCheckList.CheckList = item.CheckList;
            //    objApplicantVM.applicantCheckList.Add(objApplicantCheckList);
            //});
            //objApplicantVM.applicant.CheckListGroupID = 1;
            return objApplicantVMnew;
        }
        public ApplicantVM GetApplicantVMById(int applicantId, int GlobalCompanyId)
        {
            ApplicantVM objApplicantVM = new ApplicantVM();
            objApplicantVM.applicantCheckList = new List<ApplicantCheckList>();
            var applicant = _iApplicantService.GetApplicantsById(applicantId, GlobalCompanyId);         
            objApplicantVM.applicantFileLot = applicant.ApplicantFileLot;
            objApplicantVM.applicant = applicant;
            objApplicantVM.IsReceivedCompleted = applicant.IsReceivedCompleted;

            var lstAgent = new List<KeyValuePair<int, string>>();
            _iAgentService.GetActiveAgent(GlobalCompanyId, true).ToList().ForEach(delegate(Agent item)
            {
                lstAgent.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstApplicantType = new List<KeyValuePair<int, string>>();
            _iApplicantTypeService.GetActiveApplicantType(GlobalCompanyId, true).ToList().ForEach(delegate(ApplicantType item)
            {
                lstApplicantType.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstCountry = new List<KeyValuePair<int, string>>();
            _iCountryService.GetActiveCountry(GlobalCompanyId, true).ToList().ForEach(delegate(Country item)
            {
                lstCountry.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstDivision = new List<KeyValuePair<int, string>>();
            _iDivisionService.GetActiveDivision(true).ToList().ForEach(delegate(Division item)
            {
                lstDivision.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });
            var lstNationality = new List<KeyValuePair<int, string>>();
            _iNationalityService.GetActiveNationality(true).ToList().ForEach(delegate(Nationality item)
            {
                lstNationality.Add(new KeyValuePair<int, string>(item.ID, item.Name));
            });        
    
            objApplicantVM.kvpDistrict = _iDistrictService.GetDistrictByDivisionID(objApplicantVM.applicant.DivisionID).kvpDistrict;
            objApplicantVM.kvpUpazila = _iUpazilaService.GetUpazilaByDistrictID(objApplicantVM.applicant.DistrictID).kvpUpazila;
            objApplicantVM.kvpAgent = lstAgent;
            objApplicantVM.kvpCountry = lstCountry;
            objApplicantVM.kvpDivision = lstDivision;
            objApplicantVM.kvpNationality = lstNationality;            
            objApplicantVM.kvpApplicantType = lstApplicantType;

            return objApplicantVM;
        }
        public bool SaveApplicantVM(ApplicantVM objApplicantVM, List<CheckListGroupVM> lstCheckListGroupVM, IUnitOfWork _unitOfWork, int GlobalCompanyId, string UserId)
        {
            objApplicantVM.applicantFileLot.LotStatusDate = DateTime.Now;
            objApplicantVM.applicantFileLot.GlobalCompanyId = GlobalCompanyId;
            objApplicantVM.applicantFileLot.NumberOfFiles = 1;
            _iApplicantFileLotService.Insert(objApplicantVM.applicantFileLot);
            _unitOfWork.SaveChanges();
            objApplicantVM.applicant.ApplicantId = _iStoredProcedures.GetAutoGenerateId(GlobalCompanyId,6);//6 for Applicant
            objApplicantVM.applicant.LotID = objApplicantVM.applicantFileLot.ID;
            objApplicantVM.applicant.GlobalCompanyId = GlobalCompanyId;
            objApplicantVM.applicant.AgentID = objApplicantVM.applicantFileLot.AgentID;
            objApplicantVM.applicant.SetDate = DateTime.Now;
            objApplicantVM.applicant.SetBy = UserId;
            var objlocation = _iLocationService.Find(objApplicantVM.applicant.CurrentLocationID);
            objApplicantVM.applicant.PercentageOfComplete = objlocation.SetPercentage;
            _iApplicantService.Insert(objApplicantVM.applicant);
            _unitOfWork.SaveChanges();
            //if (objApplicantVM.applicant.ID < 10)
            //{
            //    objApplicantVM.applicant.ApplicantId = "APP0" + objApplicantVM.applicant.ID;
            //}
            //else
            //{
            //    objApplicantVM.applicant.ApplicantId = "APP" + objApplicantVM.applicant.ID;
            //}
            //_iApplicantService.Update(objApplicantVM.applicant);

            if (objApplicantVM.applicant.IsReceivedCompleted)
            {
                ApplicantMovement objApplicantMovement = new ApplicantMovement();
                objApplicantMovement.ActualMoveDate = objApplicantVM.applicant.ReceivedDate;
                objApplicantMovement.GlobalCompanyId = GlobalCompanyId;
                objApplicantMovement.ApplicantID = objApplicantVM.applicant.ID;
                objApplicantMovement.ActualReceiveDate = objApplicantVM.applicant.ReceivedDate;
                objApplicantMovement.ExpectedReceiveDate = objApplicantVM.applicant.ReceivedDate;
                objApplicantMovement.ProbableMoveDate = objApplicantVM.applicant.ReceivedDate;
                objApplicantMovement.IsSucceed = true;
                objApplicantMovement.IsTaskCompleted = false;
                objApplicantMovement.PercentageOfComplete =objlocation.SetPercentage;
                objApplicantMovement.LocationID = objApplicantVM.applicant.CurrentLocationID;
                objApplicantMovement.MovementResultID = 1;//1 means result completed;
                objApplicantMovement.Description = "File Received From Applicant";
                objApplicantMovement.SetDate = DateTime.Now;
                _iApplicantMovementService.Insert(objApplicantMovement);
                _unitOfWork.SaveChanges();
            }
            lstCheckListGroupVM.ForEach(delegate(CheckListGroupVM item)
            {
                var objApplicantCheckList = new ApplicantCheckList();
                objApplicantCheckList.ApplicantID = objApplicantVM.applicant.ID;
                objApplicantCheckList.GlobalCompanyId = GlobalCompanyId;
                objApplicantCheckList.CheckListGroupMapID = item.CheckListGroupMapID;
                objApplicantCheckList.IsCompliant = item.IsCheckList;
                objApplicantCheckList.Description = item.Description;
                objApplicantCheckList.SetDate = DateTime.Now;
                objApplicantCheckList.SetBy = UserId;

                _iApplicantCheckListService.Insert(objApplicantCheckList);
                _unitOfWork.SaveChanges();
            });
            return true;
        }
        public string UpdateApplicantVM(ApplicantVM objApplicantVM, List<CheckListGroupVM> lstCheckListGroupVM, IUnitOfWork _unitOfWork, int GlobalCompanyId, string UserId)
        {
            string msg = string.Empty;
            try
            {


                objApplicantVM.applicantFileLot.LotStatusDate = DateTime.Now;
                objApplicantVM.applicantFileLot.GlobalCompanyId = GlobalCompanyId;
                objApplicantVM.applicantFileLot.NumberOfFiles = 1;
                _iApplicantFileLotService.Update(objApplicantVM.applicantFileLot);
                _unitOfWork.SaveChanges();
                objApplicantVM.applicant.LotID = objApplicantVM.applicantFileLot.ID;
                objApplicantVM.applicant.AgentID = objApplicantVM.applicantFileLot.AgentID;
                objApplicantVM.applicant.GlobalCompanyId = GlobalCompanyId;
                objApplicantVM.applicant.SetDate = DateTime.Now;               
                _iApplicantService.Update(objApplicantVM.applicant);
                _unitOfWork.SaveChanges();

                if (objApplicantVM.applicant.IsReceivedCompleted)
                {
                    if (!objApplicantVM.IsReceivedCompleted)
                    {
                        ApplicantMovement objApplicantMovement = new ApplicantMovement();
                        objApplicantMovement.ApplicantID = objApplicantVM.applicant.ID;
                        objApplicantMovement.GlobalCompanyId = GlobalCompanyId;
                        objApplicantMovement.ActualMoveDate = objApplicantVM.applicant.ReceivedDate;
                        objApplicantMovement.ActualReceiveDate = objApplicantVM.applicant.ReceivedDate;
                        objApplicantMovement.ExpectedReceiveDate = objApplicantVM.applicant.ReceivedDate;
                        objApplicantMovement.ProbableMoveDate = objApplicantVM.applicant.ReceivedDate;
                        var SetPercentage = _iLocationService.Find(objApplicantVM.applicant.CurrentLocationID).SetPercentage;
                        objApplicantMovement.PercentageOfComplete = SetPercentage;
                        objApplicantMovement.IsSucceed = true;
                        objApplicantMovement.IsTaskCompleted = false;
                        objApplicantMovement.LocationID = objApplicantVM.applicant.CurrentLocationID;
                        objApplicantMovement.MovementResultID = 1;//1 means result completed;
                        objApplicantMovement.Description = "File Received From Applicant";
                        objApplicantMovement.SetDate = DateTime.Now;
                        _iApplicantMovementService.Insert(objApplicantMovement);
                        _unitOfWork.SaveChanges();
                    }
                }
                if (lstCheckListGroupVM==null)
                {
                    lstCheckListGroupVM = new List<CheckListGroupVM>();
                }

                if (lstCheckListGroupVM.Count > 0)
                {
                    lstCheckListGroupVM.ForEach(delegate(CheckListGroupVM item)
                    {
                        var objApplicantCheckList = new ApplicantCheckList();
                        if (item.ApplicantCheckListID > 0)
                        {

                            objApplicantCheckList.ApplicantID = objApplicantVM.applicant.ID;
                            objApplicantCheckList.GlobalCompanyId = GlobalCompanyId;
                            objApplicantCheckList.CheckListGroupMapID = item.CheckListGroupMapID;
                            objApplicantCheckList.ID = item.ApplicantCheckListID;
                            objApplicantCheckList.IsCompliant = item.IsCheckList;
                            objApplicantCheckList.Description = item.Description;
                            objApplicantCheckList.SetDate = DateTime.Now;
                            objApplicantCheckList.SetBy = UserId;

                            _iApplicantCheckListService.Update(objApplicantCheckList);
                        }
                        else
                        {

                            objApplicantCheckList.ApplicantID = objApplicantVM.applicant.ID;
                            objApplicantCheckList.GlobalCompanyId = GlobalCompanyId;
                            objApplicantCheckList.CheckListGroupMapID = item.CheckListGroupMapID;
                            objApplicantCheckList.IsCompliant = item.IsCheckList;
                            objApplicantCheckList.Description = item.Description;
                            objApplicantCheckList.SetDate = DateTime.Now;
                            objApplicantCheckList.SetBy = UserId;
                            _iApplicantCheckListService.Insert(objApplicantCheckList);
                        }

                        _unitOfWork.SaveChanges();
                    });
                }
                else
                {
                    var lstApplicantCheckList = _iApplicantCheckListService.GetApplicantCheckListByApplicantId(GlobalCompanyId,objApplicantVM.applicant.ID);
                    lstApplicantCheckList.ToList().ForEach(delegate(ApplicantCheckList item)
                    {

                        _iApplicantCheckListService.Delete(item.ID);
                        _unitOfWork.SaveChanges();
                    });


                }

                return "Applicant Update Successfully";

            }
            catch(Exception ex){
                return ex.Message.ToString();

            }

           
        }
    }
}
