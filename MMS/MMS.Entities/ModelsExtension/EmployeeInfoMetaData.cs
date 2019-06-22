using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
   public partial class EmployeeInfoMetaData
    {

        [Required(ErrorMessage = "The Employee Name field is required.")]
        [MaxLength(200)]
        [Display(Name = "Employee Name")]
        public string Name { get; set; }
        [Display(Name = "Employee Auto Id")]
        public string EmployeeAutoId { get; set; }
        [Required(ErrorMessage = "The Designation field is required.")]
        [Display(Name = "Designation")]
        public int DesignationId { get; set; }
        [Display(Name = "Date Of Birth")]
        public System.DateTime DateOfBirth { get; set; }
        [MaxLength(200)]
        [Display(Name = "Residential Address")]
        public string ResidentialAddress { get; set; }
        [Required(ErrorMessage = "The Division field is required.")]
        [Display(Name = "Division")]
        public int DivisionID { get; set; }
        [Required(ErrorMessage = "The District field is required.")]
        [Display(Name = "District")]
        public int DistrictID { get; set; }
        //[Required(ErrorMessage = "The Residential Upazila field is required.")]
        [Display(Name = "Residential Upazila")]
        public int ResidentialUpazilaID { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "The Permanent Address field is required.")]
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "The Permanent Upazila field is required.")]
        [Display(Name = "Permanent Upazila")]
        public int PermanentUpazilaID { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "The Father Name field is required.")]

        [Display(Name = "Father Name")]
        public string FathersName { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "The Mother Name field is required.")]
        [Display(Name = "Mother Name")]
        public string MothersName { get; set; }


        [Required(ErrorMessage = "The Salary Name field is required.")]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "The Nationality field is required.")]
        [Display(Name = "Nationality")]
        public int NationalityID { get; set; }

        [Required(ErrorMessage = "The Country field is required.")]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "The Mobile No field is required.")]
        // [RegularExpression(@"^([0]|\+91[\-\s]?)?[789]\d{9}$", ErrorMessage = "Entered Mobile No is not valid.")]
        [RegularExpression(@"^((\+91-?)|0)?[0-9]{10}$", ErrorMessage = "Entered phone format is not valid.")]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [Display(Name = "NID No")]
        [MaxLength(15)]
        public string NID { get; set; }
        [Display(Name = "Passport No")]
        [MaxLength(9), MinLength(7)]
        public string PassportNo { get; set; }

        [Display(Name = "Email address")]
        // [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
