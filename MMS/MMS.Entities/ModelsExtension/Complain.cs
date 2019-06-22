using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
     [MetadataType(typeof(ComplainMetaData))]
    public partial class Complain : Entity
    {
         public List<KeyValuePair<int, string>> kvpAgent { get; set; }
        public List<KeyValuePair<int, string>> kvpComplainType { get; set; }
        public List<KeyValuePair<int, string>> kvpComplainStatus { get; set; }
        public List<KeyValuePair<int, string>> kvpCountry { get; set; }
        public List<KeyValuePair<int, string>> kvpDistrict { get; set; }
        public List<KeyValuePair<int, string>> kvpDivision { get; set; }
        public List<KeyValuePair<int, string>> kvpUpazila { get; set; }
        public List<KeyValuePair<int, string>> kvpNationality { get; set; }
    }
}
