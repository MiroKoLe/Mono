namespace MVC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class ProductCategoryModel
    {
        [Key]
        public int ProductId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }


    }
}
