using System;

namespace ProjectManagement.Domain.Models
{
    public class NullLookupItem : LookupItem
    {
        #region Properties
        public new Guid? Id => null;
        #endregion
    }
}