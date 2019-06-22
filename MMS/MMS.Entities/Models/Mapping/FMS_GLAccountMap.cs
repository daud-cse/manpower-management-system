using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class FMS_GLAccountMap : EntityTypeConfiguration<FMS_GLAccount>
    {
        public FMS_GLAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.GLAccountId);

            // Properties
            this.Property(t => t.GLAccountCode)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.GLAccountName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.GLAccountTreeName)
                .HasMaxLength(50);

            this.Property(t => t.SubCtrlPrefix)
                .HasMaxLength(100);

            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.DrCr)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("FMS_GLAccount");
            this.Property(t => t.GLAccountId).HasColumnName("GLAccountId");
            this.Property(t => t.GLAccountCode).HasColumnName("GLAccountCode");
            this.Property(t => t.GLAccountIdentifyId).HasColumnName("GLAccountIdentifyId");
            this.Property(t => t.GLAccountName).HasColumnName("GLAccountName");
            this.Property(t => t.GLAccountTreeName).HasColumnName("GLAccountTreeName");
            this.Property(t => t.ParentAccountId).HasColumnName("ParentAccountId");
            this.Property(t => t.SubCtrlPrefix).HasColumnName("SubCtrlPrefix");
            this.Property(t => t.AccountTypeID).HasColumnName("AccountTypeID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.OpeningDate).HasColumnName("OpeningDate");
            this.Property(t => t.LevelCode).HasColumnName("LevelCode");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.CurrentBalance).HasColumnName("CurrentBalance");
            this.Property(t => t.CurrentYearOpeningBalance).HasColumnName("CurrentYearOpeningBalance");
            this.Property(t => t.IsSubsidyExist).HasColumnName("IsSubsidyExist");
            this.Property(t => t.HasChild).HasColumnName("HasChild");
            this.Property(t => t.ReportTypeID).HasColumnName("ReportTypeID");
            this.Property(t => t.DrCr).HasColumnName("DrCr");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasOptional(t => t.FMS_AccountingReportType)
                .WithMany(t => t.FMS_GLAccount)
                .HasForeignKey(d => d.ReportTypeID);
            this.HasOptional(t => t.FMS_GLAccount2)
                .WithMany(t => t.FMS_GLAccount1)
                .HasForeignKey(d => d.ParentAccountId);

        }
    }
}
