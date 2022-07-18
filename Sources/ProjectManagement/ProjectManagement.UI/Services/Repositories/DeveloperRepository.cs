using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.Context;
using ProjectManagement.Domain.Models;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services.Repositories
{
    public class DeveloperRepository : GenericRepository<Developer, ProjectManagementDbContext>, IDeveloperRepository
    {
        #region Ctor
        public DeveloperRepository(ProjectManagementDbContext dbContext):base(dbContext)
        { } 
        #endregion

        #region Methods
        public override async Task<Developer> GetByIdAsync(Guid developerId)
        {
            return await DbContext.Developers.Include(f => f.PhoneNumbers).SingleAsync(d => d.Id == developerId);
        }

        public void RemovePhoneNumber(DeveloperPhoneNumber model)
        {
            DbContext.DeveloperPhoneNumbers.Remove(model);
        }

        public async Task<bool> HasMeetingsAsync(Guid developerId)
        {
            return await DbContext.Meetings.AsNoTracking()
                .Include(m => m.Developers)
                .AnyAsync(m => m.Developers.Any(d => d.Id == developerId));
        }
        #endregion
    }
}