using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class FMS_SubsidyType
    {
        public FMS_SubsidyType()
        {
            this.FMS_GLSubsidyTypeMap = new List<FMS_GLSubsidyTypeMap>();
            this.FMS_SubsidyAccount = new List<FMS_SubsidyAccount>();
            this.FMS_TransactionDet = new List<FMS_TransactionDet>();
        }

        public int SubsidyTypeId { get; set; }
        public string SubsidyTypeName { get; set; }
        public string TableName { get; set; }
        public string DRCR { get; set; }
        public bool IsActive { get; set; }
        public string SetBy { get; set; }
        public System.DateTime SetDate { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<FMS_GLSubsidyTypeMap> FMS_GLSubsidyTypeMap { get; set; }
        public virtual ICollection<FMS_SubsidyAccount> FMS_SubsidyAccount { get; set; }
        public virtual ICollection<FMS_TransactionDet> FMS_TransactionDet { get; set; }
    }
}
