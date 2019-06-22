using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.ResidentialAddress)
                .HasMaxLength(200);

            this.Property(t => t.PermanentAddress)
                .HasMaxLength(200);

            this.Property(t => t.Telephone)
                .HasMaxLength(50);

            this.Property(t => t.Mobile)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.NID)
                .HasMaxLength(50);

            this.Property(t => t.Passport)
                .HasMaxLength(50);

            this.Property(t => t.FatherName)
                .HasMaxLength(200);

            this.Property(t => t.MotherName)
                .HasMaxLength(200);

            this.Property(t => t.Nationality)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.SetBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Address");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ResidentialAddress).HasColumnName("ResidentialAddress");
            this.Property(t => t.ResidentialUpazilaID).HasColumnName("ResidentialUpazilaID");
            this.Property(t => t.PermanentAddress).HasColumnName("PermanentAddress");
            this.Property(t => t.PermanentUpazilaID).HasColumnName("PermanentUpazilaID");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.NID).HasColumnName("NID");
            this.Property(t => t.Passport).HasColumnName("Passport");
            this.Property(t => t.FatherName).HasColumnName("FatherName");
            this.Property(t => t.MotherName).HasColumnName("MotherName");
            this.Property(t => t.Nationality).HasColumnName("Nationality");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.SetDate).HasColumnName("SetDate");

            // Relationships
            this.HasOptional(t => t.Country)
                .WithMany(t => t.Addresses)
                .HasForeignKey(d => d.CountryID);
            this.HasOptional(t => t.Upazila)
                .WithMany(t => t.Addresses)
                .HasForeignKey(d => d.ResidentialUpazilaID);
            this.HasOptional(t => t.Upazila1)
                .WithMany(t => t.Addresses1)
                .HasForeignKey(d => d.PermanentUpazilaID);

        }
    }
}
