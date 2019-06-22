using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class MailInfoMap : EntityTypeConfiguration<MailInfo>
    {
        public MailInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.AutoRefId)
                .HasMaxLength(10);

            this.Property(t => t.Name)
                .HasMaxLength(150);

            this.Property(t => t.EmailId)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.SetBy)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("MailInfo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MailMasterId).HasColumnName("MailMasterId");
            this.Property(t => t.AutoRefId).HasColumnName("AutoRefId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.EmailId).HasColumnName("EmailId");
            this.Property(t => t.MailBody).HasColumnName("MailBody");
            this.Property(t => t.IsSent).HasColumnName("IsSent");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SendDate).HasColumnName("SendDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.MessageMailMaster)
                .WithMany(t => t.MailInfoes)
                .HasForeignKey(d => d.MailMasterId);

        }
    }
}
