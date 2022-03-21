using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.Context;
using ProjectManagement.Domain.Models;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services
{
    public class DeveloperLookupDataService : IDeveloperLookupDataService
    {
        #region Fields
        private readonly Func<ProjectManagementDbContext> _dbContextCreator;
        #endregion

        #region Ctor
        public DeveloperLookupDataService(Func<ProjectManagementDbContext> dbContextCreator)
        {
            _dbContextCreator = dbContextCreator;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<LookupItem>> GetDeveloperLookupAsync()
        {
            await using ProjectManagementDbContext dbContext = _dbContextCreator();
            return await dbContext.Developers.AsNoTracking().Select(d => new LookupItem()
            {
                Id = d.Id,
                DisplayMember = $"{d.FirstName} {d.LastName}"
            }).ToListAsync();
        } 
        #endregion
    }
}