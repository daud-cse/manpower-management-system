using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class ApplicantMovement
    {
        public ApplicantMovement()
        {
            this.ApplicantLocationDetails = new List<ApplicantLocationDetail>();
            this.ApplicantMovementResultHistories = new List<ApplicantMovementResultHistory>();
        }

        public int ID { get; set; }
        public int ApplicantID { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public Nullable<System.DateTime> ProbableMoveDate { get; set; }
        public Nullable<System.DateTime> ActualMoveDate { get; set; }
        public Nullable<System.DateTime> ExpectedReceiveDate { get; set; }
        public Nullable<System.DateTime> ActualReceiveDate { get; set; }
        public Nullable<int> MovementResultID { get; set; }
        public string MovementResultName { get; set; }
        public Nullable<decimal> PercentageOfComplete { get; set; }
        public bool IsSucceed { get; set; }
        public bool IsTaskCompleted { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public string UserID { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Applicant Applicant { get; set; }
        public virtual ICollection<ApplicantLocationDetail> ApplicantLocationDetails { get; set; }
        public virtual MovementResult MovementResult { get; set; }
        public virtual ICollection<ApplicantMovementResultHistory> ApplicantMovementResultHistories { get; set; }
        public virtual Location Location { get; set; }
    }
}
