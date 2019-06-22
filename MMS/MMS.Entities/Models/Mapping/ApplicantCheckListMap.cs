using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class ApplicantCheckListMap : EntityTypeConfiguration<ApplicantCheckList>
    {
        public ApplicantCheckListMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.SetBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ApplicantCheckList");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ApplicantID).HasColumnName("ApplicantID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CheckListGroupMapID).HasColumnName("CheckListGroupMapID");
            this.Property(t => t.IsCompliant).HasColumnName("IsCompliant");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.Applicant)
                .WithMany(t => t.ApplicantCheckLists)
                .HasForeignKey(d => d.ApplicantID);
            this.HasRequired(t => t.CheckListGroupMapping)
                .WithMany(t => t.ApplicantCheckLists)
                .HasForeignKey(d => d.CheckListGroupMapID);

        }
    }
}
