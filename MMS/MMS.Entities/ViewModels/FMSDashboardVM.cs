using MMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.ViewModels
{
   public class FMSDashboardVM
    {
      public List<FMS_DayOpenClose> lstFMSDayOpenClose { get; set; }
      public List<FMS_CurrentLedgerSummary> lstFMSCurrentLedgerSummary { get; set; }
    }
}
