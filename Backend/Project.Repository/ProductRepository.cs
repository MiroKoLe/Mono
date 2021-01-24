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
using System.Data.Entity;

namespace Project.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly IRepository<ProductEntity> repository;
        IPaging paging; 

        public ProductRepository(IRepository<ProductEntity> _repository, IPaging _paging)

        {
            this.repository = _repository;
            this.paging = _paging; 
        }


        public async Task <IPagedList<IProduct>> GetProducts(IPaging paging)
        {

            var productTable = repository.GetProducts();

            productTable = paging.IsAscending == false ? productTable.OrderByDescending(x => x.Name) : productTable.OrderBy(x => x.Name);
            if (!string.IsNullOrEmpty(paging.Search))
            {
                paging.TotalCount = await productTable.Where(x => x.Name == paging.Search ).CountAsync();
                productTable = productTable.Where(x => x.Name == paging.Search).Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);
            }
            else
            {
                paging.TotalCount = await productTable.CountAsync();
                productTable = productTable.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);
            }

            var enumerableProduct = productTable.AsQueryable();
            var mappedProduct = Mapper.Map<IEnumerable<ProductEntity>, IEnumerable<IProduct>>(enumerableProduct);
            return new StaticPagedList<IProduct>(mappedProduct, paging.PageNumber, paging.PageSize, paging.TotalCount); 

           

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

