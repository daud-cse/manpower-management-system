using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class CheckListGroupMappingMap : EntityTypeConfiguration<CheckListGroupMapping>
    {
        public CheckListGroupMappingMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID);
               // .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .HasMaxLength(500);
            this.Property(t => t.SetBy)
             .HasMaxLength(10);
            // Table & Column Mappings
            this.ToTable("CheckListGroupMapping");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CheckListGroupID).HasColumnName("CheckListGroupID");
            this.Property(t => t.CheckListID).HasColumnName("CheckListID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            // Relationships
            this.HasRequired(t => t.ApplicantType)
                .WithMany(t => t.CheckListGroupMappings)
                .HasForeignKey(d => d.CheckListGroupID);
            this.HasRequired(t => t.CheckList)
                .WithMany(t => t.CheckListGroupMappings)
                .HasForeignKey(d => d.CheckListID);

        }
    }
}
