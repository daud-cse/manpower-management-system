using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class ModuleMap : EntityTypeConfiguration<Module>
    {
        public ModuleMap()
        {
            // Primary Key
            this.HasKey(t => t.ModuleId);

            // Properties
            this.Property(t => t.ModuleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.URL)
                .HasMaxLength(500);

            this.Property(t => t.RedirectURL)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Module");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.URL).HasColumnName("URL");
            this.Property(t => t.RedirectURL).HasColumnName("RedirectURL");
            this.Property(t => t.IsSecurityModule).HasColumnName("IsSecurityModule");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
        }
    }
}
