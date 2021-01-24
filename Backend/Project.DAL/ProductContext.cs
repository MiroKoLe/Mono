namespace Project.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProductContext : DbContext
    {
        public ProductContext()
            : base("name=ProductContext")
        {
        }

        public virtual DbSet<ProductCategoryEntity> ProductCategory { get; set; }
        public virtual DbSet<ProductEntity> ProductTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategoryEntity>()
                .HasMany(e => e.ProductEntity)
                .WithOptional(e => e.ProductCategoryEntity)
                .HasForeignKey(e => e.ProductCategoryId);
        }

        object placeHolderVariable;
    }
}
