using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class FMS_DailySubsidyLedgerSummaryMap : EntityTypeConfiguration<FMS_DailySubsidyLedgerSummary>
    {
        public FMS_DailySubsidyLedgerSummaryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.GLAccountId, t.SubsidyAccountId, t.SummaryDate, t.SubsidyTypeId, t.GlobalCompanyId });

            // Properties
            this.Property(t => t.GLAccountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SubsidyAccountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SubsidyTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.GlobalCompanyId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("FMS_DailySubsidyLedgerSummary");
            this.Property(t => t.GLAccountId).HasColumnName("GLAccountId");
            this.Property(t => t.SubsidyAccountId).HasColumnName("SubsidyAccountId");
            this.Property(t => t.SummaryDate).HasColumnName("SummaryDate");
            this.Property(t => t.SubsidyTypeId).HasColumnName("SubsidyTypeId");
            this.Property(t => t.DrAmount).HasColumnName("DrAmount");
            this.Property(t => t.CrAmount).HasColumnName("CrAmount");
            this.Property(t => t.Balance).HasColumnName("Balance");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
        }
    }
}
