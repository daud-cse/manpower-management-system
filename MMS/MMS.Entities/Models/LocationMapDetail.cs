using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class LocationMapDetail
    {
        public LocationMapDetail()
        {
            this.ApplicantLocationDetails = new List<ApplicantLocationDetail>();
        }

        public int ID { get; set; }
        public int LocationId { get; set; }
        public int ControlTypeId { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public bool IsMultiSelect { get; set; }
        public bool IsActive { get; set; }
        public string SetBy { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual ICollection<ApplicantLocationDetail> ApplicantLocationDetails { get; set; }
        public virtual ControlType ControlType { get; set; }
        public virtual Location Location { get; set; }
    }
}
