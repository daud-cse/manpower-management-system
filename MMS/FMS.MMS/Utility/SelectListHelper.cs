using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSU.web.Utility
{
    public static class SelectListHelper
    {
        public static SelectList ToSelectList<T>(this List<T> tbl, object selectedValue, string valueField, string textField)
        {
            if (selectedValue == null)
            {
                return new SelectList(tbl, valueField, textField);                
            }
            
            return new SelectList(tbl, valueField, textField, selectedValue);
            
        }

        public static string ToSupervisor(this string pin)
        {
            return "98610";
        }

        public static SelectList ToSelectList<T>(this IEnumerable<T> tbl, object selectedValue, string valueField, string textField)
        {
            if (selectedValue == null)
            {
                return new SelectList(tbl, valueField, textField); 
            }

            return new SelectList(tbl, valueField, textField, selectedValue);
        }
    }
}