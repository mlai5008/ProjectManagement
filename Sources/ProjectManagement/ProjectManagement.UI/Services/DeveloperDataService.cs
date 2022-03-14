using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.Context;
using ProjectManagement.Domain.Models;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services
{
    public class DeveloperDataService : IDeveloperDataService
    {
        #region Fields
        private readonly Func<ProjectManagementDbContext> _dbContext;
        #endregion

        #region Ctor
        public DeveloperDataService(Func<ProjectManagementDbContext> dbContext)
        {
            _dbContext = dbContext;
        } 
        #endregion

        #region Methods
        public async Task<IEnumerable<Developer>> GetAllAsync()
        {
            await using ProjectManagementDbContext dbContext = _dbContext();
            return await dbContext.Developers.AsNoTracking().ToListAsync();
        }
        #endregion
    }
}