﻿using PagedList;
using Project.Model.Common;
using Project.Repository.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
   public class ProductService : IProductService
    {

        readonly private IProductRepository Repository; 

        public ProductService(IProductRepository repository)
        {
            this.Repository = repository; 
        }



        public async Task<List<IProduct>> GetProductsAsync(string searchBy, string search, string sortBy, int? PageSize, int? page, int? PageNumber)
        {


           var productList = await Repository.GetProducts( searchBy,  search,  sortBy, PageSize,  page, PageNumber);

            return productList.ToList(); 


        }

        public async Task<int> CreateProductAsync(IProduct entity)
        {
            var createProduct = await Repository.CreateProductAsync(entity);

            return createProduct; 
        }


        public async Task<int> EditAsync(IProduct entity)
        {
            var editProduct = await Repository.EditAsync(entity);

            return editProduct; 
        }

        public async Task<int> DeleteItemAsync(int id)
        {
            var deleteItem = await Repository.DeleteItemAsync(id);

            return deleteItem; 
        }

        public async Task<IProduct> GetDetailsAsync(int id)
        {
            var get = await Repository.GetDetailsAsync(id);

            return get; 
        }


    }
}
