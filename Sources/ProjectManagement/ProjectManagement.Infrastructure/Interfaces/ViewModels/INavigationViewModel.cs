using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Interfaces.ViewModels
{
    public interface INavigationViewModel
    {
        #region Methods
        Task LoadAsync(); 
        #endregion
    }
}