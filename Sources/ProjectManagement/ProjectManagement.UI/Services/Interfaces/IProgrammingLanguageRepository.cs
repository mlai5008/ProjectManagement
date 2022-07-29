using ProjectManagement.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services.Interfaces
{
    public interface IProgrammingLanguageRepository : IGenericRepository<ProgrammingLanguage>
    {
        Task<bool> IsReferencedByDeveloperAsync(Guid programmingLanguageId);
    }
}