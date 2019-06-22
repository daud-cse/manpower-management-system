using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class FMS_TransactionDetMap : EntityTypeConfiguration<FMS_TransactionDet>
    {
        public FMS_TransactionDetMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionDetId);

            // Properties
            this.Property(t => t.Particulars)
                .HasMaxLength(500);

            this.Property(t => t.ChequeNo)
                .HasMaxLength(50);

            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(36);

            // Table & Column Mappings
            this.ToTable("FMS_TransactionDet");
            this.Property(t => t.TransactionDetId).HasColumnName("TransactionDetId");
            this.Property(t => t.TransactionId).HasColumnName("TransactionId");
            this.Property(t => t.TransactionSLNo).HasColumnName("TransactionSLNo");
            this.Property(t => t.GLAccountId).HasColumnName("GLAccountId");
            this.Property(t => t.Particulars).HasColumnName("Particulars");
            this.Property(t => t.DrAmount).HasColumnName("DrAmount");
            this.Property(t => t.CrAmount).HasColumnName("CrAmount");
            this.Property(t => t.ChequeNo).HasColumnName("ChequeNo");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SubsidyTypeId).HasColumnName("SubsidyTypeId");
            this.Property(t => t.SubsidyAccountId).HasColumnName("SubsidyAccountId");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.FMS_GLAccount)
                .WithMany(t => t.FMS_TransactionDet)
                .HasForeignKey(d => d.GLAccountId);
            this.HasOptional(t => t.FMS_SubsidyType)
                .WithMany(t => t.FMS_TransactionDet)
                .HasForeignKey(d => d.SubsidyTypeId);
            this.HasRequired(t => t.FMS_Transaction)
                .WithMany(t => t.FMS_TransactionDet)
                .HasForeignKey(d => d.TransactionId);

        }
    }
}
