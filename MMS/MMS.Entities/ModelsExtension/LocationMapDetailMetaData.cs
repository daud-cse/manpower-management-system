using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
   public partial class LocationMapDetailMetaData
    {

        [Required(ErrorMessage = "The Location Name field is required.")]
        [Display(Name = "Location Name")]
        public int LocationId { get; set; }


        [Required(ErrorMessage = "The Control Name field is required.")]
        [Display(Name = "Control Type")]
        public int ControlTypeId { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(499)]
        public string Name { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

    }
}
