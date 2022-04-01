using Prism.Commands;
using Prism.Events;
using ProjectManagement.Domain.EventArgs;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.UI.Events;
using ProjectManagement.UI.Services.Interfaces;
using ProjectManagement.UI.Wrapper;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManagement.UI.ViewModels
{
    public class DeveloperDetailViewModel : ViewModelBase, IDeveloperDetailViewModel
    {
        #region Fields
        private readonly IDeveloperDataService _developerDataService;
        private readonly IEventAggregator _eventAggregator;
        private DeveloperWrapper _developer;
        #endregion

        #region Ctor
        public DeveloperDetailViewModel(IDeveloperDataService developerDataService, IEventAggregator eventAggregator)
        {
            _developerDataService = developerDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenDeveloperDetailViewModelEvent>().Subscribe(OnOpenDeveloperDetailView);
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }
        #endregion

        #region Properties
        public DeveloperWrapper Developer
        {
            get => _developer;
            set
            {
                _developer = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        #endregion

        #region Methods
        public async Task LoadAsync(Guid developerId)
        {
            Developer developer = await _developerDataService.GetByIdAsync(developerId);
            Developer = new DeveloperWrapper(developer);
            Developer.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Developer.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void OnOpenDeveloperDetailView(Guid developerId)
        {
            Task.Run(async () => { await LoadAsync(developerId); });
        }

        private async void OnSaveExecute()
        {
            await _developerDataService.SaveAsync(Developer.Model);
            _eventAggregator.GetEvent<AfterDeveloperSavedEvent>().Publish(new AfterDeveloperSavedEventArg()
            {
                Id = Developer.Id,
                DisplayMember = $"{Developer.FirstName} {Developer.LastName}"
            });
        }

        private bool OnSaveCanExecute()
        {
            return Developer != null && !Developer.HasErrors;
        }
        #endregion
    }
}