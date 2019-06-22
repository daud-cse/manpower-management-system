using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class MovementResult
    {
        public MovementResult()
        {
            this.ApplicantMovements = new List<ApplicantMovement>();
            this.ApplicantMovementResultHistories = new List<ApplicantMovementResultHistory>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<ApplicantMovement> ApplicantMovements { get; set; }
        public virtual ICollection<ApplicantMovementResultHistory> ApplicantMovementResultHistories { get; set; }
    }
}
