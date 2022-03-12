using ProjectManagement.Domain.Models;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ProjectManagement.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private readonly Dispatcher _dispatcher;
        private readonly IDeveloperDataService _developerDataService;
        private Developer _selectedDeveloper;
        #endregion

        #region Ctor
        public MainViewModel(IDeveloperDataService developerDataService)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
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
        public async Task LoadAsync()
        {
            IEnumerable<Developer> developers = await _developerDataService.GetAllAsync();

            try
            {
                _dispatcher.Invoke(new Action(() =>
                {
                    Developers.Clear();
                    foreach (Developer developer in developers)
                    {
                        Developers.Add(developer);
                    }
                }));
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}