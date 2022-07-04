using System;

namespace ProjectManagement.Domain.EventArgs
{
    public class AfterDetailSavedEventArg
    {
        #region Properties
        public Guid Id { get; set; }
        public string DisplayMember { get; set; }
        public string ViewModelName { get; set; }
        #endregion
    }
}