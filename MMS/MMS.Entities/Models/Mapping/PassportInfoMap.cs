using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class PassportInfoMap : EntityTypeConfiguration<PassportInfo>
    {
        public PassportInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.DeliverySlipNo)
                .HasMaxLength(14);

            this.Property(t => t.OwnerName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.OwerMobileNo)
                .IsRequired()
                .HasMaxLength(14);

            this.Property(t => t.NewPassportNo)
                .HasMaxLength(9);

            this.Property(t => t.PreviousPassportNo)
                .HasMaxLength(9);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("PassportInfo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.DeliverySlipNo).HasColumnName("DeliverySlipNo");
            this.Property(t => t.EnrolementDate).HasColumnName("EnrolementDate");
            this.Property(t => t.TentativeCollectionDate).HasColumnName("TentativeCollectionDate");
            this.Property(t => t.RPOTypeId).HasColumnName("RPOTypeId");
            this.Property(t => t.OwnerName).HasColumnName("OwnerName");
            this.Property(t => t.MaritalStatusId).HasColumnName("MaritalStatusId");
            this.Property(t => t.PassPortTypeId).HasColumnName("PassPortTypeId");
            this.Property(t => t.OwerMobileNo).HasColumnName("OwerMobileNo");
            this.Property(t => t.SexTypeId).HasColumnName("SexTypeId");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.NewPassportNo).HasColumnName("NewPassportNo");
            this.Property(t => t.PreviousPassportNo).HasColumnName("PreviousPassportNo");
            this.Property(t => t.ContentIdForScan).HasColumnName("ContentIdForScan");
            this.Property(t => t.IsReceivedCompleted).HasColumnName("IsReceivedCompleted");
            this.Property(t => t.IsDeliveryDone).HasColumnName("IsDeliveryDone");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.PassportInfoes)
                .HasForeignKey(d => d.CustomerId);
            this.HasRequired(t => t.MaritalStatu)
                .WithMany(t => t.PassportInfoes)
                .HasForeignKey(d => d.MaritalStatusId);
            this.HasRequired(t => t.PassPortType)
                .WithMany(t => t.PassportInfoes)
                .HasForeignKey(d => d.PassPortTypeId);
            this.HasRequired(t => t.RPOType)
                .WithMany(t => t.PassportInfoes)
                .HasForeignKey(d => d.RPOTypeId);
            this.HasRequired(t => t.Sex)
                .WithMany(t => t.PassportInfoes)
                .HasForeignKey(d => d.SexTypeId);

        }
    }
}
