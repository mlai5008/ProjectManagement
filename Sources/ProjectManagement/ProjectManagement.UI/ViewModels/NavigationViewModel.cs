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
        private readonly IMeetingLookupDataService _meetingLookupDataService;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        #region Ctor
        public NavigationViewModel(IDeveloperLookupDataService developerLookupDataService, IMeetingLookupDataService meetingLookupDataService, IEventAggregator eventAggregator)
        {
            _developerLookupDataService = developerLookupDataService;
            _meetingLookupDataService = meetingLookupDataService;
            _eventAggregator = eventAggregator;
            LookupDevelopers = new ObservableCollection<NavigationItemViewModel>();
            LookupMeetings = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
        }
        #endregion

        #region Properties
        public ObservableCollection<NavigationItemViewModel> LookupDevelopers { get; }
        public ObservableCollection<NavigationItemViewModel> LookupMeetings { get; }
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

                IEnumerable<LookupItem> lookupMeetings = await _meetingLookupDataService.GetMeetingLookupAsync();
                Application.Current.Dispatcher.Invoke((System.Action)delegate
                {
                    LookupMeetings.Clear();
                    foreach (LookupItem item in lookupMeetings)
                    {
                        LookupMeetings.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator, nameof(MeetingDetailViewModel)));
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
                    AfterDetailSaved(LookupDevelopers, arg);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailSaved(LookupMeetings, arg);
                    break;
            }
        }

        private void AfterDetailSaved(ObservableCollection<NavigationItemViewModel> items, AfterDetailSavedEventArg arg)
        {
            NavigationItemViewModel item = items.SingleOrDefault(d => d.Id == arg.Id);
            if (item == null)
            {
                items.Add(new NavigationItemViewModel(arg.Id, arg.DisplayMember, _eventAggregator, arg.ViewModelName));
            }
            else
            {
                item.DisplayMember = arg.DisplayMember;
            }
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArg arg)
        {
            switch (arg.ViewModelName)
            {
                case nameof(DeveloperDetailViewModel):
                    AfterDetailDeleted(LookupDevelopers, arg);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailDeleted(LookupMeetings, arg);
                    break;
            }
        }

        private void AfterDetailDeleted(ObservableCollection<NavigationItemViewModel> items, AfterDetailDeletedEventArg arg)
        {
            NavigationItemViewModel item = items.SingleOrDefault(d => d.Id == arg.Id);
            if (item != null)
            {
                items.Remove(item);
            }
        }
        #endregion
    }
}