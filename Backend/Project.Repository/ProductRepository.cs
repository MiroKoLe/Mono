using AutoMapper;
using Project.Model.Common;
using Project.Repository.Common;
using Project.DAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Project.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly IRepository<ProductEntity> repository;

        public ProductRepository(IRepository<ProductEntity> _repository)
        {
            this.repository = _repository;
        }

        public async Task<IProduct> GetDetailsAsync(int id)
        {
            var product = await repository.GetDetailsAsync(id);
            return Mapper.Map<ProductEntity, IProduct>(product); 
        }

        public async Task<IProduct> GetProductAsync(int id)
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
        public async Task<List<IProduct>> GetProducts(string searchBy, string search, string sortBy, int? PageSize, int? page, int? PageNumber)
       {

            var productTable = repository.GetProducts(); 



            if (searchBy == "Name")
            {
                productTable = productTable.Where(x => x.Name == search || search == null);
            }
            else
            {
                productTable = productTable.Where(x => x.Model == search || search == null);
            }
            switch (sortBy)
            {
                case "Name desc":
                    productTable = productTable.OrderByDescending(x => x.Name);
                    break;
                case "Model desc":
                    productTable = productTable.OrderByDescending(x => x.Model);
                    break;
                default:
                    productTable = productTable.OrderBy(x => x.Name);
                    break;
            }

            int pageSize = PageSize ?? 5;
            int pageNumber = PageNumber ?? 1;


            var productList = productTable.ToList();

            List<IProduct> productDomain = new List<IProduct>(); 

            productDomain = Mapper.Map<List<ProductEntity>, List<IProduct>>(productList);

            var listPaged = productTable.Skip((pageNumber - 1) * pageSize).Take(pageSize);



            return productDomain.ToList(); 
        }

    }
  









}

