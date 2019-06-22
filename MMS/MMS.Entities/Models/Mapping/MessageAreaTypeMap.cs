using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class MessageAreaTypeMap : EntityTypeConfiguration<MessageAreaType>
    {
        public MessageAreaTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MessageAreaName)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("MessageAreaType");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MessageAreaName).HasColumnName("MessageAreaName");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
        }
    }
}
