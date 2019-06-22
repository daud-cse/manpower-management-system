using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.CustomerId)
                .HasMaxLength(10);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.ResidentialAddress)
                .HasMaxLength(500);

            this.Property(t => t.PermanentAddress)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Telephone)
                .HasMaxLength(50);

            this.Property(t => t.Mobile)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.NID)
                .HasMaxLength(50);

            this.Property(t => t.PassportNo)
                .HasMaxLength(50);

            this.Property(t => t.FatherName)
                .HasMaxLength(200);

            this.Property(t => t.MotherName)
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.SetBy)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Customer");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CustomerTypeId).HasColumnName("CustomerTypeId");
            this.Property(t => t.ResidentialAddress).HasColumnName("ResidentialAddress");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.DistrictID).HasColumnName("DistrictID");
            this.Property(t => t.ResidentialUpazilaID).HasColumnName("ResidentialUpazilaID");
            this.Property(t => t.PermanentAddress).HasColumnName("PermanentAddress");
            this.Property(t => t.PermanentUpazilaID).HasColumnName("PermanentUpazilaID");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.NID).HasColumnName("NID");
            this.Property(t => t.ContentIdForImage).HasColumnName("ContentIdForImage");
            this.Property(t => t.PassportNo).HasColumnName("PassportNo");
            this.Property(t => t.FatherName).HasColumnName("FatherName");
            this.Property(t => t.MotherName).HasColumnName("MotherName");
            this.Property(t => t.NationalityID).HasColumnName("NationalityID");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.OpeningBalance).HasColumnName("OpeningBalance");
            this.Property(t => t.DrAmount).HasColumnName("DrAmount");
            this.Property(t => t.CrAmount).HasColumnName("CrAmount");
            this.Property(t => t.CurrentBalance).HasColumnName("CurrentBalance");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.Country)
                .WithMany(t => t.Customers)
                .HasForeignKey(d => d.CountryID);
            this.HasRequired(t => t.CustomerType)
                .WithMany(t => t.Customers)
                .HasForeignKey(d => d.CustomerTypeId);
            this.HasRequired(t => t.District)
                .WithMany(t => t.Customers)
                .HasForeignKey(d => d.DistrictID);
            this.HasRequired(t => t.Division)
                .WithMany(t => t.Customers)
                .HasForeignKey(d => d.DivisionID);
            this.HasOptional(t => t.Upazila)
                .WithMany(t => t.Customers)
                .HasForeignKey(d => d.ResidentialUpazilaID);
            this.HasRequired(t => t.Upazila1)
                .WithMany(t => t.Customers1)
                .HasForeignKey(d => d.PermanentUpazilaID);

        }
    }
}
