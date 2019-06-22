using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class FMS_DailySubsidyBalanceTransactionWiseMap : EntityTypeConfiguration<FMS_DailySubsidyBalanceTransactionWise>
    {
        public FMS_DailySubsidyBalanceTransactionWiseMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.TransactionDetId });

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.TransactionDetId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("FMS_DailySubsidyBalanceTransactionWise");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TransactionDetId).HasColumnName("TransactionDetId");
            this.Property(t => t.SubsidyTypeId).HasColumnName("SubsidyTypeId");
            this.Property(t => t.SubsidyAccountId).HasColumnName("SubsidyAccountId");
            this.Property(t => t.OpenDate).HasColumnName("OpenDate");
            this.Property(t => t.DrAmount).HasColumnName("DrAmount");
            this.Property(t => t.CrAmount).HasColumnName("CrAmount");
            this.Property(t => t.Balance).HasColumnName("Balance");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
        }
    }
}
