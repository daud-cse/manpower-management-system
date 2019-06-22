using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_PaymentReceivedType
    {
        public FMS_PaymentReceivedType()
        {
            this.FMS_Transaction = new List<FMS_Transaction>();
        }

        public int PRTypeId { get; set; }
        public string PRTypeName { get; set; }
        public virtual ICollection<FMS_Transaction> FMS_Transaction { get; set; }
    }
}
