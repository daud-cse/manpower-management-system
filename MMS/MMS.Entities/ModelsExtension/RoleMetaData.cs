using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    public partial class RoleMetaData
    {


        [Required(ErrorMessage = "The Module Name field is required.")]
        [Display(Name = "Module Name")]
        public int ModuleId { get; set; }
        [Required(ErrorMessage = "The Role Name field is required.")]
        [MaxLength(100)]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }


    }
}
