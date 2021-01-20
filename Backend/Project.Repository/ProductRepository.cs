using AutoMapper;
using Project.Model.Common;
using Project.Repository.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Project.DAL;

namespace Project.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly IRepository<ProductEntity> repository;

        public ProductRepository(IRepository<ProductEntity> _repository)
        {
            this.repository = _repository;
        }


         public async Task<IList<IProduct>> GetProducts()
       {

            var productTable = await Task.Run(() => repository.GetProducts()); 


            List<IProduct> products = new List<IProduct>();

            var productList = productTable.ToList();

            products = Mapper.Map<List<ProductEntity>, List<IProduct>>(productList); 



            return products.ToList(); 
        }

        public async Task<IProduct> GetDetailsAsync(int id)
        {
            var product = await repository.GetDetailsAsync(id);
            return Mapper.Map<ProductEntity, IProduct>(product); 
        }


        public async Task<int> CreateProductAsync(IProduct product)
        {
            var entity = Mapper.Map<IProduct, ProductEntity>(product);
            return await repository.CreateProductAsync(entity); 
             
        }
        public async Task<int> EditAsync(IProduct product)
        {
            
            var entity = Mapper.Map<IProduct, ProductEntity> (product);
            return await repository.EditAsync(entity);

        }
        public async Task<int> DeleteItemAsync(int id)
        {
         return await repository.DeleteItemAsync(id); 

        }
       

    }
  









}

