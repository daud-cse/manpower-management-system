using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class FMS_TransactionMap : EntityTypeConfiguration<FMS_Transaction>
    {
        public FMS_TransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionId);

            // Properties
            this.Property(t => t.VoucherId)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.ApprovedBy)
                .HasMaxLength(10);

            this.Property(t => t.Narration)
                .HasMaxLength(500);

            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ActionType)
                .IsRequired()
                .HasMaxLength(6);

            this.Property(t => t.ReferenceId)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("FMS_Transaction");
            this.Property(t => t.TransactionId).HasColumnName("TransactionId");
            this.Property(t => t.VoucherId).HasColumnName("VoucherId");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.ValueDate).HasColumnName("ValueDate");
            this.Property(t => t.VoucherTypeId).HasColumnName("VoucherTypeId");
            this.Property(t => t.IsPosted).HasColumnName("IsPosted");
            this.Property(t => t.PRTypeId).HasColumnName("PRTypeId");
            this.Property(t => t.ApprovedBy).HasColumnName("ApprovedBy");
            this.Property(t => t.IsAuto).HasColumnName("IsAuto");
            this.Property(t => t.IsApproved).HasColumnName("IsApproved");
            this.Property(t => t.IsMultipleOrSingle).HasColumnName("IsMultipleOrSingle");
            this.Property(t => t.Narration).HasColumnName("Narration");
            this.Property(t => t.CompanyCode).HasColumnName("CompanyCode");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.ActionType).HasColumnName("ActionType");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.VoucherSLNo).HasColumnName("VoucherSLNo");
            this.Property(t => t.ReferenceId).HasColumnName("ReferenceId");
            this.Property(t => t.ApprovalDate).HasColumnName("ApprovalDate");
            this.Property(t => t.TranactionAmount).HasColumnName("TranactionAmount");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.FMS_PaymentReceivedType)
                .WithMany(t => t.FMS_Transaction)
                .HasForeignKey(d => d.PRTypeId);
            this.HasRequired(t => t.FMS_VoucherType)
                .WithMany(t => t.FMS_Transaction)
                .HasForeignKey(d => d.VoucherTypeId);

        }
    }
}
