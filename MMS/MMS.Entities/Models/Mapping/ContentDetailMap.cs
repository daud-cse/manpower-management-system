using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class ContentDetailMap : EntityTypeConfiguration<ContentDetail>
    {
        public ContentDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(500);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.Object)
                .IsRequired();

            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.FileExtension)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("ContentDetails");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ContentId).HasColumnName("ContentId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Object).HasColumnName("Object");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileExtension).HasColumnName("FileExtension");
            this.Property(t => t.ContentTypeID).HasColumnName("ContentTypeID");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasOptional(t => t.Content)
                .WithMany(t => t.ContentDetails)
                .HasForeignKey(d => d.ContentId);

        }
    }
}
