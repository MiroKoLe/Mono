namespace Project.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductTable")]
    public partial class ProductEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public int Quantity { get; set; }

        public int? ProductCategoryId { get; set; }

        public virtual ProductCategoryEntity ProductCategoryEntity { get; set; }
    }
}
