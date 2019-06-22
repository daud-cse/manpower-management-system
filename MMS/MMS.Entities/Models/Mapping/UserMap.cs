using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.UserAccountsId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Password)
                .IsRequired();

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.EmailId)
                .HasMaxLength(20);

            this.Property(t => t.PhoneNo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.UserAccountsId).HasColumnName("UserAccountsId");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.UserDescription).HasColumnName("UserDescription");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.EmailId).HasColumnName("EmailId");
            this.Property(t => t.CountryId).HasColumnName("CountryId");
            this.Property(t => t.PhoneNo).HasColumnName("PhoneNo");
            this.Property(t => t.IsTermConditionAgreed).HasColumnName("IsTermConditionAgreed");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.GlobalCompany)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.GlobalCompanyId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
