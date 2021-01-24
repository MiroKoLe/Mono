using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Common
{
    public interface IProduct
    {

        int Id { get; set; }
 
        string Name { get; set; }

        string Model { get; set; }

        int? Quantity { get; set; }

        int ProductCategoryId { get; set; }

        ICategories ProductCategory { get; set; }
    }
}
