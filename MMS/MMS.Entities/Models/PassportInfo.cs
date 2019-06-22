using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class PassportInfo
    {
        public int ID { get; set; }
        public string DeliverySlipNo { get; set; }
        public Nullable<System.DateTime> EnrolementDate { get; set; }
        public Nullable<System.DateTime> TentativeCollectionDate { get; set; }
        public int RPOTypeId { get; set; }
        public string OwnerName { get; set; }
        public int MaritalStatusId { get; set; }
        public int PassPortTypeId { get; set; }
        public string OwerMobileNo { get; set; }
        public int SexTypeId { get; set; }
        public int CustomerId { get; set; }
        public string NewPassportNo { get; set; }
        public string PreviousPassportNo { get; set; }
        public Nullable<int> ContentIdForScan { get; set; }
        public bool IsReceivedCompleted { get; set; }
        public bool IsDeliveryDone { get; set; }
        public string Description { get; set; }
        public System.DateTime SetDate { get; set; }
        public string SetBy { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual MaritalStatu MaritalStatu { get; set; }
        public virtual PassPortType PassPortType { get; set; }
        public virtual RPOType RPOType { get; set; }
        public virtual Sex Sex { get; set; }
    }
}
