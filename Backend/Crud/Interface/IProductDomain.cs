using Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Interface
{
    public interface IProductDomain
    {
         int ID { get; set; }
         string Name { get; set; }
         string Model { get; set; }
         Nullable<int> Quantity { get; set; }
         int ProductCategoryId { get; set; }

          ProductCategory ProductCategory { get; set; }
    }
}
