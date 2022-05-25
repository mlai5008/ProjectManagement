using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.Context;
using ProjectManagement.Domain.Models;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services.Repositories
{
    public class DeveloperRepository : IDeveloperRepository
    {
        #region Fields
        private readonly ProjectManagementDbContext _dbContext;
        #endregion

        #region Ctor
        public DeveloperRepository(ProjectManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        } 
        #endregion

        #region Methods
        public async Task<Developer> GetByIdAsync(Guid developerId)
        {
            return await _dbContext.Developers.SingleAsync(d => d.Id == developerId);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public bool HasChanges()
        {
            return _dbContext.ChangeTracker.HasChanges();
        }
        #endregion
    }
}