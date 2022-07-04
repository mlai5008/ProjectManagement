using System;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Interfaces.ViewModels
{
    public interface IDetailViewModel
    {
        #region Properties
        bool HasChanges { get; }
        #endregion

        #region Methods
        Task LoadAsync(Guid? id);
        #endregion
    }
}