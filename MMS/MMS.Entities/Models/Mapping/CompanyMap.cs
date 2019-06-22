using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ComapnyId)
                .HasMaxLength(10);

            this.Property(t => t.LicenseNo)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Address)
                .HasMaxLength(200);

            this.Property(t => t.Telephone)
                .HasMaxLength(50);

            this.Property(t => t.Mobile)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NID)
                .HasMaxLength(50);

            this.Property(t => t.DelegatePersonName)
                .HasMaxLength(100);

            this.Property(t => t.DelegateMobileNo)
                .HasMaxLength(20);

            this.Property(t => t.DelegateAbroadMobileNo)
                .HasMaxLength(20);

            this.Property(t => t.DelegateBangladeshiAddress)
                .HasMaxLength(1000);

            this.Property(t => t.DelegateAbroadAddress)
                .HasMaxLength(1000);

            this.Property(t => t.DelegateEmailAddress)
                .HasMaxLength(100);

            this.Property(t => t.SetBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Company");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ComapnyId).HasColumnName("ComapnyId");
            this.Property(t => t.CompanyTypeID).HasColumnName("CompanyTypeID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.DistrictID).HasColumnName("DistrictID");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.NationalityID).HasColumnName("NationalityID");
            this.Property(t => t.LicenseNo).HasColumnName("LicenseNo");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.NID).HasColumnName("NID");
            this.Property(t => t.DelegatePersonName).HasColumnName("DelegatePersonName");
            this.Property(t => t.DelegateMobileNo).HasColumnName("DelegateMobileNo");
            this.Property(t => t.DelegateAbroadMobileNo).HasColumnName("DelegateAbroadMobileNo");
            this.Property(t => t.DelegateBangladeshiAddress).HasColumnName("DelegateBangladeshiAddress");
            this.Property(t => t.DelegateAbroadAddress).HasColumnName("DelegateAbroadAddress");
            this.Property(t => t.DelegateEmailAddress).HasColumnName("DelegateEmailAddress");
            this.Property(t => t.OpeningBalance).HasColumnName("OpeningBalance");
            this.Property(t => t.DrAmount).HasColumnName("DrAmount");
            this.Property(t => t.CrAmount).HasColumnName("CrAmount");
            this.Property(t => t.CurrentBalance).HasColumnName("CurrentBalance");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.CompanyType)
                .WithMany(t => t.Companies)
                .HasForeignKey(d => d.CompanyTypeID);
            this.HasRequired(t => t.Country)
                .WithMany(t => t.Companies)
                .HasForeignKey(d => d.CountryID);
            this.HasOptional(t => t.District)
                .WithMany(t => t.Companies)
                .HasForeignKey(d => d.DistrictID);
            this.HasOptional(t => t.Division)
                .WithMany(t => t.Companies)
                .HasForeignKey(d => d.DivisionID);
            this.HasRequired(t => t.Nationality)
                .WithMany(t => t.Companies)
                .HasForeignKey(d => d.NationalityID);

        }
    }
}
