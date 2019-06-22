using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    [MetadataType(typeof(ApplicantMetaData))]
  public partial  class Applicant:Entity
    {
       
        public List<KeyValuePair<int, string>> kvpApplicantType { get; set; }
        public List<KeyValuePair<int, string>> kvpAgent { get; set; }
        public List<KeyValuePair<int, string>> kvpCountry { get; set; }
        public List<KeyValuePair<int, string>> kvpDistrict { get; set; }
        public List<KeyValuePair<int, string>> kvpDivision { get; set; }
        public List<KeyValuePair<int, string>> kvpUpazila { get; set; }
        public List<KeyValuePair<int, string>> kvpNationality { get; set; }

        public List<Applicant> lstApplicant;
        public string  ApplicantCountryName_vw;
        public bool IsAllTaskCompleted;
        public bool IsAlreadyMappedWithCompany;
    }
}
