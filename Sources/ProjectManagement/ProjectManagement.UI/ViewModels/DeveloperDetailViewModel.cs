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
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectManagement.Domain.EventArgs;
using ProjectManagement.UI.Events;

namespace ProjectManagement.UI.ViewModels
{
    public class DeveloperDetailViewModel : DetailViewModelBase, IDeveloperDetailViewModel
    {
        #region Fields
        private readonly IDeveloperRepository _developerRepository;
        private readonly IProgrammingLanguageLookupDataService _programmingLanguageLookupDataService;
        private DeveloperWrapper _developer;
        private DeveloperPhoneNumberWrapper _selectedPhoneNumber;
        #endregion

        #region Ctor
        public DeveloperDetailViewModel(IDeveloperRepository developerRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService, IProgrammingLanguageLookupDataService programmingLanguageLookupDataService):base(eventAggregator, messageDialogService)
        {
            _developerRepository = developerRepository;
            _programmingLanguageLookupDataService = programmingLanguageLookupDataService;

            eventAggregator.GetEvent<AfterCollectionSavedEvent>()
                .Subscribe(AfterCollectionSaved);

            AddPhoneNumberCommand = new DelegateCommand(OnAddPhoneNumberExecute);
            RemovePhoneNumberCommand = new DelegateCommand(OnRemovePhoneNumberExecute, OnRemovePhoneNumberCanExecute);

            ProgrammingLanguages = new ObservableCollection<LookupItem>();
            PhoneNumbers = new ObservableCollection<DeveloperPhoneNumberWrapper>();
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

        public DeveloperPhoneNumberWrapper SelectedPhoneNumber
        {
            get => _selectedPhoneNumber;
            set
            {
                _selectedPhoneNumber = value;
                OnPropertyChanged();
                ((DelegateCommand)RemovePhoneNumberCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }
        public ObservableCollection<DeveloperPhoneNumberWrapper> PhoneNumbers { get; }
        #endregion

        #region Commands
        public ICommand AddPhoneNumberCommand { get; }

        public ICommand RemovePhoneNumberCommand { get; }
        #endregion

        #region Methods
        public override async Task LoadAsync(Guid developerId)
        {
            Developer developer = developerId != Guid.Empty ? await _developerRepository.GetByIdAsync(developerId) : CreateNewDeveloper();

            Id = developerId;

            InitializeDeveloper(developer);

            InitializeDeveloperPhoneNumbers(developer.PhoneNumbers);

            await LoadProgrammingLanguagesLookupAsync();
        }

        private void InitializeDeveloper(Developer developer)
        {
            Developer = new DeveloperWrapper(developer);
            Developer.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _developerRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Developer.HasErrors))
                {
                    ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
                }
                if (e.PropertyName == nameof(Developer.FirstName) || e.PropertyName == nameof(Developer.LastName))
                {
                    SetTitle();
                }
            };
            ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();

            if (Developer.Id == Guid.Empty)
            {
                //TODO: trigger to validation
                Developer.FirstName = string.Empty;
                Developer.LastName = string.Empty;
            }

            SetTitle();
        }

        private void SetTitle()
        {
            Title = $"{Developer.FirstName} {Developer.LastName}";
        }

        private void InitializeDeveloperPhoneNumbers(ICollection<DeveloperPhoneNumber> developerPhoneNumbers)
        {
            foreach (DeveloperPhoneNumberWrapper developerPhoneNumberWrapper in PhoneNumbers)
            {
                developerPhoneNumberWrapper.PropertyChanged -= DeveloperPhoneNumberWrapper_PropertyChanged;
            }
            PhoneNumbers.Clear();
            foreach (DeveloperPhoneNumber developerPhoneNumber in developerPhoneNumbers)
            {
                DeveloperPhoneNumberWrapper developerPhoneNumberWrapper = new DeveloperPhoneNumberWrapper(developerPhoneNumber);
                PhoneNumbers.Add(developerPhoneNumberWrapper);
                developerPhoneNumberWrapper.PropertyChanged += DeveloperPhoneNumberWrapper_PropertyChanged;
            }
        }

        private void DeveloperPhoneNumberWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _developerRepository.HasChanges();
            }
            if (e.PropertyName == nameof(DeveloperPhoneNumberWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        private async Task LoadProgrammingLanguagesLookupAsync()
        {
            ProgrammingLanguages.Clear();
            ProgrammingLanguages.Add(new NullLookupItem(){DisplayMember = " - "});
            IEnumerable<LookupItem> lookup = await _programmingLanguageLookupDataService.GetProgrammingLanguageLookupAsync();
            foreach (LookupItem lookupItem in lookup)
            {
                ProgrammingLanguages.Add(lookupItem);
            }
        }

        protected override async void OnSaveExecute()
        {
            await _developerRepository.SaveAsync();
            HasChanges = _developerRepository.HasChanges();
            Id = Developer.Id;
            RaiseDetailSavedEvent(Developer.Id, $"{Developer.FirstName} {Developer.LastName}");
        }
        protected override bool OnSaveCanExecute()
        {
            return Developer != null && !Developer.HasErrors && PhoneNumbers.All(p => !p.HasErrors) && HasChanges;
        }

        protected override async void OnDeleteExecute()
        {
            if (await _developerRepository.HasMeetingsAsync(Developer.Id))
            {
                _messageDialogService.ShowInfoDialog($"{Developer.FirstName} {Developer.LastName} can't be deleted, as this friend is part of at least one meeting");
                return;
            }

            MessageDialogResult result = _messageDialogService.ShowOkCancelDialog($"Do you really wont to delete the developer {Developer.FirstName} {Developer.LastName}?", "Question");
            if (result == MessageDialogResult.Ok)
            {
                _developerRepository.Remove(Developer.Model);
                await _developerRepository.SaveAsync();
                RaiseDetailDeletedEvent(Developer.Id);
            }
        }

        private Developer CreateNewDeveloper()
        {
            Developer developer = new Developer();
            _developerRepository.Add(developer);
            return developer;
        }

        private void OnAddPhoneNumberExecute()
        {
            DeveloperPhoneNumberWrapper newDeveloperPhoneNumberWrapper = new DeveloperPhoneNumberWrapper(new DeveloperPhoneNumber());
            newDeveloperPhoneNumberWrapper.PropertyChanged += DeveloperPhoneNumberWrapper_PropertyChanged;
            PhoneNumbers.Add(newDeveloperPhoneNumberWrapper);
            Developer.Model.PhoneNumbers.Add(newDeveloperPhoneNumberWrapper.Model);
            //TODO: trigger to validation
            newDeveloperPhoneNumberWrapper.Number = string.Empty;
        }

        private void OnRemovePhoneNumberExecute()
        {
            SelectedPhoneNumber.PropertyChanged -= DeveloperPhoneNumberWrapper_PropertyChanged;
            //Developer.Model.PhoneNumbers.Remove(SelectedPhoneNumber.Model);
            _developerRepository.RemovePhoneNumber(SelectedPhoneNumber.Model);
            PhoneNumbers.Remove(SelectedPhoneNumber);
            SelectedPhoneNumber = null;
            HasChanges = _developerRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnRemovePhoneNumberCanExecute()
        {
            return SelectedPhoneNumber != null;
        }

        private async void AfterCollectionSaved(AfterCollectionSavedEventArg args)
        {
            if (args.ViewModelName == nameof(ProgrammingLanguageDetailViewModel))
            {
                await LoadProgrammingLanguagesLookupAsync();
            }
        }
        #endregion
    }
}