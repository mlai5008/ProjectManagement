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
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
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
                        LookupDevelopers.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator, nameof(DeveloperDetailViewModel)));
                    }
                });
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArg arg)
        {
            switch (arg.ViewModelName)
            {
                case nameof(DeveloperDetailViewModel):
                    NavigationItemViewModel lookupItem = LookupDevelopers.SingleOrDefault(d => d.Id == arg.Id);
                    if (lookupItem == null)
                    {
                        LookupDevelopers.Add(new NavigationItemViewModel(arg.Id, arg.DisplayMember, _eventAggregator, nameof(DeveloperDetailViewModel)));
                    }
                    else
                    {
                        lookupItem.DisplayMember = arg.DisplayMember;
                    }
                    break;
            }
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArg arg)
        {
            switch (arg.ViewModelName)
            {
                case nameof(DeveloperDetailViewModel):
                    NavigationItemViewModel developer = LookupDevelopers.SingleOrDefault(d => d.Id == arg.Id);
                    if (developer != null)
                    {
                        LookupDevelopers.Remove(developer);
                    }
                    break;
            }
        }
        #endregion
    }
}