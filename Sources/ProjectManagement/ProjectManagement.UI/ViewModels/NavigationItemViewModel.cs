using Prism.Commands;
using Prism.Events;
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
        #endregion

        #region Ctor
        public NavigationItemViewModel(Guid id, string displayMember, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayMember = displayMember;
            OpenDeveloperDetailViewCommand = new DelegateCommand(OnOpenDeveloperDetailView);
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
        public ICommand OpenDeveloperDetailViewCommand { get; } 
        #endregion

        #region Methods
        private void OnOpenDeveloperDetailView()
        {
            _eventAggregator.GetEvent<OpenDeveloperDetailViewModelEvent>().Publish(Id);
        }
        #endregion
    }
}