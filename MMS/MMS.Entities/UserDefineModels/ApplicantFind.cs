using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.UserDefineModels
{
    public class ApplicantFind
    {



        public int AgentId { get; set; }
        public string AgentAutoId { get; set; }
        public string AgentMobileNo { get; set; }
        public string AgentName { get; set; }
        public int ApplicantId { get; set; }

        public string ApplicantAutoId { get; set; }
        public string PreRefApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantMobileNo { get; set; }

        public string PassportNo { get; set; }
        public DateTime PassportIssueDate { get; set; }
        public DateTime PassportExpiryDate { get; set; }
        public string FathersName { get; set; }
        public string Description { get; set; }

        public string Comments1 { get; set; }

        public string Comments2 { get; set; }

        public string Comments3 { get; set; }

        public string Comments4 { get; set; }

        public string MothersName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ResidentialAddress { get; set; }


        public string DivisionName { get; set; }
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public int ResidentialUpazilaID { get; set; }
        public string ResidentialUpazilaName { get; set; }
        public string PermanentAddress { get; set; }
        public int PermanentUpazilaID { get; set; }

        public string PermanentUpazilaName { get; set; }
        public string NID { get; set; }

        public int CountryID { get; set; }
        public string GoingCountryName { get; set; }
        public int ApplicantTypeID { get; set; }
        public string ApplicantTypeName { get; set; }
        public string RefName { get; set; }
        public string RefPhoneNo { get; set; }
        public string VisaNo { get; set; }
        public int VisaTypeID { get; set; }
        public string IsReceivedComplete { get; set; }

        public DateTime ReceivedDate { get; set; }

        public int CurrentLocationID { get; set; }
        public string CurrentLocationName { get; set; }

        public DateTime CurrentLocationSentDate { get; set; }

        public int NextLocationID { get; set; }
        public string NextLocationName { get; set; }

        public Decimal PercentageOfComplete { get; set; }
     
        public int MoveLocationID { get; set; }
        public string MoveLocationName { get; set; }
        public DateTime ProbableMoveDate { get; set; }
        public DateTime ActualMoveDate { get; set; }
        public DateTime ExpectedReceiveDate { get; set; }
        public DateTime ActualReceiveDate { get; set; }
        public string AppMovDescription { get; set; }
        public Decimal AppMovPercentageOfComplete { get; set; }
        public string MovementResultName { get; set; }
        public string IsLocationSuccessfullyCompleted { get; set; }






    }
}
