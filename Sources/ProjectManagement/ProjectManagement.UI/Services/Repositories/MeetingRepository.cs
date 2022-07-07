using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.Context;
using ProjectManagement.Domain.Models;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Threading.Tasks;

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
        #endregion
    }
}