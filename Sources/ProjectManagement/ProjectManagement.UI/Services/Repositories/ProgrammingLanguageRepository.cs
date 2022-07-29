using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.Context;
using ProjectManagement.Domain.Models;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services.Repositories
{
    public class ProgrammingLanguageRepository : GenericRepository<ProgrammingLanguage, ProjectManagementDbContext>, IProgrammingLanguageRepository
    {
        public ProgrammingLanguageRepository(ProjectManagementDbContext context) : base(context)
        {
        }

        public async Task<bool> IsReferencedByDeveloperAsync(Guid programmingLanguageId)
        {
            return await DbContext.Developers.AsNoTracking().AnyAsync(f => f.ProgrammingLanguageId == programmingLanguageId);
        }
    }
}