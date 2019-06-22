using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class ApplicantLocationDetailMap : EntityTypeConfiguration<ApplicantLocationDetail>
    {
        public ApplicantLocationDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.SetBy)
                .HasMaxLength(10);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ApplicantLocationDetail");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ApplicantMovementID).HasColumnName("ApplicantMovementID");
            this.Property(t => t.IsSucceed).HasColumnName("IsSucceed");
            this.Property(t => t.LocationID).HasColumnName("LocationID");
            this.Property(t => t.LocationMapDetailID).HasColumnName("LocationMapDetailID");
            this.Property(t => t.ApplicantID).HasColumnName("ApplicantID");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasRequired(t => t.Applicant)
                .WithMany(t => t.ApplicantLocationDetails)
                .HasForeignKey(d => d.ApplicantID);
            this.HasRequired(t => t.ApplicantMovement)
                .WithMany(t => t.ApplicantLocationDetails)
                .HasForeignKey(d => d.ApplicantMovementID);
            this.HasRequired(t => t.Location)
                .WithMany(t => t.ApplicantLocationDetails)
                .HasForeignKey(d => d.LocationID);
            this.HasRequired(t => t.LocationMapDetail)
                .WithMany(t => t.ApplicantLocationDetails)
                .HasForeignKey(d => d.LocationMapDetailID);

        }
    }
}
