using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class ApplicantFileLotMap : EntityTypeConfiguration<ApplicantFileLot>
    {
        public ApplicantFileLotMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ApplicantFileLot");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.NumberOfFiles).HasColumnName("NumberOfFiles");
            this.Property(t => t.FileLotStatusID).HasColumnName("FileLotStatusID");
            this.Property(t => t.LotStatusDate).HasColumnName("LotStatusDate");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.Agent)
                .WithMany(t => t.ApplicantFileLots)
                .HasForeignKey(d => d.AgentID);
            this.HasOptional(t => t.ApplicantLotStatu)
                .WithMany(t => t.ApplicantFileLots)
                .HasForeignKey(d => d.FileLotStatusID);

        }
    }
}
