using MMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.ViewModels
{
   public class FMS_DayOpenCloseVM
    {

      public FMS_DayOpenClose DayOpenClose { get; set; }
      public List<FMS_DayWiseCheckListDetails> lstDayWiseCheckListDetails { get; set; }

      public FMS_Settings Settings { get; set; }
    public   bool IsDayClose { get; set; }

    }
}
