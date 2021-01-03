using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Crud.Interface;
using Crud.Models;
using Crud.Repository;
using Crud.Service;
using Ninject;
using PagedList;

namespace Crud.Service
{
    public class ProductService : IProductService
    {
        private Sample6Entities db = new Sample6Entities();


        public ProductService(IProductRepository productRepository)
        {
            ProductRepository = new ProductRepository(new Sample6Entities());
        }
        IProductRepository ProductRepository { get; set; }


        public async Task<List<ProductDomain>> GetProductsAsync(string searchBy, string search, string sortBy, int? PageSize, int? page, int? PageNumber)
        {

            var productList = await ProductRepository.GetProductsAsync(searchBy, search, sortBy, PageSize, page, PageNumber);

            return productList;

        }


        public async Task ProductCreateAsync(ProductDomain domain)
        {
            await ProductRepository.CreateProductAsync(domain);

        }


        public async Task<ProductDomain> GetDetailAsync(int? id)
        {

            var ProductDetails = await ProductRepository.GetDetailsAsync(id);

            return ProductDetails;

        }
        public async Task<ProductDomain> GetDeleteAsync(int? id)
        {

            var deleteProduct = await ProductRepository.DeleteItemAsync(id); 

            return deleteProduct;

        }


        public async Task EditAsync(ProductDomain productDomain)
        {

            await Task.Run(() => ProductRepository.EditAsync(productDomain));

        }


    }
}