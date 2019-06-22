using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class ApplicantFileLot
    {
        public ApplicantFileLot()
        {
            this.Applicants = new List<Applicant>();
        }

        public int ID { get; set; }
        public int AgentID { get; set; }
        public Nullable<int> NumberOfFiles { get; set; }
        public Nullable<int> FileLotStatusID { get; set; }
        public Nullable<System.DateTime> LotStatusDate { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual ICollection<Applicant> Applicants { get; set; }
        public virtual ApplicantLotStatu ApplicantLotStatu { get; set; }
    }
}
