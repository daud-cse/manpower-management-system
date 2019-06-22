using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class ConfigureEmailMessageMap : EntityTypeConfiguration<ConfigureEmailMessage>
    {
        public ConfigureEmailMessageMap()
        {
            // Primary Key
            this.HasKey(t => t.ConfigureId);

            // Properties
            this.Property(t => t.ConfigureId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EmailId)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ConfigureEmailMessage");
            this.Property(t => t.ConfigureId).HasColumnName("ConfigureId");
            this.Property(t => t.EmailId).HasColumnName("EmailId");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.AreaId).HasColumnName("AreaId");
            this.Property(t => t.MaxEmailDaily).HasColumnName("MaxEmailDaily");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
        }
    }
}
