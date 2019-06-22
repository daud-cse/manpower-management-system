using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_Settings
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> CurrentDate { get; set; }
        public Nullable<System.DateTime> LastClosedDate { get; set; }
        public int GlobalCompanyId { get; set; }
    }
}
