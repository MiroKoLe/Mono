using AutoMapper;
using PagedList;
using Project.Common;
using Project.Common.Interface;
using Project.DAL;
using Project.Model.Common;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {

        private readonly IRepository<ProductCategoryEntity> Repository;
        IAscending ascending;
        ICount count;
        IPageNumber number;
        ISize size;
        IItemSearch itemSearch;

        public ProductCategoryRepository(IRepository<ProductCategoryEntity> repository, IAscending _ascending, ICount _count, IPageNumber _number, ISize _size, IItemSearch _itemSearch)
        {
            this.Repository = repository;
            this.ascending = _ascending;
            this.count = _count;
            this.number = _number;
            this.size = _size;
            this.itemSearch = _itemSearch;
        }

        public async Task<IPagedList<ICategories>> GetProductsAsync(IAscending ascending, ICount count, IPageNumber number, ISize size, IItemSearch itemSearch)
        {
            var product = await Task.Run(() => Repository.GetProducts());

            product = ascending.IsAscending == false ? product.OrderByDescending(x => x.Name) : product.OrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(itemSearch.Search))
            {
                count.TotalCount = await product.Where(x => x.Name == itemSearch.Search).CountAsync();
                product = product.Where(x => x.Name == itemSearch.Search).Skip((number.pageNumber - 1) * size.PageSize).Take(size.PageSize);
            }
            else
            {
                count.TotalCount= await product.CountAsync();
                product = product.Skip((number.pageNumber - 1) * size.PageSize).Take(size.PageSize);
            }


            var enumerableQuery = product.AsEnumerable();
            var mappedQuery = Mapper.Map<IEnumerable<ProductCategoryEntity>, IEnumerable<ICategories>>(enumerableQuery);
            return new StaticPagedList<ICategories>(mappedQuery, number.pageNumber, size.PageSize, count.TotalCount);

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
