using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class UpazilaMap : EntityTypeConfiguration<Upazila>
    {
        public UpazilaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Upazila");
            this.Property(t => t.DistrictID).HasColumnName("DistrictID");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");

            // Relationships
            this.HasOptional(t => t.District)
                .WithMany(t => t.Upazilas)
                .HasForeignKey(d => d.DistrictID);

        }
    }
}
