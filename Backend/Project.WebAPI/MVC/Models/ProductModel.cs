namespace MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductTable")]
    public partial class ProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public int Quantity { get; set; }

        public int? ProductCategoryId { get; set; }

        public virtual ProductCategoryModel ProductCategory { get; set; }
    }
}
