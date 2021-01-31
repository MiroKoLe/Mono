using Project.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public class Size : ISize
    {
        public int pageSize = 5;
        public int PageSize { get { return pageSize; } }
    }
}
