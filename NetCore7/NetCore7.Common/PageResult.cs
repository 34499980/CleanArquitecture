using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Common
{
    public class PageResult<T>: PagedResultBase
    {
        public IList<T> Results { get; set; }
        public PageResult()
        { 
            Results = new List<T>();
        }

    }
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
    }
}
