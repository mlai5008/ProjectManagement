using System;

namespace ProjectManagement.Domain.EventArgs
{
    public class AfterDetailClosedEventArg
    {
        #region Properties
        public Guid Id { get; set; }
        public string ViewModelName { get; set; }
        #endregion
    }
}