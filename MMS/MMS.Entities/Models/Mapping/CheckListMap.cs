using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class CheckListMap : EntityTypeConfiguration<CheckList>
    {
        public CheckListMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("CheckList");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsMandatory).HasColumnName("IsMandatory");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
        }
    }
}
