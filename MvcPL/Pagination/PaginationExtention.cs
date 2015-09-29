using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Pagination
{
    public static class PaginationExtention
    {
        public static PaginationList<T> ToPaginationList<T>(this IEnumerable<T> items, PageInfo pageInfo) where T : class
        {
            return new PaginationList<T>(items, pageInfo);
        }
    }
}