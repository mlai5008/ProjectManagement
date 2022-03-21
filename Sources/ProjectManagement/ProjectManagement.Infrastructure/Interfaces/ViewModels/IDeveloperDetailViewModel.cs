using System;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Interfaces.ViewModels
{
    public interface IDeveloperDetailViewModel
    {
        #region Methods
        Task LoadAsync(Guid developerId); 
        #endregion
    }
}