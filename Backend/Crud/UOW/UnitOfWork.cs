using Crud.Interface;
using Crud.Models;
using Crud.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace Crud.UOW
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        public Sample6Entities DbContext { get; private set; }
        private GenericRepository<ProductCategory> categoryRepository;
        private GenericRepository<ProductTable> productRepository; 

        public UnitOfWork(Sample6Entities dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("DbContext");
            }
            DbContext = dbContext;
        }

        public GenericRepository<ProductCategory> CategoryRepository
        {
            get
            {

                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<ProductCategory>(DbContext);
                }
                return categoryRepository;
            }
        }
        public GenericRepository<ProductTable> ProductRepository
        {
            get
            {

                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<ProductTable>(DbContext);
                }
                return productRepository;
            }
        }

        public Task<int> AddAsync<T>(T entity) where T : class
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbContext.Set<T>().Add(entity);
            }
            return Task.FromResult(1);
        }

        public int Commit()
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = DbContext.SaveChanges();
                scope.Complete();
                return result; 
            }
        }
        public Task<int> UpdateAsync<T>(T entity) where T : class
        {
         

            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbContext.Set<T>().Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;

            return Task.FromResult(1);
        }



        public void Dispose()
        {
            DbContext.Dispose();
        }

        public Task<int> CommitAsync()
        {
            throw new NotImplementedException();
        }
    }
        

}





