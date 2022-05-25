using Prism.Events;
using ProjectManagement.Domain.EventArgs;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.UI.Events;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManagement.UI.ViewModels
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        #region Fields
        private readonly IDeveloperLookupDataService _developerLookupDataService;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        #region Ctor
        public NavigationViewModel(IDeveloperLookupDataService developerLookupDataService, IEventAggregator eventAggregator)
        {
            _developerLookupDataService = developerLookupDataService;
            _eventAggregator = eventAggregator;
            LookupDevelopers = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterDeveloperSavedEvent>().Subscribe(AfterDeveloperSaved);
            _eventAggregator.GetEvent<AfterDeveloperDeletedEvent>().Subscribe(AfterDeveloperDeleted);
        }
        #endregion

        #region Properties
        public ObservableCollection<NavigationItemViewModel> LookupDevelopers { get; }
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
                        LookupDevelopers.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator));
                    }
                });
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void AfterDeveloperSaved(AfterDeveloperSavedEventArg developer)
        {
            NavigationItemViewModel lookupItem = LookupDevelopers.SingleOrDefault(d => d.Id == developer.Id);
            if (lookupItem == null)
            {
                LookupDevelopers.Add(new NavigationItemViewModel(developer.Id, developer.DisplayMember, _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = developer.DisplayMember;
            }
        }

        private void AfterDeveloperDeleted(Guid developerId)
        {
            NavigationItemViewModel developer = LookupDevelopers.SingleOrDefault(d => d.Id == developerId);
            if (developer != null)
            {
                LookupDevelopers.Remove(developer);
            }
        }
        #endregion
    }
}