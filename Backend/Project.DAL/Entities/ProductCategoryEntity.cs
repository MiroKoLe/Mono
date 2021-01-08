using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Entities
{
    class ProductCategoryEntity
    {
        [Key]
        public int ProductId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<ProductTable> ProductTable { get; set; }
    }
}
