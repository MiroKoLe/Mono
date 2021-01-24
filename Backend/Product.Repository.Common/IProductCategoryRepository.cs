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
        Task<IPagedList<ICategories>> GetProductsAsync(IPaging paging);
        Task <ICategories> GetDetailsAsync(int id);
        Task<int> CreateProductAsync(ICategories entity);
        Task<int> EditAsync(ICategories entity);
        Task<int> DeleteItemAsync(int id);
    }
}
