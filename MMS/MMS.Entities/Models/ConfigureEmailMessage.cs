using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class ConfigureEmailMessage
    {
        public int ConfigureId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int AreaId { get; set; }
        public int MaxEmailDaily { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public string SetBy { get; set; }
        public int GlobalCompanyId { get; set; }
    }
}
