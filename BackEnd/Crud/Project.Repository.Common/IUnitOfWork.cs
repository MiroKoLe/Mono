using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IUnitOfWork
    {
        Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<int> CommitAsync();
        Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : class;
        Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : class;
    }
}
