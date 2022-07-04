using Prism.Commands;
using Prism.Events;
using ProjectManagement.Domain.EventArgs;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.UI.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManagement.UI.ViewModels
{
    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        private bool _hasChanges; 
        #endregion

        #region Ctor
        protected DetailViewModelBase(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
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
        #endregion

        #region Commands
        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }
        #endregion

        #region Methods
        public abstract Task LoadAsync(Guid? id);

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
        #endregion
    }
}