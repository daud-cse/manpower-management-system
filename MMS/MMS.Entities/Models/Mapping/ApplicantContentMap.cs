using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class ApplicantContentMap : EntityTypeConfiguration<ApplicantContent>
    {
        public ApplicantContentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ApplicantContent");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ApplicantID).HasColumnName("ApplicantID");
            this.Property(t => t.ContentID).HasColumnName("ContentID");
            this.Property(t => t.CheckListID).HasColumnName("CheckListID");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasOptional(t => t.Applicant)
                .WithMany(t => t.ApplicantContents)
                .HasForeignKey(d => d.ApplicantID);
            this.HasOptional(t => t.Content)
                .WithMany(t => t.ApplicantContents)
                .HasForeignKey(d => d.ContentID);

        }
    }
}
