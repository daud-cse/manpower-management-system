using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class RoleRightssMap : EntityTypeConfiguration<RoleRightss>
    {
        public RoleRightssMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.SetBy)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("RoleRightss");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.RightId).HasColumnName("RightId");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsCreate).HasColumnName("IsCreate");
            this.Property(t => t.IsEdit).HasColumnName("IsEdit");
            this.Property(t => t.IsDelete).HasColumnName("IsDelete");
            this.Property(t => t.IsList).HasColumnName("IsList");
            this.Property(t => t.IsDetails).HasColumnName("IsDetails");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.Rightss)
                .WithMany(t => t.RoleRightsses)
                .HasForeignKey(d => d.RightId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.RoleRightsses)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
