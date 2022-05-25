using ProjectManagement.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services.Interfaces
{
    public interface IDeveloperRepository
    {
        #region Methods
        Task<Developer> GetByIdAsync(Guid developerId);
        Task SaveAsync();
        bool HasChanges();
        #endregion
    }
}