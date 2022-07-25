using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.Context;
using ProjectManagement.Domain.Models;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ProjectManagement.UI.Services.Repositories
{
    public class MeetingRepository : GenericRepository<Meeting, ProjectManagementDbContext>, IMeetingRepository
    {
        #region Ctor
        public MeetingRepository(ProjectManagementDbContext context) : base(context)
        { }
        #endregion

        #region Methods
        public override async Task<Meeting> GetByIdAsync(Guid id)
        {
            return await DbContext.Meetings.Include(m => m.Developers).SingleAsync(i => i.Id == id);
        }

        public async Task<List<Developer>> GetAllDevelopersAsync()
        {
            return await DbContext.Set<Developer>().ToListAsync();
        }

        public async Task ReloadDeveloperAsync(Guid developerId)
        {
            EntityEntry<Developer> dbEntityEntry = DbContext.ChangeTracker.Entries<Developer>().SingleOrDefault(db => db.Entity.Id == developerId);
            if (dbEntityEntry != null)
            {
                await dbEntityEntry.ReloadAsync();
            }
        }
        #endregion
    }
}