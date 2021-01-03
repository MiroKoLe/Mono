using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud.Models
{
    public class CategoryDomain
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public Boolean IsActive { get; set; }

        public virtual ProductDomain ProductDomain { get; set; }
    }
}