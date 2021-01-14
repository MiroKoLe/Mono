using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    
   public class Product : IProduct
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Model { get; set; }

        public int? Quantity { get; set; }

        public int ProductCategoryId { get; set; }

        public virtual IProductCategory ProductCategory { get; set; }
    }
}

