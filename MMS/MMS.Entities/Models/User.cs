using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class User
    {
        public User()
        {
            this.UserModules = new List<UserModule>();
        }

        public int ID { get; set; }
        public string UserAccountsId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string UserDescription { get; set; }
        public System.DateTime SetDate { get; set; }
        public string SetBy { get; set; }
        public string EmailId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string PhoneNo { get; set; }
        public bool IsTermConditionAgreed { get; set; }
        public bool IsActive { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual GlobalCompany GlobalCompany { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<UserModule> UserModules { get; set; }
    }
}
