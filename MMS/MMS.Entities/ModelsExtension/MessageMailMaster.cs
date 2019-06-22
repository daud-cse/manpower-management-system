using MMS.Entities.ViewModels;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    public partial class MessageMailMaster:Entity
    {

        public List<MessageMailVM> lstMessageMailVM { get; set; }

        public List<KeyValuePair<int, string>> kvpSubsidyType { get; set; }
         public List<KeyValuePair<int,string>>  kvpSendingAreaType{get;set;}
    }
}
