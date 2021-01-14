using Project.Repository.Common;
using Project.DAL;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
     where TEntity : class
    {

        private readonly ProductContext Context;
        private DbSet<TEntity> Entities;
        public GenericRepository(ProductContext context)
        {
            this.Context = context;
            Entities = Context.Set<TEntity>();

        }


        public IQueryable<TEntity> GetProducts()
        {
            return Entities;
        }

        public async Task<TEntity> GetDetailsAsync(int id)
        {

            return await Entities.FindAsync(id);

        }

        public async Task<int> CreateProductAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentException("entity");
                }
                Entities.Add(entity);
                return await Context.SaveChangesAsync();

            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);
                throw fail;

            }

        }

        public async Task<int> EditAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentException("entity");
                }
                Context.Entry(entity).State = EntityState.Modified;
                return await Context.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public async Task<int> DeleteItemAsync(int id)
        {
            try
            {
                var entity = await Entities.FindAsync(id);
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                Context.Entry(entity).State = EntityState.Deleted;
                return await Context.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
    }
}
