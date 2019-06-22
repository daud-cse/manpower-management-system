using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class ControlTypeMap : EntityTypeConfiguration<ControlType>
    {
        public ControlTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ControlTypeName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.SetBy)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ControlType");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ControlTypeName).HasColumnName("ControlTypeName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Setdate).HasColumnName("Setdate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
        }
    }
}
