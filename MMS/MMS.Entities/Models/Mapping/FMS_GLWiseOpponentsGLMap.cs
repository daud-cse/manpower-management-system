using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class FMS_GLWiseOpponentsGLMap : EntityTypeConfiguration<FMS_GLWiseOpponentsGL>
    {
        public FMS_GLWiseOpponentsGLMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("FMS_GLWiseOpponentsGL");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.GLAccountId).HasColumnName("GLAccountId");
            this.Property(t => t.OpponentsGLId).HasColumnName("OpponentsGLId");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasOptional(t => t.FMS_GLAccount)
                .WithMany(t => t.FMS_GLWiseOpponentsGL)
                .HasForeignKey(d => d.GLAccountId);
            this.HasOptional(t => t.FMS_GLAccount1)
                .WithMany(t => t.FMS_GLWiseOpponentsGL1)
                .HasForeignKey(d => d.OpponentsGLId);

        }
    }
}
