using PagedList;
using Project.Model.Common;
using Project.Repository;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IProductService
    {
        Task<IPagedList<IProduct>> GetProductsAsync(IPaging paging);
        Task<IProduct> GetDetailsAsync(int id);
        Task<int> CreateProductAsync(IProduct entity);
        Task<int> EditAsync(IProduct entity);
        Task<int> DeleteItemAsync(int id);
    }
}
