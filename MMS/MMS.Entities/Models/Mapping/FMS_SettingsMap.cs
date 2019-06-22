using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class FMS_SettingsMap : EntityTypeConfiguration<FMS_Settings>
    {
        public FMS_SettingsMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("FMS_Settings");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CurrentDate).HasColumnName("CurrentDate");
            this.Property(t => t.LastClosedDate).HasColumnName("LastClosedDate");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
        }
    }
}
