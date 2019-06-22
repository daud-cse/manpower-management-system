using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class FMS_VoucherTypeMap : EntityTypeConfiguration<FMS_VoucherType>
    {
        public FMS_VoucherTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.VoucherTypeId);

            // Properties
            this.Property(t => t.VoucherTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.VoucherTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("FMS_VoucherType");
            this.Property(t => t.VoucherTypeId).HasColumnName("VoucherTypeId");
            this.Property(t => t.VoucherTypeName).HasColumnName("VoucherTypeName");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
        }
    }
}
