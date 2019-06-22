using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Entities.Models.Mapping
{
    public class ApplicantMap : EntityTypeConfiguration<Applicant>
    {
        public ApplicantMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ApplicantId)
                .HasMaxLength(10);

            this.Property(t => t.PreRefApplicantId)
                .HasMaxLength(20);

            this.Property(t => t.PassportNo)
                .IsRequired()
                .HasMaxLength(9);

            this.Property(t => t.FathersName)
                .HasMaxLength(100);

            this.Property(t => t.MothersName)
                .HasMaxLength(100);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ResidentialAddress)
                .HasMaxLength(200);

            this.Property(t => t.PermanentAddress)
                .HasMaxLength(200);

            this.Property(t => t.NID)
                .HasMaxLength(20);

            this.Property(t => t.ApplicantPhoneNo)
                .HasMaxLength(20);

            this.Property(t => t.RefName)
                .HasMaxLength(100);

            this.Property(t => t.RefPhoneNo)
                .HasMaxLength(20);

            this.Property(t => t.VisaNo)
                .HasMaxLength(100);

            this.Property(t => t.CurrentLocationName)
                .HasMaxLength(100);

            this.Property(t => t.Comments1)
                .HasMaxLength(500);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.Comments2)
                .HasMaxLength(500);

            this.Property(t => t.Comments3)
                .HasMaxLength(500);

            this.Property(t => t.Comments4)
                .HasMaxLength(500);

            this.Property(t => t.SetBy)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Applicant");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ApplicantId).HasColumnName("ApplicantId");
            this.Property(t => t.ApplicantPhotoID).HasColumnName("ApplicantPhotoID");
            this.Property(t => t.PreRefApplicantId).HasColumnName("PreRefApplicantId");
            this.Property(t => t.LotID).HasColumnName("LotID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.AddressID).HasColumnName("AddressID");
            this.Property(t => t.PassportNo).HasColumnName("PassportNo");
            this.Property(t => t.PassportIssueDate).HasColumnName("PassportIssueDate");
            this.Property(t => t.PassportExpiryDate).HasColumnName("PassportExpiryDate");
            this.Property(t => t.FathersName).HasColumnName("FathersName");
            this.Property(t => t.MothersName).HasColumnName("MothersName");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DateOfBirth).HasColumnName("DateOfBirth");
            this.Property(t => t.ResidentialAddress).HasColumnName("ResidentialAddress");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.DistrictID).HasColumnName("DistrictID");
            this.Property(t => t.ResidentialUpazilaID).HasColumnName("ResidentialUpazilaID");
            this.Property(t => t.PermanentAddress).HasColumnName("PermanentAddress");
            this.Property(t => t.PermanentUpazilaID).HasColumnName("PermanentUpazilaID");
            this.Property(t => t.NID).HasColumnName("NID");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.ApplicantTypeID).HasColumnName("ApplicantTypeID");
            this.Property(t => t.ApplicantPhoneNo).HasColumnName("ApplicantPhoneNo");
            this.Property(t => t.RefName).HasColumnName("RefName");
            this.Property(t => t.RefPhoneNo).HasColumnName("RefPhoneNo");
            this.Property(t => t.VisaNo).HasColumnName("VisaNo");
            this.Property(t => t.VisaTypeID).HasColumnName("VisaTypeID");
            this.Property(t => t.IsMultipleEntry).HasColumnName("IsMultipleEntry");
            this.Property(t => t.CheckListGroupID).HasColumnName("CheckListGroupID");
            this.Property(t => t.IsCheckListCompliant).HasColumnName("IsCheckListCompliant");
            this.Property(t => t.EntryDate).HasColumnName("EntryDate");
            this.Property(t => t.IsReceivedCompleted).HasColumnName("IsReceivedCompleted");
            this.Property(t => t.ReceivedDate).HasColumnName("ReceivedDate");
            this.Property(t => t.CurrentLocationID).HasColumnName("CurrentLocationID");
            this.Property(t => t.CurrentLocationName).HasColumnName("CurrentLocationName");
            this.Property(t => t.CurrentLocationSentDate).HasColumnName("CurrentLocationSentDate");
            this.Property(t => t.NextLocationID).HasColumnName("NextLocationID");
            this.Property(t => t.PercentageOfComplete).HasColumnName("PercentageOfComplete");
            this.Property(t => t.Comments1).HasColumnName("Comments1");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Comments2).HasColumnName("Comments2");
            this.Property(t => t.Comments3).HasColumnName("Comments3");
            this.Property(t => t.Comments4).HasColumnName("Comments4");
            this.Property(t => t.OpeningBalance).HasColumnName("OpeningBalance");
            this.Property(t => t.DrAmount).HasColumnName("DrAmount");
            this.Property(t => t.CrAmount).HasColumnName("CrAmount");
            this.Property(t => t.CurrentBalance).HasColumnName("CurrentBalance");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.GlobalCompanyId).HasColumnName("GlobalCompanyId");

            // Relationships
            this.HasRequired(t => t.Agent)
                .WithMany(t => t.Applicants)
                .HasForeignKey(d => d.AgentID);
            this.HasRequired(t => t.ApplicantType)
                .WithMany(t => t.Applicants)
                .HasForeignKey(d => d.ApplicantTypeID);
            this.HasRequired(t => t.Country)
                .WithMany(t => t.Applicants)
                .HasForeignKey(d => d.CountryID);
            this.HasRequired(t => t.Location)
                .WithMany(t => t.Applicants)
                .HasForeignKey(d => d.CurrentLocationID);
            this.HasRequired(t => t.Location1)
                .WithMany(t => t.Applicants1)
                .HasForeignKey(d => d.NextLocationID);
            this.HasOptional(t => t.ApplicantFileLot)
                .WithMany(t => t.Applicants)
                .HasForeignKey(d => d.LotID);
            this.HasOptional(t => t.CheckListGroup)
                .WithMany(t => t.Applicants)
                .HasForeignKey(d => d.CheckListGroupID);

        }
    }
}
