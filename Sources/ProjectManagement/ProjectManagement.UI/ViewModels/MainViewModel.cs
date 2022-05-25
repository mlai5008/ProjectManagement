using Prism.Events;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.UI.Events;
using ProjectManagement.UI.Services;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly Func<IDeveloperDetailViewModel> _developerDetailViewModelCreator;
        private IDeveloperDetailViewModel _developerDetailViewModel;
        #endregion

        #region Ctor
        public MainViewModel(INavigationViewModel navigationViewModel, Func<IDeveloperDetailViewModel> developerDetailViewModelCreator, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            _developerDetailViewModelCreator = developerDetailViewModelCreator;

            _eventAggregator.GetEvent<OpenDeveloperDetailViewModelEvent>().Subscribe(OnOpenDeveloperDetailView);

            NavigationViewModel = navigationViewModel;
        }
        #endregion

        #region Properties
        public INavigationViewModel NavigationViewModel { get; }

        public IDeveloperDetailViewModel DeveloperDetailViewModel
        {
            get => _developerDetailViewModel;
            private set
            {
                _developerDetailViewModel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods
        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenDeveloperDetailView(Guid developerId)
        {
            if (DeveloperDetailViewModel != null && DeveloperDetailViewModel.HasChanges)
            {
                MessageDialogResult result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            DeveloperDetailViewModel = _developerDetailViewModelCreator();
            await DeveloperDetailViewModel.LoadAsync(developerId);
        }
        #endregion
    }
}