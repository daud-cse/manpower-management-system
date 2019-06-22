using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.ViewModels
{
   public  class MessageMailVM
    {

       public string Id { get; set; }
       public string Name { get; set; }
       public int GlobalCompanyId { get; set; }
       public string MobileNo { get; set; }
       public string MessageBody { get; set; }
       public string AutoRefId { get; set; }       
       public string EmailId { get; set; }

       public string MailBody { get; set; }
       
       public bool IsSent { get; set; }

       


    }
}
