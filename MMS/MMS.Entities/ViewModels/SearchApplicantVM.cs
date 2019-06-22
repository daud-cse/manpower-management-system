using MMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.ViewModels
{
    public class SearchApplicantVM
    {
        public IList<Applicant> iapplicantlist;
        public List<KeyValuePair<int, string>> kvpAgent { get; set; }
        public int GlobalCompanyId { get; set; }

        //[Required(ErrorMessage = "The Agent Name field is required.")]       
        [Display(Name = "Agent Name")]
        public int? AgentId { get; set; }

        [Display(Name = "App. Id")]
        public int? ApplicantId { get; set; }
        [Display(Name = "Pass. No")]
        public string PassportNo { get; set; }

         [Display(Name = "Auto App. Id")]
        public string AutoApplicantId { get; set; }

         [Display(Name = "Ref. Applicant Id")]
        public string PreRefApplicantId { get; set; }

        [Display(Name = "Applicant Name")]
        public string ApplicantName { get; set; }
        public List<KeyValuePair<int, string>> kvpApplicantType { get; set; }

           [Display(Name = "Applicant Type")]
        public int? ApplicantTypeId { get; set; }
        public List<KeyValuePair<int, string>> kvpCountry { get; set; }

            [Display(Name = "Going Country")]
        public int? CountryId { get; set; }
        public List<KeyValuePair<int, string>> kvpDistrict { get; set; }
                [Display(Name = "District")]
        public int? DistrictId { get; set; }
        public List<KeyValuePair<int, string>> kvpDivision { get; set; }
        [Display(Name = "Division")]
        public int? DivisionId { get; set; }
        public List<KeyValuePair<int, string>> kvpUpazila { get; set; }
        [Display(Name = "Upazila")]
        public int? UpazilaId { get; set; }
        public List<KeyValuePair<int, string>> kvpNationality { get; set; }
        [Display(Name = "Activity")]
        public int? LocationId { get; set; }
        
        public List<KeyValuePair<int, string>> kvpLocation { get; set; }
        [Display(Name = "Nationality")]
        public int? NationalityId { get; set; }

        public List<Applicant> lstApplicant;
    }
}
