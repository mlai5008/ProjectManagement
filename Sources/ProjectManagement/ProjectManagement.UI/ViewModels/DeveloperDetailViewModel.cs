using Prism.Commands;
using Prism.Events;
using ProjectManagement.Domain.EventArgs;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.UI.Events;
using ProjectManagement.UI.Services.Interfaces;
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
        private Developer _developer;
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
        public Developer Developer
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
            Developer = await _developerDataService.GetByIdAsync(developerId);
        }

        private void OnOpenDeveloperDetailView(Guid developerId)
        {
            Task.Run(async () => { await LoadAsync(developerId); });
        }

        private async void OnSaveExecute()
        {
            await _developerDataService.SaveAsync(Developer);
            _eventAggregator.GetEvent<AfterDeveloperSavedEvent>().Publish(new AfterDeveloperSavedEventArg()
            {
                Id = Developer.Id,
                DisplayMember = $"{Developer.FirstName} {Developer.LastName}"
            });
        }

        private bool OnSaveCanExecute()
        {
            return true;
        }
        #endregion
    }
}