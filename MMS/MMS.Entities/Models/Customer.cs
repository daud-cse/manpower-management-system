using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Customer
    {
        public Customer()
        {
            this.PassportInfoes = new List<PassportInfo>();
        }

        public int ID { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public int CustomerTypeId { get; set; }
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
        public Nullable<int> ContentIdForImage { get; set; }
        public string PassportNo { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int NationalityID { get; set; }
        public int CountryID { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public Nullable<decimal> DrAmount { get; set; }
        public Nullable<decimal> CrAmount { get; set; }
        public Nullable<decimal> CurrentBalance { get; set; }
        public System.DateTime SetDate { get; set; }
        public string SetBy { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Country Country { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        public virtual District District { get; set; }
        public virtual Division Division { get; set; }
        public virtual Upazila Upazila { get; set; }
        public virtual Upazila Upazila1 { get; set; }
        public virtual ICollection<PassportInfo> PassportInfoes { get; set; }
    }
}
