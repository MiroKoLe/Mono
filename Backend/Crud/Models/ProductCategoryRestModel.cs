using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud.Models
{
    public class ProductCategoryRestModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
        public Boolean IsActive { get; set; }

        public virtual ProductRestModel ProductRestModel { get; set;}
    }
}