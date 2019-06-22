using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class MailInfo
    {
        public int Id { get; set; }
        public int MailMasterId { get; set; }
        public string AutoRefId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MailBody { get; set; }
        public Nullable<bool> IsSent { get; set; }
        public System.DateTime SetDate { get; set; }
        public Nullable<System.DateTime> SendDate { get; set; }
        public string SetBy { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual MessageMailMaster MessageMailMaster { get; set; }
    }
}
