using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class FMS_VoucherTypeWiseGLMapMap : EntityTypeConfiguration<FMS_VoucherTypeWiseGLMap>
    {
        public FMS_VoucherTypeWiseGLMapMap()
        {
            // Primary Key
            this.HasKey(t => t.VoucherTypeWiseGLMapId);

            // Properties
            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ActionType)
                .IsRequired()
                .HasMaxLength(6);

            // Table & Column Mappings
            this.ToTable("FMS_VoucherTypeWiseGLMap");
            this.Property(t => t.VoucherTypeWiseGLMapId).HasColumnName("VoucherTypeWiseGLMapId");
            this.Property(t => t.VoucherTypeId).HasColumnName("VoucherTypeId");
            this.Property(t => t.GLAccountId).HasColumnName("GLAccountId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.ActionType).HasColumnName("ActionType");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.FMS_GLAccount)
                .WithMany(t => t.FMS_VoucherTypeWiseGLMap)
                .HasForeignKey(d => d.GLAccountId);
            this.HasRequired(t => t.FMS_VoucherType)
                .WithMany(t => t.FMS_VoucherTypeWiseGLMap)
                .HasForeignKey(d => d.VoucherTypeId);

        }
    }
}
