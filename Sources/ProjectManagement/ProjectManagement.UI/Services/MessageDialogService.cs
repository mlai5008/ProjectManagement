using ProjectManagement.UI.Services.Interfaces;
using System.Windows;

namespace ProjectManagement.UI.Services
{
    public class MessageDialogService : IMessageDialogService
    {
        #region Methods
        public MessageDialogResult ShowOkCancelDialog(string text, string title)
        {
            MessageBoxResult result = MessageBox.Show(text, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK ? MessageDialogResult.Ok : MessageDialogResult.Cancel;
        }

        public void ShowInfoDialog(string text)
        {
            MessageBox.Show(text, "Info");
        }
        #endregion
    }
}