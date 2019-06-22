using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    public partial class ApplicantMetaData
    {
        [Display(Name = "Applicant Id")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        public int ID { get; set; }

        [Display(Name = "Applicant Auto Id")]
        public string ApplicantId { get; set; }


        [Display(Name = "Ref. Applicant Id")]
        [MaxLength(12), MinLength(3)]
        public string PreRefApplicantId { get; set; }

        [Required(ErrorMessage = "The Passport No field is required.")]
        [MaxLength(9), MinLength(7)]
        [Display(Name = "Passport No")]
        public string PassportNo { get; set; }

        [Display(Name = "Passport Issue Date")]
        public Nullable<System.DateTime> PassportIssueDate { get; set; }
        [Display(Name = "Passport Expiry Date")]
        public Nullable<System.DateTime> PassportExpiryDate { get; set; }

        [Required(ErrorMessage = "The Father Name is required.")]
        [Display(Name = "Fathers Name")]
        [MaxLength(100)]
        public string FathersName { get; set; }
         [Required(ErrorMessage = "The Mother Name is required.")]
        [Display(Name = "Mothers Name")]
        [MaxLength(100)]
        public string MothersName { get; set; }


        [Required(ErrorMessage = "The Appicant Name field is required.")]
        [MaxLength(200), MinLength(2)]
        [Display(Name = "Appicant Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Date Of Birth field is required.")]
        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "The Residential Address field is required.")]
        [Display(Name = "Residential Address")]
        public string ResidentialAddress { get; set; }

        [Display(Name = "Division")]
        public Nullable<int> DivisionID { get; set; }

        [Required(ErrorMessage = "The District field is required.")]
        [Display(Name = "District")]
        public int DistrictID { get; set; }

        [Display(Name = "Residential Upazila")]
        public Nullable<int> ResidentialUpazilaID { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "The Permanent Address is required.")]
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }
        [Display(Name = "Permanent Upazila")]
        public Nullable<int> PermanentUpazilaID { get; set; }

        [MaxLength(17)]
        public string NID { get; set; }
        // [Required(ErrorMessage = "The Country field is required.")]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "The Applicant Type field is required.")]
        [Display(Name = "Applicant Type")]
        public int ApplicantTypeID { get; set; }

        [Required(ErrorMessage = "The Agent Name field is required.")]
        [Display(Name = "Agent Name")]
        public int AgentID { get; set; }

        [Display(Name = "Applicant Phone No")]
        [Required(ErrorMessage = "The Applicant Phone No field is required.")]
        [RegularExpression(@"^((\+91-?)|0)?[0-9]{10}$", ErrorMessage = "Entered phone format is not valid.")]
        public string ApplicantPhoneNo { get; set; }

        [Display(Name = "Ref. Name")]
        [MaxLength(100)]
        public string RefName { get; set; }

        [Display(Name = "Ref. Phone No")]

        [RegularExpression(@"^((\+91-?)|0)?[0-9]{10}$", ErrorMessage = "Entered phone format is not valid.")]
        public string RefPhoneNo { get; set; }

        [Display(Name = "Visa No")]
        [MaxLength(50)]
        public string VisaNo { get; set; }
        public Nullable<int> VisaTypeID { get; set; }
        public Nullable<bool> IsMultipleEntry { get; set; }
        public Nullable<int> CheckListGroupID { get; set; }
        public Nullable<bool> IsCheckListCompliant { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }

        [Required(ErrorMessage = "The Received Date field is required.")]
        [Display(Name = "Received Date")]
        public System.DateTime ReceivedDate { get; set; }

        [Display(Name = "Current Activity")]
        public int CurrentLocationID { get; set; }
        public string CurrentLocationName { get; set; }

        [Display(Name = "Next Activity")]
        public int NextLocationID { get; set; }
        public Nullable<System.DateTime> CurrentLocationSentDate { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(500)]

        public string Comments1 { get; set; }
        [MaxLength(500)]
        public string Comments2 { get; set; }
        [MaxLength(500)]
        public string Comments3 { get; set; }
        [MaxLength(500)]
        public string Comments4 { get; set; }

        [Display(Name = "Is Received Completed")]
        public bool IsReceivedCompleted { get; set; }
          

    }
}
