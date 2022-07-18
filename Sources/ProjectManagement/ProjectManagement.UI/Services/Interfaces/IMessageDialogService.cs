namespace ProjectManagement.UI.Services.Interfaces
{
    public interface IMessageDialogService
    {
        #region Methods
        MessageDialogResult ShowOkCancelDialog(string text, string title);

        void ShowInfoDialog(string text);
        #endregion
    }
}