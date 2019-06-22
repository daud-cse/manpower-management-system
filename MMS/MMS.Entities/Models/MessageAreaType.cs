using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class MessageAreaType
    {
        public MessageAreaType()
        {
            this.MessageMailMasters = new List<MessageMailMaster>();
        }

        public int Id { get; set; }
        public string MessageAreaName { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public virtual ICollection<MessageMailMaster> MessageMailMasters { get; set; }
    }
}
