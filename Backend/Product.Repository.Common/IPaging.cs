using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IPaging
    {
        int PageSize { get; }
        bool IsAscending { get; set; }
        int PageNumber { get; set; }
        string Search { get; set; }
        int TotalCount { get; set; }
    }
}
