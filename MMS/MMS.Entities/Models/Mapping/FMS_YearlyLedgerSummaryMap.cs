using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class FMS_YearlyLedgerSummaryMap : EntityTypeConfiguration<FMS_YearlyLedgerSummary>
    {
        public FMS_YearlyLedgerSummaryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.GLAccountId, t.SummaryYear });

            // Properties
            this.Property(t => t.GLAccountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SummaryYear)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("FMS_YearlyLedgerSummary");
            this.Property(t => t.GLAccountId).HasColumnName("GLAccountId");
            this.Property(t => t.SummaryYear).HasColumnName("SummaryYear");
            this.Property(t => t.DrAmount).HasColumnName("DrAmount");
            this.Property(t => t.CrAmount).HasColumnName("CrAmount");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
        }
    }
}
