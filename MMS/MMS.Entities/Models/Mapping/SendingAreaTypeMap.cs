using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class SendingAreaTypeMap : EntityTypeConfiguration<SendingAreaType>
    {
        public SendingAreaTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SendingAreaName)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("SendingAreaType");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SendingAreaName).HasColumnName("SendingAreaName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
