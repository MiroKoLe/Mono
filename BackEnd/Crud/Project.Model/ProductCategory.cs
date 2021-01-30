using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class ProductCategory : ICategories
    {
        public int ProductId { get; set; }

        public string Name { get; set; }


        public virtual ICollection<IProduct> ProductTable { get; set; }
    }
}
