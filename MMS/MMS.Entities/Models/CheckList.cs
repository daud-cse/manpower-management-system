using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class CheckList
    {
        public CheckList()
        {
            this.CheckListGroupMappings = new List<CheckListGroupMapping>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsActive { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<CheckListGroupMapping> CheckListGroupMappings { get; set; }
    }
}
