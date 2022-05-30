using ProjectManagement.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services.Interfaces
{
    public interface IProgrammingLanguageLookupDataService
    {
        #region Methods
        Task<IEnumerable<LookupItem>> GetProgrammingLanguageLookupAsync(); 
        #endregion
    }
}