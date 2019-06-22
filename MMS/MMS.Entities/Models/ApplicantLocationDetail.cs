using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class ApplicantLocationDetail
    {
        public int ID { get; set; }
        public int ApplicantMovementID { get; set; }
        public bool IsSucceed { get; set; }
        public int LocationID { get; set; }
        public int LocationMapDetailID { get; set; }
        public int ApplicantID { get; set; }
        public string SetBy { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public int GlobalCompanyId { get; set; }
        public string Description { get; set; }
        public virtual Applicant Applicant { get; set; }
        public virtual ApplicantMovement ApplicantMovement { get; set; }
        public virtual Location Location { get; set; }
        public virtual LocationMapDetail LocationMapDetail { get; set; }
    }
}
