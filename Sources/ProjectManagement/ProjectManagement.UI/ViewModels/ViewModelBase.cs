using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectManagement.UI.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Methods
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}