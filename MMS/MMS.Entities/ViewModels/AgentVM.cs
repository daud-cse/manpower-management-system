using MMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.ViewModels
{
   public class AgentVM
    {
       public Agent agent { get; set; }
       public List<Agent> agentList { get; set; }
       public Address address { get; set; }
    }
}
