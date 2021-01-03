using Crud.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud.Models
{
    public class Product : IProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public Nullable<int> Quantity { get; set; }
        public int ProductCategoryId { get; set; }


        public virtual ProductCategory ProductCategory { get; set; }

       
    }
}