using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    public partial class ComplainTypeMetaData
    {
        [Required(ErrorMessage = "The Complain Type Name field is required.")]
        [Display(Name = "Complain Type Name")]
        public string Name { get; set; }
    }
}
