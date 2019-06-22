using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
  public partial  class FMS_SubsidyAccount:Entity
    {
      public List<KeyValuePair<int, string>> kvpSubsidyType { get; set; }
        public List<KeyValuePair<string, string>> kvpGLAccount { get; set; }
      
    }
}
