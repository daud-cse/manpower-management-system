using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class CompanyWiseApplicantMap : EntityTypeConfiguration<CompanyWiseApplicant>
    {
        public CompanyWiseApplicantMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CompanyWiseApplicant");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CompanyId).HasColumnName("CompanyId");
            this.Property(t => t.ApplicantId).HasColumnName("ApplicantId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.Applicant)
                .WithMany(t => t.CompanyWiseApplicants)
                .HasForeignKey(d => d.ApplicantId);
            this.HasRequired(t => t.Company)
                .WithMany(t => t.CompanyWiseApplicants)
                .HasForeignKey(d => d.CompanyId);

        }
    }
}
