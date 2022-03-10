using ProjectManagement.Domain.Models;
using ProjectManagement.UI.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProjectManagement.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private readonly IDeveloperDataService _developerDataService;
        private Developer _selectedDeveloper;
        #endregion

        #region Ctor
        public MainViewModel(IDeveloperDataService developerDataService)
        {
            _developerDataService = developerDataService;
            Developers = new ObservableCollection<Developer>();
        }
        #endregion

        

        #region Methods
        public void Load()
        {
            IEnumerable<Developer> developers = _developerDataService.GetAll();

            Developers.Clear();
            foreach (Developer developer in developers)
            {
                Developers.Add(developer);
            }
        }
        #endregion
    }
}