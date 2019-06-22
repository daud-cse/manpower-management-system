using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
   public partial class ApplicantFileLotMetaData
    {

       [Required(ErrorMessage = "The Agent Name field is required.")]
       [Display(Name = "Agent Name")]
       public int AgentID { get; set; }
    }
}
