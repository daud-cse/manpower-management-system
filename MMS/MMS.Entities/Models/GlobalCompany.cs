using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class GlobalCompany
    {
        public GlobalCompany()
        {
            this.Users = new List<User>();
        }

        public int GlobalCompanyId { get; set; }
        public string GlobalCompanyName { get; set; }

        public string ShortName { get; set; }
        public int PackageId { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public Nullable<decimal> latitude { get; set; }
        public Nullable<decimal> longitude { get; set; }
        public string SeoText { get; set; }
        public string WelComeText { get; set; }
        public string ContactText { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string GoogleUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public string Description { get; set; }
        public string UsefulLinkText { get; set; }
        public Nullable<int> GlobalCountryId { get; set; }
        public Nullable<int> GlobalDivisionId { get; set; }
        public Nullable<int> GlobalDistrictId { get; set; }
        public Nullable<int> GlobalSubDistrictId { get; set; }
        public Nullable<int> VisitorToday { get; set; }
        public Nullable<int> VisitorThisMonth { get; set; }
        public Nullable<int> VisitorTotal { get; set; }
        public Nullable<int> GlobalInstituteTypeId { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public Nullable<bool> IsSMSCostPayByReceipient { get; set; }
        public Nullable<bool> IsAuthenticationSMSCostPayByReceipient { get; set; }
        public Nullable<int> RestSMSCount { get; set; }
        public string MenuHtml { get; set; }
        public string CssOverwrite { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
