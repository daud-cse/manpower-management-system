using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Address
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ResidentialAddress { get; set; }
        public Nullable<int> ResidentialUpazilaID { get; set; }
        public string PermanentAddress { get; set; }
        public Nullable<int> PermanentUpazilaID { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string NID { get; set; }
        public string Passport { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Nationality { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string Description { get; set; }
        public string SetBy { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public virtual Country Country { get; set; }
        public virtual Upazila Upazila { get; set; }
        public virtual Upazila Upazila1 { get; set; }
    }
}
