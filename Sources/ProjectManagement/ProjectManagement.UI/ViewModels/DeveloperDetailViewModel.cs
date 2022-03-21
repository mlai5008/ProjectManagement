using Prism.Events;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.UI.Events;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.UI.ViewModels
{
    public class DeveloperDetailViewModel : ViewModelBase, IDeveloperDetailViewModel
    {
        #region Fields
        private readonly IDeveloperDataService _developerDataService;
        private Developer _developer;
        #endregion

        #region Ctor
        public DeveloperDetailViewModel(IDeveloperDataService developerDataService, IEventAggregator eventAggregator)
        {
            _developerDataService = developerDataService;
            eventAggregator.GetEvent<OpenDeveloperDetailViewModelEvent>().Subscribe(OnOpenDeveloperDetailView);
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
        #endregion

        #region Methods
        private void OnOpenDeveloperDetailView(Guid developerId)
        {
            Task.Run(async () => { await LoadAsync(developerId); });
        }

        public async Task LoadAsync(Guid developerId)
        {
            Developer = await _developerDataService.GetByIdAsync(developerId);
        } 
        #endregion
    }
}