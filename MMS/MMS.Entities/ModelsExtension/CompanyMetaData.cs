using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    public partial class CompanyMetaData
    {

        [Required(ErrorMessage = "The Company Type field is required.")]
        [Display(Name = "Company Type")]
        public int CompanyTypeID { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

         [Display(Name = "Company Auto Id")]
        public string ComapnyId { get; set; }

        [Display(Name = "Division")]
        public Nullable<int> DivisionID { get; set; }


        [Display(Name = "District")]
        public Nullable<int> DistrictID { get; set; }

        [Required(ErrorMessage = "The Country field is required.")]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "The Nationality field is required.")]
        [Display(Name = "Nationality")]
        public int NationalityID { get; set; }

        [MaxLength(20)]
        [Display(Name = "License No")]
        public string LicenseNo { get; set; }

        [MaxLength(500), MinLength(7)]

        [Display(Name = "Description")]
        public string Description { get; set; }

        [MaxLength(200), MinLength(2)]
        [Required(ErrorMessage = "The Company Name field is required.")]
        [Display(Name = "Company Name")]
        public string Name { get; set; }
        [Display(Name = "Company Address")]
        public string Address { get; set; }
        public string Telephone { get; set; }


        [MaxLength(20), MinLength(7)]
        [Required(ErrorMessage = "The Company Mobile No field is required.")]
        [Display(Name = "Mobile No")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "The Company Email field is required.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string NID { get; set; }
        [MaxLength(100), MinLength(2)]
        [Display(Name = "Delegate Person Name")]
        public string DelegatePersonName { get; set; }


        [RegularExpression(@"^((\+91-?)|0)?[0-9]{10}$", ErrorMessage = "Entered phone format is not valid.")]
        [Display(Name = "Mobile Ban. No")]
        public string DelegateMobileNo { get; set; }

        [MaxLength(20), MinLength(8)]
        [Display(Name = "Abroad Mobile No")]
        public string DelegateAbroadMobileNo { get; set; }
        [MaxLength(1000), MinLength(1)]
        [Display(Name = "Bangladeshi Address")]
        public string DelegateBangladeshiAddress { get; set; }
        [MaxLength(1000), MinLength(1)]
        [Display(Name = "Abroad Address")]
        public string DelegateAbroadAddress { get; set; }

        [Display(Name = "Email address")]

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string DelegateEmailAddress { get; set; }
    }
}
