using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class ApplicantCheckList
    {
        public int ID { get; set; }
        public int ApplicantID { get; set; }
        public int CheckListGroupMapID { get; set; }
        public string Description { get; set; }
        public bool IsCompliant { get; set; }
        public string SetBy { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Applicant Applicant { get; set; }
        public virtual CheckListGroupMapping CheckListGroupMapping { get; set; }
    }
}
