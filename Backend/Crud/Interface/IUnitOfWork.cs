using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Interface
{
    interface IUnitOfWork : IDisposable
    {

        Task<int> AddAsync<T>(T entity) where T : class;
        Task<int> CommitAsync();
        Task<int> UpdateAsync<T>(T entity) where T : class;


    }
}
