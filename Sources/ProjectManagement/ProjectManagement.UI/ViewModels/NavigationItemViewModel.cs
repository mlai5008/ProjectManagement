using Prism.Commands;
using Prism.Events;
using ProjectManagement.Domain.EventArgs;
using ProjectManagement.UI.Events;
using System;
using System.Windows.Input;

namespace ProjectManagement.UI.ViewModels
{
    public class NavigationItemViewModel : ViewModelBase
    {
        #region Fields
        private string _displayMember;
        private readonly IEventAggregator _eventAggregator;
        private readonly string _detailViewModelName;
        #endregion

        #region Ctor
        public NavigationItemViewModel(Guid id, string displayMember, IEventAggregator eventAggregator, string detailViewModelName)
        {
            _eventAggregator = eventAggregator;
            _detailViewModelName = detailViewModelName;
            Id = id;
            DisplayMember = displayMember;
            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailViewExecute);
        }
        #endregion

        #region Properties
        public Guid Id { get; }

        public string DisplayMember
        {
            get => _displayMember;
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand OpenDetailViewCommand { get; } 
        #endregion

        #region Methods
        private void OnOpenDetailViewExecute()
        {
            _eventAggregator.GetEvent<OpenDetailViewEvent>().Publish(
                new OpenDetailViewEventArg()
                {
                    Id = Id,
                    ViewModelName = _detailViewModelName
                });
        }
        #endregion
    }
}