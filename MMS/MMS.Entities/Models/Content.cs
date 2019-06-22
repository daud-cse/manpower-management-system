using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class Content
    {
        public Content()
        {
            this.ApplicantContents = new List<ApplicantContent>();
            this.CompanyContents = new List<CompanyContent>();
            this.ContentDetails = new List<ContentDetail>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<ApplicantContent> ApplicantContents { get; set; }
        public virtual ICollection<CompanyContent> CompanyContents { get; set; }
        public virtual ICollection<ContentDetail> ContentDetails { get; set; }
    }
}
