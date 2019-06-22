using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User : Entity
    {
        public List<KeyValuePair<int, string>> kvpRole { get; set; }

        public List<KeyValuePair<int, string>> kvpModule { get; set; }

        [Required(ErrorMessage = "The Module Name field is required.")]
        public int ModuleId;
    }
}
