using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.UI.Services
{
    public interface ILookupDataService
    {
        Task<IEnumerable<LookupItem>> GetDeveloperLookupAsync();
    }
}