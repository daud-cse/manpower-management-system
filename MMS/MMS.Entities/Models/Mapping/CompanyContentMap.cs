using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class CompanyContentMap : EntityTypeConfiguration<CompanyContent>
    {
        public CompanyContentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CompanyContent");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.ContentID).HasColumnName("ContentID");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasOptional(t => t.Company)
                .WithMany(t => t.CompanyContents)
                .HasForeignKey(d => d.CompanyID);
            this.HasOptional(t => t.Content)
                .WithMany(t => t.CompanyContents)
                .HasForeignKey(d => d.ContentID);

        }
    }
}
