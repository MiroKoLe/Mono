﻿using Project.Model.Common;
using Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
   public class ProductCategory : IProductCategory 
    {

        public int ProductId { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<ProductTable> ProductTable { get; set; }
    }
}
