using System;

namespace ProjectManagement.UI.ViewModels
{
    public class NavigationItemViewModel : ViewModelBase
    {
        #region Fields
        private string _displayMember;
        #endregion

        #region Ctor
        public NavigationItemViewModel(Guid id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
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
    }
}