using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class AgentType
    {
        public AgentType()
        {
            this.Agents = new List<Agent>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
    }
}
