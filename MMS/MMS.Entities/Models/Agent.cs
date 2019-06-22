using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Agent
    {
        public Agent()
        {
            this.ApplicantFileLots = new List<ApplicantFileLot>();
            this.Applicants = new List<Applicant>();
            this.Complains = new List<Complain>();
        }

        public int ID { get; set; }
        public string AgentId { get; set; }
        public int AgentTypeID { get; set; }
        public string LicenseNo { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> AgentPhotoID { get; set; }
        public string Name { get; set; }
        public string ResidentialAddress { get; set; }
        public int DivisionID { get; set; }
        public int DistrictID { get; set; }
        public Nullable<int> ResidentialUpazilaID { get; set; }
        public string PermanentAddress { get; set; }
        public int PermanentUpazilaID { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string NID { get; set; }
        public string PassportNo { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int NationalityID { get; set; }
        public int CountryID { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public Nullable<decimal> DrAmount { get; set; }
        public Nullable<decimal> CrAmount { get; set; }
        public Nullable<decimal> CurrentBalance { get; set; }
        public string SetBy { get; set; }
        public System.DateTime SetDate { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual AgentType AgentType { get; set; }
        public virtual Country Country { get; set; }
        public virtual District District { get; set; }
        public virtual Division Division { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual Upazila Upazila { get; set; }
        public virtual Upazila Upazila1 { get; set; }
        public virtual ICollection<ApplicantFileLot> ApplicantFileLots { get; set; }
        public virtual ICollection<Applicant> Applicants { get; set; }
        public virtual ICollection<Complain> Complains { get; set; }
    }
}
