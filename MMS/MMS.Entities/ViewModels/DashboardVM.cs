using MMS.Entities.UserDefineModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.ViewModels
{
   public class DashboardVM
    {

      public List<ApplicantLocationTotal> lstApplicantLocationTotal { get; set; }
      public List<ApplicantTypeTotal> lstApplicantTypeTotal { get; set; }
    }
}
