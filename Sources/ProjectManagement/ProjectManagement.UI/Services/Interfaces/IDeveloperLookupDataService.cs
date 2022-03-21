using ProjectManagement.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services.Interfaces
{
    public interface IDeveloperLookupDataService
    {
        #region Methods
        Task<IEnumerable<LookupItem>> GetDeveloperLookupAsync(); 
        #endregion
    }
}