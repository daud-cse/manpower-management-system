using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class ComplainMap : EntityTypeConfiguration<Complain>
    {
        public ComplainMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ComplainPersonName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ComplainPersonAddress)
                .HasMaxLength(1000);

            this.Property(t => t.ComplainDetails)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.ComplainPersonMobileNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(150);

            this.Property(t => t.Comments)
                .HasMaxLength(500);

            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Complain");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ComplainTypeId).HasColumnName("ComplainTypeId");
            this.Property(t => t.ComplainPersonName).HasColumnName("ComplainPersonName");
            this.Property(t => t.ComplainPersonAddress).HasColumnName("ComplainPersonAddress");
            this.Property(t => t.AgentId).HasColumnName("AgentId");
            this.Property(t => t.ApplicantId).HasColumnName("ApplicantId");
            this.Property(t => t.ComplainStatusId).HasColumnName("ComplainStatusId");
            this.Property(t => t.ComplainDetails).HasColumnName("ComplainDetails");
            this.Property(t => t.ComplainPersonMobileNo).HasColumnName("ComplainPersonMobileNo");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.ComplainStartDate).HasColumnName("ComplainStartDate");
            this.Property(t => t.ExpectedEndDate).HasColumnName("ExpectedEndDate");
            this.Property(t => t.ActualEndDate).HasColumnName("ActualEndDate");
            this.Property(t => t.ComplainUpdateDate).HasColumnName("ComplainUpdateDate");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.ComplainCountryId).HasColumnName("ComplainCountryId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsApprovedComplain).HasColumnName("IsApprovedComplain");
            this.Property(t => t.IsReceivedComplain).HasColumnName("IsReceivedComplain");
            this.Property(t => t.IsCompletedComplain).HasColumnName("IsCompletedComplain");
            this.Property(t => t.DivisionId).HasColumnName("DivisionId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.UpazilaId).HasColumnName("UpazilaId");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.Agent)
                .WithMany(t => t.Complains)
                .HasForeignKey(d => d.AgentId);
            this.HasOptional(t => t.Applicant)
                .WithMany(t => t.Complains)
                .HasForeignKey(d => d.ApplicantId);
            this.HasRequired(t => t.ComplainType)
                .WithMany(t => t.Complains)
                .HasForeignKey(d => d.ComplainTypeId);
            this.HasRequired(t => t.Country)
                .WithMany(t => t.Complains)
                .HasForeignKey(d => d.ComplainCountryId);
            this.HasOptional(t => t.District)
                .WithMany(t => t.Complains)
                .HasForeignKey(d => d.DistrictId);
            this.HasOptional(t => t.Division)
                .WithMany(t => t.Complains)
                .HasForeignKey(d => d.DivisionId);
            this.HasRequired(t => t.Status)
                .WithMany(t => t.Complains)
                .HasForeignKey(d => d.ComplainStatusId);
            this.HasOptional(t => t.Upazila)
                .WithMany(t => t.Complains)
                .HasForeignKey(d => d.UpazilaId);

        }
    }
}
