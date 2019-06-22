using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class AgentContentMap : EntityTypeConfiguration<AgentContent>
    {
        public AgentContentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.SetBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AgentContent");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.ContentID).HasColumnName("ContentID");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
        }
    }
}
