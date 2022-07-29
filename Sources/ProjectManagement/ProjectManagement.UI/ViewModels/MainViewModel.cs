using Autofac.Features.Indexed;
using Prism.Commands;
using Prism.Events;
using ProjectManagement.Domain.EventArgs;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.UI.Events;
using ProjectManagement.UI.Services;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManagement.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IIndex<string, IDetailViewModel> _detailViewModelCreator;
        private IDetailViewModel _selectedDetailViewModel;
        #endregion

        #region Ctor
        public MainViewModel(INavigationViewModel navigationViewModel, IIndex<string, IDetailViewModel> detailViewModelCreator, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            _detailViewModelCreator = detailViewModelCreator;

            DetailViewModels = new ObservableCollection<IDetailViewModel>();

            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(OnAfterDetailDeleted);
            _eventAggregator.GetEvent<AfterDetailClosedEvent>().Subscribe(OnAfterDetailClosed);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);
            OpenSingleDetailViewCommand = new DelegateCommand<Type>(OnOpenSingleDetailViewExecute);

            NavigationViewModel = navigationViewModel;
        }
        #endregion

        #region Properties
        public INavigationViewModel NavigationViewModel { get; }

        public IDetailViewModel SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }
        #endregion

        #region Commands
        public ICommand CreateNewDetailCommand { get; }
        public ICommand OpenSingleDetailViewCommand { get; }
        #endregion

        #region Methods
        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenDetailView(OpenDetailViewEventArg arg)
        {
            IDetailViewModel detailViewModel = DetailViewModels.SingleOrDefault(vm => vm.Id == arg.Id && vm.GetType().Name == arg.ViewModelName);
            if (detailViewModel == null)
            {
                detailViewModel = _detailViewModelCreator[arg.ViewModelName];
                await detailViewModel.LoadAsync(arg.Id);
                DetailViewModels.Add(detailViewModel);
            }
            SelectedDetailViewModel = detailViewModel;
        }

        private void OnAfterDetailDeleted(AfterDetailDeletedEventArg arg)
        {
            RemoveDetailViewModel(arg.Id, arg.ViewModelName);
        }

        private void OnAfterDetailClosed(AfterDetailClosedEventArg arg)
        {
            RemoveDetailViewModel(arg.Id, arg.ViewModelName);
        }

        private void RemoveDetailViewModel(Guid id, string viewModelName)
        {
            IDetailViewModel detailViewModel = DetailViewModels.SingleOrDefault(vm => vm.Id == id && vm.GetType().Name == viewModelName);
            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArg(){ViewModelName = viewModelType.Name});
        }

        private void OnOpenSingleDetailViewExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArg() { ViewModelName = viewModelType.Name });
        }
        #endregion
    }
}