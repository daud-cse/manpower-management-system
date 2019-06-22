using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class EmployeeInfo
    {
        public int ID { get; set; }
        public string EmployeeAutoId { get; set; }
        public string PIN { get; set; }
        public string Name { get; set; }
        public int DesignationId { get; set; }
        public string MobileNo { get; set; }
        public string NID { get; set; }
        public string Email { get; set; }
        public string PassportNo { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public decimal Salary { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public Nullable<decimal> DrAmount { get; set; }
        public Nullable<decimal> CrAmount { get; set; }
        public Nullable<decimal> CurrentBalance { get; set; }
        public string ResidentialAddress { get; set; }
        public int DivisionID { get; set; }
        public int DistrictID { get; set; }
        public Nullable<int> ResidentialUpazilaID { get; set; }
        public string PermanentAddress { get; set; }
        public int PermanentUpazilaID { get; set; }
        public bool IsActive { get; set; }
        public int NationalityID { get; set; }
        public int CountryID { get; set; }
        public string SetBy { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Country Country { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual District District { get; set; }
        public virtual Division Division { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual Upazila Upazila { get; set; }
        public virtual Upazila Upazila1 { get; set; }
    }
}
