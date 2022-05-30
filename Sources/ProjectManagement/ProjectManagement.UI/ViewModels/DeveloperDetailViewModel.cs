using Prism.Commands;
using Prism.Events;
using ProjectManagement.Domain.EventArgs;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.UI.Events;
using ProjectManagement.UI.Services;
using ProjectManagement.UI.Services.Interfaces;
using ProjectManagement.UI.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManagement.UI.ViewModels
{
    public class DeveloperDetailViewModel : ViewModelBase, IDeveloperDetailViewModel
    {
        #region Fields
        private readonly IDeveloperRepository _developerRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IProgrammingLanguageLookupDataService _programmingLanguageLookupDataService;
        private DeveloperWrapper _developer;
        private bool _hasChanges;
        #endregion

        #region Ctor
        public DeveloperDetailViewModel(IDeveloperRepository developerRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService, IProgrammingLanguageLookupDataService programmingLanguageLookupDataService)
        {
            _developerRepository = developerRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _programmingLanguageLookupDataService = programmingLanguageLookupDataService;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);

            ProgrammingLanguages = new ObservableCollection<LookupItem>();
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

        public bool HasChanges
        {
            get => _hasChanges;
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }
        #endregion

        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        #endregion

        #region Methods
        public async Task LoadAsync(Guid? developerId)
        {
            Developer developer = developerId.HasValue ? await _developerRepository.GetByIdAsync(developerId.Value) : CreateNewDeveloper();
            
            InitializeDeveloper(developer);

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
            };
            ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();

            if (Developer.Id == Guid.Empty)
            {
                //TODO: trigger to validation
                Developer.FirstName = string.Empty;
                Developer.LastName = string.Empty;
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

        private async void OnSaveExecute()
        {
            await _developerRepository.SaveAsync();
            HasChanges = _developerRepository.HasChanges();
            _eventAggregator.GetEvent<AfterDeveloperSavedEvent>().Publish(new AfterDeveloperSavedEventArg()
            {
                Id = Developer.Id,
                DisplayMember = $"{Developer.FirstName} {Developer.LastName}"
            });
        }

        private bool OnSaveCanExecute()
        {
            return Developer != null && !Developer.HasErrors && HasChanges;
        }

        private async void OnDeleteExecute()
        {
            MessageDialogResult result = _messageDialogService.ShowOkCancelDialog($"Do you really wont to delete the developer {Developer.FirstName} {Developer.LastName}?", "Question");
            if (result == MessageDialogResult.Ok)
            {
                _developerRepository.Remove(Developer.Model);
                await _developerRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterDeveloperDeletedEvent>().Publish(Developer.Id);
            }
        }

        private Developer CreateNewDeveloper()
        {
            Developer developer = new Developer();
            _developerRepository.Add(developer);
            return developer;
        }
        #endregion
    }
}