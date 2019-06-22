using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class FMS_CurrentLedgerSummaryMap : EntityTypeConfiguration<FMS_CurrentLedgerSummary>
    {
        public FMS_CurrentLedgerSummaryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.GLAccountId, t.GlobalCompanyId });

            // Properties
            this.Property(t => t.GLAccountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DrCr)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GlobalCompanyId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("FMS_CurrentLedgerSummary");
            this.Property(t => t.GLAccountId).HasColumnName("GLAccountId");
            this.Property(t => t.DrCr).HasColumnName("DrCr");
            this.Property(t => t.OpenDate).HasColumnName("OpenDate");
            this.Property(t => t.OpeningBalance).HasColumnName("OpeningBalance");
            this.Property(t => t.DrAmount).HasColumnName("DrAmount");
            this.Property(t => t.CrAmount).HasColumnName("CrAmount");
            this.Property(t => t.ClosingBalance).HasColumnName("ClosingBalance");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
        }
    }
}
