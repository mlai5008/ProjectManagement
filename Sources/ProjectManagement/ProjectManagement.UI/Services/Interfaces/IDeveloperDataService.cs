using ProjectManagement.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services.Interfaces
{
    public interface IDeveloperDataService
    {
        #region Methods
        Task<IEnumerable<Developer>> GetAllAsync();
        #endregion
    }
}