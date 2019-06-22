using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    [MetadataType(typeof(RoleMetaData))]
    public partial class Role : Entity
    {
        public List<KeyValuePair<int, string>> kvpModule { get; set; }
        public List<KeyValuePair<int, string>> kvpRole { get; set; }
    }
}
