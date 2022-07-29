using Prism.Commands;
using Prism.Events;
using ProjectManagement.Domain.Models;
using ProjectManagement.UI.Services.Interfaces;
using ProjectManagement.UI.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManagement.UI.ViewModels
{
    public class ProgrammingLanguageDetailViewModel : DetailViewModelBase
    {
        #region Fields
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private ProgrammingLanguageWrapper _selectedProgrammingLanguage;
        #endregion

        #region Ctor
        public ProgrammingLanguageDetailViewModel(IEventAggregator eventAggregator, IMessageDialogService messageDialogService, IProgrammingLanguageRepository programmingLanguageRepository) : base(eventAggregator, messageDialogService)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            Title = "Programming Languages";
            ProgrammingLanguages = new ObservableCollection<ProgrammingLanguageWrapper>();

            AddCommand = new DelegateCommand(OnAddExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);
        }
        #endregion

        #region Properties
        public ObservableCollection<ProgrammingLanguageWrapper> ProgrammingLanguages { get; }

        public ProgrammingLanguageWrapper SelectedProgrammingLanguage
        {
            get => _selectedProgrammingLanguage;
            set
            {
                _selectedProgrammingLanguage = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand RemoveCommand { get; }

        public ICommand AddCommand { get; }
        #endregion

        #region Methods
        public async override Task LoadAsync(Guid id)
        {
            Id = id;
            foreach (ProgrammingLanguageWrapper wrapper in ProgrammingLanguages)
            {
                wrapper.PropertyChanged -= Wrapper_PropertyChanged;
            }

            ProgrammingLanguages.Clear();

            IEnumerable<ProgrammingLanguage> languages = await _programmingLanguageRepository.GetAllAsync();

            foreach (ProgrammingLanguage model in languages)
            {
                ProgrammingLanguageWrapper wrapper = new ProgrammingLanguageWrapper(model);
                wrapper.PropertyChanged += Wrapper_PropertyChanged;
                ProgrammingLanguages.Add(wrapper);
            }
        }

        private void Wrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _programmingLanguageRepository.HasChanges();
            }
            if (e.PropertyName == nameof(ProgrammingLanguageWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        protected async override void OnSaveExecute()
        {
            try
            {
                await _programmingLanguageRepository.SaveAsync();
                HasChanges = _programmingLanguageRepository.HasChanges();
                RaiseCollectionSavedEvent();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                _messageDialogService.ShowInfoDialog("Error while saving the entities, " + "the data will be reloaded. Details: " + ex.Message);
                await LoadAsync(Id);
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return HasChanges && ProgrammingLanguages.All(p => !p.HasErrors);
        }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }

        private void OnAddExecute()
        {
            ProgrammingLanguageWrapper wrapper = new ProgrammingLanguageWrapper(new ProgrammingLanguage());
            wrapper.PropertyChanged += Wrapper_PropertyChanged;
            _programmingLanguageRepository.Add(wrapper.Model);
            ProgrammingLanguages.Add(wrapper);

            // Trigger the validation
            wrapper.Name = string.Empty;
        }

        private async void OnRemoveExecute()
        {
            bool isReferenced = await _programmingLanguageRepository.IsReferencedByDeveloperAsync(SelectedProgrammingLanguage.Id);
            if (isReferenced)
            {
                _messageDialogService.ShowInfoDialog($"The language {SelectedProgrammingLanguage.Name}" +
                                                    $" can't be removed, as it is referenced by at least one friend");
                return;
            }

            SelectedProgrammingLanguage.PropertyChanged -= Wrapper_PropertyChanged;
            _programmingLanguageRepository.Remove(SelectedProgrammingLanguage.Model);
            ProgrammingLanguages.Remove(SelectedProgrammingLanguage);
            SelectedProgrammingLanguage = null;
            HasChanges = _programmingLanguageRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnRemoveCanExecute()
        {
            return SelectedProgrammingLanguage != null;
        }
        #endregion
    }
}