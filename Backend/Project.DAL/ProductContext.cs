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

        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductTable> ProductTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.ProductTable)
                .WithRequired(e => e.ProductCategory)
                .HasForeignKey(e => e.ProductCategoryId)
                .WillCascadeOnDelete(false);
        }
    }
}
