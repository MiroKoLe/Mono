using AutoMapper;
using PagedList;
using Project.Model.Common;
using Project.Repository.Common;
using Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {

        private readonly IRepository<ProductCategoryEntity> Repository;

        public ProductCategoryRepository(IRepository<ProductCategoryEntity> repository)
        {
            this.Repository = repository;
        }

        public async Task<IList<ICategories>> GetProductsAsync()
        {

            var product = await Task.Run(() => Repository.GetProducts());

            List<ICategories> productCategories = new List<ICategories>();

            productCategories = Mapper.Map<List<ProductCategoryEntity>, List<ICategories>>(product.ToList()); 

            return productCategories.ToList(); 
        }
        public async Task<ICategories> GetDetailsAsync(int id)
        {
            var product = await Repository.GetDetailsAsync(id);
            return Mapper.Map<ProductCategoryEntity, ICategories>(product);
        }

        public async Task<ICategories> GetProductAsync(int id)
        {
            var product = await Repository.GetDetailsAsync(id);
            return Mapper.Map<ProductCategoryEntity, ICategories>(product);
        }

        public async Task<int> CreateProductAsync(ICategories product)
        {
            var entity = Mapper.Map<ICategories, ProductCategoryEntity>(product);
            return await Repository.CreateProductAsync(entity);

        }
        public async Task<int> EditAsync(ICategories product)
        {

            var entity = Mapper.Map<ICategories, ProductCategoryEntity>(product);
            return await Repository.EditAsync(entity);

        }
        public async Task<int> DeleteItemAsync(int id)
        {
            return await Repository.DeleteItemAsync(id);

        }



    }
    }
