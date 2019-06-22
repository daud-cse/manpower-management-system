using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
     [MetadataType(typeof(PassportInfoMetaData))]
    public partial class PassportInfo:Entity
    {
        public List<KeyValuePair<int, string>> kvpCustomer { get; set; }
        public List<KeyValuePair<int, string>> kvpPassPortType { get; set; }
        public List<KeyValuePair<int, string>> kvpRPOType { get; set; }
        public List<KeyValuePair<int, string>> kvpSex { get; set; }
        public List<KeyValuePair<int, string>> kvpMaritalStatus { get; set; }
    }
}
