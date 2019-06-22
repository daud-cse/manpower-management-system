using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    public partial class ComplainMetaData
    {

        [Required(ErrorMessage = "The Complain Type field is required.")]
        [Display(Name = "Complain Type")]
        public int ComplainTypeId { get; set; }
        [Required(ErrorMessage = "The Complain Person Name field is required.")]
        [Display(Name = "Complain Person Name")]
        public string ComplainPersonName { get; set; }

           [Display(Name = "Address")]
        public string ComplainPersonAddress { get; set; }

        [Required(ErrorMessage = "The Complain Status field is required.")]
        [Display(Name = "Complain Status")]
        public int ComplainStatusId { get; set; }
        
        [Required(ErrorMessage = "The Agent Id field is required.")]
        [Display(Name = "Agent Id")]
        public Nullable<int> AgentId { get; set; }

        [Display(Name = "Applicant")]
        public Nullable<int> ApplicantId { get; set; }

        [Display(Name = "Expected End Date")]
        public Nullable<System.DateTime> ExpectedEndDate { get; set; }
        [Display(Name = "Actual End Date")]
        public string ActualEndDate { get; set; }

        [Required(ErrorMessage = "The Complain Details field is required.")]
        [Display(Name = "Complain Details")]
        public string ComplainDetails { get; set; }

        [Required(ErrorMessage = "The Mobile No field is required.")]
        [RegularExpression(@"^((\+91-?)|0)?[0-9]{10}$", ErrorMessage = "Entered Mobile format is not valid.")]
        [Display(Name = "Mobile No")]
        public string ComplainPersonMobileNo { get; set; }

        
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "The Complain Date field is required.")]
        [Display(Name = "Complain Date")]
        public System.DateTime ComplainStartDate { get; set; }
        public Nullable<System.DateTime> ComplainUpdateDate { get; set; }
        public string Comments { get; set; }

        [Required(ErrorMessage = "The Complain Country field is required.")]
        [Display(Name = "Complain Country")]
        public int ComplainCountryId { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public bool IsApprovedComplain { get; set; }
        [Display(Name = "Is Received Complain")]
        public bool IsReceivedComplain { get; set; }
        [Display(Name = "Is Completed Complain")]
        public bool IsCompletedComplain { get; set; }
        [Display(Name = "Division")]
        public Nullable<int> DivisionId { get; set; }
        [Display(Name = "District")]
        public Nullable<int> DistrictId { get; set; }
        [Display(Name = "Upazila")]
        public Nullable<int> UpazilaId { get; set; }
    }
}
