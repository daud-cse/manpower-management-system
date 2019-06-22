using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class UserModuleMap : EntityTypeConfiguration<UserModule>
    {
        public UserModuleMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UserModule");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Setdate).HasColumnName("Setdate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.Module)
                .WithMany(t => t.UserModules)
                .HasForeignKey(d => d.ModuleId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserModules)
                .HasForeignKey(d => d.UserId);

        }
    }
}
