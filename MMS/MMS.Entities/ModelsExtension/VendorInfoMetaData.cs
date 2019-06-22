using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
  public partial  class VendorInfoMetaData
    {
       [Display(Name = "Vendor Auto Id")]
        public string VendorAutoId { get; set; }


        [Required(ErrorMessage = "The Vendor Name field is required.")]
        [MaxLength(200)]
        [Display(Name = "Vendor Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "The Vendor Address field is required.")]
        [MaxLength(500)]
        [Display(Name = "Vendor Address")]
        public string VendorAddress { get; set; }


        [Required(ErrorMessage = "The Vendor Type field is required.")]
        [Display(Name = "Vendor Type")]
        public int VendorTypeId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
      
    }
}
