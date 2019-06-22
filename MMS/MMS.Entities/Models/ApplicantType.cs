using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class ApplicantType
    {
        public ApplicantType()
        {
            this.Applicants = new List<Applicant>();
            this.CheckListGroupMappings = new List<CheckListGroupMapping>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<Applicant> Applicants { get; set; }
        public virtual ICollection<CheckListGroupMapping> CheckListGroupMappings { get; set; }
    }
}
