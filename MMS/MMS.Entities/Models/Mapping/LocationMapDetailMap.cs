using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class LocationMapDetailMap : EntityTypeConfiguration<LocationMapDetail>
    {
        public LocationMapDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(500);

            this.Property(t => t.SetBy)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("LocationMapDetail");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.LocationId).HasColumnName("LocationId");
            this.Property(t => t.ControlTypeId).HasColumnName("ControlTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.EffectiveDate).HasColumnName("EffectiveDate");
            this.Property(t => t.IsMultiSelect).HasColumnName("IsMultiSelect");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.ControlType)
                .WithMany(t => t.LocationMapDetails)
                .HasForeignKey(d => d.ControlTypeId);
            this.HasRequired(t => t.Location)
                .WithMany(t => t.LocationMapDetails)
                .HasForeignKey(d => d.LocationId);

        }
    }
}
