using Microsoft.EntityFrameworkCore;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        #region Fields
        protected readonly TContext DbContext;
        #endregion

        #region Ctor
        protected GenericRepository(TContext context)
        {
            DbContext = context;
        }
        #endregion

        #region Methods
        public void Add(TEntity model)
        {
            DbContext.Set<TEntity>().Add(model);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbContext.Set<TEntity>().ToListAsync();
        }

        public void Remove(TEntity model)
        {
            DbContext.Set<TEntity>().Remove(model);
        }

        public async Task SaveAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public bool HasChanges()
        {
            return DbContext.ChangeTracker.HasChanges();
        }
        #endregion
    }
}