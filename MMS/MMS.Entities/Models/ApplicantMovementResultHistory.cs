using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class ApplicantMovementResultHistory
    {
        public int ID { get; set; }
        public Nullable<int> ApplicantMovementID { get; set; }
        public Nullable<int> MovementResultID { get; set; }
        public Nullable<System.DateTime> ResultDate { get; set; }
        public string Remarks { get; set; }
        public System.DateTime SetDate { get; set; }
        public string SetBy { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ApplicantMovement ApplicantMovement { get; set; }
        public virtual MovementResult MovementResult { get; set; }
    }
}
