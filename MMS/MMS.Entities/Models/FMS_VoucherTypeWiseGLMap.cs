using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_VoucherTypeWiseGLMap
    {
        public int VoucherTypeWiseGLMapId { get; set; }
        public int VoucherTypeId { get; set; }
        public int GLAccountId { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime SetDate { get; set; }
        public string SetBy { get; set; }
        public string ActionType { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual FMS_GLAccount FMS_GLAccount { get; set; }
        public virtual FMS_VoucherType FMS_VoucherType { get; set; }
    }
}
