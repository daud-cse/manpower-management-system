using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class SubsidySummaryMap : EntityTypeConfiguration<SubsidySummary>
    {
        public SubsidySummaryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TransactionDetId, t.SubsidyTypeId, t.SubsidyAccountId, t.GlobalCompanyId });

            // Properties
            this.Property(t => t.TransactionDetId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SubsidyTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SubsidyAccountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DrCr)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GlobalCompanyId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SubsidySummary", "tmp");
            this.Property(t => t.TransactionDetId).HasColumnName("TransactionDetId");
            this.Property(t => t.SubsidyTypeId).HasColumnName("SubsidyTypeId");
            this.Property(t => t.SubsidyAccountId).HasColumnName("SubsidyAccountId");
            this.Property(t => t.DrCr).HasColumnName("DrCr");
            this.Property(t => t.DrAmount).HasColumnName("DrAmount");
            this.Property(t => t.CrAmount).HasColumnName("CrAmount");
            this.Property(t => t.Balance).HasColumnName("Balance");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
        }
    }
}
