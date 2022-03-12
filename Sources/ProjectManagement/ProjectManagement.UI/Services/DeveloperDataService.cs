using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.Context;
using ProjectManagement.Domain.Models;
using ProjectManagement.UI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services
{
    public class DeveloperDataService : IDeveloperDataService
    {
        #region Methods
        public async Task<IEnumerable<Developer>> GetAllAsync()
        {
            await using ProjectManagementDbContext projectManagementDbContext = new ProjectManagementDbContext();
            return await projectManagementDbContext.Developers.AsNoTracking().ToListAsync();
        }
        #endregion
    }
}