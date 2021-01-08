using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Model.Common
{
    public interface IProduct
    {

        int ID { get; set; }

        [StringLength(50)]
        string Name { get; set; }

        [StringLength(50)]
        string Model { get; set; }

        int? Quantity { get; set; }

        int ProductCategoryId { get; set; }

        IProductCategory ProductCategory { get; set; }
    }
}
