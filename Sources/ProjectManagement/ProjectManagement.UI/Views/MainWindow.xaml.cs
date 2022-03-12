using ProjectManagement.UI.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManagement.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private readonly MainViewModel _mainViewModels; 
        #endregion

        #region Ctor
        public MainWindow(MainViewModel mainViewModels)
        {
            InitializeComponent();
            DataContext = _mainViewModels = mainViewModels;
            Task.Run(LoadDataAsync);
        }
        #endregion

        #region Methods
        public Task LoadDataAsync()
        {
            try
            {
                Task.Run(_mainViewModels.LoadAsync);
            }
            catch (Exception)
            {
                throw;
            }

            return Task.CompletedTask;
        }
        #endregion
    }
}
