using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
   public partial class BankAccountInfoMetaData
    {
        [Display(Name = "Account Auto Id")]
        public string BankAccountAutoId { get; set; }


        [Required(ErrorMessage = "The Account No field is required.")]
        [MaxLength(50)]
        [Display(Name = "Account No")]
        public string BankAccountNo { get; set; }


        [Required(ErrorMessage = "The Account Name field is required.")]
        [MaxLength(200)]
        [Display(Name = "Account Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Account Type field is required.")]

        [Display(Name = "Account Type")]
        public int BankAccountTypeId { get; set; }


        [Required(ErrorMessage = "The Bank Name field is required.")]
        [MaxLength(200)]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "The Branch Name field is required.")]
        [MaxLength(150)]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }
        [Required(ErrorMessage = "The Account Opening Date field is required.")]        
        [Display(Name = "Account Opening Date")]
        public System.DateTime AccountOpeningDate { get; set; }

        [Required(ErrorMessage = "The Account Closing Date is required.")]        
        [Display(Name = "Account Closing Date")]
        public System.DateTime AccountClosingDate { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
      
    }
}
