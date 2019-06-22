using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.ViewModels
{
   public class CheckListGroupVM
    {
       public int ApplicantCheckListID { get; set; }
       public int CheckListGroupMapID { get; set; }
       public int CheckListGroupID { get; set; }
       public string CheckListGroupName { get; set; }
       public int CheckListID { get; set; }
       public string CheckListName { get; set; }

       public string Description { get; set; }
       public bool IsCheckList { get; set; }

       public int GlobalCompanyId { get; set; }
       
    }
}
