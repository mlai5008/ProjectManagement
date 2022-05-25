using System;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Interfaces.ViewModels
{
    public interface IDeveloperDetailViewModel
    {
        #region Properties
        bool HasChanges { get; } 
        #endregion

        #region Methods
        Task LoadAsync(Guid developerId);
        #endregion
    }
}