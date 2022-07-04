using Prism.Commands;
using Prism.Events;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.UI.Events;
using ProjectManagement.UI.Services;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectManagement.Domain.EventArgs;

namespace ProjectManagement.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly Func<IDeveloperDetailViewModel> _developerDetailViewModelCreator;
        private IDetailViewModel _detailViewModel;
        #endregion

        #region Ctor
        public MainViewModel(INavigationViewModel navigationViewModel, Func<IDeveloperDetailViewModel> developerDetailViewModelCreator, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            _developerDetailViewModelCreator = developerDetailViewModelCreator;

            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(OnAfterDetailDeleted);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);

            NavigationViewModel = navigationViewModel;
        }
        #endregion

        #region Properties
        public INavigationViewModel NavigationViewModel { get; }

        public IDetailViewModel DetailViewModel
        {
            get => _detailViewModel;
            private set
            {
                _detailViewModel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand CreateNewDetailCommand { get; } 
        #endregion

        #region Methods
        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenDetailView(OpenDetailViewEventArg arg)
        {
            if (DetailViewModel != null && DetailViewModel.HasChanges)
            {
                MessageDialogResult result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            switch (arg.ViewModelName)
            {
                case nameof(DeveloperDetailViewModel):
                    DetailViewModel = _developerDetailViewModelCreator();
                    break;
            }
            
            await DetailViewModel.LoadAsync(arg.Id);
        }

        private void OnAfterDetailDeleted(AfterDetailDeletedEventArg arg)
        {
            DetailViewModel = null;
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArg(){ViewModelName = viewModelType.Name});
        }
        #endregion
    }
}