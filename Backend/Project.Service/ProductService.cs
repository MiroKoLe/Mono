using Castle.Core.Internal;
using PagedList;
using Project.Common;
using Project.Common.Interface;
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



        public async Task<IPagedList<IProduct>> GetProductsAsync(IAscending ascending, ICount count, IPageNumber number, ISize size, IItemSearch itemSearch)
        {

            return await Repository.GetProductsAsync(ascending, count, number, size, itemSearch); 


        }

        public async Task<int> CreateProductAsync(IProduct entity)
        {

            if (entity != null
                  && string.IsNullOrEmpty(entity.Name) 
                  && string.IsNullOrEmpty(entity.Model)
                  && entity.Quantity != null
                 )
            {
                throw new ArgumentNullException("Fields cannot be empty");
            }

            var createProduct = await Repository.CreateProductAsync(entity);

            return createProduct;



        }
        

        public async Task<int> EditAsync(IProduct entity)
        {

            if (entity != null
                 && string.IsNullOrEmpty(entity.Name)
                 && string.IsNullOrEmpty(entity.Model)
                 && entity.Quantity != null
                )
            {
                throw new ArgumentNullException("Unknown entity");
            }

            var editProduct = await Repository.EditAsync(entity);

            return editProduct;
        }

        public async Task<int> DeleteItemAsync(int? id)
        {
            if(id == null)
            {
                throw new ArgumentNullException("id"); 
            }
            var deleteItem = await Repository.DeleteItemAsync(id);

            return deleteItem;
        }

        public async Task<IProduct> GetDetailsAsync(int? id)
        {
            if(id == null)
            {
                throw new ArgumentNullException("unknown entity");
            }
            var get = await Repository.GetDetailsAsync(id);

            return get;
        }


    }
}