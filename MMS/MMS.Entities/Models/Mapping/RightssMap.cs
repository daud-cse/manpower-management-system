using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class RightssMap : EntityTypeConfiguration<Rightss>
    {
        public RightssMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RightId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.RightsName)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Rightss");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.RightId).HasColumnName("RightId");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.RightsName).HasColumnName("RightsName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.Module)
                .WithMany(t => t.Rightsses)
                .HasForeignKey(d => d.ModuleId);

        }
    }
}
