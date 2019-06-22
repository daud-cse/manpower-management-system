using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.ReportsModels
{
  public  class LedgerStatement
    {
      public DateTime Fromdate { get; set; }
      public DateTime Todate { get; set; }
      public List<KeyValuePair<int, string>> kvpSubsidyType { get; set; }
      public List<KeyValuePair<int, string>> kvpAgent { get; set; }
      public int SubsidyTypeId { get; set; }
      public int AgentId { get; set; }
      public int SubsidyId { get; set; }
      public bool IsApproved { get; set; }
      
    }
}
