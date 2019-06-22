using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.SECURITY.Utility
{
    public class Utility
    {
        public IEnumerable<KeyValuePair<int, string>> RequsitionTypes()
        {
            IEnumerable<KeyValuePair<int, string>> ReqType = new[] 
            {
                new KeyValuePair<int,string>(1,"Stock In"),
                new KeyValuePair<int,string>(2,"Stock Out"),
                new KeyValuePair<int,string>(3,"Stock Return"),
                new KeyValuePair<int,string>(3,"Stock Transfer")
            };
            return ReqType;
        }
        public IEnumerable<KeyValuePair<int, string>> YearsListForRangeofProjectYears(int years)
        {

            switch (years)
            {
                case 1:
                    {
                        IEnumerable<KeyValuePair<int, string>> YearsList = new[] 
                       {
                        new KeyValuePair<int,string>(1,"1st"),
                       };
                        return YearsList;
                    }
                case 2:
                    {
                        IEnumerable<KeyValuePair<int, string>> YearsList = new[] 
                       {
                        new KeyValuePair<int,string>(1,"1st"),
                        new KeyValuePair<int,string>(2,"2nd"),
                       };
                        return YearsList;
                    }

                case 3:
                    {
                        IEnumerable<KeyValuePair<int, string>> YearsList = new[] 
                       {
                        new KeyValuePair<int,string>(1,"1st"),
                        new KeyValuePair<int,string>(2,"2nd"),
                        new KeyValuePair<int,string>(3,"3rd"),
                       };
                        return YearsList;
                    }

                case 4:
                    {
                        IEnumerable<KeyValuePair<int, string>> YearsList = new[] 
                       {
                        new KeyValuePair<int,string>(1,"1st"),
                        new KeyValuePair<int,string>(2,"2nd"),
                        new KeyValuePair<int,string>(3,"3rd"),
                        new KeyValuePair<int,string>(4,"4th"),
                       };
                        return YearsList;
                    }
                case 5:
                    {
                        IEnumerable<KeyValuePair<int, string>> YearsList = new[] 
                       {
                        new KeyValuePair<int,string>(1,"1st"),
                        new KeyValuePair<int,string>(2,"2nd"),
                        new KeyValuePair<int,string>(3,"3rd"),
                        new KeyValuePair<int,string>(4,"4th"),
                        new KeyValuePair<int,string>(5,"5th"),
                       };
                        return YearsList;
                    }

                case 6:
                    {
                        IEnumerable<KeyValuePair<int, string>> YearsList = new[] 
                       {
                        new KeyValuePair<int,string>(1,"1st"),
                        new KeyValuePair<int,string>(2,"2nd"),
                        new KeyValuePair<int,string>(3,"3rd"),
                        new KeyValuePair<int,string>(4,"4th"),
                        new KeyValuePair<int,string>(5,"5th"),
                        new KeyValuePair<int,string>(6,"6th"),
                       };
                        return YearsList;
                    }
                case 7:
                    {
                        IEnumerable<KeyValuePair<int, string>> YearsList = new[] 
                       {
                        new KeyValuePair<int,string>(1,"1st"),
                        new KeyValuePair<int,string>(2,"2nd"),
                        new KeyValuePair<int,string>(3,"3rd"),
                        new KeyValuePair<int,string>(4,"4th"),
                        new KeyValuePair<int,string>(5,"5th"),
                        new KeyValuePair<int,string>(6,"6th"),
                        new KeyValuePair<int,string>(7,"7th")
                       };
                        return YearsList;
                    }
                default:
                    {
                        IEnumerable<KeyValuePair<int, string>> YearsList = new[] 
                       {
                        new KeyValuePair<int,string>(1,"1st"),
                        new KeyValuePair<int,string>(2,"2nd"),
                        new KeyValuePair<int,string>(3,"3rd"),
                        new KeyValuePair<int,string>(4,"4th"),
                        new KeyValuePair<int,string>(5,"5th"),
                        new KeyValuePair<int,string>(6,"6th"),
                        new KeyValuePair<int,string>(7,"7th")
                       };
                        return YearsList;
                    }

            }

        }
        public IEnumerable<KeyValuePair<int, string>> YearsListForFD6(int yearsCompleted, int years)
        {
            IEnumerable<KeyValuePair<int, string>> YearsList=new[]{ new KeyValuePair<int,string>()};
        
               if(yearsCompleted==1)
                    {
                       YearsList = new[] 
                       {
                       // new KeyValuePair<int,string>(1,"1st"),
                        new KeyValuePair<int,string>(2,"2nd"),
                        new KeyValuePair<int,string>(3,"3rd"),
                        new KeyValuePair<int,string>(4,"4th"),
                        new KeyValuePair<int,string>(5,"5th"),
                        new KeyValuePair<int,string>(6,"6th"),
                        new KeyValuePair<int,string>(7,"7th")
                       };
                   
                       // return YearsList;
                    }
               else if(yearsCompleted==2)
               {
                      YearsList = new[] 
                       {
                        //new KeyValuePair<int,string>(1,"1st"),
                       // new KeyValuePair<int,string>(2,"2nd"),
                        new KeyValuePair<int,string>(3,"3rd"),
                        new KeyValuePair<int,string>(4,"4th"),
                        new KeyValuePair<int,string>(5,"5th"),
                        new KeyValuePair<int,string>(6,"6th"),
                        new KeyValuePair<int,string>(7,"7th")
                       };
                      //  return YearsList;
                    }

                else if(yearsCompleted==3)
               {
                         YearsList = new[] 
                       {
                       // new KeyValuePair<int,string>(1,"1st"),
                       // new KeyValuePair<int,string>(2,"2nd"),
                        //new KeyValuePair<int,string>(3,"3rd"),
                        new KeyValuePair<int,string>(4,"4th"),
                        new KeyValuePair<int,string>(5,"5th"),
                        new KeyValuePair<int,string>(6,"6th"),
                        new KeyValuePair<int,string>(7,"7th")
                       };
                       // return YearsList;
                    }

                else if(yearsCompleted==4)
               {
                    
                        YearsList = new[] 
                       {
                        //new KeyValuePair<int,string>(1,"1st"),
                        //new KeyValuePair<int,string>(2,"2nd"),
                        //new KeyValuePair<int,string>(3,"3rd"),
                      //  new KeyValuePair<int,string>(4,"4th"),
                        new KeyValuePair<int,string>(5,"5th"),
                        new KeyValuePair<int,string>(6,"6th"),
                        new KeyValuePair<int,string>(7,"7th")
                       };
                       // return YearsList;
                    }
              else if(yearsCompleted==5)
               {
                        YearsList = new[] 
                       {
                        //new KeyValuePair<int,string>(1,"1st"),
                        //new KeyValuePair<int,string>(2,"2nd"),
                        //new KeyValuePair<int,string>(3,"3rd"),
                        //new KeyValuePair<int,string>(4,"4th"),
                       // new KeyValuePair<int,string>(5,"5th"),
                        new KeyValuePair<int,string>(6,"6th"),
                        new KeyValuePair<int,string>(7,"7th")
                       };
                       // return YearsList;
                    }

                else if(yearsCompleted==6)
               {
                        YearsList = new[] 
                       {
                        //new KeyValuePair<int,string>(1,"1st"),
                        //new KeyValuePair<int,string>(2,"2nd"),
                        //new KeyValuePair<int,string>(3,"3rd"),
                        //new KeyValuePair<int,string>(4,"4th"),
                        //new KeyValuePair<int,string>(5,"5th"),
                        //new KeyValuePair<int,string>(6,"6th"),
                        new KeyValuePair<int,string>(7,"7th")
                       };
                       // return YearsList;
                    }
                return YearsList;
                   
            }       
    }
}