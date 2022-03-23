using System;

namespace ProjectManagement.Domain.EventArgs
{
    public class AfterDeveloperSavedEventArg
    {
        #region Properties
        public Guid Id { get; set; }
        public string DisplayMember { get; set; }
        #endregion
    }
}