using Prism.Events;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.UI.Events;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManagement.UI.ViewModels
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        #region Fields
        private readonly IDeveloperLookupDataService _developerLookupDataService;
        private readonly IEventAggregator _eventAggregator;
        private LookupItem _selectedDeveloper;
        #endregion

        #region Ctor
        public NavigationViewModel(IDeveloperLookupDataService developerLookupDataService, IEventAggregator eventAggregator)
        {
            _developerLookupDataService = developerLookupDataService;
            _eventAggregator = eventAggregator;
            LookupDevelopers = new ObservableCollection<LookupItem>();
        }
        #endregion

        #region Properties
        public ObservableCollection<LookupItem> LookupDevelopers { get; }

        public LookupItem SelectedDeveloper
        {
            get => _selectedDeveloper;
            set
            {
                _selectedDeveloper = value;
                OnPropertyChanged();
                if (_selectedDeveloper != null)
                {
                    _eventAggregator.GetEvent<OpenDeveloperDetailViewModelEvent>().Publish(_selectedDeveloper.Id);
                }
            }
        }
        #endregion

        #region Methods
        public async Task LoadAsync()
        {
            try
            {
                IEnumerable<LookupItem> lookupDevelopers = await _developerLookupDataService.GetDeveloperLookupAsync();
                Application.Current.Dispatcher.Invoke((System.Action)delegate
                {
                    LookupDevelopers.Clear();
                    foreach (LookupItem item in lookupDevelopers)
                    {
                        LookupDevelopers.Add(item);
                    }
                });
                
            }
            catch (Exception ex)
            {
                throw;
            }
        } 
        #endregion
    }
}