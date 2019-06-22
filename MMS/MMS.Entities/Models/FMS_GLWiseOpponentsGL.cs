using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_GLWiseOpponentsGL
    {
        public int ID { get; set; }
        public Nullable<int> GLAccountId { get; set; }
        public Nullable<int> OpponentsGLId { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual FMS_GLAccount FMS_GLAccount { get; set; }
        public virtual FMS_GLAccount FMS_GLAccount1 { get; set; }
    }
}
