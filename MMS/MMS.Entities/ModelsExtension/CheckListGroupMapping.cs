using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{

    [MetadataType(typeof(CheckListGroupMappingMetaData))]
  public partial  class CheckListGroupMapping:Entity
    {

        public List<KeyValuePair<int, string>> kvpApplicantType { get; set; }
    }
}
