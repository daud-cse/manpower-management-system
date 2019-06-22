using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.SECURITY.Utility
{
    [Serializable()]
    public sealed class SECURITYMMSSession
    {

        public int GlobalCompanyId { get; set; }
        public string GlobalCompanyName { get; set; }
        public string RoleId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDayOpened { get; set; }
        public List<int> UserRights { get; set; }

    }//End of Class

}