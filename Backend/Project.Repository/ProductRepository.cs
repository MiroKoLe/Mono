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
    public class ProductRepository : IProductRepository
    {

        private readonly IRepository<ProductEntity> repository;
        IAscending ascending;
        ICount count;
        IPageNumber number;
        ISize size;
        IItemSearch itemSearch; 
        

        public ProductRepository(IRepository<ProductEntity> _repository, IAscending _ascending, ICount _count, IPageNumber _number, ISize _size, IItemSearch _itemSearch)

        {
            this.repository = _repository;
            this.ascending = _ascending;
            this.count = _count;
            this.number = _number;
            this.size = _size;
            this.itemSearch = _itemSearch; 
        }


        public async Task<IPagedList<IProduct>> GetProductsAsync(IAscending ascending, ICount count, IPageNumber number, ISize size, IItemSearch itemSearch)
        {

            var productTable = repository.GetProducts();

            productTable = ascending.IsAscending == false ? productTable.OrderByDescending(x => x.Name) : productTable.OrderBy(x => x.Name);
            if (!string.IsNullOrEmpty(itemSearch.Search))
            {
                count.TotalCount = await productTable.Where(x => x.Name == itemSearch.Search).CountAsync();
                productTable = productTable.Where(x => x.Name == itemSearch.Search).Skip((number.pageNumber - 1) * size.PageSize).Take(size.PageSize);
            }
            else
            {
                count.TotalCount = await productTable.CountAsync();
                productTable = productTable.Skip((number.pageNumber - 1) * size.PageSize).Take(size.PageSize);
            }

            var enumerableProduct = productTable.AsQueryable();
            var mappedProduct = Mapper.Map<IEnumerable<ProductEntity>, IEnumerable<IProduct>>(enumerableProduct);
            return new StaticPagedList<IProduct>(mappedProduct, number.pageNumber, size.PageSize, count.TotalCount);



        }

        public async Task<IProduct> GetDetailsAsync(int? id)
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

            var entity = Mapper.Map<IProduct, ProductEntity>(product);
            return await repository.EditAsync(entity);

        }
        public async Task<int> DeleteItemAsync(int? id)
        {
            return await repository.DeleteItemAsync(id);

        }


    }




}

