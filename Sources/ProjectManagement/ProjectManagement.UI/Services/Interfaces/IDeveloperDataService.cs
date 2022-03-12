using System.Collections.Generic;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.UI.Services.Interfaces
{
    public interface IDeveloperDataService
    {
        #region Methods
        IEnumerable<Developer> GetAll();
        #endregion
    }
}