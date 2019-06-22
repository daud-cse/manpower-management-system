using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Location
    {
        public Location()
        {
            this.Applicants = new List<Applicant>();
            this.Applicants1 = new List<Applicant>();
            this.ApplicantLocationDetails = new List<ApplicantLocationDetail>();
            this.ApplicantMovements = new List<ApplicantMovement>();
            this.LocationMapDetails = new List<LocationMapDetail>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> NextLocationID { get; set; }
        public bool IsStartingCompanyWise { get; set; }
        public bool IsNextLocationExist { get; set; }
        public int PreRequisitID { get; set; }
        public Nullable<int> SetPercentage { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsMandatory { get; set; }
        public bool IsSendGeneralMesasge { get; set; }
        public string GeneralMessageBody { get; set; }
        public bool IsSendApplicantMesasge { get; set; }
        public string ApplicantMessageBody { get; set; }
        public bool IsSendAgentMesasge { get; set; }
        public string AgentMessageBody { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<Applicant> Applicants { get; set; }
        public virtual ICollection<Applicant> Applicants1 { get; set; }
        public virtual ICollection<ApplicantLocationDetail> ApplicantLocationDetails { get; set; }
        public virtual ICollection<ApplicantMovement> ApplicantMovements { get; set; }
        public virtual ICollection<LocationMapDetail> LocationMapDetails { get; set; }
    }
}
