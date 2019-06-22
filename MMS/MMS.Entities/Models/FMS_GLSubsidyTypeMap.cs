using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_GLSubsidyTypeMap
    {
        public int GlSubsidyTypeMapId { get; set; }
        public int GLAccountId { get; set; }
        public int SubsidyTypeId { get; set; }
        public bool IsActive { get; set; }
        public string SetBy { get; set; }
        public System.DateTime SetDate { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual FMS_GLAccount FMS_GLAccount { get; set; }
        public virtual FMS_SubsidyType FMS_SubsidyType { get; set; }
    }
}
