using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
   public partial class AgentTypeMetaData
    {
        [Required(ErrorMessage = "The Agent Type Name field is required.")]
        [MaxLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Is Active")]
        public string IsActive { get; set; }
    }
}
