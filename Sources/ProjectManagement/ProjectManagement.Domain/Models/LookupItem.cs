using System;

namespace ProjectManagement.Domain.Models
{
    public class LookupItem
    {
        #region Properties
        public Guid Id { get; set; }
        public string DisplayMember { get; set; }
        #endregion
    }
}