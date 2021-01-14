using PagedList;
using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IProductRepository
    {

        Task<List<IProduct>> GetProducts(string searchBy, string search, string sortBy, int? PageSize, int? page, int? PageNumber);
        Task<IProduct> GetDetailsAsync(int id);
        Task<int> CreateProductAsync(IProduct entity);
        Task<int> EditAsync(IProduct entity);
        Task<int> DeleteItemAsync(int id);

    }
}
