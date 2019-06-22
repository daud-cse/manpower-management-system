using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class ApplicantMovementResultHistoryMap : EntityTypeConfiguration<ApplicantMovementResultHistory>
    {
        public ApplicantMovementResultHistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Remarks)
                .HasMaxLength(200);

            this.Property(t => t.SetBy)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ApplicantMovementResultHistory");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ApplicantMovementID).HasColumnName("ApplicantMovementID");
            this.Property(t => t.MovementResultID).HasColumnName("MovementResultID");
            this.Property(t => t.ResultDate).HasColumnName("ResultDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasOptional(t => t.ApplicantMovement)
                .WithMany(t => t.ApplicantMovementResultHistories)
                .HasForeignKey(d => d.ApplicantMovementID);
            this.HasOptional(t => t.MovementResult)
                .WithMany(t => t.ApplicantMovementResultHistories)
                .HasForeignKey(d => d.MovementResultID);

        }
    }
}
