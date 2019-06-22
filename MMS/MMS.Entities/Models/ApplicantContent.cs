using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class ApplicantContent
    {
        public int ID { get; set; }
        public Nullable<int> ApplicantID { get; set; }
        public Nullable<int> ContentID { get; set; }
        public Nullable<int> CheckListID { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Applicant Applicant { get; set; }
        public virtual Content Content { get; set; }
    }
}
