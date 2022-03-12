using ProjectManagement.Domain.Models;
using ProjectManagement.UI.Services.Interfaces;
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

        #region Properties
        public ObservableCollection<Developer> Developers { get; set; }

        public Developer SelectedDeveloper
        {
            get => _selectedDeveloper;
            set
            {
                _selectedDeveloper = value;
                OnPropertyChanged();
            }
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