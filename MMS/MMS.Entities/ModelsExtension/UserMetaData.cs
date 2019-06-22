using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    public partial class UserMetaData
    {

        [Required(ErrorMessage = "The User Id field is required.")]
        [MaxLength(10), MinLength(1)]
        [Display(Name = "User Id")]
        public string UserAccountsId { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [MaxLength(10), MinLength(4)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The User Name field is required.")]
        [MaxLength(100), MinLength(1)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The Role Name field is required.")]       
        [Display(Name = "Role Name")]
        public int RoleId { get; set; }
        public string UserDescription { get; set; }
        public System.DateTime SetDate { get; set; }
        public string SetBy { get; set; }

        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }
        public Nullable<int> CountryId { get; set; }

        [Required(ErrorMessage = "The Mobile No field is required.")]
        [RegularExpression(@"^((\+91-?)|0)?[0-9]{10}$", ErrorMessage = "Entered phone format is not valid.")]
        [Display(Name = "Mobile No")]
        public string PhoneNo { get; set; }
        public bool IsTermConditionAgreed { get; set; }
         [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
