using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class FMS_DayWiseCheckListDetailsMap : EntityTypeConfiguration<FMS_DayWiseCheckListDetails>
    {
        public FMS_DayWiseCheckListDetailsMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.SetBy)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("FMS_DayWiseCheckListDetails");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.OpenDate).HasColumnName("OpenDate");
            this.Property(t => t.DayOpenCloseCheckListId).HasColumnName("DayOpenCloseCheckListId");
            this.Property(t => t.IsCheckListDone).HasColumnName("IsCheckListDone");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.FMS_DayOpenCloseCheckList)
                .WithMany(t => t.FMS_DayWiseCheckListDetails)
                .HasForeignKey(d => d.DayOpenCloseCheckListId);

        }
    }
}
