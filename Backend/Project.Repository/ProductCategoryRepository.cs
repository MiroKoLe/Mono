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
using System.Data.Entity;

namespace Project.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {

        private readonly IRepository<ProductCategoryEntity> Repository;
        IPaging paging; 

        public ProductCategoryRepository(IRepository<ProductCategoryEntity> repository, IPaging _paging)
        {
            this.Repository = repository;
            this.paging = _paging; 
        }

        public async Task<IPagedList<ICategories>> GetProductsAsync(IPaging paging)
        {
            var product = await Task.Run(() => Repository.GetProducts());

            product = paging.IsAscending == false ? product.OrderByDescending(x => x.Name) : product.OrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(paging.Search))
            {
                paging.TotalCount = await product.Where(x => x.Name == paging.Search).CountAsync();
                product = product.Where(x => x.Name == paging.Search).Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize); 
            }
            else
            {
                paging.TotalCount = await product.CountAsync();
                product = product.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);
            }


            var enumerableQuery = product.AsEnumerable();
            var mappedQuery = Mapper.Map<IEnumerable<ProductCategoryEntity>, IEnumerable<ICategories>>(enumerableQuery);
            return new StaticPagedList<ICategories>(mappedQuery, paging.PageNumber, paging.PageSize, paging.TotalCount);

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
