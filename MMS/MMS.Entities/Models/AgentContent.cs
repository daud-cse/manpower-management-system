using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class AgentContent
    {
        public int ID { get; set; }
        public Nullable<int> AgentID { get; set; }
        public Nullable<int> ContentID { get; set; }
        public string SetBy { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
    }
}
