using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using System.Threading.Tasks;

namespace ProjectManagement.UI.ViewModels
{
    public class MainViewModel
    {
        #region Ctor
        public MainViewModel(INavigationViewModel navigationViewModel, IDeveloperDetailViewModel developerDetailViewModel)
        {
            NavigationViewModel = navigationViewModel;
            DeveloperDetailViewModel = developerDetailViewModel;
        }
        #endregion

        #region Properties
        public INavigationViewModel NavigationViewModel { get; }
        public IDeveloperDetailViewModel DeveloperDetailViewModel { get; }
        #endregion

        #region Methods
        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
        #endregion
    }
}