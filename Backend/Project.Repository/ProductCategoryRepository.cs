using AutoMapper;
using PagedList;
using Project.Model.Common;
using Project.Repository.Common;
using Project.DAL;
using Project.DAL.Entities;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {

        private readonly IRepository<ProductCategoryEntity> Repository;

        public ProductCategoryRepository(IRepository<ProductCategoryEntity> repository)
        {
            this.Repository = repository;
        }

        public async Task<IList<IProductCategory>> GetProductsAsync()
        {

            var productCategory = Repository.GetProducts();

            var categoryEntity = productCategory.ToList(); 


            var products = Mapper.Map<List<ProductCategoryEntity>, List<IProductCategory>>(categoryEntity); 

            return products.ToList(); 
        }
        public async Task<IProductCategory> GetDetailsAsync(int id)
        {
            var product = await Repository.GetDetailsAsync(id);
            return Mapper.Map<ProductCategoryEntity, IProductCategory>(product);
        }

        public async Task<IProductCategory> GetProductAsync(int id)
        {
            var product = await Repository.GetDetailsAsync(id);
            return Mapper.Map<ProductCategoryEntity, IProductCategory>(product);
        }

        public async Task<int> CreateProductAsync(IProductCategory product)
        {
            var entity = Mapper.Map<IProductCategory, ProductCategoryEntity>(product);
            return await Repository.CreateProductAsync(entity);

        }
        public async Task<int> EditAsync(IProductCategory product)
        {

            var entity = Mapper.Map<IProductCategory, ProductCategoryEntity>(product);
            return await Repository.EditAsync(entity);

        }
        public async Task<int> DeleteItemAsync(int id)
        {
            return await Repository.DeleteItemAsync(id);

        }



    }
    }
