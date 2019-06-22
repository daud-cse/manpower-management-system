using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class GlobalCompanyMap : EntityTypeConfiguration<GlobalCompany>
    {
        public GlobalCompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.GlobalCompanyId);

            // Properties
            this.Property(t => t.GlobalCompanyId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.GlobalCompanyName)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Code)
                .HasMaxLength(128);

            this.Property(t => t.Url)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.FacebookUrl)
                .HasMaxLength(256);

            this.Property(t => t.TwitterUrl)
                .HasMaxLength(256);

            this.Property(t => t.GoogleUrl)
                .HasMaxLength(256);

            this.Property(t => t.LinkedinUrl)
                .HasMaxLength(256);

            this.Property(t => t.Email)
                .HasMaxLength(128);

            this.Property(t => t.Contact)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("GlobalCompany");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");
            this.Property(t => t.GlobalCompanyName).HasColumnName("GlobalCompanyName");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.PackageId).HasColumnName("PackageId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.latitude).HasColumnName("latitude");
            this.Property(t => t.longitude).HasColumnName("longitude");
            this.Property(t => t.SeoText).HasColumnName("SeoText");
            this.Property(t => t.WelComeText).HasColumnName("WelComeText");
            this.Property(t => t.ContactText).HasColumnName("ContactText");
            this.Property(t => t.FacebookUrl).HasColumnName("FacebookUrl");
            this.Property(t => t.TwitterUrl).HasColumnName("TwitterUrl");
            this.Property(t => t.GoogleUrl).HasColumnName("GoogleUrl");
            this.Property(t => t.LinkedinUrl).HasColumnName("LinkedinUrl");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.UsefulLinkText).HasColumnName("UsefulLinkText");
            this.Property(t => t.GlobalCountryId).HasColumnName("GlobalCountryId");
            this.Property(t => t.GlobalDivisionId).HasColumnName("GlobalDivisionId");
            this.Property(t => t.GlobalDistrictId).HasColumnName("GlobalDistrictId");
            this.Property(t => t.GlobalSubDistrictId).HasColumnName("GlobalSubDistrictId");
            this.Property(t => t.VisitorToday).HasColumnName("VisitorToday");
            this.Property(t => t.VisitorThisMonth).HasColumnName("VisitorThisMonth");
            this.Property(t => t.VisitorTotal).HasColumnName("VisitorTotal");
            this.Property(t => t.GlobalInstituteTypeId).HasColumnName("GlobalInstituteTypeId");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Contact).HasColumnName("Contact");
            this.Property(t => t.IsSMSCostPayByReceipient).HasColumnName("IsSMSCostPayByReceipient");
            this.Property(t => t.IsAuthenticationSMSCostPayByReceipient).HasColumnName("IsAuthenticationSMSCostPayByReceipient");
            this.Property(t => t.RestSMSCount).HasColumnName("RestSMSCount");
            this.Property(t => t.MenuHtml).HasColumnName("MenuHtml");
            this.Property(t => t.CssOverwrite).HasColumnName("CssOverwrite");
        }
    }
}
