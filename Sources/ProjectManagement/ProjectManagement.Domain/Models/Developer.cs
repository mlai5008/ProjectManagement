using System;

namespace ProjectManagement.Domain.Models
{
    public class Developer
    {
        #region Properties
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        #endregion
    }
}