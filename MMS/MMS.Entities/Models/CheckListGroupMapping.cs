using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class CheckListGroupMapping
    {
        public CheckListGroupMapping()
        {
            this.ApplicantCheckLists = new List<ApplicantCheckList>();
        }

        public int ID { get; set; }
        public int CheckListGroupID { get; set; }
        public int CheckListID { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int GlobalCompanyId { get; set; }
        public string SetBy { get; set; }
      
        public System.DateTime? SetDate { get; set; }
        public virtual ICollection<ApplicantCheckList> ApplicantCheckLists { get; set; }
        public virtual ApplicantType ApplicantType { get; set; }
        public virtual CheckList CheckList { get; set; }
    }
}
