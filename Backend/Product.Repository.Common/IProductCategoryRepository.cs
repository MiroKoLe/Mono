using PagedList;
using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IProductCategoryRepository
    {
        Task<IList<IProductCategory>> GetProductsAsync();
        Task <IProductCategory> GetDetailsAsync(int id);
        Task<int> CreateProductAsync(IProductCategory entity);
        Task<int> EditAsync(IProductCategory entity);
        Task<int> DeleteItemAsync(int id);
    }
}
