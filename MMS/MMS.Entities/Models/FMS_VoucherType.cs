using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_VoucherType
    {
        public FMS_VoucherType()
        {
            this.FMS_Transaction = new List<FMS_Transaction>();
            this.FMS_VoucherTypeWiseOppositeGLMap = new List<FMS_VoucherTypeWiseOppositeGLMap>();
            this.FMS_VoucherTypeWiseGLMap = new List<FMS_VoucherTypeWiseGLMap>();
        }

        public int VoucherTypeId { get; set; }
        public string VoucherTypeName { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<FMS_Transaction> FMS_Transaction { get; set; }
        public virtual ICollection<FMS_VoucherTypeWiseOppositeGLMap> FMS_VoucherTypeWiseOppositeGLMap { get; set; }
        public virtual ICollection<FMS_VoucherTypeWiseGLMap> FMS_VoucherTypeWiseGLMap { get; set; }
    }
}
