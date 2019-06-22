using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    public partial class PassportInfoMetaData
    {
        [Required(ErrorMessage = "The Delivery Slip No field is required.")]
        [MaxLength(14), MinLength(12)]
        [Display(Name = "Delivery Slip No")]
        public string DeliverySlipNo { get; set; }

     
         [Display(Name = "Enrolement Date")]
        public Nullable<System.DateTime> EnrolementDate { get; set; }

        [Display(Name = "Tentative Collection Date")]
        public Nullable<System.DateTime> TentativeCollectionDate { get; set; }

         [Required(ErrorMessage = "The RPO Name field is required.")]
        [Display(Name = "RPO Name")]
        public int RPOTypeId { get; set; }

        [Display(Name = "Passport Holder Name")]
        [MaxLength(200), MinLength(2)]
        public string OwnerName { get; set; }

        [Display(Name = "Marital Status")]
        public int MaritalStatusId { get; set; }

         [Display(Name = "Passport Type Name")]
        public int PassPortTypeId { get; set; }

        [Required(ErrorMessage = "The Mobile No field is required.")]
        [RegularExpression(@"^((\+91-?)|0)?[0-9]{10}$", ErrorMessage = "Entered phone format is not valid.")]
        [Display(Name = "Pass. Holder Mob. No")]
        public string OwerMobileNo { get; set; }

        [Display(Name = "Sex")]
        [Required(ErrorMessage = "The Sex field is required.")]
        public int SexTypeId { get; set; }

        [Required(ErrorMessage = "The Customer Name field is required.")]
        [Display(Name = "Customer Name")]
        public int CustomerId { get; set; }

        [MaxLength(9), MinLength(7)]
         [Display(Name = "New Passport No")]
        public string NewPassportNo { get; set; }

        [MaxLength(9), MinLength(7)]

        [Display(Name = "Old Passport No")]
        public string PreviousPassportNo { get; set; }

        [Display(Name = "Passport Scan Copy")]
        public Nullable<int> ContentIdForScan { get; set; }

         [Display(Name = "Is Received Completed")]
        public bool IsReceivedCompleted { get; set; }
        [Display(Name = "Is Passport Delivery Completed")]
        public bool IsDeliveryDone { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
