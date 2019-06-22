using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class EmployeeInfoMap : EntityTypeConfiguration<EmployeeInfo>
    {
        public EmployeeInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.EmployeeAutoId)
                .HasMaxLength(10);

            this.Property(t => t.PIN)
                .HasMaxLength(10);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.MobileNo)
                .HasMaxLength(15);

            this.Property(t => t.NID)
                .HasMaxLength(20);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.PassportNo)
                .HasMaxLength(9);

            this.Property(t => t.FathersName)
                .HasMaxLength(100);

            this.Property(t => t.MothersName)
                .HasMaxLength(100);

            this.Property(t => t.ResidentialAddress)
                .HasMaxLength(200);

            this.Property(t => t.PermanentAddress)
                .HasMaxLength(200);

            this.Property(t => t.SetBy)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("EmployeeInfo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.EmployeeAutoId).HasColumnName("EmployeeAutoId");
            this.Property(t => t.PIN).HasColumnName("PIN");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DesignationId).HasColumnName("DesignationId");
            this.Property(t => t.MobileNo).HasColumnName("MobileNo");
            this.Property(t => t.NID).HasColumnName("NID");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.PassportNo).HasColumnName("PassportNo");
            this.Property(t => t.FathersName).HasColumnName("FathersName");
            this.Property(t => t.MothersName).HasColumnName("MothersName");
            this.Property(t => t.DateOfBirth).HasColumnName("DateOfBirth");
            this.Property(t => t.Salary).HasColumnName("Salary");
            this.Property(t => t.OpeningBalance).HasColumnName("OpeningBalance");
            this.Property(t => t.DrAmount).HasColumnName("DrAmount");
            this.Property(t => t.CrAmount).HasColumnName("CrAmount");
            this.Property(t => t.CurrentBalance).HasColumnName("CurrentBalance");
            this.Property(t => t.ResidentialAddress).HasColumnName("ResidentialAddress");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.DistrictID).HasColumnName("DistrictID");
            this.Property(t => t.ResidentialUpazilaID).HasColumnName("ResidentialUpazilaID");
            this.Property(t => t.PermanentAddress).HasColumnName("PermanentAddress");
            this.Property(t => t.PermanentUpazilaID).HasColumnName("PermanentUpazilaID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.NationalityID).HasColumnName("NationalityID");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.Country)
                .WithMany(t => t.EmployeeInfoes)
                .HasForeignKey(d => d.CountryID);
            this.HasRequired(t => t.Designation)
                .WithMany(t => t.EmployeeInfoes)
                .HasForeignKey(d => d.DesignationId);
            this.HasRequired(t => t.District)
                .WithMany(t => t.EmployeeInfoes)
                .HasForeignKey(d => d.DistrictID);
            this.HasRequired(t => t.Division)
                .WithMany(t => t.EmployeeInfoes)
                .HasForeignKey(d => d.DivisionID);
            this.HasRequired(t => t.Nationality)
                .WithMany(t => t.EmployeeInfoes)
                .HasForeignKey(d => d.NationalityID);
            this.HasRequired(t => t.Upazila)
                .WithMany(t => t.EmployeeInfoes)
                .HasForeignKey(d => d.PermanentUpazilaID);
            this.HasOptional(t => t.Upazila1)
                .WithMany(t => t.EmployeeInfoes1)
                .HasForeignKey(d => d.ResidentialUpazilaID);

        }
    }
}
