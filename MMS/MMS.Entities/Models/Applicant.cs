using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Applicant
    {
        public Applicant()
        {
            this.ApplicantContents = new List<ApplicantContent>();
            this.ApplicantLocationDetails = new List<ApplicantLocationDetail>();
            this.CompanyWiseApplicants = new List<CompanyWiseApplicant>();
            this.Complains = new List<Complain>();
            this.ApplicantCheckLists = new List<ApplicantCheckList>();
            this.ApplicantMovements = new List<ApplicantMovement>();
        }

        public int ID { get; set; }
        public string ApplicantId { get; set; }

        public Nullable<int> ApplicantPhotoID { get; set; }
        public string PreRefApplicantId { get; set; }
        public Nullable<int> LotID { get; set; }
        public int AgentID { get; set; }
        public Nullable<int> AddressID { get; set; }
        public string PassportNo { get; set; }
        public Nullable<System.DateTime> PassportIssueDate { get; set; }
        public Nullable<System.DateTime> PassportExpiryDate { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string Name { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string ResidentialAddress { get; set; }
        public int DivisionID { get; set; }
        public int DistrictID { get; set; }
        public Nullable<int> ResidentialUpazilaID { get; set; }
        public string PermanentAddress { get; set; }
        public int PermanentUpazilaID { get; set; }
        public string NID { get; set; }
        public int CountryID { get; set; }
        public int ApplicantTypeID { get; set; }
        public string ApplicantPhoneNo { get; set; }
        public string RefName { get; set; }
        public string RefPhoneNo { get; set; }
        public string VisaNo { get; set; }
        public Nullable<int> VisaTypeID { get; set; }
        public Nullable<bool> IsMultipleEntry { get; set; }
        public Nullable<int> CheckListGroupID { get; set; }
        public Nullable<bool> IsCheckListCompliant { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public bool IsReceivedCompleted { get; set; }
        public System.DateTime ReceivedDate { get; set; }
        public int CurrentLocationID { get; set; }
        public string CurrentLocationName { get; set; }
        public Nullable<System.DateTime> CurrentLocationSentDate { get; set; }
        public int NextLocationID { get; set; }
        public Nullable<decimal> PercentageOfComplete { get; set; }
        public string Comments1 { get; set; }
        public string Description { get; set; }
        public string Comments2 { get; set; }
        public string Comments3 { get; set; }
        public string Comments4 { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public Nullable<decimal> DrAmount { get; set; }
        public Nullable<decimal> CrAmount { get; set; }
        public Nullable<decimal> CurrentBalance { get; set; }
        public string SetBy { get; set; }
        public System.DateTime SetDate { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual ApplicantType ApplicantType { get; set; }
        public virtual Country Country { get; set; }
        public virtual Location Location { get; set; }
        public virtual Location Location1 { get; set; }
        public virtual ICollection<ApplicantContent> ApplicantContents { get; set; }
        public virtual ICollection<ApplicantLocationDetail> ApplicantLocationDetails { get; set; }
        public virtual ICollection<CompanyWiseApplicant> CompanyWiseApplicants { get; set; }
        public virtual ICollection<Complain> Complains { get; set; }
        public virtual ICollection<ApplicantCheckList> ApplicantCheckLists { get; set; }
        public virtual ApplicantFileLot ApplicantFileLot { get; set; }
        public virtual CheckListGroup CheckListGroup { get; set; }
        public virtual ICollection<ApplicantMovement> ApplicantMovements { get; set; }
    }
}
