using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    [MetadataType(typeof(FMS_VoucherTypeWiseOppositeGLMapMetaData))]
    public partial class FMS_VoucherTypeWiseOppositeGLMap : Entity
    {

        public List<KeyValuePair<int, string>> kvpVoucherType { get; set; }
        public List<KeyValuePair<int, string>> kvpGLAccount { get; set; }
      
    }
}
