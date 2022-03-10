using ProjectManagement.Domain.Models;
using System.Collections.Generic;

namespace ProjectManagement.UI.Services
{
    #region Methods
    public interface IDeveloperDataService
    {
        IEnumerable<Developer> GetAll();
    }
    #endregion
}