using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class SendingAreaType
    {
        public SendingAreaType()
        {
            this.MessageMailMasters = new List<MessageMailMaster>();
        }

        public int Id { get; set; }
        public string SendingAreaName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<MessageMailMaster> MessageMailMasters { get; set; }
    }
}
