using MMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.ViewModels
{
   public class ApplicantVM
    {
       public ApplicantFileLot applicantFileLot { get; set; }
      
       public ApplicantCheckList applicantCheck { get; set; }
       public List<ApplicantCheckList> applicantCheckList { get; set; }
       public List<CheckListGroupMapping> lstCheckListGroupMapping { get; set; }
       public List<KeyValuePair<int, string>> kvpAgent { get; set; }
       public List<KeyValuePair<int, string>> kvpApplicantType { get; set; }
       public List<KeyValuePair<int, string>> kvpCountry { get; set; }
       public List<KeyValuePair<int, string>> kvpDistrict { get; set; }
       public List<KeyValuePair<int, string>> kvpDivision { get; set; }
       public List<KeyValuePair<int, string>> kvpUpazila { get; set; }
       public List<KeyValuePair<int, string>> kvpNationality { get; set; }

       public Applicant applicant { get; set; }
       public bool IsReceivedCompleted { get; set; }
    }
}
