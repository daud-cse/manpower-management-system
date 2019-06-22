using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class MessageMailMaster
    {
        public MessageMailMaster()
        {
            this.MailInfoes = new List<MailInfo>();
            this.MessageInfoes = new List<MessageInfo>();
        }

        public int Id { get; set; }
        public string AutoId { get; set; }
        public int SendingAreaTypeId { get; set; }
        public int MessageAreaTypeId { get; set; }
        public string MessageBody { get; set; }
        public string MailBody { get; set; }
        public System.DateTime SendDate { get; set; }
        public bool IsCompleted { get; set; }
        public string SetBy { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<MailInfo> MailInfoes { get; set; }
        public virtual MessageAreaType MessageAreaType { get; set; }
        public virtual ICollection<MessageInfo> MessageInfoes { get; set; }
        public virtual SendingAreaType SendingAreaType { get; set; }
    }
}
