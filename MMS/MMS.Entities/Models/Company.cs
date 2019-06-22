using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Company
    {
        public Company()
        {
            this.CompanyContents = new List<CompanyContent>();
            this.CompanyWiseApplicants = new List<CompanyWiseApplicant>();
        }

        public int ID { get; set; }
        public string ComapnyId { get; set; }
        public int CompanyTypeID { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> DivisionID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public int CountryID { get; set; }
        public int NationalityID { get; set; }
        public string LicenseNo { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string NID { get; set; }
        public string DelegatePersonName { get; set; }
        public string DelegateMobileNo { get; set; }
        public string DelegateAbroadMobileNo { get; set; }
        public string DelegateBangladeshiAddress { get; set; }
        public string DelegateAbroadAddress { get; set; }
        public string DelegateEmailAddress { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public Nullable<decimal> DrAmount { get; set; }
        public Nullable<decimal> CrAmount { get; set; }
        public Nullable<decimal> CurrentBalance { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public string SetBy { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual CompanyType CompanyType { get; set; }
        public virtual Country Country { get; set; }
        public virtual District District { get; set; }
        public virtual Division Division { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual ICollection<CompanyContent> CompanyContents { get; set; }
        public virtual ICollection<CompanyWiseApplicant> CompanyWiseApplicants { get; set; }
    }
}
