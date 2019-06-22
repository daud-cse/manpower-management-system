using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class LocationMap : EntityTypeConfiguration<Location>
    {
        public LocationMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.GeneralMessageBody)
                .HasMaxLength(160);

            this.Property(t => t.ApplicantMessageBody)
                .HasMaxLength(160);

            this.Property(t => t.AgentMessageBody)
                .HasMaxLength(160);

            // Table & Column Mappings
            this.ToTable("Location");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NextLocationID).HasColumnName("NextLocationID");
            this.Property(t => t.IsStartingCompanyWise).HasColumnName("IsStartingCompanyWise");
            this.Property(t => t.IsNextLocationExist).HasColumnName("IsNextLocationExist");
            this.Property(t => t.PreRequisitID).HasColumnName("PreRequisitID");
            this.Property(t => t.SetPercentage).HasColumnName("SetPercentage");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsMandatory).HasColumnName("IsMandatory");
            this.Property(t => t.IsSendGeneralMesasge).HasColumnName("IsSendGeneralMesasge");
            this.Property(t => t.GeneralMessageBody).HasColumnName("GeneralMessageBody");
            this.Property(t => t.IsSendApplicantMesasge).HasColumnName("IsSendApplicantMesasge");
            this.Property(t => t.ApplicantMessageBody).HasColumnName("ApplicantMessageBody");
            this.Property(t => t.IsSendAgentMesasge).HasColumnName("IsSendAgentMesasge");
            this.Property(t => t.AgentMessageBody).HasColumnName("AgentMessageBody");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
        }
    }
}
