namespace Project.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductCategory")]
    public class ProductCategoryEntity
    {
        public ProductCategoryEntity()
        {
            this.ProductEntity = new HashSet<ProductEntity>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<ProductEntity> ProductEntity { get; set; }
    }
}
