using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class CompanyWiseApplicant
    {
        public int ID { get; set; }
        public int CompanyId { get; set; }
        public int ApplicantId { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public System.DateTime SetDate { get; set; }
        public string SetBy { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Applicant Applicant { get; set; }
        public virtual Company Company { get; set; }
    }
}
