using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    public partial class RoleRightss : Entity
    {
        public List<KeyValuePair<int, string>> kvpModule { get; set; }
        public List<KeyValuePair<int, string>> kvpRole { get; set; }
        public List<Rightss> rights;
    }
}
