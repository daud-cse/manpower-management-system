using MMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.ViewModels
{
   public  class LocationChangeVM
    {
       public Applicant applicant { get; set; }

       public List<KeyValuePair<int, string>> kvpMovementResult { get; set; }

       public List<LocationMapDetail> lstLocationMapDetail { get; set; }
       public List<KeyValuePair<int, string>> kvpLocationMapDetail { get; set; }
       public List<ApplicantLocationDetail> lstApplicantLocationDetail { get; set; }
       public ApplicantLocationDetail applicantLocationDetail { get; set; }
       public List<KeyValuePair<int, string>> kvpApplicantLocationDetail { get; set; }
       public ApplicantMovement applicantMovement { get; set; }

       public bool IsMultiSelect;

       public int ControlTypeId;

       
    }
}
