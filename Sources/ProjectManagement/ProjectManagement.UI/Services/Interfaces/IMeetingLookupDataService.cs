using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.UI.Services.Interfaces
{
    public interface IMeetingLookupDataService
    {
        #region Methods
        Task<IEnumerable<LookupItem>> GetMeetingLookupAsync();
        #endregion
    }
}