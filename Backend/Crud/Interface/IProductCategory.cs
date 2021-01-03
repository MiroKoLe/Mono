using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Interface
{
    public interface IProductCategory
    {
         int ProductId { get; set; }
         string Name { get; set; }
         Nullable<Boolean> IsActive { get; set; }
    }
}
