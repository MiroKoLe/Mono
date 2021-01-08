using Project.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Model.Common
{
    public interface IProductCategory
    {
        [Key]
        int ProductId { get; set; }

        [StringLength(50)]
        string Name { get; set; }

        bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        ICollection<ProductTable> ProductTable { get; set; }
    }
}
