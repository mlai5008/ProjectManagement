using Prism.Commands;
using Prism.Events;
using ProjectManagement.Domain.EventArgs;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.UI.Events;
using ProjectManagement.UI.Services;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManagement.UI.ViewModels
{
    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        protected readonly IMessageDialogService _messageDialogService;
        private bool _hasChanges;
        private string _title;
        #endregion

        #region Ctor
        protected DetailViewModelBase(IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
            CloseDetailViewCommand = new DelegateCommand(OnCloseDetailViewExecute);
        }
        #endregion

        #region Properties
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

        public Guid Id { get; protected set; }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public ICommand CloseDetailViewCommand { get; }
        #endregion

        #region Methods
        public abstract Task LoadAsync(Guid id);

        protected abstract void OnSaveExecute();

        protected abstract bool OnSaveCanExecute();

        protected abstract void OnDeleteExecute();

        protected virtual void RaiseDetailDeletedEvent(Guid modelId)
        {
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Publish(new AfterDetailDeletedEventArg()
            {
                Id = modelId,
                ViewModelName = this.GetType().Name
            });
        }

        protected virtual void RaiseDetailSavedEvent(Guid modelId, string displayMember)
        {
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(new AfterDetailSavedEventArg()
            {
                Id = modelId,
                DisplayMember = displayMember,
                ViewModelName = this.GetType().Name
            });
        }

        protected virtual void OnCloseDetailViewExecute()
        {
            if (HasChanges)
            {
                MessageDialogResult result = _messageDialogService.ShowOkCancelDialog("You've made changes. Close this item?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            _eventAggregator.GetEvent<AfterDetailClosedEvent>()
                .Publish(new AfterDetailClosedEventArg
                {
                    Id = this.Id,
                    ViewModelName = this.GetType().Name
                });
        }

        protected virtual void RaiseCollectionSavedEvent()
        {
            _eventAggregator.GetEvent<AfterCollectionSavedEvent>()
                .Publish(new AfterCollectionSavedEventArg
                {
                    ViewModelName = this.GetType().Name
                });
        }
        #endregion
    }
}