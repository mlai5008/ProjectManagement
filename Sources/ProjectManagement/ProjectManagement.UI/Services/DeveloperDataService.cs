using ProjectManagement.Domain.Models;
using System.Collections.Generic;

namespace ProjectManagement.UI.Services
{
    public class DeveloperDataService : IDeveloperDataService
    {
        #region Methods
        public IEnumerable<Developer> GetAll()
        {
            yield return new Developer() { FirstName = "Larry", LastName = "Page" };
            yield return new Developer() { FirstName = "Bill", LastName = "Gates" };
            yield return new Developer() { FirstName = "Mark", LastName = "Zuckerberg" };
            yield return new Developer() { FirstName = "Ken", LastName = "Thompson" };
            yield return new Developer() { FirstName = "Linus", LastName = "Torvalds" };
            yield return new Developer() { FirstName = "Ada", LastName = "Lovelace" };
            yield return new Developer() { FirstName = "Alan", LastName = "Turing" };
        }
        #endregion
    }
}