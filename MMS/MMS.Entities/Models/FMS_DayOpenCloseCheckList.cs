using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_DayOpenCloseCheckList
    {
        public FMS_DayOpenCloseCheckList()
        {
            this.FMS_DayWiseCheckListDetails = new List<FMS_DayWiseCheckListDetails>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string ActionURL { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public string SetBy { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<FMS_DayWiseCheckListDetails> FMS_DayWiseCheckListDetails { get; set; }
    }
}
