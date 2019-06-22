using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
   [MetadataType(typeof(EmployeeInfoMetaData))]
   public partial class EmployeeInfo:Entity
    {
         public List<KeyValuePair<int, string>> kvpDestination { get; set; }
        public List<KeyValuePair<int, string>> kvpCountry { get; set; }
        public List<KeyValuePair<int, string>> kvpDistrict { get; set; }
        public List<KeyValuePair<int, string>> kvpDivision { get; set; }
        public List<KeyValuePair<int, string>> kvpUpazila { get; set; }
        public List<KeyValuePair<int, string>> kvpNationality { get; set; }
    }
}
