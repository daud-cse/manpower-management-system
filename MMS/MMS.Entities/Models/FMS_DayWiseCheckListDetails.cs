using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_DayWiseCheckListDetails
    {
        public int ID { get; set; }
        public System.DateTime OpenDate { get; set; }
        public int DayOpenCloseCheckListId { get; set; }
        public bool IsCheckListDone { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public string SetBy { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual FMS_DayOpenCloseCheckList FMS_DayOpenCloseCheckList { get; set; }
    }
}
