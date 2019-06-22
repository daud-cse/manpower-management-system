using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    public partial class LocationMetaData
    {
        [Display(Name = "Is Send General Mesasge")]
        public bool IsSendGeneralMesasge { get; set; }

        [MaxLength(159), MinLength(3)]
        [Display(Name = "General Message Body")]
        public string GeneralMessageBody { get; set; }

        [Display(Name = "Is Send Applicant Mesasge")]
        public bool IsSendApplicantMesasge { get; set; }

        [Display(Name = "Applicant Message Body")]
        [MaxLength(159), MinLength(3)]
        public string ApplicantMessageBody { get; set; }

        [Display(Name = "Is Send Agent Mesasge")]
        public bool IsSendAgentMesasge { get; set; }

        [Display(Name = "Agent Message Body")]
        [MaxLength(159), MinLength(3)]
        public string AgentMessageBody { get; set; }
    }
}
