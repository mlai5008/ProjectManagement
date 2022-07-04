using ProjectManagement.Domain.Models;

namespace ProjectManagement.UI.Services.Interfaces
{
    public interface IDeveloperRepository : IGenericRepository<Developer>
    {
        #region Methods
        void RemovePhoneNumber(DeveloperPhoneNumber model);
        #endregion
    }
}