using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
   public partial class ApplicantMovementMetaData
    {
        // [Required(ErrorMessage = "The Agent Name field is required.")]
        //[MaxLength(500)]
        [Display(Name = "Expected Start Date")]

     
        public Nullable<System.DateTime> ProbableMoveDate { get; set; }
        [Display(Name = "Expected End Date")]

        public Nullable<System.DateTime> ExpectedReceiveDate { get; set; }

        [Display(Name = "Actual Start Date")]
       
         public Nullable<System.DateTime> ActualMoveDate { get; set; }

        [Display(Name = "Actual End Date")]
         public Nullable<System.DateTime> ActualReceiveDate { get; set; }

        [Display(Name = "Activity Status")]
         public Nullable<int> MovementResultID { get; set; }
        [Display(Name = "Is Succeed")]
        public bool IsSucceed { get; set; }
        [Display(Name = "Is All Task Completed ")]
        public bool IsTaskCompleted { get; set; }

        [MaxLength(500)]
        //[Display(Name = "Particulars")]
        public string Description { get; set; }
    }
}
