using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class FMS_VoucherTypeWiseOppositeGLMapMap : EntityTypeConfiguration<FMS_VoucherTypeWiseOppositeGLMap>
    {
        public FMS_VoucherTypeWiseOppositeGLMapMap()
        {
            // Primary Key
            this.HasKey(t => t.VoucherTypeWiseOppositeGLMapId);

            // Properties
            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("FMS_VoucherTypeWiseOppositeGLMap");
            this.Property(t => t.VoucherTypeWiseOppositeGLMapId).HasColumnName("VoucherTypeWiseOppositeGLMapId");
            this.Property(t => t.VoucherTypeId).HasColumnName("VoucherTypeId");
            this.Property(t => t.GLAccountId).HasColumnName("GLAccountId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.FMS_GLAccount)
                .WithMany(t => t.FMS_VoucherTypeWiseOppositeGLMap)
                .HasForeignKey(d => d.GLAccountId);
            this.HasRequired(t => t.FMS_VoucherType)
                .WithMany(t => t.FMS_VoucherTypeWiseOppositeGLMap)
                .HasForeignKey(d => d.VoucherTypeId);

        }
    }
}
