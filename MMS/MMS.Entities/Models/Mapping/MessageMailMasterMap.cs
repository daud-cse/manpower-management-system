using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class MessageMailMasterMap : EntityTypeConfiguration<MessageMailMaster>
    {
        public MessageMailMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.AutoId)
                .HasMaxLength(10);

            this.Property(t => t.MessageBody)
                .IsRequired()
                .HasMaxLength(160);

            this.Property(t => t.MailBody)
                .IsRequired();

            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("MessageMailMaster");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AutoId).HasColumnName("AutoId");
            this.Property(t => t.SendingAreaTypeId).HasColumnName("SendingAreaTypeId");
            this.Property(t => t.MessageAreaTypeId).HasColumnName("MessageAreaTypeId");
            this.Property(t => t.MessageBody).HasColumnName("MessageBody");
            this.Property(t => t.MailBody).HasColumnName("MailBody");
            this.Property(t => t.SendDate).HasColumnName("SendDate");
            this.Property(t => t.IsCompleted).HasColumnName("IsCompleted");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.MessageAreaType)
                .WithMany(t => t.MessageMailMasters)
                .HasForeignKey(d => d.MessageAreaTypeId);
            this.HasRequired(t => t.SendingAreaType)
                .WithMany(t => t.MessageMailMasters)
                .HasForeignKey(d => d.SendingAreaTypeId);

        }
    }
}
