using System;
using System.Collections.Generic;

namespace MMS.Entities.Models
{
    public partial class ContentDetail
    {
        public int ID { get; set; }
        public Nullable<int> ContentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Object { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public int ContentTypeID { get; set; }
        public int GlobalCompanyId { get; set; }
        public virtual Content Content { get; set; }
    }
}
