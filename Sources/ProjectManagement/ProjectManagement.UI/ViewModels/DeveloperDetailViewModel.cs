using Prism.Commands;
using Prism.Events;
using ProjectManagement.Domain.EventArgs;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.UI.Events;
using ProjectManagement.UI.Services.Interfaces;
using ProjectManagement.UI.Wrapper;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManagement.UI.ViewModels
{
    public class DeveloperDetailViewModel : ViewModelBase, IDeveloperDetailViewModel
    {
        #region Fields
        private readonly IDeveloperRepository _developerRepository;
        private readonly IEventAggregator _eventAggregator;
        private DeveloperWrapper _developer;
        private bool _hasChanges;
        #endregion

        #region Ctor
        public DeveloperDetailViewModel(IDeveloperRepository developerRepository, IEventAggregator eventAggregator)
        {
            _developerRepository = developerRepository;
            _eventAggregator = eventAggregator;
            
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
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
        #endregion

        #region Commands
        public ICommand SaveCommand { get; } 
        #endregion

        #region Methods
        public async Task LoadAsync(Guid developerId)
        {
            Developer developer = await _developerRepository.GetByIdAsync(developerId);
            Developer = new DeveloperWrapper(developer);
            Developer.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _developerRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Developer.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
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
        #endregion
    }
}