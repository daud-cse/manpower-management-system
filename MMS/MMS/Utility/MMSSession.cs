using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Utility
{
    [Serializable()]
    public sealed class MMSSession
    {

        public int GlobalCompanyId { get; set; }
        public string GlobalCompanyName { get; set; }
        public string RoleId { get; set; }
        public string UserId { get; set; }
         public int PId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }      
        public List<string> UserRights { get; set; }

    }//End of Class

}