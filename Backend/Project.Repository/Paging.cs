using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class Paging : IPaging
    {
        public int pageSize = 5;
        public int PageSize { get { return pageSize; } }
        public bool IsAscending { get; set;  }
        public int PageNumber { get; set; }
        public string Search { get; set; }
        public int TotalCount { get; set; }
    }
}
