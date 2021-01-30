using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetProducts();
        Task<TEntity> GetDetailsAsync(int id);
        Task<int> CreateProductAsync(TEntity entity);
        Task<int> EditAsync(TEntity entity);
        Task<int> DeleteItemAsync(int id);
    }
}
