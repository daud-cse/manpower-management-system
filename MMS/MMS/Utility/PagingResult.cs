using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Reflection;


namespace MMS.Utility
{
    public class PagingResult<T>
    {
        /*
        private const int defaultPageSize = 10;
        public IList<T> Prepare(List<T> lstItem, string SearchTx, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            IList<T> IlstItem = lstItem;//.Features.ToList();

            if (string.IsNullOrWhiteSpace(SearchTx))
            {
                IlstItem = IlstItem.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                
                IlstItem = IlstItem.Where(p => p.Name.ToLower() == SearchTx.ToLower()).ToPagedList(currentPageIndex, defaultPageSize);
            }
        }
         */
    }
}