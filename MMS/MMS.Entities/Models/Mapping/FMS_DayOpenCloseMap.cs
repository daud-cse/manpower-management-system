using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class FMS_DayOpenCloseMap : EntityTypeConfiguration<FMS_DayOpenClose>
    {
        public FMS_DayOpenCloseMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FMS_DayOpenClose");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OpenDate).HasColumnName("OpenDate");
            this.Property(t => t.OpenedOn).HasColumnName("OpenedOn");
            this.Property(t => t.IsDayClosed).HasColumnName("IsDayClosed");
            this.Property(t => t.ClosedOn).HasColumnName("ClosedOn");
            this.Property(t => t.OpeningBankBalance).HasColumnName("OpeningBankBalance");
            this.Property(t => t.OpeningCashBalance).HasColumnName("OpeningCashBalance");
            this.Property(t => t.ClosingBankBalance).HasColumnName("ClosingBankBalance");
            this.Property(t => t.ClosingCashBalance).HasColumnName("ClosingCashBalance");
            this.Property(t => t.Revenue).HasColumnName("Revenue");
            this.Property(t => t.Expenses).HasColumnName("Expenses");
            this.Property(t => t.GrossProfitOrLoss).HasColumnName("GrossProfitOrLoss");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
        }
    }
}
