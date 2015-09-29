using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Pagination
{
    public class PaginationList<T> where T:  class
    {
        public IEnumerable<T> Items { get; private set; }
        public PageInfo PageInfo { get; private set; }

        public PaginationList(IEnumerable<T> items, PageInfo pageInfo)
        {
            Items = items;
            PageInfo = pageInfo;
        }
    }
}