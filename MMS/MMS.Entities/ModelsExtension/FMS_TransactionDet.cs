using MMS.Entities.ViewModels;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
    [MetadataType(typeof(FMS_TransactionDetMetaData))]
    public partial class FMS_TransactionDet:Entity
    {
        public List<KeyValuePair<int, string>> kvpGLAccount { get; set; }
        public List<KeyValuePair<int, string>> kvpSubsidyAccount { get; set; }
        public List<KeyValuePair<int, string>> kvpSubsidyType { get; set; }

        public FMS_SubSidyVM FMS_SubSidyVM;
      
       
    }
}
