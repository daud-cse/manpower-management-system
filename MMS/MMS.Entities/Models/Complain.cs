using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Complain
    {
        public int ID { get; set; }
        public int ComplainTypeId { get; set; }
        public string ComplainPersonName { get; set; }
        public string ComplainPersonAddress { get; set; }
        public int AgentId { get; set; }
        public Nullable<int> ApplicantId { get; set; }
        public int ComplainStatusId { get; set; }
        public string ComplainDetails { get; set; }
        public string ComplainPersonMobileNo { get; set; }
        public string EmailAddress { get; set; }
        public System.DateTime ComplainStartDate { get; set; }
        public Nullable<System.DateTime> ExpectedEndDate { get; set; }
        public Nullable<System.DateTime> ActualEndDate { get; set; }
        public Nullable<System.DateTime> ComplainUpdateDate { get; set; }
        public string Comments { get; set; }
        public int ComplainCountryId { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedComplain { get; set; }
        public bool IsReceivedComplain { get; set; }
        public bool IsCompletedComplain { get; set; }
        public Nullable<int> DivisionId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> UpazilaId { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public string SetBy { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual Applicant Applicant { get; set; }
        public virtual ComplainType ComplainType { get; set; }
        public virtual Country Country { get; set; }
        public virtual District District { get; set; }
        public virtual Division Division { get; set; }
        public virtual Status Status { get; set; }
        public virtual Upazila Upazila { get; set; }
    }
}
