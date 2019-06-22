using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{

    public partial class FMS_TransactionDetMetaData
    {
        [Required(ErrorMessage = "The GL Account Name is required.")]
        [Display(Name = "GL Account")]
        public string GLAccountId { get; set; }
    }
}
