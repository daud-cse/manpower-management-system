using MMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.ViewModels
{
    public class SearchPassportInfoVM
    {
        [Display(Name = "Customer Name")]
        public int? CustomerId { get; set; }
        public List<KeyValuePair<int, string>> kvpCustomer { get; set; }

        //[Required(ErrorMessage = "The Customer Name field is required.")]
       
        public List<KeyValuePair<int, string>> kvpCountry { get; set; }

        [Display(Name = "Country")]
        public int? CountryId { get; set; }
        [Display(Name = "Passport No")]
        public string PassportNo { get; set; }
         [Display(Name = "Delivery Slip No")]
        public string DeliverySlipNo { get; set; }
        [Display(Name = "Passport Type Name")]
         public int? PassPortTypeId { get; set; }
         public List<KeyValuePair<int, string>> kvpPassPortType { get; set; }
        [Display(Name = "RPO Name")]
         public int? RPOTypeId { get; set; }
         public List<KeyValuePair<int, string>> kvpRPOType { get; set; }
         public List<KeyValuePair<int, string>> kvpSex { get; set; }
         [Display(Name = "Sex")]
         public int? SexId { get; set; }
         public List<KeyValuePair<int, string>> kvpMaritalStatus { get; set; }
         [Display(Name = "Marital Status")]
         public int? MaritalStatusId { get; set; }
        public List<KeyValuePair<int, string>> kvpDistrict { get; set; }


        [Display(Name = "District")]
        public int? DistrictId { get; set; }
        public List<KeyValuePair<int, string>> kvpDivision { get; set; }


        [Display(Name = "Division")]
        public int? DivisionId { get; set; }

        public List<KeyValuePair<int, string>> kvpUpazila { get; set; }

        [Display(Name = "Upazila")]
        public int? UpazilaId { get; set; }
        public List<KeyValuePair<int, string>> kvpNationality { get; set; }

        [Display(Name = "Nationality Name")]
        public int? NationalityId { get; set; }




        public List<PassportInfo> lstpassportInfo { get; set; }
    }
}
