using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
      [MetadataType(typeof(LocationMetaData))]
   public partial class Location:Entity
    {
    }
}
