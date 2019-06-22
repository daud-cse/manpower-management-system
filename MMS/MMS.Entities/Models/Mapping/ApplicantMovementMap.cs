using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class ApplicantMovementMap : EntityTypeConfiguration<ApplicantMovement>
    {
        public ApplicantMovementMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.LocationName)
                .HasMaxLength(200);

            this.Property(t => t.MovementResultName)
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .HasMaxLength(1000);

            this.Property(t => t.UserID)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ApplicantMovement");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ApplicantID).HasColumnName("ApplicantID");
            this.Property(t => t.LocationID).HasColumnName("LocationID");
            this.Property(t => t.LocationName).HasColumnName("LocationName");
            this.Property(t => t.ProbableMoveDate).HasColumnName("ProbableMoveDate");
            this.Property(t => t.ActualMoveDate).HasColumnName("ActualMoveDate");
            this.Property(t => t.ExpectedReceiveDate).HasColumnName("ExpectedReceiveDate");
            this.Property(t => t.ActualReceiveDate).HasColumnName("ActualReceiveDate");
            this.Property(t => t.MovementResultID).HasColumnName("MovementResultID");
            this.Property(t => t.MovementResultName).HasColumnName("MovementResultName");
            this.Property(t => t.PercentageOfComplete).HasColumnName("PercentageOfComplete");
            this.Property(t => t.IsSucceed).HasColumnName("IsSucceed");
            this.Property(t => t.IsTaskCompleted).HasColumnName("IsTaskCompleted");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.Applicant)
                .WithMany(t => t.ApplicantMovements)
                .HasForeignKey(d => d.ApplicantID);
            this.HasOptional(t => t.MovementResult)
                .WithMany(t => t.ApplicantMovements)
                .HasForeignKey(d => d.MovementResultID);
            this.HasRequired(t => t.Location)
                .WithMany(t => t.ApplicantMovements)
                .HasForeignKey(d => d.LocationID);

        }
    }
}
