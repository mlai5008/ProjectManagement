using Prism.Commands;
using Prism.Events;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.UI.Services;
using ProjectManagement.UI.Services.Interfaces;
using ProjectManagement.UI.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.UI.ViewModels
{
    public class MeetingDetailViewModel : DetailViewModelBase, IMeetingDetailViewModel
    {
        #region Fields
        private readonly IMessageDialogService _messageDialogService;
        private readonly IMeetingRepository _meetingRepository;
        private MeetingWrapper _meeting;
        private Developer _selectedAvailableDeveloper;
        private Developer _selectedAddedDeveloper;
        private List<Developer> _allDevelopers;
        #endregion

        #region Ctor
        public MeetingDetailViewModel(IEventAggregator eventAggregator, IMessageDialogService messageDialogService,
            IMeetingRepository meetingRepository) : base(eventAggregator)
        {
            _messageDialogService = messageDialogService;
            _meetingRepository = meetingRepository;

            AddedDevelopers = new ObservableCollection<Developer>();
            AvailableDevelopers = new ObservableCollection<Developer>();
            AddDeveloperCommand = new DelegateCommand(OnAddDeveloperExecute, OnAddDeveloperCanExecute);
            RemoveDeveloperCommand = new DelegateCommand(OnRemoveDeveloperExecute, OnRemoveDeveloperCanExecute);
        }
        #endregion

        #region Properties
        public MeetingWrapper Meeting
        {
            get => _meeting;
            private set
            {
                _meeting = value;
                OnPropertyChanged();
            }
        }

        public Developer SelectedAvailableDeveloper
        {
            get => _selectedAvailableDeveloper;
            set
            {
                _selectedAvailableDeveloper = value;
                OnPropertyChanged();
                ((DelegateCommand)AddDeveloperCommand).RaiseCanExecuteChanged();
            }
        }

        public Developer SelectedAddedDeveloper
        {
            get => _selectedAddedDeveloper;
            set
            {
                _selectedAddedDeveloper = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveDeveloperCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Developer> AddedDevelopers { get; }

        public ObservableCollection<Developer> AvailableDevelopers { get; }
        #endregion

        #region Commands
        public DelegateCommand AddDeveloperCommand { get; set; }
        public DelegateCommand RemoveDeveloperCommand { get; set; } 
        #endregion

        #region Methods
        public override async Task LoadAsync(Guid? id)
        {
            Meeting meeting = id.HasValue ? await _meetingRepository.GetByIdAsync(id.Value) : CreateNewMeeting();

            InitializeMeeting(meeting);

            _allDevelopers = await _meetingRepository.GetAllDevelopersAsync();

            SetupPicklist();
        }

        protected override async void OnSaveExecute()
        {
            await _meetingRepository.SaveAsync();
            HasChanges = _meetingRepository.HasChanges();
            RaiseDetailSavedEvent(Meeting.Id, Meeting.Title);
        }

        protected override bool OnSaveCanExecute()
        {
            return Meeting != null && !Meeting.HasErrors && HasChanges;
        }

        protected override void OnDeleteExecute()
        {
            MessageDialogResult result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the meeting {Meeting.Title}?", "Question");
            if (result == MessageDialogResult.Ok)
            {
                _meetingRepository.Remove(Meeting.Model);
                _meetingRepository.SaveAsync();
                RaiseDetailDeletedEvent(Meeting.Id);
            }
        }

        private Meeting CreateNewMeeting()
        {
            var meeting = new Meeting
            {
                DateFrom = DateTime.Now.Date,
                DateTo = DateTime.Now.Date
            };
            _meetingRepository.Add(meeting);
            return meeting;
        }

        private void InitializeMeeting(Meeting meeting)
        {
            Meeting = new MeetingWrapper(meeting);
            Meeting.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _meetingRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Meeting.HasErrors))
                {
                    ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();

            if (Meeting.Id == Guid.Empty)
            {
                // Little trick to trigger the validation
                Meeting.Title = "";
            }
        }

        private void OnAddDeveloperExecute()
        {
            Developer developerToAdd = SelectedAvailableDeveloper;

            Meeting.Model.Developers.Add(developerToAdd);
            AddedDevelopers.Add(developerToAdd);
            AvailableDevelopers.Remove(developerToAdd);
            HasChanges = _meetingRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnAddDeveloperCanExecute()
        {
            return SelectedAvailableDeveloper != null;
        }

        private void OnRemoveDeveloperExecute()
        {
            Developer developerToRemove = SelectedAddedDeveloper;

            Meeting.Model.Developers.Remove(developerToRemove);
            AddedDevelopers.Remove(developerToRemove);
            AvailableDevelopers.Add(developerToRemove);
            HasChanges = _meetingRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnRemoveDeveloperCanExecute()
        {
            return SelectedAddedDeveloper != null;
        }

        private void SetupPicklist()
        {
            List<Guid> meetingDeveloperIds = Meeting.Model.Developers.Select(d => d.Id).ToList();
            var addedDevelopers = _allDevelopers.Where(d => meetingDeveloperIds.Contains(d.Id)).OrderBy(d => d.FirstName);
            var availableFriends = _allDevelopers.Except(addedDevelopers).OrderBy(d => d.FirstName);

            AddedDevelopers.Clear();
            AvailableDevelopers.Clear();
            foreach (var addedFriend in addedDevelopers)
            {
                AddedDevelopers.Add(addedFriend);
            }
            foreach (var availableFriend in availableFriends)
            {
                AvailableDevelopers.Add(availableFriend);
            }
        }
        #endregion
    }
}