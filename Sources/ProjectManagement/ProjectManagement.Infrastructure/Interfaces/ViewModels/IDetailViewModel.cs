using System;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Interfaces.ViewModels
{
    public interface IDetailViewModel
    {
        #region Properties
        bool HasChanges { get; }
        Guid Id { get; }
        #endregion

        #region Methods
        Task LoadAsync(Guid id);
        #endregion
    }
}