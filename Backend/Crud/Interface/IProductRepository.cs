using Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Interface
{
    public interface IProductRepository
    {

        Task<List<ProductDomain>> GetProductsAsync(string searchBy, string search, string sortBy, int? PageSize, int? page, int? PageNumber);
        Task CreateProductAsync(ProductDomain productDomain); 
        Task<ProductDomain> GetDetailsAsync(int? id);
        Task EditAsync(ProductDomain productDomain);
        Task<ProductDomain> DeleteItemAsync(int? id); 


    }
}
