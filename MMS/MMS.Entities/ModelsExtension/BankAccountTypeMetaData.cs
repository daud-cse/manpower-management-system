using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
   
    public partial class BankAccountTypeMetaData
    {
        [Required(ErrorMessage = "The Bank Account Type Name field is required.")]
        [MaxLength(100)]
        [Display(Name = "Bank Account Type Name")]
        public string BankAccountTypeName { get; set; }

        [Display(Name = "Bank Account Type Id")]
        public string BankAccountTypeId { get; set; }

        //[Display(Name = "Is Active")]
        //public string IsActive { get; set; }
    }
}
