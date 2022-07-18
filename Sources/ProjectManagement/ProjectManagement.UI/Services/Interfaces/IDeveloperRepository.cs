using System;
using ProjectManagement.Domain.Models;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services.Interfaces
{
    public interface IDeveloperRepository : IGenericRepository<Developer>
    {
        #region Methods
        void RemovePhoneNumber(DeveloperPhoneNumber model);

        Task<bool> HasMeetingsAsync(Guid developerId);
        #endregion
    }
}