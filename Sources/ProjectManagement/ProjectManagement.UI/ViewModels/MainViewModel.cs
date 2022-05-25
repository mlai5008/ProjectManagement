using Prism.Commands;
using Prism.Events;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.UI.Events;
using ProjectManagement.UI.Services;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

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
            _eventAggregator.GetEvent<AfterDeveloperDeletedEvent>().Subscribe(OnAfterDeveloperDeleted);

            CreateNewDeveloperCommand = new DelegateCommand(OnCreateNewDeveloperExecute);

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

        #region Commands
        public ICommand CreateNewDeveloperCommand { get; } 
        #endregion

        #region Methods
        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenDeveloperDetailView(Guid? developerId)
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

        private void OnAfterDeveloperDeleted(Guid developerId)
        {
            DeveloperDetailViewModel = null;
        }

        private void OnCreateNewDeveloperExecute()
        {
            OnOpenDeveloperDetailView(null);
        }
        #endregion
    }
}