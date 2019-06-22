using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using MMS.Entities.Models;

namespace MMS.web.Utility
{
    //public class DateRange : ValidationAttribute
    //{
    //    public string StartDate { get; set; }
    //    public string EndDate { get; set; }

    //    public DateRange()
    //    {
    //        //this.StartDate = new DateTime(1900, 1, 1).ToString();
    //        //this.EndDate = new DateTime(2099, 1, 1).ToString();
    //        this.StartDate = new DateTime(1900, 1, 1).ToString();
    //        this.EndDate = new DateTime(2099, 1, 1).ToString();
    //    }

    //    public override bool IsValid(object value, ValidationContext validationContext)
    //    {
    //        var valueToString = value as string;
           
            
    //       // validationContext.

    //        if (!string.IsNullOrEmpty(valueToString))
    //        {
    //            DateTime dateTimeResult;

    //            if (DateTime.TryParse(valueToString, out dateTimeResult))
    //            {
    //                return ((dateTimeResult >= DateTime.Parse(this.StartDate)) && (dateTimeResult <= DateTime.Parse(this.EndDate)));
    //            }

    //            return false;
    //        }
    //        return true;
    //    }
    //}


    public class ProjectDateAttribute : ValidationAttribute
    {



        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{

        //   // validationContext.ObjectType.GetMembers();
        //    ProjectsYear projectsYear = (ProjectsYear)validationContext.ObjectInstance;
        //    //if(value.){

        //    //}
        //    DateTime StartDate = projectsYear.Project.StartDate;
        //    DateTime EndDate = projectsYear.Project.EndDate;

        //    if (StartDate > EndDate)
        //    {


        //      //  return validationContext.
        //        return new ValidationResult("Message can't be more than 160 charecter");
                
        //    }
        //    else
        //    {
        //        return new ValidationResult("Message can't be more than 160 charecter");
        //    }
        //}
    }

    public class IfPresent : ValidationAttribute
    {
        private ValidationAttribute attr;
        public IfPresent(Type attr)
        {
            this.attr = (ValidationAttribute)Activator.CreateInstance(attr);
        }
        public override bool IsValid(object value)
        {

            if (value == null)
                return true;
            return attr.IsValid(value);
        }
    }

    public class MessageValidation : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value.ToString().Length <= 160)
            {

                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Message can't be more than 160 charecter");
            }
        }
    }

    public class CellNumberValidation : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<string> lstCellNumbers = value.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            bool IsVaid = true;
            List<string> ValidOperator = new List<string> { "017", "018", "011", "016","019" };
            Regex mobilePattern = new Regex(@"^[0-9]\d{10}$");
            string returnText = string.Empty;
            //lstCellNumbers.ForEach(delegate(string item)
            foreach (string item in lstCellNumbers)
            {
                string Operator = item.Substring(0, 3);

                if (ValidOperator.Exists(z => z.Substring(0,3) == Operator))
                {
                    if (!mobilePattern.IsMatch(item))
                    {
                        IsVaid = false;

                        returnText = FormatErrorMessage(item + " is not a valid mobile no");
                        break;
                    }
                }
                else
                {
                    IsVaid = false;
                    returnText = FormatErrorMessage(item + " not a is not valid mobile operator");
                    break;
                }
            };

            if (IsVaid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(returnText);
            }

            /*
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            Object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (proprtyvalue.ToString() == DesiredValue.ToString())
            {
                if (value == null)
                {
                    return   new ValidationResult("required");
                }

            }*/

        }
    }

    public class FirstNameValidator : ValidationAttribute
    {
        private int maxlength = 20;
        private Object DesiredValue { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value != null)
            {
                string UserFirstName = value.ToString();
                if (UserFirstName.Count() > maxlength)
                {
                    return new ValidationResult("First name exceed 20 letter");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return new ValidationResult("First name is mandatory");
            }

        }
    }


    public class ExcludeChar : ValidationAttribute
    {
        private readonly string _chars;

        public ExcludeChar(string chars)
            : base("{0} contains invalid character.")
        {
            _chars = chars;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                for (int i = 0; i < _chars.Length; i++)
                {
                    var valueAsString = value.ToString();
                    if (valueAsString.Contains(_chars[i]))
                    {
                        var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                        return new ValidationResult(errorMessage);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }

}
