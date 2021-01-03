using AutoMapper;
using Crud.Interface;
using Crud.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Crud.Repository
{

    public class ProductRepository : IProductRepository
    {
        private Sample6Entities context = new Sample6Entities();

        public ProductRepository(Sample6Entities context)
        {
            this.context = context;
        }


        public async Task<List<ProductDomain>> GetProductsAsync(string searchBy, string search, string sortBy, int? PageSize, int? id, int? PageNumber)
        {

            var productTable = await Task.Run(() => context.ProductTable.AsQueryable());


            var products = productTable.Include(p => p.ProductCategory);


            if (searchBy == "Name")
            {
                products = products.Where(x => x.Name == search || search == null);
            }
            else
            {
                products = products.Where(x => x.Model == search || search == null);
            }
            switch (sortBy)
            {
                case "Name desc":
                    products = products.OrderByDescending(x => x.Name);
                    break;
                case "Model desc":
                    products = products.OrderByDescending(x => x.Model);
                    break;
                default:
                    products = products.OrderBy(x => x.Name);
                    break;
            }

            int pageSize = PageSize ?? 5;
            int pageNumber = PageNumber ?? 1;


            List<ProductDomain> productDomains = new List<ProductDomain>();

            var productList = products.ToList();

            productDomains = Mapper.Map<List<ProductTable>, List<ProductDomain>>(productList);

            var listPaged = productTable.Skip((pageNumber - 1) * pageSize).Take(pageSize);



            return productDomains.ToList();
        }


        public async Task CreateProductAsync(ProductDomain productDomain)
        {


            ProductTable product = new ProductTable();

            product = Mapper.Map<ProductDomain, ProductTable>(productDomain);


            await Task.Run(() => context.ProductTable.Add(product));
            context.SaveChanges();


        }


        public async Task<ProductDomain> GetDetailsAsync(int? id)
        {
            var productTable = await Task.Run(() => context.ProductTable.Find(id));
           

            ProductDomain productDomain = new ProductDomain();

            productDomain = Mapper.Map<ProductTable, ProductDomain>(productTable);


            return productDomain;

        }




        public async Task EditAsync(ProductDomain productDomain)
        {
            ProductTable product = new ProductTable();

            product = Mapper.Map<ProductDomain, ProductTable>(productDomain);
            product.ProductCategory = null; 

            await Task.Run(() => context.Entry(product).State = EntityState.Modified);
            context.SaveChanges();

        }

        public void ProductEdit(ProductTable productTable)
        {

            context.Entry(productTable).State = EntityState.Modified;
            context.SaveChanges();

        }
        public async Task<ProductDomain> DeleteItemAsync(int? id)
        {
            var productTable = await Task.Run(() => context.ProductTable.Find(id));
            context.ProductTable.Remove(productTable);
            context.SaveChanges();

            ProductDomain productDomain = new ProductDomain();

            productDomain = Mapper.Map<ProductTable, ProductDomain>(productTable);

            return productDomain;



        }


        public ProductTable GetDetails(int? id)
        {


            return context.ProductTable.Find(id);

        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
