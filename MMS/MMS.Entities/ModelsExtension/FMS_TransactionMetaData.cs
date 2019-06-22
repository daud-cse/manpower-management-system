using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
  public partial  class FMS_TransactionMetaData
    {
      [DataType(DataType.Date)]
      public System.DateTime TransactionDate { get; set; }
    }
}
